using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace helper
{
    public class Seguridad
    {
        public static bool sessionActiva(object user)
        {
            Trainee trainee = user != null ? (Trainee)user : null;
            if (trainee != null && trainee.Id != 0)
                return true;
            else
                return false;
        }
    }
}
