using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Dtos;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserLogadoFilter]
    public class AlterarSenhaController(ISessao sessao, IUserRepository userRepository) : Controller
    {
        private readonly ISessao _sessao = sessao;
        private readonly IUserRepository _userRepository = userRepository;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogado = _sessao.BuscarSessao();
                    alterarSenhaDto.Id = userLogado!.Id;
                    var user = _userRepository.AlterarSenha(alterarSenhaDto);
                    TempData["MensagemSucesso"] = "Senha atualizada com sucesso";
                    return RedirectToAction("Index", "Home", user);
                }
                return View("Index", alterarSenhaDto);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Ops, houve o seguinte problema ao redefinir sua senha: {e.Message}";
                return View("Index", alterarSenhaDto);
            }
        }
    }
}
