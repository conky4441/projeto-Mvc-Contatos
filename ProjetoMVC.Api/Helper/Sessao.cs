using Newtonsoft.Json;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Helper
{
    public class Sessao(IHttpContextAccessor httpContext) : ISessao
    {
        private readonly IHttpContextAccessor _httpContext = httpContext;

        public void IniciarSessao(UserModel user)
        {
            string userJson = JsonConvert.SerializeObject(user);
            _httpContext.HttpContext.Session.SetString("sessaoIniciada", userJson);
        }
        public UserModel? BuscarSessao()
        {
            string userJson = _httpContext.HttpContext.Session.GetString("sessaoIniciada");
            if(string.IsNullOrEmpty(userJson)) return null;
            return JsonConvert.DeserializeObject<UserModel>(userJson);
        }

        public void FinalizarSessao()
        {
            _httpContext.HttpContext.Session.Remove("sessaoIniciada");
        }

       
    }
}
