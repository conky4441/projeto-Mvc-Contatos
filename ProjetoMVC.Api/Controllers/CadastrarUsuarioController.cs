using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserDeslogadoFilter]
    public class CadastrarUsuarioController(IUserRepository userRepository, ISessao sessao) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ISessao _sessao = sessao;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogin = _userRepository.BuscarLogin(user.Login);
                    if (userLogin != null)
                    {
                        throw new Exception("O login informado já existe.");
                    }
                    var userEmail = _userRepository.BuscarPorEmail(user.Email);
                    if (userEmail != null)
                    {
                        throw new Exception("O e-mail informado já existe.");
                    }
                    _userRepository.AdicionarUser(user);
                    _sessao.IniciarSessao(user);

                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index", "Home");
                }
                return View("Index", user);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve o seguinte erro ao criar o usuário: {e.Message}";
                return View("Index");
            }
        }
    }
}
