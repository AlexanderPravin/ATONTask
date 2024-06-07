using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.ExceptionFilters;

public class UserExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case NullEntityException exception:
                context.Result = new NotFoundObjectResult(exception.Message);
                break;
            case ArgumentException exception:
                context.Result = new BadRequestObjectResult(exception.Message);
                break;
            case DuplicateException exception:
                context.Result = new ConflictObjectResult(exception.Message);
                break;
        }
    }
}