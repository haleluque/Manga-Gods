using System;
using MangaGods.Models;
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
            try
            {
                return Contexto.Autor;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosAutores")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosAutores")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene un autor por Id
        /// </summary>
        /// <returns></returns>
        public Autor ObtenerAutorXId(int id)
        {
            try
            {
                return Contexto.Autor.FirstOrDefault(x => x.Id == id);
            }
            catch (NullReferenceException e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaAutor")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaAutor")?.ToString());
            }
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
                try
                {
                    Contexto.Autor.Add(nuevo);
                    Contexto.SaveChanges();
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionAutor")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionAutor")?.ToString());
                }
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
                try
                {
                    var consulta = Contexto.Autor.FirstOrDefault(x => x.Id == autor.Id);
                    if (consulta == null) return true;
                    consulta.Nombre = autor.Nombre;
                    consulta.Edad = autor.Edad;
                    consulta.Empresa = autor.Empresa;
                    Contexto.SaveChanges();
                }
                catch (NullReferenceException e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarAutor")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarAutor")?.ToString());
                }
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
                try
                {
                    Contexto.Autor.Remove(Contexto.Autor.FirstOrDefault(x => x.Id == id));
                    Contexto.SaveChanges();
                }
                catch (NullReferenceException e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarAutor")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarAutor")?.ToString());
                }
            }
            return true;
        }
    }
}