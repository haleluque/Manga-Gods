<%@ Page Title="Administración de Autores" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminAutores.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminAutores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Autores</h1>
    <hr />
    <div class="form-horizontal">
        <h3>Crear Un Autor:</h3>
        <div class="form-group">
            <hr />
            <asp:Label ID="lblNombreAutor" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtNombreAutor" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoNombre" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                    ControlToValidate="txtNombreAutor" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblEdad" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblEdad %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtEdad" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="formatoEdad" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                    ControlToValidate="txtEdad" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblEmpresa" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblEmpresa %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtEmpresa" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoEmpresa" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                    ControlToValidate="txtEmpresa" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearAutor_Click" ValidationGroup="grupoCrear" CausesValidation="true" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <h3>Actualizar Un Autor</h3>
        <hr />
        <div class="form-group">
            <asp:Label ID="lblId" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblId %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtId" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoId" ValidationGroup="grupoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorId %>"
                    ControlToValidate="txtId" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="expresionNumeros" ValidationGroup="grupoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                    ControlToValidate="txtId" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div id="datosAutor" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="lblNombreConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtNombreConsulta" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoNombreConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                        ControlToValidate="txtNombreConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEdadConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEdad %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtEdadConsulta" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="formatoEdadConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                        ControlToValidate="txtEdadConsulta" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmpresaConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblEmpresa %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtEmpresaConsulta" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoEmpresaConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreAutor %>"
                        ControlToValidate="txtEmpresaConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnBuscar" ValidationGroup="grupoConsulta" runat="server" Text="Buscar" OnClick="Buscar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnActualizar" ValidationGroup="grupoActualizar" runat="server" Visible="false" Text="Actualizar" OnClick="Actualizar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnBorrar" ValidationGroup="grupoConsulta" runat="server" Visible="false" Text="Borrar" OnClick="Borrar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnCancelar" runat="server" Visible="false" Text="Cancelar" OnClick="Cancelar_Click" CausesValidation="false" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <br />
    <div>
        <span id="alerta" runat="server"></span>
    </div>
</asp:Content>
