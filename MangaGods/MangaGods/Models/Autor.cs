namespace MangaGods.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Autor")]
    public partial class Autor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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
