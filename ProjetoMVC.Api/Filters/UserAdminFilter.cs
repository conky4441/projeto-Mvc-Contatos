using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Filters
{
    public class UserAdminFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userJson = context.HttpContext.Session.GetString("sessaoIniciada");
            if (string.IsNullOrEmpty(userJson))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else
            {
                var userObject = JsonConvert.DeserializeObject<UserModel>(userJson);
                if (userObject == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);
                }
                else if (userObject.TipoConta != Enums.TipoConta.Admin)
                {
                    context.Result = new RedirectToActionResult("Index", "Restrito", null);
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
