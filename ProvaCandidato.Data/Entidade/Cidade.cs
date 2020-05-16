using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cidade")]
    public class Cidade : IEntidade
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("nome")]
        [StringLength(50)]
        [Required]
        [MinLength(3, ErrorMessage = "O nome da cidade deve possuir no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome da cidade deve possuir no máximo 50 caracteres")]
        public string Nome { get; set; }
    }
}
