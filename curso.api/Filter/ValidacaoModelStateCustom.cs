using curso.api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace curso.api.Filter
{
    public class ValidacaoModelStateCustom: ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                 var  validaCampoModel = new ValidaCampoViewModelOuput(context.ModelState.SelectMany(v => v.Value.Errors).Select(s => s.ErrorMessage));
                context.Result = new BadRequestObjectResult(validaCampoModel);
            }
        }
    }
}
