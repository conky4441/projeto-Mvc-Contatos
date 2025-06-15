using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Dtos;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserAdminFilter]
    public class UsersController(IUserRepository userRepository, IContatoRepository contatos, ISessao sessao) : Controller
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IContatoRepository _contatos = contatos;
        private readonly ISessao _sessao = sessao;
        public IActionResult Index() => View(_userRepository.BuscarTodos());
        public IActionResult ListarContatosUserId(int id)
        {
            var lista = _contatos.BuscarTodos(id);
            return PartialView("_ContatosUsuario", lista);
        }
        public IActionResult Adicionar() => View();
        public IActionResult Editar(int id)
        {
            var user = _userRepository.BuscarPorId(id);
            return View(user);
        }
        public IActionResult ApagarConfirmar(int id) => View(_userRepository.BuscarPorId(id));
        public IActionResult Apagar(int id)
        {
            try
            {
                if (_userRepository.ApagarUser(id))
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
                    return RedirectToAction("Index");
                }

                TempData["MensagemErro"] = "Houve um erro ao deletar o usuário";
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve o seguinte erro ao deletar o usuário: {e.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Adicionar(UserModel user)
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
                    TempData["MensagemSucesso"] = "Usuário adicionado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve o seguinte erro ao criar o usuário: {e.Message}";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Editar(UserDto userDto)
        {
            try
            {
                UserModel user = null;
                if (ModelState.IsValid)
                {
                    user = new()
                    {
                        Id = userDto.Id,
                        Nome = userDto.Nome,
                        Login = userDto.Login,
                        Email = userDto.Email
                    };
                    _userRepository.EditarUser(user);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve o seguinte erro ao atualizar o usuário: {e.Message}";
                return RedirectToAction("Index");
            }

        }
       
    }
}