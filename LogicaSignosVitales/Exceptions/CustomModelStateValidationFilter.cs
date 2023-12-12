using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaSignosVitales.Exceptions
{
    public class CustomModelStateValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                context.Result = new BadRequestObjectResult(new
                {
                    type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                    title = "El campo numero_SignoVital es Obligatorio.",
                    status = 400,
                    traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
                });
            }
        }
    }

}
