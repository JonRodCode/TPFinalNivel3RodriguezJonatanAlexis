﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppComercio
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["error"] == "registro")
                lblError.Text = "El email ya se encuentra registrado.";
            else if (!(Session["Error"] is null))
                lblError.Text = Session["Error"].ToString();
            else
                Response.Redirect("default.aspx", false);
        }
    }
}