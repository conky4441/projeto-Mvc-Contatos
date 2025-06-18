using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Dtos;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserDeslogadoFilter]
    public class RedefinirSenhaController(IUserRepository repository, IEmail email) : Controller
    {
        private readonly IUserRepository _userRepository = repository;
        private readonly IEmail _email = email;
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Enviar(RedefinirSenhaDto userRedefinir)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = _userRepository.BuscarPorEmail(userRedefinir.Email);
                    if (user != null)
                    {
                        var novaSenha = user.GerarNovaSenha();
                        

                        string mensagem = $"Sua nova senha é: <strong>{novaSenha}</strong>";
                        _email.Enviar(user.Email, "Redefinição de Senha - Sistema", mensagem);

                        _userRepository.EditarUser(user);
                        TempData["MensagemSucesso"] = "Sua nova senha foi encaminhada para seu e-mail";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = "Usuário não encontrado.\nVerifique o e-mail e digite novamente";
                    return View("Index");
                }
                return View("Index", userRedefinir);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, houve o seguinte erro ao tentar redefinir sua senha: {e.Message}";
                return View("Index");
            }

        }
    }
}
