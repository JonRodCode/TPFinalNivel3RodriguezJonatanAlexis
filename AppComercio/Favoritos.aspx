<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="AppComercio.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .oculto {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Favoritos</h2>
    <asp:GridView ID="dgvFavoritos" DataKeyNames="Id" runat="server"
        CssClass="table" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvFavoritos_SelectedIndexChanged"
        OnPageIndexChanging="dgvFavoritos_PageIndexChanging"
        AllowPaging="true" PageSize="6">
        <Columns>
            <asp:BoundField HeaderText="Id" DataField="Id" HeaderStyle-CssClass="oculto" ItemStyle-CssClass="oculto" />
            <asp:BoundField HeaderText="Código" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Marca" DataField="Marca.Descripcion" />
            <asp:BoundField HeaderText="Categoria" DataField="Categoria.Descripcion" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField HeaderText="Acción" SelectText="Seleccionar" ShowSelectButton="true" />
        </Columns>
    </asp:GridView>
</asp:Content>
