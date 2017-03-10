using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Web.UI;

namespace MangaGods.Views.Administrador
{
    /// <summary>
    /// Clase que maneja los eventos, atributos y eventos de la página de 
    /// administración de autores
    /// </summary>
    public partial class AdminAutores : Page
    {
        private CoreAutor Core;

        /// <summary>
        /// Método de Inicio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Core = new CoreAutor();
        }

        /// <summary>
        /// Evento que maneja la creación de un nuevo autor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CrearAutor_Click(object sender, EventArgs e)
        {
            // Se valida que la creación haya sido exitosa
            if (Core.CrearAutor(new Autor
            {
                Nombre = txtNombreAutor.Text,
                Edad = string.IsNullOrEmpty(txtEdad.Text) ? (int?)null : Convert.ToInt32(txtEdad.Text),
                Empresa = txtEmpresa.Text
            }))
            {
                alerta.InnerText = "El autor se ha creado exitosamente";
                LimpiarCampos();
            }
            else
            {
                alerta.InnerText = "Ha ocurrido un error al crear el autor.";
            }           
        }

        /// <summary>
        /// Evento que maneja la búsqueda de un autor por id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buscar_Click(object sender, EventArgs e)
        {
            var autor = Core.ObtenerAutorXId(Convert.ToInt32(txtId.Text));
            if (autor != null)
            {
                MostrarDatosAutor();
                CargarDatosAutor(autor);
            }
            else
            {
                alerta.InnerText = "El id escrito no exite";
            }
        }

        protected void Actualizar_Click(object sender, EventArgs e)
        {
        }

        protected void Borrar_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Muestra los datos de un autor Consultado
        /// </summary>
        private void MostrarDatosAutor()
        {
            datosAutor.Visible = true;
            btnActualizar.Visible = true;
            btnBorrar.Visible = true;
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
        /// Limpia los campos del formulario de creación de la
        /// página de autor
        /// </summary>
        private void LimpiarCampos()
        {
            txtNombreAutor.Text = string.Empty;
            txtEdad.Text = string.Empty;
            txtEmpresa.Text = string.Empty;
        }
    }
}