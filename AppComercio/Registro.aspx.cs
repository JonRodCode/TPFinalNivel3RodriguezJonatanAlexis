﻿using Dominio;
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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(Page.IsValid))
                    return;

                Trainee user = new Trainee();
                NegocioTrainee negocio = new NegocioTrainee();
                user.Email = txtEmail.Text;
                user.Pass = txtPassword.Text;

                if (!negocio.estaRegistrado(user))
                {
                    negocio.insertarNuevo(user);
                    negocio.Login(user);
                    Session.Add("trainee", user);

                    Response.Redirect("Default.aspx", false);
                }
                else
                    Response.Redirect("Error.aspx?error=registro", false);

            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
                Response.Redirect(Excepciones.paginaError(ex), false);
            }
        }
    }
}