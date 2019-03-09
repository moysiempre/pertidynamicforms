using FormsAdminGP.Core.Entities;
using FormsAdminGP.Core.Utilities;
using FormsAdminGP.Data.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.Services
{
    public interface ITokenStoreService
    {
        Task AddUserTokenAsync(UserToken userToken);
        Task AddUserTokenAsync(User user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial);
        Task<bool> IsValidTokenAsync(string accessToken, string userId);
        Task DeleteExpiredTokensAsync();
        Task<UserToken> FindTokenAsync(string refreshTokenValue);
        Task DeleteTokenAsync(string refreshTokenValue);
        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource);
        Task InvalidateUserTokensAsync(string userId);
        Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue);
    }

    public class TokenStoreService : ITokenStoreService
    {

        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IOptionsSnapshot<BearerTokensOptions> _configuration;
        private readonly ITokenFactoryService _tokenFactoryService;

        public TokenStoreService(
            IUserTokenRepository userTokenRepository,
            IOptionsSnapshot<BearerTokensOptions> configuration,
            ITokenFactoryService tokenFactoryService)
        {
            _userTokenRepository = userTokenRepository;

            _configuration = configuration;
            _configuration.CheckArgumentIsNull(nameof(configuration));

            _tokenFactoryService = tokenFactoryService;
            _tokenFactoryService.CheckArgumentIsNull(nameof(tokenFactoryService));
        }

        public async Task AddUserTokenAsync(UserToken userToken)
        {
            if (!_configuration.Value.AllowMultipleLoginsFromTheSameUser)
            {
                await InvalidateUserTokensAsync(userToken.UserId);
            }
            await DeleteTokensWithSameRefreshTokenSourceAsync(userToken.RefreshTokenIdHashSource);
            _userTokenRepository.Add(userToken);
            await _userTokenRepository.SaveChanges();
        }

        public async Task AddUserTokenAsync(User user, string refreshTokenSerial, string accessToken, string refreshTokenSourceSerial)
        {
            var now = DateTimeOffset.UtcNow;
            var token = new UserToken
            {
                UserId = user.Id,
                // Refresh token handles should be treated as secrets and should be stored hashed
                RefreshTokenIdHash = Utils.GetSha256Hash(refreshTokenSerial),
                RefreshTokenIdHashSource = string.IsNullOrWhiteSpace(refreshTokenSourceSerial) ?
                                           null : Utils.GetSha256Hash(refreshTokenSourceSerial),
                AccessTokenHash = Utils.GetSha256Hash(accessToken),
                RefreshTokenExpiresDateTime = now.AddMinutes(_configuration.Value.RefreshTokenExpirationMinutes),
                AccessTokenExpiresDateTime = now.AddMinutes(_configuration.Value.AccessTokenExpirationMinutes)
            };
            await AddUserTokenAsync(token);
        }

        public async Task DeleteExpiredTokensAsync()
        {
            var now = DateTimeOffset.UtcNow;
            var userTokens = await _userTokenRepository.FindBy(x => x.RefreshTokenExpiresDateTime < now); 
            if(userTokens.Count() > 0)
            {
                foreach (var item in userTokens)
                {
                    _userTokenRepository.Delete(item);
                }

                await _userTokenRepository.SaveChanges();
            }
            
        }

        public async Task DeleteTokenAsync(string refreshTokenValue)
        {
            var token = await FindTokenAsync(refreshTokenValue);
            if (token != null)
            {
                _userTokenRepository.Delete(token);
                await _userTokenRepository.SaveChanges();
            }
        }

        public async Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenIdHashSource))
            {
                return;
            }
            var userTokens = await _userTokenRepository.FindBy(t => t.RefreshTokenIdHashSource == refreshTokenIdHashSource);
            if (userTokens.Count() > 0)
            {
                foreach (var item in userTokens)
                {
                    _userTokenRepository.Delete(item);
                }

                await _userTokenRepository.SaveChanges();
            }
        }

        public async Task RevokeUserBearerTokensAsync(string userIdValue, string refreshTokenValue)
        {
            if (!string.IsNullOrWhiteSpace(userIdValue))
            {
                if (_configuration.Value.AllowSignoutAllUserActiveClients)
                {
                    await InvalidateUserTokensAsync(userIdValue);
                }
            }

            if (!string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
                if (!string.IsNullOrWhiteSpace(refreshTokenSerial))
                {
                    var refreshTokenIdHashSource = Utils.GetSha256Hash(refreshTokenSerial);
                    await DeleteTokensWithSameRefreshTokenSourceAsync(refreshTokenIdHashSource);
                }
            }

            await DeleteExpiredTokensAsync();
        }

        public Task<UserToken> FindTokenAsync(string refreshTokenValue)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenValue))
            {
                return null;
            }

            var refreshTokenSerial = _tokenFactoryService.GetRefreshTokenSerial(refreshTokenValue);
            if (string.IsNullOrWhiteSpace(refreshTokenSerial))
            {
                return null;
            }

            var refreshTokenIdHash = Utils.GetSha256Hash(refreshTokenSerial);
            return _userTokenRepository.FindEntityBy(x => x.RefreshTokenIdHash == refreshTokenIdHash, t => t.User);

        }

        public async Task InvalidateUserTokensAsync(string userId)
        {
            var userTokens = await _userTokenRepository.FindBy(x => x.UserId == userId);
            if (userTokens.Count() > 0)
            {
                foreach (var item in userTokens)
                {
                    _userTokenRepository.Delete(item);
                }

                await _userTokenRepository.SaveChanges();
            }
        }

        public async Task<bool> IsValidTokenAsync(string accessToken, string userId)
        {
            var accessTokenHash = Utils.GetSha256Hash(accessToken);
            var userToken = await _userTokenRepository.FindEntityBy(
                x => x.AccessTokenHash == accessTokenHash && x.UserId == userId);
            return userToken?.AccessTokenExpiresDateTime >= DateTimeOffset.UtcNow;
        }
    }
}