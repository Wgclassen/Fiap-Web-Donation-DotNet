using Microsoft.AspNetCore.Mvc.Filters;
namespace Fiap.Web.Donation5.Controllers.Filters;

public class AutenticadoAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }
}
