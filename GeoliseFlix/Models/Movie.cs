using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeoliseFlix.Models;

[Table("Movie")]
public class Movie
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(100, ErrorMessage = "O título deve possuir no máximo 100 caracteres")]

    public string Title { get; set; }

     [Display(Name = "Título Original")]
    [Required(ErrorMessage = "O título original é obrigatório")]
    [StringLength(100, ErrorMessage = "O título original deve possuir no máximo 100 caracteres")]

    public string TitleOriginal { get; set; }

     [Display(Name = "Sinopse")]
    [Required(ErrorMessage = "A sinopse é obrigatória")]
    [StringLength(8000, ErrorMessage = "A sinopse deve possuir no máximo 8000 caracteres")]

    public string Synopsis { get; set; }

    [Column(TypeName = "Year")]
    [Display(Name = "Ano de Estreia")]
    [Required(ErrorMessage = "O ano de estreia é obrigatório")]
    public Int16 MovieYear { get; set; }

    [Display(Name = "Duração (em minutos")]
    [Required(ErrorMessage = "A duração é obrigatória")]
    public Int16 Duration { get; set; }

    [Display(Name = "Classificação Etária")]
    [Required(ErrorMessage = "A classificação etária é obrigatória")]
    public byte MyProperty { get; set; }

    [StringLength(200)]
    [Display(Name = "Foto")]
    public string Image { get; set; }

    [NotMapped]
    [Display(Name = "Duração")]
    public string HourDuration { get {
        return TimeSpan.FromMinutes(Duration).ToString(@"%h'h 'mm'min'");
    }}







}
