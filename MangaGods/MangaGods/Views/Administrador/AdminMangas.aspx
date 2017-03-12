<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMangas.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminMangas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Aministración de Mangas</h1>
    <hr />
    <div>
        <div class="col-md-6">
            <h3>Crear Un Manga:</h3>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreManga" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreManga" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoNombre" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreManga %>"
                            ControlToValidate="txtNombreManga" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescripcionManga" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescripcionManga" TextMode="multiline" Columns="22" Rows="3" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoDescripcionManga" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionManga %>"
                            ControlToValidate="txtDescripcionManga" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGenero" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="comboGenero" runat="server"
                            ItemType="MangaGods.Models.Genero"
                            SelectMethod="ObtenerTodosGeneros" AppendDataBoundItems="true"
                            DataTextField="Nombre" DataValueField="Id">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="requeridoGenero" runat="server" ControlToValidate="comboGenero" ValidationGroup="grupoCrear" 
                            InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorGenero %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAutor" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreAutor %>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="comboAutor" runat="server"
                            ItemType="MangaGods.Models.Autor"
                            SelectMethod="ObtenerTodosAutores" AppendDataBoundItems="true"
                            DataTextField="Nombre" DataValueField="Id">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="requeridoAutor" runat="server" ControlToValidate="comboAutor" ValidationGroup="grupoCrear" 
                            InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorAutor %>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblVolumen" runat="server" Text="<%$ Resources:RecursosMangaGods, lblVolumen %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtVolumen" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoVolumen" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorVolumen %>"
                            ControlToValidate="txtVolumen" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPrecio" runat="server" Text="<%$ Resources:RecursosMangaGods, lblPrecio %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoPrecio" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPrecio %>"
                            ControlToValidate="txtPrecio" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="expresionRPrecio" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoPrecio %>"
                            ControlToValidate="txtPrecio" SetFocusOnError="True"
                            Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAniadirImagen" runat="server" Text="<%$ Resources:RecursosMangaGods, lblAniadirImagen %>"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="Archivo" runat="server" />
                        <asp:RequiredFieldValidator ID="requeridoArchivo" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPathImagen %>"
                            ControlToValidate="Archivo" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <p></p>
            <p></p>
            <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearManga_Click" ValidationGroup="grupoCrear" CausesValidation="true" />
        </div>
        <br />
        <div>
            <span id="alerta" runat="server"></span>
        </div>
    </div>
</asp:Content>
