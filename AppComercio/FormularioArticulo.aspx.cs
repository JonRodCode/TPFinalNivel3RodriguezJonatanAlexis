using Dominio;
using helper;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppComercio
{
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public Trainee trainee { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trainee = (Trainee)Session["trainee"];
                if (!IsPostBack)
                {
                    NegocioCategoria_marca negocioCatMar = new NegocioCategoria_marca();

                    //Controlamos si no estamos viendo detalles de un articulo
                    if (Request.QueryString["id"] is null && (trainee is null || trainee.Admin == false))
                        Response.Redirect("Default.aspx", false);
                    // Aca confirmamos que estamos en detalles de un articulo
                    else if (!(Request.QueryString["id"] is null))
                    {
                        Articulo articulo = new Articulo();
                        NegocioArticulo negocio = new NegocioArticulo();
                        articulo = negocio.listar(Request.QueryString["id"].ToString())[0];
                    }
                    // Habilitamos los elementos para el Admin
                    if (!(trainee is null) && trainee.Admin)
                    {
                        txtPrecio.Enabled = true;
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }

       

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!(Page.IsValid))
                return;

            NegocioArticulo negocio = new NegocioArticulo();
            Articulo articulo = new Articulo();
            try
            {
                if (!(Request.QueryString["id"] is null))
                    articulo.Id = int.Parse(Request.QueryString["id"]);
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                


            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }

       
        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["from"] == "AL")
                Response.Redirect("ArticuloLista.aspx", false);
            else if (Request.QueryString["from"] == "F")
                Response.Redirect("Favoritos.aspx", false);
            else
                Response.Redirect("Default.aspx", false);
        }

       
    }
}