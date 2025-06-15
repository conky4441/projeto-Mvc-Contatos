using ProjetoMVC.Api.Data;
using ProjetoMVC.Api.Models;
using ProjetoMVC.Api.Repositories.Interfaces;

namespace ProjetoMVC.Api.Repositories
{
    public class ContatoRepository(AppDbContext context) : IContatoRepository
    {
        private readonly AppDbContext _context = context;

        public List<ContatoModel> BuscarTodos(int userId) => _context.Contatos.Where(x => x.UserId == userId).ToList();
        public ContatoModel AdicionarContato(ContatoModel contato)
        {
            _context.Contatos.Add(contato);
            _context.SaveChanges();

            return contato;
        }

        public ContatoModel BuscarPorId(int id) => _context.Contatos.Find(id) ?? throw new System.Exception("Houve um erro com o Id informado");


        public bool ApagarContato(int id)
        {
            var contato = BuscarPorId(id);
            _context.Contatos.Remove(contato);
            _context.SaveChanges();
            return true;
        }

        public ContatoModel EditarContato(ContatoModel contato)
        {
            var contatoAtualizado = BuscarPorId(contato.Id) ?? throw new Exception("Usuário não encontrado");
            contatoAtualizado.Nome = contato.Nome;
            contatoAtualizado.Email = contato.Email;
            contatoAtualizado.Telefone = contato.Telefone;

            _context.Contatos.Update(contatoAtualizado);
            _context.SaveChanges();
            return contatoAtualizado;
        }

    }
}
