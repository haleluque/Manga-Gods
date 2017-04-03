using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MangaGods.Views.Administrador
{
    /// <summary>
    /// Clase que maneja los eventos, atributos y métodos de la página de 
    /// administración de mangas
    /// </summary>
    public partial class AdminMangas : Page
    {
        private CoreManga _core;
        private CoreGenero _coreGenero;
        private CoreAutor _coreAutor;

        /// <summary>
        /// Método de Inicio de página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            _core = new CoreManga();
            _coreGenero = new CoreGenero();
            _coreAutor = new CoreAutor();

            if (comboAutor.Items.Count <= 0) comboAutor.Items.Insert(0, "Seleccione.....");
            if (comboGenero.Items.Count <= 0) comboGenero.Items.Insert(0, "Seleccione.....");
        }

        /// <summary>
        /// Obtiene todos los generos de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosGeneros()
        {
            return _coreGenero.ObtenerTodosGeneros();
        }

        /// <summary>
        /// Obtiene todos los autores de la tabla
        /// </summary>
        /// <returns></returns>
        public IQueryable ObtenerTodosAutores()
        {
            return _coreAutor.ObtenerTodosAutores();
        }

        /// <summary>
        /// Evento que maneja la creación de un nuevo manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CrearManga_Click(object sender, EventArgs e)
        {
            try
            {
                var cumple = false;
                var path = Server.MapPath("~/Catalogo/Imagenes/");

                //valida que la extensión del archivo sea la correcta
                if (Archivo.HasFile)
                {
                    var fileExtension = System.IO.Path.GetExtension(Archivo.FileName)?.ToLower();
                    string[] extensionArchivo = { ".gif", ".png", ".jpeg", ".jpg" };
                    foreach (var extension in extensionArchivo)
                    {
                        if (fileExtension == extension)
                        {
                            cumple = true;
                        }
                    }
                }

                if (cumple)
                {
                    // Save to Images folder.
                    Archivo.PostedFile.SaveAs(path + Archivo.FileName);

                    //Se crea la entidad a crear
                    var manga = new Manga
                    {
                        Nombre = txtNombreManga.Text,
                        Descripcion = txtDescripcionManga.Text,
                        IdGenero = ConvertirAInt(comboGenero.SelectedValue),
                        IdAutor = ConvertirAInt(comboAutor.SelectedValue),
                        Volumen = ConvertirAInt(txtVolumen.Text),
                        Precio = ConvertirADouble(txtPrecio.Text),
                        ImagePath = Archivo.FileName
                    };

                    if (_core.CrearManga(manga))
                    {
                        alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionCreacionManga")?.ToString();
                        LimpiarCampos(1);
                    }
                    else
                    {
                        alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCreacionManga")?.ToString();
                    }
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorFormatoArchivo")?.ToString();
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
            }
        }

        /// <summary>
        /// Evento que maneja la búsqueda de un manga por id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Buscar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarComboGeneros();
                CargarComboAutores();
                alerta.InnerText = string.Empty;
                var manga = _core.ObtenerMangaXId(Convert.ToInt32(txtId.Text));
                if (manga != null)
                {
                    MostrarDatos(true);
                    CargarDatos(manga);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorIdManga")?.ToString();
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
            }
        }

        /// <summary>
        /// Evento que maneja la actualización de los datos de un manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                var cumple = false;
                var path = Server.MapPath("~/Catalogo/Imagenes/");

                //valida que la extensión del archivo sea la correcta
                if (archivoConsulta.HasFile)
                {
                    var fileExtension = System.IO.Path.GetExtension(archivoConsulta.FileName)?.ToLower();
                    string[] extensionArchivo = { ".gif", ".png", ".jpeg", ".jpg" };
                    foreach (var extension in extensionArchivo)
                    {
                        if (fileExtension == extension)
                        {
                            cumple = true;
                        }
                    }
                }

                if (cumple)
                {
                    // Save to Images folder.
                    archivoConsulta.PostedFile.SaveAs(path + archivoConsulta.FileName);

                    //Se crea la entidad a crear
                    var manga = new Manga
                    {
                        Id = ConvertirAInt(txtId.Text),
                        Nombre = txtMangaConsulta.Text,
                        Descripcion = txtDescripcionConsulta.Text,
                        IdGenero = ConvertirAInt(comboGeneroConsulta.SelectedValue),
                        IdAutor = ConvertirAInt(comboAutorConsulta.SelectedValue),
                        Volumen = ConvertirAInt(txtVolumenConsulta.Text),
                        Precio = ConvertirADouble(txtPrecioConsulta.Text),
                        ImagePath = archivoConsulta.FileName
                    };

                    if (_core.ActualizarManga(manga))
                    {
                        alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionActualizacionManga")?.ToString();
                        LimpiarCampos(1);
                    }
                    else
                    {
                        alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarManga")?.ToString();
                    }
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorFormatoArchivo")?.ToString();
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
            }
        }

        /// <summary>
        /// Evento que maneja el borrado de un manga
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Borrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_core.BorrarManga(Convert.ToInt32(txtId.Text)))
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ConfirmacionBorradoManga")?.ToString();
                    LimpiarCampos(2);
                    MostrarDatos(false);
                }
                else
                {
                    alerta.InnerText = HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorradoManga")?.ToString();
                }
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
            }
        }

        /// <summary>
        /// Evento que maneja la cancelación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos(2);
            MostrarDatos(false);
        }

        /// <summary>
        /// convierte un string a int
        /// </summary>
        /// <returns></returns>
        public int ConvertirAInt(string valor)
        {
            try
            {
                return Convert.ToInt32(valor);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
            }
        }

        /// <summary>
        /// convierte un string a double
        /// </summary>
        /// <returns></returns>
        public double ConvertirADouble(string valor)
        {
            try
            {
                return Convert.ToDouble(valor);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString());
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
            switch (evento)
            {
                case 1:
                    txtNombreManga.Text = string.Empty;
                    txtDescripcionManga.Text = string.Empty;
                    comboGenero.SelectedIndex = 0;
                    comboAutor.SelectedIndex = 0;
                    txtVolumen.Text = string.Empty;
                    txtPrecio.Text = string.Empty;
                    Archivo.Attributes.Clear();
                    break;
                case 2:
                    txtId.Text = string.Empty;
                    txtMangaConsulta.Text = string.Empty;
                    txtDescripcionConsulta.Text = string.Empty;
                    comboGeneroConsulta.SelectedIndex = 0;
                    comboAutorConsulta.SelectedIndex = 0;
                    txtVolumenConsulta.Text = string.Empty;
                    txtPrecioConsulta.Text = string.Empty;
                    archivoConsulta.Attributes.Clear();
                    break;
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
            btnCancelar.Visible = estado;
        }

        /// <summary>
        /// Asigna a la interfaz los datos consultados
        /// </summary>
        /// <param name="consulta"></param>
        private void CargarDatos(Manga consulta)
        {
            txtMangaConsulta.Text = consulta.Nombre;
            txtDescripcionConsulta.Text = consulta.Descripcion;
            txtPrecioConsulta.Text = consulta.Precio.ToString(CultureInfo.CurrentCulture);
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
            comboGeneroConsulta.DataSource = (ObtenerTodosGeneros() as IEnumerable<Genero>).ToList();
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

        /// <summary>
        /// Manejador de errores de la página de administración de mangas de la app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Error(object sender, EventArgs e)
        {
            // Redireccionan a la página de errores
            Server.Transfer("/Views/Errores/ErrorPersonalizado.aspx?handler=Page_Error%20-%AdminMangas.aspx.cs", false);
        }
    }
}