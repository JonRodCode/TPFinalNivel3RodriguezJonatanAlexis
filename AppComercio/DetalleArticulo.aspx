<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="AppComercio.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h2>Detalle de artículo</h2>
    <div class="row">
        <div class="col-md-4">
            <div class="mb-3">
                <% if (!(trainee is null) && trainee.Admin)
                    { %>
                <<input type="file" id="txtImagen" runat="server" class="form-control" />
                <%} %>
            </div>
            <asp:Image ID="imgArticulo" ImageUrl="https://www.palomacornejo.com/wp-content/uploads/2021/08/no-image.jpg"
                runat="server" CssClass="img-fluid mb-3" />
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Código</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtCodigo" Enabled="false" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Precio</label>
                <asp:TextBox runat="server" ID="txtPrecio" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <% if (!(trainee is null) && trainee.Admin)
                    { %>
                <asp:Button Text="Guardar" CssClass="btn btn-primary" ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" />
                <%} %>
            </div>
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Marca</label>
                <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="txtMarca" />
            </div>
            <div class="mb-3">
                <label class="form-label">Categoria</label>
                <asp:TextBox runat="server" ID="txtCategoria" Enabled="false" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Descripción</label>
                <asp:TextBox runat="server" ID="txtDescripcion" TextMode="MultiLine" Enabled="false" CssClass="form-control"></asp:TextBox>

            </div>
        </div>
        <div class="row">
            <div class="col-md-4">
                <% if (!(trainee is null))
                    { %>
                <asp:Button ID="btnAFavoritos" Enabled="false" runat="server" Text="Guardar en favoritos" CssClass="btn btn-secondary" OnClick="btnAFavoritos_Click" />
                <%} %>
                <a href="/">Regresar</a>
            </div>
        </div>
    </div>
</asp:Content>
