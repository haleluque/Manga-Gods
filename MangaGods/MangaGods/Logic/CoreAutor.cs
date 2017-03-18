using MangaGods.Models;
using System.Linq;

namespace MangaGods.Logic
{
    /// <summary>
    /// Clase que contiene la Lógica referente a autores de manga
    /// </summary>
    public class CoreAutor
    {
        /// <summary>
        /// Variable de tipo contexto para manejo de datos con 
        /// EF
        /// </summary>
        private MangaContext Contexto { get; set; }

        /// <summary>
        /// Constructor que instancia el contexto
        /// </summary>
        public CoreAutor()
        {
            Contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene todos los autores de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosAutores()
        {
            return Contexto.Autor;
        }

        /// <summary>
        /// Obtiene un autor por Id
        /// </summary>
        /// <returns></returns>
        public Autor ObtenerAutorXId(int id)
        {
            return Contexto.Autor.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Crea un nuevo autor en la db
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public bool CrearAutor(Autor nuevo)
        {
            using (Contexto = new MangaContext())
            {
                Contexto.Autor.Add(nuevo);
                Contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Actualia los datos de un autor de la db
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        public bool ActualizarAutor(Autor autor)
        {
            using (Contexto = new MangaContext())
            {
                var consulta = Contexto.Autor.FirstOrDefault(x => x.Id == autor.Id);
                if (consulta != null)
                {
                    consulta.Nombre = autor.Nombre;
                    consulta.Edad = autor.Edad;
                    consulta.Empresa = autor.Empresa;
                }
                Contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Elimina un autor de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool BorrarAutor(int id)
        {
            using (Contexto = new MangaContext())
            {
                Contexto.Autor.Remove(Contexto.Autor.FirstOrDefault(x => x.Id == id));
                Contexto.SaveChanges();
            }
            return true;
        }
    }
}