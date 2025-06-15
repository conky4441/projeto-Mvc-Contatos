using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ProjetoMVC.Api.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        [EmailAddress(ErrorMessage = "O e-mail não é valido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o telefone")]
        public string Telefone { get; set; }

        public int UserId { get; set; }
        [ValidateNever]
        public virtual UserModel User { get; set; }
    }
}