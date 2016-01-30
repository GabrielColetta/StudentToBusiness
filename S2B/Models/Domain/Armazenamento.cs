using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S2B.Models.Domain
{
    /// <summary>
    /// Classe Armazenamento que serve como uma lista de todos materiais que a pessoa pode possuir.
    /// </summary>
    public class Armazenamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da lista é necessário!", AllowEmptyStrings = false)]
        [StringLength(25)]
        public string Nome { get; set; }

        [StringLength(140)]
        public string Comentario { get; set; }

        [Required(ErrorMessage = "A categoria é necessária!", AllowEmptyStrings = false)]
        public string Categorias { get; set; }

        public DateTime DataCriacao { get; set; }

        public virtual ICollection<Material> Materiais { get; set; }
    }
}