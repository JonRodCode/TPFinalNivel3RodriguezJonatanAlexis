using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using accesoDB;
using helperDB;
using System.Xml.Linq;

namespace Negocio
{
    public class NegocioArticulo
    {
        toolsDB toolDB = new toolsDB();

        public string consultaLectura = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, IdMarca, IdCategoria from ARTICULOS A inner join CATEGORIAS C ON A.IdCategoria = C.Id inner join MARCAS M ON A.IdMarca = M.Id ";
        public List<Articulo> lista_articulos;

        public List<Articulo> listar(string id = "")

        //Retorna una lista de articulos consultada en la base de datos 
        {
            lista_articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta;

            try
            {
                if (id != "")
                {
                    consulta = consultaLectura + "Where A.id = @id";
                    datos.setearParametro("@id", id);
                }
                else
                    consulta = consultaLectura;
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];

                    articulo.Descripcion = (string)datos.Lector["Descripcion"];


                    Categoria_marca marca = new Categoria_marca();
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Marca = marca;

                    Categoria_marca categoria = new Categoria_marca();
                    categoria.Id = (int)datos.Lector["IdCategoria"];
                    categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Categoria = categoria;

                    articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    lista_articulos.Add(articulo);
                }

                return lista_articulos;
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
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            AccesoDatos datos = new AccesoDatos();
            lista_articulos = new List<Articulo>();
            try
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, IdMarca, IdCategoria from ARTICULOS A inner join CATEGORIAS C ON A.IdCategoria = C.Id inner join MARCAS M ON A.IdMarca = M.Id Where ";
              
                if (campo == "Precio")
                {
                    if (filtro.Contains(","))
                        filtro.Replace(",", ".");

                    switch (criterio)
                    {
                        case "Mayor a":
                            consulta += "Precio > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Precio < " + filtro;
                            break;
                        default:
                            consulta += "Precio = " + filtro;
                            break;
                    }
                }
                else if (campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "Nombre like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Nombre like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Código")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "Codigo like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Codigo like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "Codigo like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "M.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "M.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "M.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }
                else if (campo == "Categoria")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "C.Descripcion like '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "C.Descripcion like '%" + filtro + "'";
                            break;
                        default:
                            consulta += "C.Descripcion like '%" + filtro + "%'";
                            break;
                    }
                }


                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];

                    articulo.Descripcion = (string)datos.Lector["Descripcion"];


                    Categoria_marca marca = new Categoria_marca();
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Marca = marca;

                    Categoria_marca categoria = new Categoria_marca();
                    categoria.Id = (int)datos.Lector["IdCategoria"];
                    categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Categoria = categoria;

                    articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    lista_articulos.Add(articulo);
                }

                return lista_articulos;
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

        public int agregarArticulo(Articulo nuevo)

        // Agrega un nuevo articulo a la base de datos
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                consulta = "insert into ARTICULOS(Codigo,Nombre,Descripcion,IdMarca,IdCategoria,ImagenUrl,Precio) values (@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@ImagenUrl,@Precio)";
                toolDB.setearAllParammetros(datos, nuevo);

                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
                datos.cerrarConexion();

                return ultimoArticulo();
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

        public int ultimoArticulo()
        {
            AccesoDatos datos = new AccesoDatos();
            int id = 0;
            try
            {
                string consulta = "select max(id) idMax from articulos";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    id = (int)datos.Lector["idMax"];
                }
                return id;
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

        public void modificarArticulo(Articulo modificado)

        // Modifica un articulo de la base de datos
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                consulta = "update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where id = @Id";

                toolDB.setearAllParammetros(datos, modificado);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
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

        public void eliminarArticuloParaSiempre(string id)

        //Elimina permanentemente 1 articulo de la DB (Eliminacion fisica)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                consulta = "delete From ARTICULOS where id = @Id";
                datos.setearParametro("@Id", id);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public List<Articulo> listarFavoritos(int id)
        {
            lista_articulos = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select A.Id, Codigo, A.Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, IdMarca, IdCategoria from ARTICULOS A inner join CATEGORIAS C ON A.IdCategoria = C.Id inner join MARCAS M ON A.IdMarca = M.Id inner join FAVORITOS F On A.id = F.IdArticulo inner join USERS U On F.IdUser = U.Id Where U.id = @id";

            try
            {
                datos.setearParametro("@id", id);
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo articulo = new Articulo();
                    articulo.Id = (int)datos.Lector["Id"];
                    articulo.Codigo = (string)datos.Lector["Codigo"];
                    articulo.Nombre = (string)datos.Lector["Nombre"];

                    articulo.Descripcion = (string)datos.Lector["Descripcion"];


                    Categoria_marca marca = new Categoria_marca();
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Descripcion = (string)datos.Lector["Marca"];
                    articulo.Marca = marca;

                    Categoria_marca categoria = new Categoria_marca();
                    categoria.Id = (int)datos.Lector["IdCategoria"];
                    categoria.Descripcion = (string)datos.Lector["Categoria"];
                    articulo.Categoria = categoria;

                    articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    articulo.Precio = (decimal)datos.Lector["Precio"];

                    lista_articulos.Add(articulo);
                }

                return lista_articulos;
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

        public void agregarFavorito(string idTrainee, string idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "insert into FAVORITOS(IdUser, IdArticulo) values(@idUser, @idArticulo)";
                datos.setearParametro("@idUser", idTrainee);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }

        public bool estaEnFavoritos(string idTrainee, string idArticulo)
        {
            List<int> lista = new List<int>();
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                consulta = "select * From FAVORITOS where IdArticulo = @idArticulo and IdUser = @idUser";
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@idUser", idTrainee);
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }

        }

        public void quitarFavorito(string idTrainee, string idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "delete From FAVORITOS where IdUser = @idUser and IdArticulo = @idArticulo";
                datos.setearParametro("@idUser", idTrainee);
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
    }
}
