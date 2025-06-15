using ProjetoMVC.Api.Enums;
using ProjetoMVC.Api.Helper;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Api.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite seu nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite seu e-mail")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite seu login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite sua senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Selecione o tipo de conta")]
        public TipoConta? TipoConta { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? UltimaAtualizacao { get; set; }
        public virtual List<ContatoModel> Contatos { get; set; } = new();

        public void CriptografarSenha()
        {
            Senha = Senha.GerarHash();
        }
        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }
        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid()
                                   .ToString()
                                   .Substring(0, 8);
            Senha = novaSenha;
            CriptografarSenha();
            return novaSenha;
        }
        public void TrocarSenha(string novaSenha)
        {
            Senha = novaSenha;
            CriptografarSenha();
        }
    }
}
