<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleManga.aspx.cs" Inherits="MangaGods.Views.DetalleManga" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:FormView ID="Detalle" runat="server" ItemType="MangaGods.Models.Manga" SelectMethod="ObtenerMangaXId"
        RenderOuterTable="false">
        <ItemTemplate>
            <div>
                <h1><%#:Item.Nombre %></h1>
            </div>
            <br />
            <table>
                <tr>
                    <td>
                        <img src="/Catalogo/Imagenes/<%#:Item.ImagePath %>" style="border: solid; height: 300px"
                            alt="<%#:Item.Nombre %>" />
                    </td>
                    <td>&nbsp;</td>
                    <td style="vertical-align: top; text-align: left;">
                        <b>Autor:</b><br />
                        <%#:Item.Autor.Nombre %>
                        <br />
                        <b>Género:</b><br />
                        <%#:Item.Genero.Nombre %>
                        <br />
                        <b>Descripción:</b><br />
                        <%#:Item.Descripcion %>
                        <br />
                        <span><b>No. Volumen:</b>&nbsp;<%#:Item.Volumen %></span>
                        <br />
                        <span><b>Precio:</b>&nbsp;<%#:String.Format("{0:N2}", "$" + Item.Precio)%></span>
                        <br />
                        <span><b>No. Producto:</b>&nbsp;<%#:Item.Id %></span>
                        <br />
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
