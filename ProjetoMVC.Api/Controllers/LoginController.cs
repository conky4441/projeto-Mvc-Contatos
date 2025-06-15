using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserDeslogadoFilter]
    public class LoginController(IUserRepository userRepository, ISessao sessao) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISessao _sessao = sessao;
        public IActionResult Index()
        {
            return View();

        }
     
        [HttpPost]
        public IActionResult Entrar(LoginModel logn)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userRepository.BuscarLogin(logn.Login);
                    if (user != null)
                    {
                        if (user.SenhaValida(logn.Senha))
                        {
                            _sessao.IniciarSessao(user);
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    var userEmail = _userRepository.BuscarPorEmail(logn.Login);
                    if (userEmail != null)
                    {
                        if (userEmail.SenhaValida(logn.Senha))
                        {
                            _sessao.IniciarSessao(userEmail);
                            return RedirectToAction("Index", "Home");
                        }
                    }

                    TempData["MensagemErro"] = "Verifique seus dados e digite novamente";
                    return RedirectToAction("Index");
                }

                return View("Index", logn);

            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Houve um erro ao executar o login";
                return RedirectToAction("Index");
            }
        }
    }
}
