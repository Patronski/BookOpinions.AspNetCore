using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace BookOpinions.Web.Infrastructure.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// If model is not valid sets ViewResult with the model.
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;

                if (controller == null)
                {
                    return;
                }

                var model = context
                    .ActionArguments
                    .FirstOrDefault(a => a.Key.ToLower().Contains("model"))
                    .Value;

                if (model == null)
                {
                    return;
                }

                context.Result = controller.View(model);
            }
        }
    }
}
