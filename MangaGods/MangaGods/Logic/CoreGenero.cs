using System;
using System.Collections.Generic;
using MangaGods.Models;
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
            try
            {
                return Contexto.Genero;
            }
            catch (Exception)
            {
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosGeneros")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public List<Genero> ObtenerTodosGenerosLista()
        {
            try
            {
                return Contexto.Genero.ToList();
            }
            catch (Exception)
            {
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosGeneros")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene un genero por Id
        /// </summary>
        /// <returns></returns>
        public Genero ObtenerGeneroXId(int id)
        {
            try
            {
                return Contexto.Genero.FirstOrDefault(x => x.Id == id);
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
            }
            catch (Exception)
            {
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaGenero")?.ToString());
            }
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
                try
                {
                    Contexto.Genero.Add(nuevo);
                    Contexto.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionGenero")?.ToString());
                }
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
                try
                {
                    var consulta = Contexto.Genero.FirstOrDefault(x => x.Id == genero.Id);
                    if (consulta != null)
                    {
                        consulta.Nombre = genero.Nombre;
                        consulta.Descripcion = genero.Descripcion;
                    }
                    Contexto.SaveChanges();
                }
                catch (NullReferenceException)
                {
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception)
                {
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarGenero")?.ToString());
                }
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
                try
                {
                    Contexto.Genero.Remove(Contexto.Genero.FirstOrDefault(x => x.Id == id));
                    Contexto.SaveChanges();
                }
                catch (NullReferenceException)
                {                    
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception)
                {
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarGenero")?.ToString());
                }
            }
            return true;
        }
    }
}