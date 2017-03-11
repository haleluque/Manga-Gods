<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAutores.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Autores</h1>
    <hr />
    <div>
        <div class="col-md-6">
            <h3>Crear Un Autor:</h3>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblNombreAutor" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNombreAutor" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoNombre" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                            ControlToValidate="txtNombreAutor" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEdad" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEdad %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEdad" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblEmpresa" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEmpresa %>"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="requeridoEmpresa" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                            ControlToValidate="txtEmpresa" SetFocusOnError="true" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <p></p>
            <p></p>
            <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearAutor_Click" ValidationGroup="grupoCrear" CausesValidation="true" />
        </div>
        <div class="col-md-6">
            <h3>Actualizar Un Autor:</h3>
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
                <div id="datosAutor" runat="server" visible="false">
                    <div>
                        <asp:Label ID="lblNombreConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtNombreConsulta" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoNombreConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                                ControlToValidate="txtNombreConsulta" SetFocusOnError="true" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblEdadConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEdad %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtEdadConsulta" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblEmpresaConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEmpresa %>"></asp:Label>
                        <div>
                            <asp:TextBox ID="txtEmpresaConsulta" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="requeridoEmpresaConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                                ControlToValidate="txtEmpresaConsulta" SetFocusOnError="true" Display="Dynamic">
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
