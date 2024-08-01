using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Web;
using System.Web.UI.WebControls;

namespace helper
{
    public static class Excepciones
    {
        private static string errorEstandar = "Error.aspx";

        public static string paginaError(Exception ex)
        {
            return errorEstandar;
        }

        public static void validatorsDetalles(bool estado, RequiredFieldValidator n1, RequiredFieldValidator n2, RequiredFieldValidator n3, RegularExpressionValidator n4)
        {
            n1.Enabled = estado;
            n2.Enabled = estado;
            n3.Enabled = estado;
            n4.Enabled = estado;
        }
            
    }
}
