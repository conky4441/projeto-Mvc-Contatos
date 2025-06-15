using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Helper.Interfaces
{
    public interface ISessao
    {
        void IniciarSessao(UserModel user);
        UserModel? BuscarSessao();
        void FinalizarSessao();
    }
}
