using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Controllers
{
    [UserLogadoFilter]
    public class ContatosController(IContatoRepository contatoRepository, ISessao sessao) : Controller
    {
        private readonly IContatoRepository _contatoRepository = contatoRepository;
        private readonly ISessao _sessao = sessao;
        public IActionResult Index()
        {
            var userLogado = _sessao.BuscarSessao();
            var contatos = _contatoRepository.BuscarTodos(userLogado.Id);
            return View(contatos);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            var contato = _contatoRepository.BuscarPorId(id);
            return View(contato);
        }
        public IActionResult ApagarConfirmar(int id)
        {
            var contato = _contatoRepository.BuscarPorId(id);
            return View(contato);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                var apagado = _contatoRepository.ApagarContato(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "Houve um erro ao apagar o contato";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve um erro ao apagar o contato\nErro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Criar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogado = _sessao.BuscarSessao();
                    if (userLogado == null)
                    {
                        TempData["MensagemErro"] = "Sessão expirada. Faça login novamente.";
                        return RedirectToAction("Login", "Login");
                    }

                    contato.UserId = userLogado.Id;
                    _contatoRepository.AdicionarContato(contato);

                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                
                return View(contato);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Erro ao efetuar cadastro\nErro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Editar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userLogado = _sessao.BuscarSessao();
                    contato.UserId = userLogado!.Id;
                    _contatoRepository.EditarContato(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(contato);
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = $"Houve um erro ao atualizar o contato.\nErro: {e.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
