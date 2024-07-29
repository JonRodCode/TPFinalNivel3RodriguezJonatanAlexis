<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AppComercio.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="col-4">
        <h2>Creá tu usuario</h2>
        <div class="mb-3">
            <asp:Label CssClass="form-label" runat="server" Text="Email" id="lblEmail"></asp:Label>
            <asp:TextBox runat="server" Id="txtEmail" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="mb-3">
            <asp:Label CssClass="form-label" runat="server" Text="Password" id="lblPassword"></asp:Label>
            <asp:TextBox runat="server" Id="txtPassword" CssClass="form-control" Textmode="Password" ></asp:TextBox>
        </div>
        <asp:Button runat="server" Text="Registrarse" CssClass="btn btn-primary" id="btnRegistrarse" OnClick="btnRegistrarse_Click" />
        <a href="/">Cancelar</a>
    </div>

</div>
</asp:Content>
