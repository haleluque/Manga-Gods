<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoCompra.aspx.cs" Inherits="MangaGods.Views.CarritoCompra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="Titulo" runat="server" class="ContentHead">
        <h1>Carro de Compra</h1>
    </div>
    <asp:GridView ID="ListaCarro" runat="server" AutoGenerateColumns="False" ShowFooter="True"
        GridLines="Vertical" CellPadding="4"
        ItemType="MangaGods.Models.Carrito" SelectMethod="ConsultarCarros"
        CssClass="table table-striped table-bordered">
        <Columns>
            <asp:BoundField DataField="Manga.Id" HeaderText="Código" SortExpression="Id" />
            <asp:BoundField DataField="Manga.Nombre" HeaderText="Nombre Manga" />
            <asp:BoundField DataField="Manga.Precio" HeaderText="Precio/Unidad" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Manga.Volumen" HeaderText="No. Volumen" />
            <asp:TemplateField HeaderText="Cantidad">
                <ItemTemplate>
                    <asp:TextBox ID="CantidadManga" Width="40" runat="server" Text="<%#:Item.Cantidad %>"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Precio Total">
                <ItemTemplate>
                    <%#: String.Format("{0:N2}", ((Convert.ToDouble(Item.Cantidad)) * Convert.ToDouble(Item.Manga.Precio)))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Quitar Manga">
                <ItemTemplate>
                    <asp:CheckBox ID="chkQuitarManga" runat="server"></asp:CheckBox>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <div>
        <p></p>
        <strong>
            <asp:Label ID="lblPrecioTotal" runat="server" Text="Total a Pagar: "></asp:Label>
            <asp:Label ID="lblTotal" runat="server" EnableViewState="false"></asp:Label>
        </strong>
    </div>
    <br />
    <table>
        <tr>
            <td>
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="Actualizar_Click" />
            </td>
            <td>
                <asp:ImageButton ID="btnCompra" runat="server"
                    ImageUrl="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif"
                    Width="145" AlternateText="Compra con PayPal"
                    OnClick="btnCompra_Click"
                    BackColor="Transparent" BorderWidth="0" />
            </td>
        </tr>
    </table>
</asp:Content>
