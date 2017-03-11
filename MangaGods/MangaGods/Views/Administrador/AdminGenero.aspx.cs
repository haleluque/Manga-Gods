using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MangaGods.Views.Administrador
{
    /// <summary>
    /// Clase que maneja los eventos, atributos y métodos de la página de 
    /// administración de géneros de manga
    /// </summary>
    public partial class AdminGenero : Page
    {
        private CoreGenero Core;

        /// <summary>
        /// Método de Inicio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Core = new CoreGenero();
        }

        /// <summary>
        /// Evento que maneja la creación de un nuevo género
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CrearGenero_Click(object sender, EventArgs e)
        {
            // Se valida que la creación haya sido exitosa
            if (Core.CrearGenero(new Genero
            {
                Nombre = txtNombreGenero.Text,
                Descripcion = txtDescripcionGenero.Text
            }))
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionCreacionGenero").ToString();
                LimpiarCampos(1);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionGenero").ToString();
            }
        }

        /// <summary>
        /// Evento que maneja la búsqueda de un género por id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buscar_Click(object sender, EventArgs e)
        {
            alerta.InnerText = string.Empty;
            var genero = Core.ObtenerGeneroXId(Convert.ToInt32(txtId.Text));
            if (genero != null)
            {
                MostrarDatosGenero(true);
                CargarDatosGenero(genero);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdGenero").ToString();
            }
        }

        /// <summary>
        /// Evento que maneja la actualización de los datos de un género de manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            // Se valida que la creación haya sido exitosa
            if (Core.ActualizarGenero(new Genero
            {
                Id = Convert.ToInt32(txtId.Text),
                Nombre = txtGeneroConsulta.Text,
                Descripcion = txtDescripcionConsulta.Text
            }))
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionActualizacionGenero").ToString();
                LimpiarCampos(2);
                MostrarDatosGenero(false);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarAutor").ToString();
            }
        }

        /// <summary>
        /// Evento que maneja el borrado de un género
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Borrar_Click(object sender, EventArgs e)
        {
            if (Core.BorrarGenero(Convert.ToInt32(txtId.Text)))
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionBorradoGenero").ToString();
                LimpiarCampos(2);
                MostrarDatosGenero(false);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarAutor").ToString();
            }
        }

        /// <summary>
        /// Limpia los campos de la página de genero dependiendo del tipo
        /// de evento que se envía
        /// </summary>
        /// <param name="evento"></param>
        private void LimpiarCampos(int evento)
        {
            //Creando genero
            if (evento == 1)
            {
                txtNombreGenero.Text = string.Empty;
                txtDescripcionGenero.Text = string.Empty;
            }
            else if (evento == 2) //Actualizando
            {
                txtId.Text = string.Empty;
                txtGeneroConsulta.Text = string.Empty;
                txtDescripcionConsulta.Text = string.Empty;
            }
        }

        /// <summary>
        /// Muestra los datos de un autor Consultado
        /// </summary>
        private void MostrarDatosGenero(bool estado)
        {
            datosGenero.Visible = estado;
            btnActualizar.Visible = estado;
            btnBorrar.Visible = estado;
            btnBuscar.Visible = !estado;
        }

        /// <summary>
        /// Asigna a la interfaz los datos consultados
        /// </summary>
        /// <param name="consulta"></param>
        private void CargarDatosGenero(Genero consulta)
        {
            txtGeneroConsulta.Text = consulta.Nombre;
            txtDescripcionConsulta.Text = consulta.Descripcion;
        }
    }
}