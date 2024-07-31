using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using helper;

namespace AppComercio
{
    public partial class ArticuloLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        public Trainee trainee { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trainee = (Trainee)Session["trainee"];
                if (!IsPostBack)
                {
                    FiltroAvanzado = chkFiltroAvanzado.Checked;
                    NegocioArticulo negocio = new NegocioArticulo();
                    Session.Add("listaArticulos", negocio.listar());
                    dgvArticulos.DataSource = Session["listaArticulos"];
                    dgvArticulos.DataBind();
                    Session.Add("listaArticulosActual", Session["listaArticulos"]);
                }
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex));
            }

        }

        protected void txtFitro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(
                x => (x.Nombre.ToLower().Contains(txtFitro.Text.ToLower()))
                || (x.Codigo.ToLower().Contains(txtFitro.Text.ToLower()))
                || (x.Marca.Descripcion.ToLower().Contains(txtFitro.Text.ToLower()))
                || (x.Categoria.Descripcion.ToLower().Contains(txtFitro.Text.ToLower()))
                || (x.Precio.ToString().Contains(txtFitro.Text))
                );

            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkFiltroAvanzado.Checked;
            txtFitro.Enabled = !FiltroAvanzado;
            if (FiltroAvanzado == false)
            {
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
                ddlCampo.Items.Clear();
            }
            else
            {
                List<string> listaCampo = new List<string>() { "Código", "Nombre", "Marca", "Categoria", "Precio" };
                foreach (string elem in listaCampo)
                    ddlCampo.Items.Add(elem);

                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
            }
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlCampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Igual a");
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
            }
            else
            {
                ddlCriterio.Items.Add("Empieza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioArticulo negocio = new NegocioArticulo();

                Session.Add("listaArticulosActual", negocio.filtrar(
                    ddlCampo.SelectedItem.ToString(),
                    ddlCriterio.SelectedItem.ToString(),
                    txtFiltroAvanzado.Text));
                dgvArticulos.DataSource = Session["listaArticulosActual"];
                dgvArticulos.DataBind();

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = dgvArticulos.SelectedIndex;
            string id = dgvArticulos.Rows[index].Cells[0].Text;
            Response.Redirect("DetalleArticulo.aspx?id=" + id + "&from=AL", false);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.DataSource = Session["listaArticulosActual"];
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }

        protected void btnAgregarArticulo_Click(object sender, EventArgs e)
        {
            Response.Redirect("DetalleArticulo.aspx?from=AL", false);
        }
    }
}