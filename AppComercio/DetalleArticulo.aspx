<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="AppComercio.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 14px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle de artículo</h2>
    <div class="row">
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Código</label>
                <asp:RequiredFieldValidator ID="rfvCodigo" ValidationGroup="Group1" runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtCodigo"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCodigo" Enabled="false" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:RequiredFieldValidator ID="rfvNombre" ValidationGroup="Group1" runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtNombre"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtNombre" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Precio</label>
                <asp:RequiredFieldValidator ID="rfvPrecio" ValidationGroup="Group1" runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtPrecio"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtPrecio" Enabled="false" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revPrecio" ValidationGroup="Group1" ErrorMessage="Ingrese un valor válido." CssClass="validacion" ControlToValidate="txtPrecio" ValidationExpression="^\d{1,5}([\,\.]\d{1,4})?$" runat="server" />
            </div>

            <div class="col-md-3">
                <% if (!(trainee is null))
                    { %>
                <asp:Button ID="btnAFavoritos" runat="server" Text="Agregar a favoritos" CssClass="btn btn-secondary" OnClick="btnAFavoritos_Click" />
                <%} %>
                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-primary" OnClick="btnRegresar_Click" />
            </div>
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Marca</label>
                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select" Enabled="false"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label class="form-label">Categoria</label>
                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select" Enabled="false"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <label class="form-label">Descripción</label>
                <asp:TextBox runat="server" ID="txtDescripcion" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>

            <% if (!(trainee is null) && trainee.Admin)
                { %>
            <div class="mb-3">

                <asp:Button Text="Guardar" ValidationGroup="Group1" CssClass="btn btn-success" ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" />
            </div>
            <%}
                if (ConfirmaEliminacion)
                { %>
            <div class="mb-3">
                <asp:CheckBox ID="chkConfirmarEliminacion" Text="Confimar Eliminacion" runat="server" />
                <asp:Button ID="btnEliminarConfirmacion" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger" OnClick="btnEliminarConfirmacion_Click" />
            </div>
            <%}%>
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <% if (!(trainee is null) && trainee.Admin)
                    { %>
                <asp:Label ID="lblCargarImagen" runat="server" Text="Cargar Imagen: "></asp:Label>
                <div class="mb-3">
                    <asp:Button ID="btnUrlImagen" CssClass="btn btn-link" runat="server" Text="Por Url" OnClick="btnUrlImagen_Click" />
                    <asp:Button ID="btnArchivoImagen" CssClass="btn btn-link" runat="server" Text="Por Archivo" OnClick="btnArchivoImagen_Click" />
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtUrlImagen" CssClass="form-control" runat="server"></asp:TextBox>
                    <input type="file" id="txtImagen" runat="server" class="form-control" />
                </div>
                <%} %>
                <asp:Image ID="imgArticulo" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                    runat="server" CssClass="img-fluid mb-3" AlternateText="Imagen no disponible" />
            </div>
        </div>
    </div>
</asp:Content>
