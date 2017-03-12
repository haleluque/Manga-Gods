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
    /// administración de mangas
    /// </summary>
    public partial class AdminMangas : System.Web.UI.Page
    {
        private CoreManga Core;
        private CoreGenero CoreGenero;
        private CoreAutor CoreAutor;

        /// <summary>
        /// Método de Inicio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Core = new CoreManga();
            CoreGenero = new CoreGenero();
            CoreAutor = new CoreAutor();
            comboAutor.Items.Insert(0, "Seleccione.....");
            comboGenero.Items.Insert(0, "Seleccione.....");
        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosGeneros()
        {
            return CoreGenero.ObtenerTodosGeneros();
        }

        /// Obtiene todos los autores de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosAutores()
        {
            return CoreAutor.ObtenerTodosAutores();
        }

        /// <summary>
        /// Evento que maneja la creación de un nuevo manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CrearManga_Click(object sender, EventArgs e)
        {
            bool cumple = false;
            string path = Server.MapPath("~/Catalogo/Imagenes/");

            //valida que la extensión del archivo sea la correcta
            if (Archivo.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(Archivo.FileName).ToLower();
                string[] extensionArchivo = { ".gif", ".png", ".jpeg", ".jpg" };
                for (int i = 0; i < extensionArchivo.Length; i++)
                {
                    if (fileExtension == extensionArchivo[i])
                    {
                        cumple = true;
                    }
                }
            }

            if (cumple)
            {
                try
                {
                    // Save to Images folder.
                    Archivo.PostedFile.SaveAs(path + Archivo.FileName);
                }
                catch (Exception ex)
                {
                    alerta.InnerText = ex.Message;
                }

                //Se crea la entidad a crear
                var manga = new Manga
                {
                    Nombre = txtNombreManga.Text,
                    Descripcion = txtDescripcionManga.Text,
                    IdGenero = convertirAInt(comboGenero.SelectedValue),
                    IdAutor = convertirAInt(comboAutor.SelectedValue),
                    Volumen = convertirAInt(txtVolumen.Text),
                    Precio = convertirAInt(txtPrecio.Text),
                    ImagePath = Archivo.FileName
                };

                if (Core.CrearManga(manga))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionCreacionManga").ToString();
                    LimpiarCampos(1);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionManga").ToString();
                }
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorFormatoArchivo").ToString();
                
            }
        }

        /// <summary>
        /// convierte un string a int
        /// </summary>
        /// <returns></returns>
        public int convertirAInt(string valor)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch (FormatException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Limpia los campos de la página de manga dependiendo del tipo
        /// de evento que se envía
        /// </summary>
        /// <param name="evento"></param>
        private void LimpiarCampos(int evento)
        {
            //Creando genero
            if (evento == 1)
            {
                txtNombreManga.Text = string.Empty;
                txtDescripcionManga.Text = string.Empty;
                comboGenero.SelectedIndex = 0;
                comboAutor.SelectedIndex = 0;
                txtVolumen.Text = string.Empty;
                txtPrecio.Text = string.Empty;
                Archivo.Attributes.Clear();
            }
            else if (evento == 2) //Actualizando
            {
            }
        }
    }
}