using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        private MangaContext contexto { get; set; }

        /// <summary>
        /// Constructor que instancia el contexto
        /// </summary>
        public CoreAutor()
        {
            contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene todos los autores de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosAutores()
        {
            return contexto.Autor;
        }

        /// <summary>
        /// Obtiene un autor por Id
        /// </summary>
        /// <returns></returns>
        public Autor ObtenerAutorXId(int id)
        {
            return contexto.Autor.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Crea un nuevo autor en la db
        /// </summary>
        /// <param name="nuevo"></param>
        /// <returns></returns>
        public bool CrearAutor(Autor nuevo)
        {
            using (contexto = new MangaContext())
            {
                contexto.Autor.Add(nuevo);
                contexto.SaveChanges();
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
            using (contexto = new MangaContext())
            {
                var consulta = contexto.Autor.FirstOrDefault(x => x.Id == autor.Id);
                consulta.Nombre = autor.Nombre;
                consulta.Edad = autor.Edad;
                consulta.Empresa = autor.Empresa;
                contexto.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Elimina un autor de la base de datos
        /// </summary>
        /// <returns></returns>
        public bool BorrarAutor(int id)
        {
            using (contexto = new MangaContext())
            {
                contexto.Autor.Remove(contexto.Autor.FirstOrDefault(x => x.Id == id));
            }
            return true;
        }
    }
}