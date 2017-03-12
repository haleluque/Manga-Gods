<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminGenero.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminGenero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Géneros</h1>
    <hr />
    <div>
        <div class="col-md-6">
            <h3>Crear Un Genero:</h3>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreGenero" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreGenero" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoGenero" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreGenero %>"
                            ControlToValidate="txtNombreGenero" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDescripcionGenero" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescripcionGenero" TextMode="multiline" Columns="22" Rows="3" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoDescripcionGenero" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionGenero %>"
                            ControlToValidate="txtDescripcionGenero" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <p></p>
            <p></p>
            <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearGenero_Click" ValidationGroup="grupoCrear" CausesValidation="true" />
        </div>
        <div class="col-md-6">
            <h3>Actualizar Un Género:</h3>
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
                <div id="datosGenero" runat="server" visible="false">
                    <div>
                        <asp:Label ID="lblGeneroConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtGeneroConsulta" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoGeneroConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreGenero %>"
                                ControlToValidate="txtGeneroConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblDescripcionConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtDescripcionConsulta" TextMode="multiline" Columns="22" Rows="3" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoDescripcionConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionGenero %>"
                                ControlToValidate="txtDescripcionConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
            </div>
            <p></p>
            <p></p>
            <asp:Button ID="btnBuscar" ValidationGroup="grupoConsulta" runat="server" Text="Buscar" OnClick="Buscar_Click" CausesValidation="true" />
            <asp:Button ID="btnActualizar" ValidationGroup="grupoConsulta" runat="server" Visible="false" Text="Actualizar" OnClick="Actualizar_Click" CausesValidation="true" />
            <asp:Button ID="btnBorrar" ValidationGroup="grupoConsulta" runat="server" Visible="false" Text="Borrar" OnClick="Borrar_Click" CausesValidation="true" />
        </div>
    </div>
    <br />
    <div>
        <span id="alerta" runat="server"></span>
    </div>
</asp:Content>
