using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Web.ModelBinding;
using System.Web.UI;

namespace MangaGods.Views
{
    public partial class DetalleManga : Page
    {
        private CoreManga _core;

        protected void Page_Load(object sender, EventArgs e)
        {
            _core = new CoreManga();
        }

        /// <summary>
        /// Obtiene un manga por Id
        /// </summary>
        /// <returns></returns>
        public Manga ObtenerMangaXId([QueryString("Id")] int? id)
        {
            return _core.ObtenerMangaXId(id ?? 0);
        }
    }
}