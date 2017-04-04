using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MangaGods.Logic;
using MangaGods.Models;

namespace MangaGods.Views
{
    public partial class ListaGeneros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public List<Genero> ObtenerTodosGeneros()
        {
            var core = new CoreGenero();
            return core.ObtenerTodosGenerosLista();
        }
    }
}