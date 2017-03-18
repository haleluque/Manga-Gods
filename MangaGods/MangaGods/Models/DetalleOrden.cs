namespace MangaGods.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DetalleOrden")]
    public class DetalleOrden
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
