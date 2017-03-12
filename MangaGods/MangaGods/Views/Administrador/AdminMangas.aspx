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
        <div class="col-md-6">
            <h3>Actualizar Un Manga:</h3>
            <div>
                <div>
                    <div>
                        <asp:Label ID="lblId" runat="server" Text="<%$ Resources:RecursosMangaGods, lblId %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtId" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoId" ValidationGroup="grupoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorId %>"
                                ControlToValidate="txtId" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div id="datosManga" runat="server" visible="false">
                    <div>
                        <asp:Label ID="lblMangaConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtMangaConsulta" runat="server"></asp:TextBox>
                            <div>
                                <asp:RequiredFieldValidator ID="requeridoMangaConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreManga %>"
                                    ControlToValidate="txtMangaConsulta" SetFocusOnError="true" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblDescripcionConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtDescripcionConsulta" TextMode="multiline" Columns="22" Rows="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoDescripcionConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionManga %>"
                                ControlToValidate="txtDescripcionConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblGeneroConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                        <div>
                            <asp:DropDownList ID="comboGeneroConsulta" runat="server"
                                AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requeridoGeneroConsulta" runat="server" ControlToValidate="comboGeneroConsulta" ValidationGroup="grupoActualizar"
                                InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorGenero %>" />
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblAutorConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreAutor %>"></asp:Label>
                        <div>
                            <asp:DropDownList ID="comboAutorConsulta" runat="server"
                                AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="requeridoAutorConsulta" runat="server" ControlToValidate="comboAutorConsulta" ValidationGroup="grupoActualizar"
                                InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorAutor %>" />
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblVolumenConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblVolumen %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtVolumenConsulta" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoVolumenConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorVolumen %>"
                                ControlToValidate="txtVolumenConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblPrecioConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblPrecio %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtPrecioConsulta" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoPrecioConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPrecio %>"
                                ControlToValidate="txtPrecioConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="expresionPrecioConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoPrecio %>"
                                ControlToValidate="txtPrecioConsulta" SetFocusOnError="True"
                                Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblArchivoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblAniadirImagen %>"></asp:Label>
                        <div>
                            <asp:FileUpload ID="archivoConsulta" runat="server" />
                            <asp:RequiredFieldValidator ID="requeridoArchivoConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPathImagen %>"
                                ControlToValidate="archivoConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <p></p>
            <p></p>
            <asp:Button ID="btnBuscar" ValidationGroup="grupoActualizar" runat="server" Text="Buscar" OnClick="Buscar_Click" CausesValidation="true" />
            <asp:Button ID="btnActualizar" ValidationGroup="grupoActualizar" runat="server" Visible="false" Text="Actualizar" OnClick="Actualizar_Click" CausesValidation="true" />
            <asp:Button ID="btnBorrar" runat="server" Visible="false" Text="Borrar" OnClick="Borrar_Click" CausesValidation="true" />
        </div>
    </div>
    <br />
    <div>
        <span id="alerta" runat="server"></span>
    </div>
</asp:Content>
