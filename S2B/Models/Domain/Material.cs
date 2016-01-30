using System;
using System.ComponentModel.DataAnnotations;

namespace S2B.Models.Domain
{
    /// <summary>
    /// Classe Material que é o material que uma lista pode possuir.
    /// </summary>
    public class Material
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da lista é necessário!", AllowEmptyStrings = false)]
        [StringLength(25)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A quantida de objetos é necessário!", AllowEmptyStrings = false)]
        [MaxLength(10000, ErrorMessage = "Limite atingido para sua segurança!")]
        public int Quantidade { get; set; }

        [StringLength(60)]
        public string Comentario { get; set; }

        public DateTime? Validade { get; set; }

        public int ArmazenamentoId { get; set; }
        public virtual Armazenamento Armazenamentos { get; set; }
    }
}
