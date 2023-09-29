using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Models;
namespace SampleProject.Common
{
    public class Attribute
    {
        public class WebValidateModelAttribute : Attribute, IActionFilter
        {
            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    var message = "";
                    var errors = context.ModelState
                      .Where(a => a.Value.Errors.Count > 0)
                      .SelectMany(x => x.Value.Errors)
                      .ToList();
                    if (errors != null && errors.Count > 0)
                    {
                        message = errors[0].ErrorMessage;
                    }
                    var isAjax = context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                    if (isAjax)
                    {
                        context.Result = new JsonResult(new ActionOutput
                        {
                            Status = ActionStatus.Error,
                            Message = message
                        });
                    }
                }
            }
            public void OnActionExecuted(ActionExecutedContext context)
            {

            }

        }
    }
}
