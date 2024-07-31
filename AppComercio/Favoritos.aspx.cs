using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using helper;
using Negocio;

namespace AppComercio
{
    public partial class Favoritos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sessionActiva(Session["trainee"]))
                    {
                        NegocioArticulo negocio = new NegocioArticulo();
                        Trainee trainee = (Trainee)Session["trainee"];
                        Session.Add("listaFavoritos", negocio.listarFavoritos(trainee.Id));
                        dgvFavoritos.DataSource = Session["listaFavoritos"];
                        dgvFavoritos.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }

        }
        protected void dgvFavoritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = dgvFavoritos.SelectedIndex;
            string id = dgvFavoritos.Rows[index].Cells[0].Text;
            Response.Redirect("DetalleArticulo.aspx?id=" + id + "&from=F", false);
        }

        protected void dgvFavoritos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvFavoritos.DataSource = Session["listaFavoritos"];
            dgvFavoritos.PageIndex = e.NewPageIndex;
            dgvFavoritos.DataBind();
        }
    }
}