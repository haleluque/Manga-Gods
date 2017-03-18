namespace MangaGods.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Carrito")]
    public class Carrito
    {
        public string Id { get; set; }

        public string IdCarrito { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaCreacion { get; set; }

        public int IdManga { get; set; }

        public virtual Manga Manga { get; set; }
    }
}
