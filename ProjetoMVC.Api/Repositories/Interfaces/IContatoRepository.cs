using ProjetoMVC.Api.Models;

namespace ProjetoMVC.Api.Repositories.Interfaces
{
    public interface IContatoRepository
    {
        public List<ContatoModel> BuscarTodos(int userId);
        public ContatoModel AdicionarContato(ContatoModel contato);
        public ContatoModel BuscarPorId(int id);
        public ContatoModel EditarContato(ContatoModel contato);
        public bool ApagarContato(int id);

    }
}
