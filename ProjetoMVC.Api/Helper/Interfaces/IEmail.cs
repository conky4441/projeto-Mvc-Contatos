namespace ProjetoMVC.Api.Helper.Interfaces
{
    public interface IEmail
    {
        public void Enviar(string email, string assunto, string mensagem);
    }
}
