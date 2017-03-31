<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminGenero.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminGenero" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Administración de Géneros</h1>
    <hr />
    <div class="form-horizontal">
        <h3>Crear Un Genero:</h3>
        <hr />
        <div class="form-group">
            <asp:Label ID="lblNombreGenero" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtNombreGenero" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoGenero" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreGenero %>"
                    ControlToValidate="txtNombreGenero" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblDescripcionGenero" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
            <div class="col-md-6">
                <asp:TextBox ID="txtDescripcionGenero" Wrap="true" CssClass="form-control text-area" TextMode="multiline" Rows="3" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoDescripcionGenero" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionGenero %>"
                    ControlToValidate="txtDescripcionGenero" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearGenero_Click" ValidationGroup="grupoCrear" CausesValidation="true" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <h3>Actualizar Un Género:</h3>
        <hr />
        <div class="form-group">
            <asp:Label ID="lblId" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblId %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtId" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoId" ValidationGroup="grupoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorId %>"
                    ControlToValidate="txtId" SetFocusOnError="true" Display="Dynamic">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="expresionNumeros" ValidationGroup="grupoConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                    ControlToValidate="txtId" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div id="datosGenero" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="lblGeneroConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtGeneroConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoGeneroConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreGenero %>"
                        ControlToValidate="txtGeneroConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblDescripcionConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtDescripcionConsulta" Wrap="true" CssClass="form-control text-area" TextMode="multiline" Rows="3" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoDescripcionConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionGenero %>"
                        ControlToValidate="txtDescripcionConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
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
