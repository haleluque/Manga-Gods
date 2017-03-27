using System;
using MangaGods.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

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
            try
            {
                return Contexto.Manga;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosManga")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosManga")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene un manga por Id
        /// </summary>
        /// <returns></returns>
        public Manga ObtenerMangaXId(int id)
        {
            try
            {
                return Contexto.Manga.FirstOrDefault(x => x.Id == id);
            }
            catch (NullReferenceException e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaManga")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaManga")?.ToString());
            }
        }


        /// <summary>
        /// Obtiene un manga por nombre
        /// </summary>
        /// <returns></returns>
        public Manga ObtenerMangaXNombre(string nombre)
        {
            try
            {
                return Contexto.Manga.FirstOrDefault(x => x.Nombre.Contains(nombre));
            }
            catch (NullReferenceException e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorNombreMangaIncorrecto")?.ToString());
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorNombreMangaIncorrecto")?.ToString());
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaNombre")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaNombre")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene un manga por el id del género
        /// </summary>
        /// <returns></returns>
        public IQueryable<Manga> ObtenerMangaXIdGenero(int id)
        {
            try
            {
                return Contexto.Manga.Where(x => x.IdGenero == id);
            }
            catch (NullReferenceException e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaGenero")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaGenero")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene un manga por el nombre del genero
        /// </summary>
        /// <returns></returns>
        public IQueryable<Manga> ObtenerMangaXNombreGenero(string nombre)
        {
            try
            {
                return Contexto.Manga.Where(x => x.Genero.Nombre.Contains(nombre));
            }
            catch (NullReferenceException e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaNombreGenero")?.ToString());
                throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaNombreGenero")?.ToString());
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaGenero")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultaMangaGenero")?.ToString());
            }
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
                        Trace.TraceInformation($"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}");
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionManga")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionManga")?.ToString());
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
                try
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
                catch (NullReferenceException e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarManga")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarManga")?.ToString());
                }
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
                try
                {
                    Contexto.Manga.Remove(Contexto.Manga.FirstOrDefault(x => x.Id == id));
                    Contexto.SaveChanges();
                }
                catch (NullReferenceException e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                    throw new NullReferenceException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdIncorrecto")?.ToString());
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorradoManga")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorradoManga")?.ToString());
                }
            }
            return true;
        }
    }
}