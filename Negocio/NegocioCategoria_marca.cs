using accesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    internal class NegocioCategoria_marca
    {
        List<Categoria_marca> lista;

        public List<Categoria_marca> listar(string cat_mar)
        {
            lista = new List<Categoria_marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta;
                consulta = "select * from " + cat_mar;
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Categoria_marca categoria_marca = new Categoria_marca();
                    categoria_marca.Id = (int)datos.Lector["Id"];
                    categoria_marca.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(categoria_marca);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }
    }
}
