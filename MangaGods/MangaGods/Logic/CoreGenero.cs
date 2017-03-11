using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        private MangaContext contexto { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public CoreGenero()
        {
            contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosGeneros()
        {
            return contexto.Genero;
        }

        /// <summary>
        /// Obtiene un genero por Id
        /// </summary>
        /// <returns></returns>
        public Genero ObtenerGeneroXId(int id)
        {
            return contexto.Genero.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Crea un nuevo genero en la db
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public bool CrearGenero(Genero nuevo)
        {
            using (contexto = new MangaContext())
            {
                contexto.Genero.Add(nuevo);
                contexto.SaveChanges();
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
            using (contexto = new MangaContext())
            {
                var consulta = contexto.Genero.FirstOrDefault(x => x.Id == genero.Id);
                consulta.Nombre = genero.Nombre;
                consulta.Descripcion = genero.Descripcion;
                contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Elimina un genero de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool BorrarGenero(int id)
        {
            using (contexto = new MangaContext())
            {
                contexto.Genero.Remove(contexto.Genero.FirstOrDefault(x => x.Id == id));
                contexto.SaveChanges();
            }
            return true;
        }
    }
}