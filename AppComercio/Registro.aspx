<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AppComercio.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 14px
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-4">
            <h2>Creá tu usuario</h2>
            <div class="mb-3">
                <asp:Label CssClass="form-label" runat="server" Text="Email" ID="lblEmail"></asp:Label>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ControlToValidate="txtEmail" runat="server"
                    ErrorMessage="Ingrese un email." CssClass="validacion"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </div>
            <div class="mb-3">
                <asp:Label CssClass="form-label" runat="server" Text="Password" ID="lblPassword"></asp:Label>
                <asp:RequiredFieldValidator runat="server" ErrorMessage="Campo requerido." CssClass="validacion" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
            </div>
            <asp:Button runat="server" Text="Registrarse" CssClass="btn btn-primary" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" />
            <a href="/">Cancelar</a>
        </div>
    </div>
</asp:Content>
