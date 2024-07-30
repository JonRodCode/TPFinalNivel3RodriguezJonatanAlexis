<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppComercio.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Hola!</h2>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repHome" runat="server">
            <ItemTemplate>
                <div class="card mb-3" style="max-width: 540px;">
                    <div class="row g-0">
                        <div class="col-md-7">
                            <img src="<%# Eval("ImagenUrl") %>" class="img-fluid rounded-start" alt="Imagen no disponible">
                        </div>
                        <div class="col-md-5">
                            <div class="card-body">
                                <h5 class="card-title"><%#Eval("Nombre")%></h5>
                                <p class="card-text"><%#Eval("Descripcion") %></p>
                                <a href="DetalleArticulo.aspx?id=<%#Eval("Id")%>">Ver detalle</a>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

   
</asp:Content>
