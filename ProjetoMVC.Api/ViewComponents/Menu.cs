using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sessaoUser = HttpContext.Session.GetString("sessaoIniciada");

            if (string.IsNullOrEmpty(sessaoUser)) return null;

            var userLogado = JsonConvert.DeserializeObject<UserModel>(sessaoUser);
            return View(userLogado);
        }
    }
}
