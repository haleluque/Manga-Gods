using MangaGods.Logic;
using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MangaGods.Views
{
    public partial class CarritoCompra : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var idManga = RouteData.Values.Any() ? RouteData.Values["Id"].ToString() : null;

            if (!string.IsNullOrEmpty(idManga))
            {
                using (var core = new CoreCarrito())
                {
                    core.AgregarManga(Convert.ToInt16(idManga));
                    RouteData.Values.Remove("Id");
                    Response.RedirectToRoute("RutaCarritoCompra");
                }
            }

            ValidarCarro();
        }

        private void ValidarCarro()
        {
            using (CoreCarrito core = new CoreCarrito())
            {
                var totalCarro = core.CalcularTotalPago();
                if (totalCarro > 0)
                {
                    lblTotal.Text = $"{totalCarro:N2}";
                }
                else
                {
                    lblPrecioTotal.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                    Titulo.InnerText = "El Carro de compra está vacio";
                    btnActualizar.Visible = false;
                    btnCompra.Visible = false;
                }
            }
        }

        /// <summary>
        /// Obtiene los productos de cada uno de los carritos que se cargan
        /// </summary>
        /// <returns></returns>
        public List<Carrito> ConsultarCarros()
        {
            CoreCarrito core = new CoreCarrito();
            return core.ConsultarCarros();
        }

        /// <summary>
        /// Evento que maneja la actualización de los productos de un carro de compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Actualizar_Click(object sender, EventArgs e)
        {
            ActualizarProductosCarro();
            ValidarCarro();
        }

        /// <summary>
        /// Actualiza los items de un carro de compra
        /// </summary>
        /// <returns></returns>
        public List<Carrito> ActualizarProductosCarro()
        {
            try
            {
                using (CoreCarrito core = new CoreCarrito())
                {
                    string idCarro = core.ObtenerIdCarrito();

                    //Se instancia la estructura que contiene las actualizaciones a 
                    CoreCarrito.ActualizacionesCarrito[] actualizaciones = new
                    CoreCarrito.ActualizacionesCarrito[ListaCarro.Rows.Count];

                    // Se recorre la matriz y se extraen los datos para actualizar
                    for (int i = 0; i < ListaCarro.Rows.Count; i++)
                    {
                        var rowValues = ObtenerValoresGrilla(ListaCarro.Rows[i]);
                        actualizaciones[i].IdManga = Convert.ToInt32(rowValues["Manga.Id"]);
                        var cbRemover = (CheckBox)ListaCarro.Rows[i].FindControl("chkQuitarManga");
                        actualizaciones[i].QuitarManga = cbRemover.Checked;
                        var txtCantidad = (TextBox)ListaCarro.Rows[i].FindControl("CantidadManga");
                        actualizaciones[i].Cantidad = Convert.ToInt16(txtCantidad.Text);
                    }
                    core.ActualizarCarroCompra(idCarro, actualizaciones);
                    ListaCarro.DataBind();
                    lblTotal.Text = $"{core.CalcularTotalPago():N2}";
                    return core.ConsultarCarros();
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
        }

        /// <summary>
        /// Obtiene los valores de la grilla del carrito de compra
        /// </summary>
        /// <param name="fila"></param>
        /// <returns></returns>
        public static IOrderedDictionary ObtenerValoresGrilla(GridViewRow fila)
        {
            IOrderedDictionary values = new OrderedDictionary();
            foreach (DataControlFieldCell cell in fila.Cells)
            {
                if (cell.Visible)
                {
                    cell.ContainingField.ExtractValuesFromCell(values, cell, fila.RowState, true);
                }
            }
            return values;
        }

        /// <summary>
        /// Evento que maneja la compra con paypal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCompra_Click(object sender, ImageClickEventArgs e)
        {
            using (var core = new CoreCarrito())
            {
                Session["payment_amt"] = core.CalcularTotalPago();
            }
            Response.Redirect("Checkout/CheckoutStart.aspx");
        }
    }
}