using ProjetoMVC.Api.Dtos;
using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<UserModel> BuscarTodos();
        UserModel AdicionarUser(UserModel user);
        UserModel BuscarLogin(string login);
        UserModel BuscarPorEmail(string email);
        UserModel BuscarPorId(int id);
        UserModel AlterarSenha(AlterarSenhaDto alterarSenhaDto);
        UserModel EditarUser(UserModel user);
        bool ApagarUser(int id);

    }

}
