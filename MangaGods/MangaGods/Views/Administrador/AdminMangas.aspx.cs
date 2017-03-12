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
    public partial class AdminMangas : Page
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

            if (comboAutor.Items.Count <= 0) comboAutor.Items.Insert(0, "Seleccione.....");
            if (comboGenero.Items.Count <= 0) comboGenero.Items.Insert(0, "Seleccione.....");
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
                    Precio = convertirADouble(txtPrecio.Text),
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
        /// Evento que maneja la búsqueda de un manga por id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buscar_Click(object sender, EventArgs e)
        {
            CargarComboGeneros();
            CargarComboAutores();
            alerta.InnerText = string.Empty;
            var manga = Core.ObtenerMangaXId(Convert.ToInt32(txtId.Text));
            if (manga != null)
            {
                MostrarDatos(true);
                CargarDatos(manga);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdManga").ToString();
            }
        }

        /// <summary>
        /// Evento que maneja la actualización de los datos de un manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            bool cumple = false;
            string path = Server.MapPath("~/Catalogo/Imagenes/");

            //valida que la extensión del archivo sea la correcta
            if (archivoConsulta.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(archivoConsulta.FileName).ToLower();
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
                    archivoConsulta.PostedFile.SaveAs(path + archivoConsulta.FileName);
                }
                catch (Exception ex)
                {
                    alerta.InnerText = ex.Message;
                }

                //Se crea la entidad a crear
                var manga = new Manga
                {
                    Id = convertirAInt(txtId.Text),
                    Nombre = txtMangaConsulta.Text,
                    Descripcion = txtDescripcionConsulta.Text,
                    IdGenero = convertirAInt(comboGeneroConsulta.SelectedValue),
                    IdAutor = convertirAInt(comboAutorConsulta.SelectedValue),
                    Volumen = convertirAInt(txtVolumenConsulta.Text),
                    Precio = convertirADouble(txtPrecioConsulta.Text),
                    ImagePath = archivoConsulta.FileName
                };

                if (Core.ActualizarManga(manga))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionActualizacionManga").ToString();
                    LimpiarCampos(1);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarManga").ToString();
                }
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorFormatoArchivo").ToString();

            }
        }

        /// <summary>
        /// Evento que maneja el borrado de un manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Borrar_Click(object sender, EventArgs e)
        {
            if (Core.BorrarManga(Convert.ToInt32(txtId.Text)))
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionBorradoManga").ToString();
                LimpiarCampos(2);
                MostrarDatos(false);
            }
            else
            {
                alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorradoManga").ToString();
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
        /// convierte un string a double
        /// </summary>
        /// <returns></returns>
        public double convertirADouble(string valor)
        {
            try
            {
                return Convert.ToDouble(valor);
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

        /// <summary>
        /// Muestra los datos de un manga Consultado
        /// </summary>
        private void MostrarDatos(bool estado)
        {
            datosManga.Visible = estado;
            btnActualizar.Visible = estado;
            btnBorrar.Visible = estado;
            btnBuscar.Visible = !estado;
        }

        /// <summary>
        /// Asigna a la interfaz los datos consultados
        /// </summary>
        /// <param name="consulta"></param>
        private void CargarDatos(Manga consulta)
        {
            txtMangaConsulta.Text = consulta.Nombre;
            txtDescripcionConsulta.Text = consulta.Descripcion;
            txtPrecioConsulta.Text = consulta.Precio.ToString();
            txtVolumenConsulta.Text = consulta.Volumen.ToString();
            comboAutorConsulta.SelectedValue = consulta.IdAutor.ToString();
            comboGeneroConsulta.SelectedValue = consulta.IdGenero.ToString();
        }

        /// <summary>
        /// Carga la información de los géneros de manga manualmente
        /// </summary>
        public void CargarComboGeneros()
        {
            if (comboGeneroConsulta.Items.Count >= 2) return;
            comboGeneroConsulta.Items.Insert(0, "Seleccione.....");
            comboGeneroConsulta.DataSource = ((IEnumerable<Genero>)ObtenerTodosGeneros()).ToList();
            comboGeneroConsulta.DataBind();
        }

        /// <summary>
        /// Carga la información de los autores de manga manualmente
        /// </summary>
        public void CargarComboAutores()
        {
            if (comboAutorConsulta.Items.Count >= 2) return;
            comboAutorConsulta.Items.Insert(0, "Seleccione.....");
            comboAutorConsulta.DataSource = ((IEnumerable<Autor>)ObtenerTodosAutores()).ToList();
            comboAutorConsulta.DataBind();

        }
    }
}