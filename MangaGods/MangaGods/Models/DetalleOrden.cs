namespace MangaGods.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetalleOrden")]
    public partial class DetalleOrden
    {
        public int Id { get; set; }

        public int IdOrden { get; set; }

        public string NombreUsuario { get; set; }

        public int IdManga { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

        public virtual Manga Manga { get; set; }

        public virtual Orden Orden { get; set; }
    }
}
