using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AoTTG2.IDS.Filters
{
    public class ExceptionHandler : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                context.Result = new ConflictResult();
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            return Task.CompletedTask;
        }
    }
}
