using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using accesoDB;
using Dominio;

namespace helperDB
{
    public class toolsDB
    {
        public void setearAllParammetros(AccesoDatos datos, Articulo articulo)

        // Setea todos los parametros, con los datos del articulo, para ser guardados en la base de datos
        {
            datos.setearParametro("@Codigo", articulo.Codigo);
            datos.setearParametro("@Nombre", articulo.Nombre);
            datos.setearParametro("@Descripcion", articulo.Descripcion);
            datos.setearParametro("@IdMarca", articulo.Marca.Id.ToString());
            datos.setearParametro("@IdCategoria", articulo.Categoria.Id.ToString());
            datos.setearParametro("@ImagenUrl", articulo.ImagenUrl);
            datos.setearParametro("@Precio", articulo.Precio);



            if (articulo.Id != 0)
            {
                datos.setearParametro("@Id", articulo.Id);
            }
        }
    }
}
