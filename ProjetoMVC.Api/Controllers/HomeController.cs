using Microsoft.AspNetCore.Mvc;
using ProjetoMVC.Api.Filters;
using ProjetoMVC.Api.Helper.Interfaces;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;
using System.Diagnostics;

namespace ProjetoMVC.Api.Controllers
{
    [UserLogadoFilter]
    public class HomeController(ISessao sessao, IUserRepository userRepository, IContatoRepository contatoRepository) : Controller
    {
        private readonly ISessao _sessao = sessao;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IContatoRepository _contatoRepository = contatoRepository;

        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessao();
            var perfil = usuario.TipoConta.ToString();

            int totalUsuarios = 0;
            int totalContatos = 0;

            if (perfil == "Admin")
            {
                totalUsuarios = _userRepository.BuscarTodos().Count();
                totalContatos = _contatoRepository.BuscarTodos(usuario.Id).Count();
            }
            else
            {
                totalContatos = _contatoRepository.BuscarTodos(usuario.Id).Count();
            }

            ViewBag.PerfilUsuario = perfil;
            ViewBag.TotalUsuarios = totalUsuarios;
            ViewBag.TotalContatos = totalContatos;

            return View(usuario);
        }


        public IActionResult SairConfirmar()
        {
            return View();
        }
        public IActionResult Sair()
        {
            _sessao.FinalizarSessao();
            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
