using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Linq;
using System.Web.ModelBinding;
using System.Web.UI;

namespace MangaGods.Views
{
    public partial class Generos : Page
    {
        private CoreManga _core;

        protected void Page_Load(object sender, EventArgs e)
        {
            _core = new CoreManga();
        }

        /// <summary>
        /// Obtiene los mangas por id o en general
        /// </summary>
        /// <param name="genero"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public IQueryable<Manga> ObtenerTodosMangas([QueryString("id")] int? genero, [RouteData] string nombre)
        {
            try
            {
                if (genero != null)
                {
                    return _core.ObtenerMangaXIdGenero((int)genero);
                }
                return !string.IsNullOrEmpty(nombre) ? _core.ObtenerMangaXNombreGenero(nombre) : null;
            }
            catch (Exception n)
            {
                throw new Exception(n.Message, n);
            }
        }
    }
}