using Microsoft.EntityFrameworkCore;
using ProjetoMVC.Api.Data;
using ProjetoMVC.Api.Dtos;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public List<UserModel> BuscarTodos() 
        {
            return _context.Users
                .Include(x=>x.Contatos)
                .ToList();
        }
        public UserModel BuscarPorId(int id)
        {
            return _context.Users.Find(id) ?? throw new Exception("Usuário não encontrado");
        }
        public UserModel BuscarLogin(string login)
        {
            return _context.Users.FirstOrDefault(a => a.Login.ToUpper() == login.ToUpper());
        }
        public UserModel BuscarPorEmail(string email) => _context.Users.FirstOrDefault(a => a.Email.ToUpper() == email.ToUpper());
        public UserModel AdicionarUser(UserModel user)
        {
            user.DataCadastro = DateTime.Now;
            user.CriptografarSenha();
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public UserModel EditarUser(UserModel user)
        {
            var userAtual = BuscarPorId(user.Id);
            userAtual.Nome = user.Nome;
            userAtual.Email = user.Email;
            userAtual.Login = user.Login;
            userAtual!.UltimaAtualizacao = DateTime.Now;

            _context.Update(userAtual);
            _context.SaveChanges();

            return userAtual;
        }
        public UserModel AlterarSenha(AlterarSenhaDto alterarSenhaDto)
        {
            var userDb = BuscarPorId(alterarSenhaDto.Id) ?? throw new Exception("Usuário não encontrado");

            if (!userDb.SenhaValida(alterarSenhaDto.AntigaSenha)) throw new Exception("A senha atual não confere");

            if (userDb.SenhaValida(alterarSenhaDto.NovaSenha)) throw new Exception("A nova senha precisa ser diferente da atual");

            userDb.TrocarSenha(alterarSenhaDto.NovaSenha);
            userDb.UltimaAtualizacao = DateTime.Now;
            _context.Users.Update(userDb);
            _context.SaveChanges();
            return userDb;

        }
        public bool ApagarUser(int id)
        {
            var user = BuscarPorId(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }

    }
}
