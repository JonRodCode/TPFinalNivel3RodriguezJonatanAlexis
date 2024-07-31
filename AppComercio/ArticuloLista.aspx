<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ArticuloLista.aspx.cs" Inherits="AppComercio.ArticuloLista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .oculto {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Lista de articulos</h2>

    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:Label ID="lblFiltrar" runat="server" Text="Filtrar"></asp:Label>
                <asp:TextBox ID="txtFitro" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFitro_TextChanged"></asp:TextBox>
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox ID="chkFiltroAvanzado"
                    Text="Filtro Avanzado" runat="server"
                    AutoPostBack="true"
                    OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" />
            </div>
        </div>
    </div>

    <% if (FiltroAvanzado)
        {%>
    <div class="row">
        <div class="col-3">
            <div class="mb-3">
                <asp:Label ID="lblCampo" runat="server" Text="Campo"></asp:Label>
                <asp:DropDownList ID="ddlCampo" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label ID="lblCriterio" runat="server" Text="Criterio"></asp:Label>
                <asp:DropDownList ID="ddlCriterio" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-3">
                <asp:Label ID="lblFiltro" runat="server" Text="Filtro"></asp:Label>
                <asp:TextBox ID="txtFiltroAvanzado" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="col-3">
            <div class="mb-4"></div>
            <div class="mb-3">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            </div>
        </div>
    </div>
    <%}%>


    <asp:GridView ID="dgvArticulos" DataKeyNames="Id" runat="server"
        CssClass="table" AutoGenerateColumns="false"
        OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="true" PageSize="4">
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
    <% if (!(trainee is null) && trainee.Admin)
        { %>
    <asp:Button ID="btnAgregarArticulo" CssClass="btn btn-primary" runat="server" Text="Agregar articulo" OnClick="btnAgregarArticulo_Click" />
    <% }%>
</asp:Content>
