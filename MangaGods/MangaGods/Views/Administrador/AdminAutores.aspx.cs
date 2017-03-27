using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Web;
using System.Web.UI;

namespace MangaGods.Views.Administrador
{
    /// <summary>
    /// Clase que maneja los eventos, atributos y métodos de la página de 
    /// administración de autores de manga
    /// </summary>
    public partial class AdminAutores : Page
    {
        /// <summary>
        /// Instancia de la clase core correspondiente
        /// </summary>
        private CoreAutor _core;

        /// <summary>
        /// Método de Inicio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _core = new CoreAutor();
        }

        /// <summary>
        /// Evento que maneja la búsqueda de un autor por id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                alerta.InnerText = string.Empty;
                var autor = _core.ObtenerAutorXId(Convert.ToInt32(txtId.Text));
                if (autor != null)
                {
                    MostrarDatosAutor(true);
                    CargarDatosAutor(autor);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdAutor")?.ToString();
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
            catch (Exception n)
            {
                throw new Exception(n.Message, n);
            }
        }

        /// <summary>
        /// Evento que maneja la creación de un nuevo autor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CrearAutor_Click(object sender, EventArgs e)
        {
            try
            {
                // Se valida que la creación haya sido exitosa
                if (_core.CrearAutor(new Autor
                {
                    Nombre = txtNombreAutor.Text,
                    Edad = string.IsNullOrEmpty(txtEdad.Text) ? (int?)null : Convert.ToInt32(txtEdad.Text),
                    Empresa = txtEmpresa.Text
                }))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionCreacionAutor")?.ToString();
                    LimpiarCampos(1);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionAutor")?.ToString();
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
            catch (Exception n)
            {
                throw new Exception(n.Message, n);
            }
        }

        /// <summary>
        /// Evento que maneja la actualización de los datos de un autor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Se valida que la creación haya sido exitosa
                if (_core.ActualizarAutor(new Autor
                {
                    Id = Convert.ToInt32(txtId.Text),
                    Nombre = txtNombreConsulta.Text,
                    Edad = string.IsNullOrEmpty(txtEdadConsulta.Text) ? (int?)null : Convert.ToInt32(txtEdadConsulta.Text),
                    Empresa = txtEmpresaConsulta.Text
                }))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionActualizacionAutor")?.ToString();
                    LimpiarCampos(2);
                    MostrarDatosAutor(false);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarAutor")?.ToString();
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
            catch (Exception n)
            {
                throw new Exception(n.Message, n);
            }
        }

        /// <summary>
        /// Evento que maneja el borrado de un autor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Borrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_core.BorrarAutor(Convert.ToInt32(txtId.Text)))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionBorradoAutor")?.ToString();
                    LimpiarCampos(2);
                    MostrarDatosAutor(false);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarAutor")?.ToString();
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
            catch (Exception n)
            {
                throw new Exception(n.Message, n);
            }
        }

        /// <summary>
        /// Muestra los datos de un autor Consultado
        /// </summary>
        private void MostrarDatosAutor(bool estado)
        {
            datosAutor.Visible = estado;
            btnActualizar.Visible = estado;
            btnBorrar.Visible = estado;
            btnBuscar.Visible = !estado;
        }

        /// <summary>
        /// Asigna a la interfaz los datos consultados
        /// </summary>
        /// <param name="consulta"></param>
        private void CargarDatosAutor(Autor consulta)
        {
            txtNombreConsulta.Text = consulta.Nombre;
            txtEdadConsulta.Text = consulta.Edad == null ? "" : consulta.Edad.ToString();
            txtEmpresaConsulta.Text = consulta.Empresa;
        }

        /// <summary>
        /// Limpia los campos de la página de autor dependiendo del tipo
        /// de evento que se envía
        /// </summary>
        /// <param name="evento"></param>
        private void LimpiarCampos(int evento)
        {
            switch (evento)
            {
                case 1:
                    txtNombreAutor.Text = string.Empty;
                    txtEdad.Text = string.Empty;
                    txtEmpresa.Text = string.Empty;
                    break;
                case 2:
                    txtId.Text = string.Empty;
                    txtNombreConsulta.Text = string.Empty;
                    txtEdadConsulta.Text = string.Empty;
                    txtEmpresaConsulta.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Manejador de errores de la página de administración de autores de la app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Error(object sender, EventArgs e)
        {
            // Redireccionan a la página de errores
            Server.Transfer("/Views/Errores/ErrorPersonalizado.aspx?handler=Page_Error%20-%20AdminAutores.aspx.cs", true);
        }
    }
}