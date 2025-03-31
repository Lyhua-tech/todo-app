using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace TodoController.exception
{
    [ApiController]
    public class ErrorException : ControllerBase
    {
        [Microsoft.AspNetCore.Mvc.Route("/error")]
        public IActionResult HandleError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            return Problem(
                detail: exception?.Message ?? "An unexpected error occurred.",
                statusCode: 500, 
                title: "Server Error"
            );
        }

        [Microsoft.AspNetCore.Mvc.Route("/error/{statusCode}")]
        public IActionResult HandleStatusCode(int statusCode)
        {
            var errorMessage = statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                500 => "Internal Server Error",
                _ => "An unexpected error occurred"
            };

            return Problem(
                detail: errorMessage,
                statusCode: statusCode,
                title: "Error"
            );
        }
    }
}