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
    public partial class CarritoCompra : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idManga = Request.QueryString["Id"];
            if (!string.IsNullOrEmpty(idManga))
            {
                using (CoreCarrito core = new CoreCarrito())
                {
                    core.AgregarManga(Convert.ToInt16(idManga));
                    Response.Redirect("CarritoCompra.aspx");
                }
            }

            ValidarCarro();
        }

        private void ValidarCarro()
        {
            using (CoreCarrito core = new CoreCarrito())
            {
                decimal TotalCarro = 0;
                TotalCarro = core.CalcularTotalPago();
                if (TotalCarro > 0)
                {
                    lblTotal.Text = string.Format("{0:N2}", TotalCarro);
                }
                else
                {
                    lblPrecioTotal.Text = string.Empty;
                    lblTotal.Text = string.Empty;
                    Titulo.InnerText = "El Carro de compra está vacio";
                    btnActualizar.Visible = false;
                    //CheckoutImageBtn.Visible = false;
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
            using (CoreCarrito core = new CoreCarrito())
            {
                string idCarro = core.ObtenerIdCarrito();

                //Se instancia la estructura que contiene las actualizaciones a 
                CoreCarrito.ActualizacionesCarrito[] actualizaciones = new
                CoreCarrito.ActualizacionesCarrito[ListaCarro.Rows.Count];

                // Se recorre la matriz y se extraen los datos para actualizar
                for (int i = 0; i < ListaCarro.Rows.Count; i++)
                {
                    IOrderedDictionary rowValues = new OrderedDictionary();
                    rowValues = ObtenerValoresGrilla(ListaCarro.Rows[i]);
                    actualizaciones[i].IdManga = Convert.ToInt32(rowValues["Manga.Id"]);
                    CheckBox cbRemover = new CheckBox();
                    cbRemover = (CheckBox)ListaCarro.Rows[i].FindControl("chkQuitarManga");
                    actualizaciones[i].QuitarManga = cbRemover.Checked;
                    TextBox txtCantidad = new TextBox();
                    txtCantidad = (TextBox)ListaCarro.Rows[i].FindControl("CantidadManga");
                    actualizaciones[i].Cantidad = Convert.ToInt16(txtCantidad.Text.ToString());
                }
                core.ActualizarCarroCompra(idCarro, actualizaciones);
                ListaCarro.DataBind();
                lblTotal.Text = string.Format("{0:N2}", core.CalcularTotalPago());
                return core.ConsultarCarros();
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
    }
}