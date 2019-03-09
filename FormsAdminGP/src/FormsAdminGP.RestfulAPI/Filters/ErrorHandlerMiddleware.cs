using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;
 

namespace FormsAdminGP.RestfulAPI.Filters
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = context.Features.Get<IExceptionHandlerFeature>();
                var message = "Oops! Sorry! Something went wrong." +
                              "Please contact support@grupoperti.com.mx so we can try to fix it.";

                if (error != null)
                {
                    
                    var innerException = error.Error.Message;
                    if (innerException.Contains("duplicate key"))
                    {
                        message = "EXITE EN EL SISTEMA " + message;
                    }

                    context.Response.ContentType = "application/json";
                    var json = JToken.FromObject(new ApiResponse(HttpStatusCode.InternalServerError, false, message));
                    await context.Response.WriteAsync(json.ToString());
                }
                else
                {
                    HttpStatusCode status;
                    var exceptionType = ex.GetType();
                    if (exceptionType == typeof(UnauthorizedAccessException))
                    {
                        message = "Unauthorized Access";
                        status = HttpStatusCode.Unauthorized;
                    }
                    else if (exceptionType == typeof(NotImplementedException))
                    {
                        message = "A server error occurred.";
                        status = HttpStatusCode.NotImplemented;
                    }
                    //else if (exceptionType == typeof(xException))
                    //{
                    //    status = HttpStatusCode.ExpectationFailed;
                    //    message = ex.Message;
                    //}
                    else if (exceptionType == typeof(SqlException))
                    {
                        var err = ex as SqlException;
                        status = HttpStatusCode.ExpectationFailed;
                        message = err?.Message;
                    }
                    else
                    {
                        status = HttpStatusCode.NotFound;
                    }


                    context.Response.ContentType = "application/json";
                    var json = JToken.FromObject(new ApiResponse(status, false, message));
                    await context.Response.WriteAsync(json.ToString());
                }
            }
        }
    }

    public class ApiResponse
    {
        public int ErrorCode { get; }
        public string StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }
        public bool Success { get; }

        public ApiResponse(HttpStatusCode statusCode, bool success, string message = null)
        {
            Success = success;
            StatusCode = statusCode.ToString();
            ErrorCode = (int)statusCode;
            Message = message;
        }


    }
}
