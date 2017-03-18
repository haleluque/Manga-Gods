using System.Diagnostics.CodeAnalysis;

namespace MangaGods.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Genero")]
    public class Genero
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genero()
        {
            Manga = new HashSet<Manga>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manga> Manga { get; set; }
    }
}
