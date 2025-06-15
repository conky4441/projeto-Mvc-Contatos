using ProjetoMVC.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoMVC.Api.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite seu nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite seu e-mail")]
        [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite seu login")]
        public string Login { get; set; }


    }
}
