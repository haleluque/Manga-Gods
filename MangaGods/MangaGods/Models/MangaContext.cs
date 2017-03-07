namespace MangaGods.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MangaContext : DbContext
    {
        public MangaContext()
            : base("name=MangaContext")
        {
        }

        public virtual DbSet<Autor> Autor { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrden { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Manga> Manga { get; set; }
        public virtual DbSet<Orden> Orden { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>()
                .HasMany(e => e.Manga)
                .WithRequired(e => e.Autor)
                .HasForeignKey(e => e.IdAutor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genero>()
                .HasMany(e => e.Manga)
                .WithRequired(e => e.Genero)
                .HasForeignKey(e => e.IdGenero)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.Carrito)
                .WithRequired(e => e.Manga)
                .HasForeignKey(e => e.IdManga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manga>()
                .HasMany(e => e.DetalleOrden)
                .WithRequired(e => e.Manga)
                .HasForeignKey(e => e.IdManga)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Orden>()
                .HasMany(e => e.DetalleOrden)
                .WithRequired(e => e.Orden)
                .HasForeignKey(e => e.IdOrden)
                .WillCascadeOnDelete(false);
        }
    }
}
