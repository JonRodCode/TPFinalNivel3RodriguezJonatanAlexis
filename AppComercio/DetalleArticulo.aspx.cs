using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using helper;

namespace AppComercio
{
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        public Trainee trainee { get; set; }
        public bool ConfirmaEliminacion { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trainee = (Trainee)Session["trainee"];
                if (!IsPostBack)
                {
                    NegocioCategoria_marca negocioCatMar = new NegocioCategoria_marca();

                    ddlMarca.DataSource = negocioCatMar.listar("Marcas");
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = negocioCatMar.listar("Categorias");
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();


                    //Controlamos si no estamos viendo detalles de un articulo
                    if (Request.QueryString["id"] is null && (trainee is null || trainee.Admin == false))
                        Response.Redirect("Default.aspx", false);
                    // Aca confirmamos que estamos en detalles de un articulo
                    else if (!(Request.QueryString["id"] is null))
                    {
                        Articulo articulo = new Articulo();
                        NegocioArticulo negocio = new NegocioArticulo();
                        articulo = negocio.listar(Request.QueryString["id"].ToString())[0];

                        imgArticulo.ImageUrl = articulo.ImagenUrl + "?v=" + DateTime.Now.Ticks.ToString();
                        txtUrlImagen.Text = articulo.ImagenUrl;
                        txtCodigo.Text = articulo.Codigo;
                        txtNombre.Text = articulo.Nombre;
                        txtPrecio.Text = articulo.Precio.ToString();

                        ddlMarca.SelectedValue = articulo.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = articulo.Categoria.Id.ToString();
                        txtDescripcion.Text = articulo.Descripcion;

                        if (!(trainee is null) && (negocio.estaEnFavoritos(trainee.Id.ToString(), articulo.Id.ToString())))
                            btnAFavoritos.Text = "Quitar de favoritos";
                    }
                    // Habilitamos los elementos para el Admin
                    if (!(trainee is null) && trainee.Admin)
                    {
                        txtCodigo.Enabled = true;
                        txtNombre.Enabled = true;
                        txtPrecio.Enabled = true;
                        ddlMarca.Enabled = true;
                        ddlCategoria.Enabled = true;
                        txtDescripcion.Enabled = true;
                        txtImagen.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }

        protected void btnAFavoritos_Click(object sender, EventArgs e)
        {
            try
            {
                NegocioArticulo negocio = new NegocioArticulo();
                if (!negocio.estaEnFavoritos(trainee.Id.ToString(), Request.QueryString["id"].ToString()))
                {
                    negocio.agregarFavorito(trainee.Id.ToString(), Request.QueryString["id"]);
                    btnAFavoritos.Text = "Quitar de favoritos";
                }
                else
                {
                    negocio.quitarFavorito(trainee.Id.ToString(), Request.QueryString["id"]);
                    btnAFavoritos.Text = "Agregar de favoritos";
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

                if (txtImagen.Visible && (!(string.IsNullOrEmpty(txtImagen.PostedFile.FileName))))
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    string nombreImagenArticulo = "articulo-" + articulo.Id.ToString() + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreImagenArticulo);
                    articulo.ImagenUrl = "Imagenes/" + nombreImagenArticulo;
                    imgArticulo.ImageUrl = articulo.ImagenUrl + "?v=" + DateTime.Now.Ticks.ToString();
                }
                else
                {
                    articulo.ImagenUrl = txtUrlImagen.Text;
                    imgArticulo.ImageUrl = articulo.ImagenUrl + "?v=" + DateTime.Now.Ticks.ToString();
                }


                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                articulo.Marca = new Categoria_marca();
                articulo.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                articulo.Categoria = new Categoria_marca();
                articulo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                articulo.Descripcion = txtDescripcion.Text;

                if (articulo.Id == 0)
                {
                    int id = negocio.agregarArticulo(articulo);
                    Response.Redirect("DetalleArticulo.aspx?from=" + Request.QueryString["from"].ToString()+"&id="+id, false);
                }
                else
                    negocio.modificarArticulo(articulo);


            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }

        protected void btnUrlImagen_Click(object sender, EventArgs e)
        {
            txtUrlImagen.Visible = true;
            txtImagen.Visible = false;
        }

        protected void btnArchivoImagen_Click(object sender, EventArgs e)
        {
            txtUrlImagen.Visible = false;
            txtImagen.Visible = true;

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

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnEliminarConfirmacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked)
                {
                    NegocioArticulo negocio = new NegocioArticulo();
                    negocio.eliminarArticuloParaSiempre(Request.QueryString["id"].ToString());
                    btnRegresar_Click(sender, e);
                }

            }
            catch (Exception ex)
            {
                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }
    }
}