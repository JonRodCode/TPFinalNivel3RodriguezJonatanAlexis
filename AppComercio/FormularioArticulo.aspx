<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="AppComercio.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Detalle de artículo</h2>
    <div class="row">

        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Precio</label>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtPrecio"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ErrorMessage="Solo ingrese números" CssClass="validacion" ControlToValidate="txtPrecio" ValidationExpression="^[0-9]+$" runat="server" />
            </div>
        </div>
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
</asp:Content>
