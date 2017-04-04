<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaGeneros.aspx.cs" Inherits="MangaGods.Views.ListaGeneros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Lista de Géneros de Manga</h1>

    <div id="MenuGenero" style="text-align: center">
        <asp:ListView ID="listaGeneros"
            ItemType="MangaGods.Models.Genero"
            runat="server"
            SelectMethod="ObtenerTodosGeneros">
            <ItemTemplate>
                <b style="font-size: large; font-style: normal">
                    <a href="<%#: GetRouteUrl("RutaGeneros", new {nombre = Item.Nombre})%>"><%#: Item.Nombre %></a>
                </b>
            </ItemTemplate>
            <ItemSeparatorTemplate>| </ItemSeparatorTemplate>
        </asp:ListView>
    </div>
</asp:Content>
