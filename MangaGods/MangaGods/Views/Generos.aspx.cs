using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MangaGods.Views
{
    public partial class Generos : System.Web.UI.Page
    {
        private CoreManga Core;

        protected void Page_Load(object sender, EventArgs e)
        {
            Core = new CoreManga();
        }

        /// <summary>
        /// Obtiene los mangas por id o en general
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        public IQueryable<Manga> ObtenerTodosMangas([QueryString("id")] int? genero)
        {
            return genero != null ? Core.ObtenerMangaXIdGenero((int)genero): null;
        }
    }
}