<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAutores.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Autores</h1>
    <hr />
    <div>
        <h3>Crear Un Autor:</h3>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNombreAutor" runat="server">Nombre</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNombreAutor" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoNombre" runat="server" Text="* El nombre del autor es obligatorio"
                        ControlToValidate="txtNombreAutor" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEdad" runat="server">Edad</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEdad" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmpresa" runat="server">Empresa</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoEmpresa" runat="server" Text="* La descripción de la empresa es obligatoria"
                        ControlToValidate="txtEmpresa" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
        </table>
        <p></p>
        <p></p>
        <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearAutor_Click" CausesValidation="true" />
    </div>
    <br />
    <div>
        <span ID="alerta" runat="server"></span>
    </div>
</asp:Content>
