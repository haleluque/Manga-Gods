namespace MangaGods.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Manga")]
    public class Manga
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manga()
        {
            Carrito = new HashSet<Carrito>();
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(70)]
        public string Nombre { get; set; }

        [Required]
        //[StringLength(500)]
        public string Descripcion { get; set; }

        public int IdGenero { get; set; }

        public int Volumen { get; set; }

        public double Precio { get; set; }

        [Required]
        public string ImagePath { get; set; }

        public int IdAutor { get; set; }

        public virtual Autor Autor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Carrito> Carrito { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }

        public virtual Genero Genero { get; set; }
    }
}
