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
        private readonly IUserRepository _repository = repository;
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
                    var user = _repository.BuscarPorEmail(userRedefinir.Email);
                    if (user != null)
                    {
                        var novaSenha = user.GerarNovaSenha();
                        _repository.EditarUser(user);

                        string mensagem = $"Sua nova senha é: <strong>{novaSenha}</strong>";
                        _email.Enviar(user.Email, "Redefinição de Senha - Sistema", mensagem);

                        TempData["MensagemSucesso"] = "Sua nova senha foi encaminhada para seu e-mail";
                        return RedirectToAction("Index", "Login");
                    }

                    TempData["MensagemErro"] = "Usuário não encontrado.\nVerifique o e-mail e digite novamente";
                    return View("Index");
                }
                return View("Index", userRedefinir);
            }
            catch (Exception)
            {
                TempData["MensagemErro"] = "Ops, houve um erro ao tentar redefinir sua senha";
                return View("Index");
            }

        }
    }
}
