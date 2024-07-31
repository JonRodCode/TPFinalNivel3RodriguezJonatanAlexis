using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppComercio
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["trainee"] is null)
            {
                if (Page is Favoritos || Page is MiPerfil)
                    Response.Redirect("Login.aspx", false);
            }
            else
            {
                Trainee user = (Trainee)Session["trainee"];
                if (!string.IsNullOrEmpty(user.ImagenPerfil))
                    imgAvatar.ImageUrl = "~/Imagenes/" + user.ImagenPerfil;
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx", false);
        }
    }
}