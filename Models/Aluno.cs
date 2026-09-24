using System.ComponentModel.DataAnnotations;

namespace CadastroAlunos.Models;

public sealed class Aluno
{
    [Key]
    public Guid Id { get; set; }

    [Display(Name = "Ativo")]
    public bool Status { get; set; } = true;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(150, ErrorMessage = "O nome deve possuir no máximo 150 caracteres.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório.")]
    [StringLength(150, ErrorMessage = "O email deve possuir no máximo 150 caracteres.")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    [Display(Name = "Data de nascimento")]
    public DateTime DataNascimento { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "CPF inválido.")]
    [Display(Name = "CPF")]
    public string CPF { get; set; } = string.Empty;

    [StringLength(20)]
    [Display(Name = "Telefone")]
    public string Telefone { get; set; } = string.Empty;
}
