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
        /// <returns></returns>
        public IQueryable<Manga> ObtenerTodosMangas([QueryString("id")] int? genero)
        {
            return genero != null ? _core.ObtenerMangaXIdGenero((int)genero): null;
        }
    }
}