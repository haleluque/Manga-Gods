using MangaGods.Models;
using System.Linq;

namespace MangaGods.Logic
{
    /// <summary>
    /// Clase que almacena la lógica de los generos de manga
    /// </summary>
    public class CoreGenero
    {
        /// <summary>
        /// Variable de tipo contexto para manejo de datos con 
        /// EF
        /// </summary>
        private MangaContext Contexto { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CoreGenero()
        {
            Contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosGeneros()
        {
            return Contexto.Genero;
        }

        /// <summary>
        /// Obtiene un genero por Id
        /// </summary>
        /// <returns></returns>
        public Genero ObtenerGeneroXId(int id)
        {
            return Contexto.Genero.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Crea un nuevo genero en la db
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public bool CrearGenero(Genero nuevo)
        {
            using (Contexto = new MangaContext())
            {
                Contexto.Genero.Add(nuevo);
                Contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualia los datos de un genero de la db
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        public bool ActualizarGenero(Genero genero)
        {
            using (Contexto = new MangaContext())
            {
                var consulta = Contexto.Genero.FirstOrDefault(x => x.Id == genero.Id);
                if (consulta != null)
                {
                    consulta.Nombre = genero.Nombre;
                    consulta.Descripcion = genero.Descripcion;
                }
                Contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Elimina un genero de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool BorrarGenero(int id)
        {
            using (Contexto = new MangaContext())
            {
                Contexto.Genero.Remove(Contexto.Genero.FirstOrDefault(x => x.Id == id));
                Contexto.SaveChanges();
            }
            return true;
        }
    }
}