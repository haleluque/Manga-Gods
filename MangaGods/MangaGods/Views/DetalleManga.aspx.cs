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
    public partial class DetalleManga : System.Web.UI.Page
    {
        CoreManga core;

        protected void Page_Load(object sender, EventArgs e)
        {
            core = new CoreManga();
        }

        /// <summary>
        /// Obtiene un manga por Id
        /// </summary>
        /// <returns></returns>
        public Manga ObtenerMangaXId([QueryString("Id")] int? id)
        {
            return core.ObtenerMangaXId(id != null ? (int)id : 0);
        }
    }
}