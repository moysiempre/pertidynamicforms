namespace FormsAdminGP.Services.Responses
{
    public partial class BaseResponse
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int ErrorId { get; set; }
    }
}
