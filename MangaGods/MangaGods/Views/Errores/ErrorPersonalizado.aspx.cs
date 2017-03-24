using System;
using System.Web;
using System.Web.UI;
using MangaGods.Logic;

namespace MangaGods.Views.Errores
{
    public partial class ErrorPersonalizado : Page
    {
        /// <summary>
        /// Para poder recibir la información del error se debe activar la configuración
        /// de customErrors en el web.config de la app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se crean los mensajes de errores.
            const string generalErrorMsg = "Ha ocurrido un problema con la aplicación. Por favor intente de nuevo. Si el error persiste, por favor contacte con soporte técnico.";
            const string httpErrorMsg = "Ha ocurrido un error de tipo HTTP. Página no encontrada. Por favor intente de nuevo.";
            const string unhandledErrorMsg = "The error was unhandled by application code.";
            
            // Se despliega el mensaje
            FriendlyErrorMsg.Text = generalErrorMsg;

            // Se determina si el error fue manejado correctamente.
            var errorHandler = Request.QueryString["handler"] ?? "Error Page";
            
            // Obtiene el último error del servidor
            var ex = Server.GetLastError();

            // Obtiene el no. del error enviado por query string.
            var errorMsg = Request.QueryString["msg"];

            if (errorMsg == "404")
            {
                ex = new HttpException(404, httpErrorMsg, ex);
                FriendlyErrorMsg.Text = ex.Message;
            }

            // Si la excepcion no existe, envia un error genérico
            if (ex == null)
            {
                ex = new Exception(unhandledErrorMsg);
            }

            // Muestra el detalle del error, solo a los desarrolladores
            if (Request.IsLocal)
            {
                // Detailed Error Message.
                ErrorDetailedMsg.Text = ex.Message;
                // Show where the error was handled.
                ErrorHandler.Text = errorHandler;
                // Show local access details.
                DetailedErrorPanel.Visible = true;
                if (ex.InnerException != null)
                {
                    InnerMessage.Text = ex.GetType() + @"<br/>" +
                    ex.InnerException.Message;
                    InnerTrace.Text = ex.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = ex.GetType().ToString();
                    if (ex.StackTrace != null)
                    {
                        InnerTrace.Text = ex.StackTrace.TrimStart();
                    }
                }
            }

            // Captura en el log la excepción
            ExceptionUtility.LogException(ex, errorHandler);
            // Limpia el objeto
            Server.ClearError();
        }
    }
}