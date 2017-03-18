<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckoutReview.aspx.cs" Inherits="MangaGods.Checkout.CheckoutReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Resumen Orden</h1>
    <p></p>
    <h3 style="padding-left: 33px">Mangas:</h3>
    <asp:GridView ID="OrderItemList" runat="server" AutoGenerateColumns="False" GridLines="Both"
        CellPadding="10" Width="500" BorderColor="#efeeef" BorderWidth="33">
        <Columns>
            <asp:BoundField DataField="Manga.Id" HeaderText=" Código" />
            <asp:BoundField DataField="Manga.Nombre" HeaderText=" Nombre" />
            <asp:BoundField DataField="Manga.Precio" HeaderText="Precio Por Unidad" DataFormatString="{0:N2}" />
            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
        </Columns>
    </asp:GridView>
    <asp:DetailsView ID="ShipInfo" runat="server" AutoGenerateRows="false" GridLines="None"
        CellPadding="10" BorderStyle="None" CommandRowStyle-BorderStyle="None">
        <Fields>
            <asp:TemplateField>
                <ItemTemplate>
                    <h3>Dirección Entrega:</h3>
                    <br />
                    <asp:Label ID="FirstName" runat="server" Text="Nombres"></asp:Label>
                    <asp:Label ID="LastName" runat="server" Text="Apellidos"></asp:Label>
                    <br />
                    <asp:Label ID="Address" runat="server" Text="Dirección"></asp:Label>
                    <br />
                    <asp:Label ID="City" runat="server" Text="Ciudad"></asp:Label>
                    <asp:Label ID="State" runat="server" Text="Estado"></asp:Label>
                    <asp:Label ID="PostalCode" runat="server" Text="Código Postal">
                    </asp:Label>
                    <p></p>
                    <h3>Total Compra:</h3>
                    <br />
                    <asp:Label ID="Total" runat="server" Text='<%#: Eval("Total", "{0:N2}") %>'>
                    </asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Fields>
    </asp:DetailsView>
    <p></p>
    <hr />
    <asp:Button ID="bntConfirmarCompra" runat="server" Text="Complete Order" OnClick="bntConfirmarCompra_Click" />
</asp:Content>
