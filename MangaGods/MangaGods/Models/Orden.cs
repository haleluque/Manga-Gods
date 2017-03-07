namespace MangaGods.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Orden")]
    public partial class Orden
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orden()
        {
            DetalleOrden = new HashSet<DetalleOrden>();
        }

        public int Id { get; set; }

        public DateTime FechaOrden { get; set; }

        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(160)]
        public string PrimerNombre { get; set; }

        [Required]
        [StringLength(160)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(70)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(40)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(40)]
        public string Departamento { get; set; }

        [Required]
        [StringLength(10)]
        public string CodigoPostal { get; set; }

        [Required]
        [StringLength(40)]
        public string Pais { get; set; }

        [Required]
        [StringLength(24)]
        public string Telefono { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal Total { get; set; }

        [Required]
        public string IdTransaccionPago { get; set; }

        public bool HaSidoEnviado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetalleOrden> DetalleOrden { get; set; }
    }
}
