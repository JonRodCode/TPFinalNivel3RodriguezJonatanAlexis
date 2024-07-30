﻿using System;
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

        public string consultaLectura = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, IdMarca, IdCategoria from ARTICULOS A left join CATEGORIAS C ON A.IdCategoria = C.Id left join MARCAS M ON A.IdMarca = M.Id ";
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
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, IdMarca, IdCategoria from ARTICULOS A left join CATEGORIAS C ON A.IdCategoria = C.Id left join MARCAS M ON A.IdMarca = M.Id Where ";
                if (campo == "Precio")
                {
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
                        case "Comienza con":
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
                        case "Comienza con":
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
                        case "Comienza con":
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
                        case "Comienza con":
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

        public void agregarArticulo(Articulo nuevo)

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

        public void eliminarArticuloParaSiempre(Articulo eliminado)

        //Elimina permanentemente 1 articulo de la DB (Eliminacion fisica)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                consulta = "delete From ARTICULOS where id = @Id";
                datos.setearParametro("@Id", eliminado.Id);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void eliminarArticulosParaSiempre(List<Articulo> listaEliminados)

        //Elimina permanentemente mas de 1 articulo de la DB (Eliminacion fisica)
        {
            foreach (Articulo art in listaEliminados)
            {
                eliminarArticuloParaSiempre(art);
            }
        }
        public void eliminarArticulo(Articulo eliminado)

        //Elimina 1 articulo (Eliminacion logica)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            string codigo;
            try
            {
                codigo = eliminado.Codigo + " /oculto\\";
                consulta = "update ARTICULOS set codigo = @Codigo where id = @Id";
                datos.setearParametro("@Codigo", codigo);
                datos.setearParametro("@Id", eliminado.Id.ToString());
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void eliminarArticulos(List<Articulo> listaEliminados)

        //Elimina mas de 1 articulo (Eliminacion logica)
        {
            foreach (Articulo art in listaEliminados)
            {
                eliminarArticulo(art);
            }
        }

        public void restaurarArticulo(Articulo restaurado)

        //  Restaura 1 articulo seleccionado (devuelve a la lista Principal)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta;
            try
            {
                restaurado.Codigo = restaurado.Codigo.Replace(" /oculto\\", "");
                consulta = consulta = "update ARTICULOS set Codigo = @Codigo where id = @Id";
                datos.setearParametro("@Codigo", restaurado.Codigo);
                datos.setearParametro("@Id", restaurado.Id);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally { datos.cerrarConexion(); }
        }

        public void restaurarArticulos(List<Articulo> listaRestaurados)

        //  Restaura los articulos seleccionados (devuelve a la lista Principal)
        {
            foreach (Articulo art in listaRestaurados)
            {
                restaurarArticulo(art);
            }
        }
    }
}
