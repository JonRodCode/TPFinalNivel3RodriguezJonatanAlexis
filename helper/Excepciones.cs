using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using System.Web;

namespace helper
{
    public static class Excepciones
    {
        private static string errorEstandar = "Error.aspx";

        public static string paginaError(Exception ex)
        {
            return errorEstandar;
        }
    }
}
