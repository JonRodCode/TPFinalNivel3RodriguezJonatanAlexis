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

        Articulo articulo;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trainee = (Trainee)Session["trainee"];
                if (Request.QueryString["id"] == null)
                    Response.Redirect("Default.aspx", false);
                else
                {
                    NegocioArticulo negocio = new NegocioArticulo();
                    articulo = negocio.listar(Request.QueryString["id"].ToString())[0];
                    imgArticulo.ImageUrl = articulo.ImagenUrl;
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtMarca.Text = articulo.Marca.ToString();
                    txtCategoria.Text = articulo.Categoria.ToString();
                    txtDescripcion.Text = articulo.Descripcion;

                    if (!(trainee is null))
                    {


                        if (trainee.Admin)
                        {
                            txtCodigo.Enabled = true;
                            txtNombre.Enabled = true;
                            txtPrecio.Enabled = true;
                            txtMarca.Enabled = true;
                            txtCategoria.Enabled = true;
                            txtDescripcion.Enabled = true;
                        }
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

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            NegocioArticulo negocio = new NegocioArticulo();
            try
            {
                if (!(string.IsNullOrEmpty(txtImagen.PostedFile.FileName)))
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    string nombreImagenArticulo = "articulo-" + articulo.Id.ToString() + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreImagenArticulo);

                    articulo.ImagenUrl = nombreImagenArticulo;
                }

                //FALTA TODO LO DEMAS

                negocio.modificarArticulo(articulo);


            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }
    }
}