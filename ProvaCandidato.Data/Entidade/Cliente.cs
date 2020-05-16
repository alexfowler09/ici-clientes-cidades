using ProvaCandidato.Data.Validador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaCandidato.Data.Entidade
{
    [Table("Cliente")]
    public class Cliente : IEntidade
    {
        [Key]
        [Column("codigo")]
        public int Codigo { get; set; }

        [StringLength(50)]
        [Required]
        [Column("nome")]
        [MinLength(3, ErrorMessage = "O nome do cliente deve possuir no mínimo 3 caracteres")]
        [MaxLength(50, ErrorMessage = "O nome do cliente deve possuir no máximo 50 caracteres")]

        public string Nome { get; set; }

        [Column("data_nascimento")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataNascimento(ErrorMessage = "Data deve preenchida e menor do que data atual")]
        public DateTime? DataNascimento { get; set; }

        [Column("codigo_cidade")]
        [Display(Name = "Cidade")]
        [Cidade(ErrorMessage = "Cidade deve ser preenchida")]
        public int CidadeId { get; set; }

        public bool Ativo { get; set; }

        [ForeignKey("CidadeId")]
        public virtual Cidade Cidade { get; set; }

        public virtual ICollection<ClienteObservacao> Observacoes { get; set; }
    }
}