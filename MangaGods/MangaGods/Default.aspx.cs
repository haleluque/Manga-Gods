using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MangaGods
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Manejador de errores a nivel de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Error(object sender, EventArgs e)
        {
            // Get last error from the server.
            var exc = Server.GetLastError();
            // Handle specific exception.
            if (exc is InvalidOperationException)
            {
                // Pass the error on to the error page.
                Server.Transfer("/Views/Errores/ErrorPersonalizado.aspx?handler=Page_Error%20-%20Default.aspx", true);
            }
        }
    }
}