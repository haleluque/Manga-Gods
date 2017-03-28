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
        public Manga ObtenerMangaXId([QueryString("Id")] int? id, [RouteData] string nombre)
        {
            if (id != null)
            {
                return _core.ObtenerMangaXId(id ?? 0);
            }
            return !string.IsNullOrEmpty(nombre) ? _core.ObtenerMangaXNombre(nombre) : null;
        }

        /// <summary>
        /// Manejador de errores de la página del detalle del manga.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Error(object sender, EventArgs e)
        {
            // Redireccionan a la página de errores
            Server.Transfer("/Views/Errores/ErrorPersonalizado.aspx?handler=Page_Error%20-%DetalleManga.aspx.cs", false);
        }
    }
}