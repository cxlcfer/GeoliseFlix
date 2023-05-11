using System.ComponentModel.DataAnnotations;

namespace GeoliseFlix.DataTransferObjects;

public class LoginDto
{
    [Display(Name = "E-mail ou Nome de Usuário")]
    [Required(ErrorMessage = "Por favor, informe seu e-mail ou nome de usário")]
    
    public string Email { get; set; }

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "Por favor, informe sua senha de acesso")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Manter Conectado?")]
    public bool RememberMe { get; set; }

    public string ReturnUrl { get; set; }
}