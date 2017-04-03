<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminMangas.aspx.cs" Inherits="MangaGods.Views.Administrador.AdminMangas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Aministración de Mangas</h1>
    <hr />
    <div class="form-horizontal">
        <h3>Crear Un Manga:</h3>
        <hr />
        <div class="form-group">
            <asp:Label ID="lblNombreManga" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtNombreManga" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoNombre" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreManga %>"
                    ControlToValidate="txtNombreManga" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblDescripcionManga" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtDescripcionManga" Wrap="true" CssClass="form-control text-area" TextMode="multiline" Rows="3" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoDescripcionManga" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionManga %>"
                    ControlToValidate="txtDescripcionManga" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblGenero" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="comboGenero" runat="server" CssClass="form-control combo-box" ItemType="MangaGods.Models.Genero" SelectMethod="ObtenerTodosGeneros"
                    AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="requeridoGenero" runat="server" ControlToValidate="comboGenero" ValidationGroup="grupoCrear"
                    InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorGenero %>" CssClass="text-danger" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblAutor" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombreAutor %>"></asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="comboAutor" runat="server" CssClass="form-control combo-box" ItemType="MangaGods.Models.Autor" SelectMethod="ObtenerTodosAutores"
                    AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="requeridoAutor" runat="server" ControlToValidate="comboAutor" ValidationGroup="grupoCrear"
                    InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorAutor %>" CssClass="text-danger" />
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblVolumen" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblVolumen %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtVolumen" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoVolumen" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorVolumen %>"
                    ControlToValidate="txtVolumen" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="formatoVolumen" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                    ControlToValidate="txtVolumen" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblPrecio" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblPrecio %>"></asp:Label>
            <div class="col-md-10">
                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="requeridoPrecio" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPrecio %>"
                    ControlToValidate="txtPrecio" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="expresionRPrecio" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoPrecio %>"
                    ControlToValidate="txtPrecio" SetFocusOnError="True"
                    Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$" CssClass="text-danger">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="form-group">
            <asp:Label ID="lblAniadirImagen" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblAniadirImagen %>"></asp:Label>
            <div class="col-md-10">
                <asp:FileUpload ID="Archivo" runat="server" CssClass="text-area" BackColor="Gray" />
                <asp:RequiredFieldValidator ID="requeridoArchivo" ValidationGroup="grupoCrear" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPathImagen %>"
                    ControlToValidate="Archivo" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                </asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnCrear" runat="server" Text="Guardar" OnClick="CrearManga_Click" ValidationGroup="grupoCrear" CausesValidation="true" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
    <div class="form-horizontal">
        <h3>Actualizar Un Género:</h3>
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
        <div id="datosManga" runat="server" visible="false">
            <div class="form-group">
                <asp:Label ID="lblMangaConsulta" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombre %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtMangaConsulta" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoMangaConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorNombreManga %>"
                        ControlToValidate="txtMangaConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblDescripcionConsulta" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblDescripcion %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtDescripcionConsulta" Wrap="true" CssClass="form-control text-area" TextMode="multiline" Rows="3" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoDescripcionConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorDescripcionManga %>"
                        ControlToValidate="txtDescripcionConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblGeneroConsulta" runat="server" CssClass="col-md-2 control-label" Text="<%$ Resources:RecursosMangaGods, lblNombreGenero %>"></asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList ID="comboGeneroConsulta" runat="server" CssClass="form-control combo-box" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="requeridoGeneroConsulta" runat="server" ControlToValidate="comboGeneroConsulta" ValidationGroup="grupoActualizar"
                        InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorGenero %>" CssClass="text-danger" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblAutorConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblNombreAutor %>"></asp:Label>
                <div class="col-md-10">
                    <asp:DropDownList ID="comboAutorConsulta" runat="server" CssClass="form-control combo-box" AppendDataBoundItems="true" DataTextField="Nombre" DataValueField="Id"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="requeridoAutorConsulta" runat="server" ControlToValidate="comboAutorConsulta" ValidationGroup="grupoActualizar"
                        InitialValue="Seleccione....." ErrorMessage="<%$ Resources:RecursosMangaGods, spanErrorAutor %>" CssClass="text-danger" />
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblVolumenConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblVolumen %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtVolumenConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoVolumenConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorVolumen %>"
                        ControlToValidate="txtVolumenConsulta" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="formatoVolumenConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoId %>"
                        ControlToValidate="txtVolumen" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*$" CssClass="text-danger">
                    </asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPrecioConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblPrecio %>"></asp:Label>
                <div class="col-md-10">
                    <asp:TextBox ID="txtPrecioConsulta" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requeridoPrecioConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPrecio %>"
                        ControlToValidate="txtPrecioConsulta" SetFocusOnError="true" Display="Dynamic" CssClass="text-danger">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="expresionPrecioConsulta" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorFormatoPrecio %>"
                        ControlToValidate="txtPrecioConsulta" SetFocusOnError="True" Display="Dynamic" ValidationExpression="^[0-9]*(\.)?[0-9]?[0-9]?$" CssClass="text-danger"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblArchivoConsulta" CssClass="col-md-2 control-label" runat="server" Text="<%$ Resources:RecursosMangaGods, lblAniadirImagen %>"></asp:Label>
                <div class="col-md-10">
                    <asp:FileUpload ID="archivoConsulta" runat="server" CssClass="text-area" BackColor="Gray" />
                    <asp:RequiredFieldValidator ID="requeridoArchivoConsulta" ValidationGroup="grupoActualizar" runat="server" Text="<%$ Resources:RecursosMangaGods, spanErrorPathImagen %>"
                        ControlToValidate="archivoConsulta" SetFocusOnError="true" Display="Dynamic">
                    </asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="btnBuscar" ValidationGroup="grupoActualizar" runat="server" Text="Buscar" OnClick="Buscar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnActualizar" ValidationGroup="grupoActualizar" runat="server" Visible="false" Text="Actualizar" OnClick="Actualizar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnBorrar" runat="server" Visible="false" Text="Borrar" OnClick="Borrar_Click" CausesValidation="true" CssClass="btn btn-default" />
                <asp:Button ID="btnCancelar" runat="server" Visible="false" Text="Cancelar" OnClick="Cancelar_Click" CausesValidation="false" CssClass="btn btn-default" />
            </div>
        </div>
        <br />
        <div>
            <span id="alerta" runat="server"></span>
        </div>
    </div>
</asp:Content>
