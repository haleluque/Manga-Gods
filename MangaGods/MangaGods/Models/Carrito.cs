namespace MangaGods.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Carrito")]
    public partial class Carrito
    {
        public string Id { get; set; }

        public string IdCarrito { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int IdManga { get; set; }

        public virtual Manga Manga { get; set; }
    }
}
