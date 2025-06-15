using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Api.Dtos
{
    public class RedefinirSenhaDto
    {
        [Required(ErrorMessage = "Digite seu e-mail")]
        [EmailAddress(ErrorMessage = "Digite seu e-mail corretamente")]
        public string Email { get; set; }
    }
}
