using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dominio;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(){

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.ImagenUrl, A.Precio, C.Descripcion as CategoriaDescripcion, M.Descripcion as MarcaDescripcion from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //validamos imagen
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    aux.Precio = (decimal)datos.Lector["Precio"];


                    lista.Add(aux);
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
        public void agregar (Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos ();

            try
            {
                datos.setearConsulta("insert into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values(@codigo, @nombre, @descripcion, @idMarca, @idCategoria, @imagenUrl, @precio)");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre",nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@imagenUrl",nuevo.UrlImagen);
                datos.setearParametro("@precio",nuevo.Precio);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion ();
            }     
        }

        public void modificiar (Articulo nuevo) {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @codigo, Nombre = @nombre, Descripcion = @descripcion, IdMarca = @idMarca, IdCategoria = @idCategoria, ImagenUrl = @imagenUrl, Precio = @precio Where Id = @id");
                datos.setearParametro("@codigo", nuevo.Codigo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.Id);
                datos.setearParametro("@idCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@imagenUrl", nuevo.UrlImagen);
                datos.setearParametro("@precio", nuevo.Precio);
                datos.setearParametro("@id", nuevo.Id);

                datos.ejecutarAccion ();
            }
            catch (Exception ex){
                throw ex;
            
            }finally { 
                datos.cerrarConexion();
            }
            
            
        }
    
        public void eliminar( int id )
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where id =@id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "select A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.ImagenUrl, A.Precio, C.Descripcion as CategoriaDescripcion, M.Descripcion as MarcaDescripcion from ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id AND A.IdCategoria = C.Id AND ";

                switch (campo){
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "A.Precio < " + filtro;
                                break;
                            default:
                                consulta += "A.Precio = " + filtro;
                                break;
                        }
                        break;
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Nombre like '" + filtro + "%' ";
                                break;
                            case "Termina con":
                                consulta += "A.Nombre like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "A.Nombre like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Marca":
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
                        break;
                    default:
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
                        break;
                }








                //if (campo == "Precio"){
                //    switch (criterio) {
                //        case "Mayor a":
                //            consulta += "A.Precio > " + filtro;
                //            break;
                //        case "Menor a":
                //            consulta += "A.Precio < " + filtro;
                //            break;
                //        default:
                //            consulta += "A.Precio = " + filtro;
                //            break;
                //    }
                //}
                //else if (campo == "Nombre"){
                //    switch (criterio){
                //        case "Comienza con":
                //            consulta += "A.Nombre like '" + filtro + "%' ";
                //            break;
                //        case "Termina con":
                //            consulta += "A.Nombre like '%" + filtro + "'";
                //            break;
                //        default:
                //            consulta += "A.Nombre like '%" + filtro + "%'";
                //            break;
                //    }
                //}else if (campo == "Marca"){
                //    switch (criterio)
                //    {
                //        case "Comienza con":
                //            consulta += "M.Descripcion like '" + filtro + "%' ";
                //            break;
                //        case "Termina con":
                //            consulta += "M.Descripcion like '%" + filtro + "'";
                //            break;
                //        default:
                //            consulta += "M.Descripcion like '%" + filtro + "%'";
                //            break;
                //    }
                //}else{
                //    switch (criterio){
                //        case "Comienza con":
                //            consulta += "C.Descripcion like '" + filtro + "%' ";
                //            break;
                //        case "Termina con":
                //            consulta += "C.Descripcion like '%" + filtro + "'";
                //            break;
                //        default:
                //            consulta += "C.Descripcion like '%" + filtro + "%'";
                //            break;
                //    }
                //}
                datos.setearConsulta(consulta );
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["MarcaDescripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["CategoriaDescripcion"];

                    aux.Precio = (decimal)datos.Lector["Precio"];


                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
            
        
        
        }
    }


         
}
