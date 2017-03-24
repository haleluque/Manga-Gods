using System;
using System.Collections.Generic;
using MangaGods.Logic;
using MangaGods.Models;

namespace MangaGods.Checkout
{
    public partial class CheckoutReview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            NVPAPICaller payPalCaller = new NVPAPICaller();
            string retMsg = "";
            string payerId = "";
            NvpCodec decoder = new NvpCodec();
            var token = Session["token"].ToString();
            bool ret = payPalCaller.GetCheckoutDetails(token, ref payerId, ref decoder, ref retMsg);
            if (ret)
            {
                Session["payerId"] = payerId;
                var myOrder = new Orden();
                myOrder.FechaOrden = Convert.ToDateTime(decoder["TIMESTAMP"]);
                myOrder.NombreUsuario = User.Identity.Name;
                myOrder.PrimerNombre = decoder["FIRSTNAME"];
                myOrder.Apellido = decoder["LASTNAME"];
                myOrder.Direccion = decoder["SHIPTOSTREET"];
                myOrder.Ciudad = decoder["SHIPTOCITY"];
                myOrder.Departamento = decoder["SHIPTOSTATE"];
                myOrder.CodigoPostal = decoder["SHIPTOZIP"];
                myOrder.Pais = decoder["SHIPTOCOUNTRYCODE"];
                myOrder.Email = decoder["EMAIL"];
                myOrder.Total = Convert.ToDecimal(decoder["AMT"].Replace(".", ","));

                // Verify total payment amount as set on CheckoutStart.aspx.
                try
                {
                    decimal paymentAmountOnCheckout = Convert.ToDecimal(Session["payment_amt"].ToString());
                    decimal paymentAmoutFromPayPal = Convert.ToDecimal(decoder["AMT"].Replace(".", ","));
                    if (paymentAmountOnCheckout != paymentAmoutFromPayPal)
                    {
                        Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                    }
                }
                catch (Exception)
                {
                    Response.Redirect("CheckoutError.aspx?" + "Desc=Amount%20total%20mismatch.");
                }
                // Get DB context.
                MangaContext db = new MangaContext();
                // Add order to DB.
                db.Orden.Add(myOrder);
                db.SaveChanges();

                // Get the shopping cart items and process them.
                using (CoreCarrito core = new CoreCarrito())
                {
                    List<Carrito> myOrderList = core.ConsultarCarros();
                    // Add OrderDetail information to the DB for each product purchased.
                    for (int i = 0; i < myOrderList.Count; i++)
                    {
                        // Create a new OrderDetail object.
                        var myOrderDetail = new DetalleOrden();
                        myOrderDetail.IdOrden = myOrder.Id;
                        myOrderDetail.NombreUsuario = User.Identity.Name;
                        myOrderDetail.IdManga = myOrderList[i].IdManga;
                        myOrderDetail.Cantidad = myOrderList[i].Cantidad;
                        myOrderDetail.Precio = myOrderList[i].Manga.Precio;
                        // Add OrderDetail to DB.
                        db.DetalleOrden.Add(myOrderDetail);
                        db.SaveChanges();
                    }
                    // Set OrderId.
                    Session["currentOrderId"] = myOrder.Id;
                    // Display Order information.
                    List<Orden> orderList = new List<Orden>();
                    orderList.Add(myOrder);
                    ShipInfo.DataSource = orderList;
                    ShipInfo.DataBind();
                    // Display OrderDetails.
                    OrderItemList.DataSource = myOrderList;
                    OrderItemList.DataBind();
                }
            }
            else
            {
                Response.Redirect("CheckoutError.aspx?" + retMsg);
            }
        }

        protected void bntConfirmarCompra_Click(object sender, EventArgs e)
        {
            Session["userCheckoutCompleted"] = "true";
            Response.Redirect("~/Checkout/CheckoutComplete.aspx");
        }
    }
}