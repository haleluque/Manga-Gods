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