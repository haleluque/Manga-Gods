using MangaGods.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;

namespace MangaGods.Logic
{
    /// <summary>
    /// Clase que contiene la Lógica referente a mangas
    /// </summary>
    public class CoreManga
    {
        /// <summary>
        /// Variable de tipo contexto para manejo de datos con 
        /// EF
        /// </summary>
        private MangaContext Contexto { get; set; }

        /// <summary>
        /// Constructor que instancia el contexto
        /// </summary>
        public CoreManga()
        {
            Contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene todos los mangas de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosMangas()
        {
            return Contexto.Manga;
        }

        /// <summary>
        /// Obtiene un manga por Id
        /// </summary>
        /// <returns></returns>
        public Manga ObtenerMangaXId(int id)
        {
            return Contexto.Manga.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Obtiene un manga por el id del género
        /// </summary>
        /// <returns></returns>
        public IQueryable<Manga> ObtenerMangaXIdGenero(int id)
        {
            return Contexto.Manga.Where(x => x.IdGenero == id);
        }

        /// <summary>
        /// Crea un nuevo manga en la db
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public bool CrearManga(Manga nuevo)
        {
            try
            {
                using (Contexto = new MangaContext())
                {
                    Contexto.Manga.Add(nuevo);
                    Contexto.SaveChanges();
                }
                return true;
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Actualia los datos de un manga de la db
        /// </summary>
        /// <param name="manga"></param>
        /// <returns></returns>
        public bool ActualizarManga(Manga manga)
        {
            using (Contexto = new MangaContext())
            {
                var consulta = Contexto.Manga.FirstOrDefault(x => x.Id == manga.Id);
                if (consulta != null)
                {
                    consulta.Nombre = manga.Nombre;
                    consulta.Descripcion = manga.Descripcion;
                    consulta.IdGenero = manga.IdGenero;
                    consulta.Volumen = manga.Volumen;
                    consulta.Precio = manga.Precio;
                    consulta.ImagePath = manga.ImagePath;
                    consulta.IdAutor = manga.IdAutor;
                }
                Contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Elimina un manga de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool BorrarManga(int id)
        {
            using (Contexto = new MangaContext())
            {
                Contexto.Manga.Remove(Contexto.Manga.FirstOrDefault(x => x.Id == id));
                Contexto.SaveChanges();
            }
            return true;
        }
    }
}