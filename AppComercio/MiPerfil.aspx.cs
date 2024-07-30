using Dominio;
using helper;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppComercio
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Trainee trainee = new Trainee();
            try
            {
                if (!IsPostBack)
                {
                    trainee = (Trainee)Session["trainee"];
                    txtEmail.Text = trainee.Email;
                    txtNombre.Text = trainee.Nombre;
                    txtApellido.Text = trainee.Apellido;
                    if (!(string.IsNullOrEmpty(trainee.ImagenPerfil)))
                        imgNuevoPerfil.ImageUrl = "Imagenes/" + trainee.ImagenPerfil + "?v=" + DateTime.Now.Ticks.ToString();
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
            NegocioTrainee negocio = new NegocioTrainee();
            Trainee user = (Trainee)Session["trainee"];

            try
            {
                if (!(string.IsNullOrEmpty(txtImagen.PostedFile.FileName)))
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    string nombreImagenPerfil = "perfil-" + user.Id.ToString() + ".jpg";
                    txtImagen.PostedFile.SaveAs(ruta + nombreImagenPerfil);

                    user.ImagenPerfil = nombreImagenPerfil;
                    imgNuevoPerfil.ImageUrl = "Imagenes/" + nombreImagenPerfil + "?v=" + DateTime.Now.Ticks.ToString();
                }
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;

                negocio.actualizar(user);


            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }
    }
}