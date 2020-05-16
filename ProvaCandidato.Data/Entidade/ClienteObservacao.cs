using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("ClienteObservacao")]
    public class ClienteObservacao : IEntidade
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [Column("observacao")]
        [StringLength(300)]
        [Required]
        [MinLength(3, ErrorMessage = "A observação deve possuir no mínimo 3 caracteres")]
        [MaxLength(300, ErrorMessage = "A observação deve possuir no máximo 50 caracteres")]
        public string Observacao { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }
    }
}
