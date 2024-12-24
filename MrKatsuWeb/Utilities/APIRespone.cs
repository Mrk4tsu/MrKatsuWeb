using Microsoft.AspNetCore.Mvc;

namespace MrKatsuWeb.Utilities
{
    public class APIRespone
    {
        public static IActionResult Success(string? message = null, object data = null)
        {
            return new OkObjectResult(new
            {
                success = true,
                message,
                data
            });
        }
        public static IActionResult Success(object data = null)
        {
            return new OkObjectResult(new
            {
                success = true,
                data
            });
        }
        public static IActionResult Error(string? message = null, CodeStatus statusCode = CodeStatus.BadRequest, object errors = null)
        {
            return new ObjectResult(new
            {
                success = false,
                message,
                errors
            })
            {
                StatusCode = (int)statusCode
            };
        }
        public static IActionResult NotFound(string? message = null, object errors = null)
        {
            return new ObjectResult(new
            {
                success = false,
                message,
                errors
            })
            {
                StatusCode = CodeStatus.NotFound.GetHashCode()
            };
        }
    }
    public enum CodeStatus
    {
        OK = 200,
        Created = 201,
        NoContent = 204,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServerError = 500,
        GatewayTimeout = 504
    }
}
