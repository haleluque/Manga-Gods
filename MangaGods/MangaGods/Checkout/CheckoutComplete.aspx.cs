using System;
using System.Linq;
using System.Web;
using MangaGods.Logic;
using MangaGods.Models;

namespace MangaGods.Checkout
{
    public partial class CheckoutComplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;
                // Verifica el usuario que ha terminado de hacer la compra
                if ((string)Session["userCheckoutCompleted"] != "true")
                {
                    Session["userCheckoutCompleted"] = string.Empty;
                    Response.Redirect("CheckoutError.aspx?" + "Desc=Unvalidated%20Checkout.");
                }
                NVPAPICaller payPalCaller = new NVPAPICaller();
                string retMsg = "";
                NvpCodec decoder = new NvpCodec();
                var token = Session["token"].ToString();
                var payerId = Session["payerId"].ToString();
                var finalPaymentAmount = Session["payment_amt"].ToString().Replace(",", ".");
                bool ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, payerId, ref decoder, ref retMsg);

                if (ret)
                {
                    // Retrieve PayPal confirmation value.
                    string paymentConfirmation = decoder["PAYMENTINFO_0_TRANSACTIONID"];
                    lblIdTransaccion.Text = paymentConfirmation;
                    MangaContext db = new MangaContext();
                    // Get the current order id.
                    int currentOrderId = -1;
                    if (!ReferenceEquals(Session["currentOrderId"], string.Empty))
                    {
                        currentOrderId = Convert.ToInt32(Session["currentOrderID"]);
                    }
                    Orden myCurrentOrder;
                    if (currentOrderId >= 0)
                    {
                        // Get the order based on order id.
                        myCurrentOrder = db.Orden.Single(o => o.Id == currentOrderId);
                        // Update the order to reflect payment has been completed.
                        myCurrentOrder.IdTransaccionPago = paymentConfirmation;
                        // Save to DB.
                        db.SaveChanges();
                    }
                    // Clear shopping cart.
                    using (CoreCarrito core = new CoreCarrito())
                    {
                        core.VaciarCarro();
                    }
                    // Clear order id.
                    Session["currentOrderId"] = string.Empty;
                }
                else
                {
                    Response.Redirect("CheckoutError.aspx?" + retMsg);
                }
            }
            catch (InvalidCastException a)
            {
                throw new InvalidCastException(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConversionDato")?.ToString(), a);
            }
        }

        /// <summary>
        /// Evento que maneja el retorno a la app después de hace una compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bntContinuar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}