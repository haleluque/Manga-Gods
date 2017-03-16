using System;
using System.Collections.Generic;
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
                //using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions())
                //{
                //    usersShoppingCart.AddToCart(Convert.ToInt16(idManga));
                //}
            }
            else
            {
                throw new Exception("ERROR : It is illegal to load AddToCart.aspx without setting a ProductId.");
            }
        }
    }
}