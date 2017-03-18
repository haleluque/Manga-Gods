using System.Diagnostics.CodeAnalysis;

namespace MangaGods.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Autor")]
    public class Autor
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Autor()
        {
            Manga = new HashSet<Manga>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }

        public int? Edad { get; set; }

        [Required]
        [StringLength(100)]
        public string Empresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Manga> Manga { get; set; }
    }
}
