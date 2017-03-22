<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Generos.aspx.cs" Inherits="MangaGods.Views.Generos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section>
        <div>
            <hgroup>
                <h2><%: Page.Title %></h2>
            </hgroup>
            <asp:ListView ID="listaMangas" runat="server"
                DataKeyNames="Id" GroupItemCount="4"
                ItemType="MangaGods.Models.Manga" SelectMethod="ObtenerTodosMangas">
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td>No hay Datos.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td />
                </EmptyItemTemplate>
                <GroupTemplate>
                    <tr id="itemPlaceholderContainer" runat="server">
                        <td id="itemPlaceholder" runat="server"></td>
                    </tr>
                </GroupTemplate>
                <ItemTemplate>
                    <td runat="server">
                        <table>
                            <tr>
                                <td>
                                    <%--<a href="DetalleManga.aspx?Id=<%#:Item.Id%>">
                                        <img src="/Catalogo/Imagenes/<%#:Item.ImagePath%>"
                                            width="100%" height="100%" style="border: solid" />
                                    </a>--%>
                                     <a href="<%#: GetRouteUrl("RutaDetalleManga", new {nombre = Item.Nombre})%>">
                                         <img src="/Catalogo/Imagenes/<%#:Item.ImagePath %>" width="100%" style="border: solid; height: 300px" alt=""/>
                                     </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<a href="DetalleManga.aspx?Id=<%#:Item.Id%>">
                                        <span>
                                            <%#:Item.Nombre%>
                                        </span>
                                    </a>--%>
                                    <a href="<%#: GetRouteUrl("RutaDetalleManga", new {nombre = Item.Nombre})%>"><%#: Item.Nombre %></a>
                                    <br />
                                    <br />
                                    <span>
                                        <b>Precio: </b><%#:String.Format("{0:N2}", "$" + Item.Precio)%>
                                    </span>
                                    <br />
                                    <%--<a href="CarritoCompra.aspx?Id=<%#:Item.Id %>">
                                        <span class="ListaCarro">
                                            <b>Agregar al Carrito<b>
                                        </span>
                                    </a>--%>
                                    <a href="<%#: GetRouteUrl("RutaCarritoCompraD", new {Id = Item.Id})%>">
                                        <span class="ListaCarro">
                                            <b>Agregar al Carrito<b>
                                        </span>
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </td>
                </ItemTemplate>
                <LayoutTemplate>
                    <table style="width: 100%;">
                        <tbody>
                            <tr>
                                <td>
                                    <table id="groupPlaceholderContainer" runat="server" style="width: 100%">
                                        <tr id="groupPlaceholder"></tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>
                            <tr></tr>
                        </tbody>
                    </table>
                </LayoutTemplate>
            </asp:ListView>
        </div>
    </section>
</asp:Content>
