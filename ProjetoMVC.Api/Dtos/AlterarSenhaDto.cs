using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Api.Dtos
{
    public class AlterarSenhaDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite sua senha atual")]
        public string AntigaSenha { get; set; }
        [Required(ErrorMessage = "Digite a nova senha")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Digite novamente a nova senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas precisam ser idênticas")]
        public string NovaSenhaConfirma { get; set; }
    }
}
