using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Compra
    {
        public List<Compra> Listar()
        {

            List<Compra> lista = new List<Compra>();

            try
            {
                using (SqlConnection connection = new SqlConnection(Conexion.cn))
                {

                    //StringBuilder sb = new StringBuilder();


                    string query = @"
            SELECT c.idCompra, c.precioCompra,
            p.IdProducto, p.Nombre AS NomProduct,
            u.IdUsuario, u.Nombres AS NomUser, u.Apellidos AS ApeUser,
            c.cantidad, c.Activo
            FROM Compra c
            INNER JOIN PRODUCTO p ON p.IdProducto = c.idProducto
            INNER JOIN USUARIO u ON u.IdUsuario = c.idEmpleado";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Compra compra = new Compra()
                                {
                                    idCompra = reader.GetInt32(reader.GetOrdinal("idCompra")),
                                    precioCompra = reader.GetDecimal(reader.GetOrdinal("precioCompra")),
                                    oProducto = new Producto()
                                    {
                                        IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                                        Nombre = reader.GetString(reader.GetOrdinal("NomProduct"))
                                    },
                                    oUsuario = new Usuario()
                                    {
                                        IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                                        Nombres = reader.GetString(reader.GetOrdinal("NomUser")),
                                        Apellidos = reader.GetString(reader.GetOrdinal("ApeUser"))
                                    },
                                    cantidad = reader.GetInt32(reader.GetOrdinal("cantidad")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))
                                };

                                lista.Add(compra);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción de forma adecuada
                Console.WriteLine("Error: " + ex.Message);
                lista = new List<Compra>();
            }

            return lista;
        }
        //       @precioCompra decimal (18,2),
        //@idProducto varchar(100),
        //@idEmpleado varchar(100),
        //@cantidad int,
        //@Activo bit,
        //   @Mensaje varchar(500) output,
        //@Resultado int output,
        //   @cantidadMandar int output
        //public int Registrar(Compra obj, out string Mensaje)
        //{
        //    int idautogenerado = 0;

        //    Mensaje = string.Empty;
        //    try
        //    {


        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", oconexion);
        //            cmd.Parameters.AddWithValue("precioCompra", obj.precioCompra);
        //            cmd.Parameters.AddWithValue("idProducto", obj.oProducto.IdProducto);
        //            cmd.Parameters.AddWithValue("idEmpleado", obj.oUsuario.IdUsuario);
        //            cmd.Parameters.AddWithValue("cantidad", obj.cantidad);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("cantidadMandar", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();

        //            cmd.ExecuteNonQuery();

        //            idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        idautogenerado = 0;
        //        Mensaje = ex.Message;
        //    }
        //    return idautogenerado;
        //}
        public int Registrar(Compra obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;
            try
            {


                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarCompra", oconexion);
                    cmd.Parameters.AddWithValue("precioCompra", obj.precioCompra);
                    cmd.Parameters.AddWithValue("idProducto", obj.oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("idEmpleado", obj.oUsuario.IdUsuario);
                    cmd.Parameters.AddWithValue("cantidad", obj.cantidad);
                    cmd.Parameters.AddWithValue("Activo", obj.Activo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("cantidadMandar", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }
            return idautogenerado;
        }
        //public bool Editar(Compra obj, out string Mensaje)
        //{
        //    bool resultado = false;
        //    Mensaje = string.Empty;
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            SqlCommand cmd = new SqlCommand("sp_EditarCompra", oconexion);
        //            cmd.Parameters.AddWithValue("IdCompra", obj.IdCompra);
        //            cmd.Parameters.AddWithValue("precioCompra", obj.precioCompra);
        //            cmd.Parameters.AddWithValue("idProducto", obj.oProducto.IdProducto);
        //            cmd.Parameters.AddWithValue("idEmpleado", obj.oUsuario.IdUsuario);
        //            cmd.Parameters.AddWithValue("cantidad", obj.cantidad);
        //            cmd.Parameters.AddWithValue("Activo", obj.Activo);
        //            cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();

        //            cmd.ExecuteNonQuery();

        //            resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
        //            Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultado = false;
        //        Mensaje = ex.Message;
        //    }
        //    return resultado;
        //}

        //public List<Compra> ObtenerCompras(int idMarca, int idCategoria, int nroPagina, int obtenerRegistros, out int TotalRegistros, out int TotalPaginas)
        //{

        //    List<Compra> lista = new List<Compra>();
        //    TotalRegistros = 0;
        //    TotalPaginas = 0;
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {

        //            SqlCommand cmd = new SqlCommand("sp_ObtenerCompras", oconexion);
        //            cmd.Parameters.AddWithValue("idMarca", idMarca);
        //            cmd.Parameters.AddWithValue("idCategoria", idCategoria);
        //            cmd.Parameters.AddWithValue("nroPagina", nroPagina);
        //            cmd.Parameters.AddWithValue("obtenerRegistros", obtenerRegistros);
        //            cmd.Parameters.Add("TotalRegistros", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.Parameters.Add("TotalPaginas", SqlDbType.Int).Direction = ParameterDirection.Output;
        //            cmd.CommandType = CommandType.StoredProcedure;

        //            oconexion.Open();

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    lista.Add(new Compra()
        //                    {
        //                        IdCompra = Convert.ToInt32(dr["IdCompra"]),
        //                        Nombre = dr["Nombre"].ToString(),
        //                        Descripcion = dr["Descripcion"].ToString(),
        //                        oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
        //                        oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
        //                        Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
        //                        Stock = Convert.ToInt32(dr["Stock"]),
        //                        RutaImagen = dr["RutaImagen"].ToString(),
        //                        NombreImagen = dr["NombreImagen"].ToString(),
        //                        Activo = Convert.ToBoolean(dr["Activo"])
        //                    });
        //                }
        //            }

        //            TotalRegistros = Convert.ToInt32(cmd.Parameters["TotalRegistros"].Value);
        //            TotalPaginas = Convert.ToInt32(cmd.Parameters["TotalPaginas"].Value);
        //        }
        //    }
        //    catch
        //    {
        //        lista = new List<Compra>();

        //    }
        //    return lista;
        //}
        //public List<Compra> Listar()
        //{

        //    List<Compra> lista = new List<Compra>();
        //    //Controlar errores
        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            string query = "select idCompra,precioCompra,idCompra,idEmpleado,cantidad,Activo from Compra";
        //            StringBuilder sb = new StringBuilder();

        //            SqlCommand cmd = new SqlCommand(query, oconexion);
        //            cmd.CommandType = CommandType.Text;

        //            oconexion.Open();

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {

        //                    lista.Add(
        //                        new Compra()
        //                        {
        //                            IdCompra = Convert.ToInt32(dr["IdCompra"]),
        //                            precioCompra = Convert.ToDecimal(dr["precioCompra"], new CultureInfo("es-PE")),
        //                            oCompra = new Compra() { IdCompra = Convert.ToInt32(dr["IdCompra"]), Descripcion = dr["DesCompra"].ToString() },
        //                            oUsuario = new Usuario() { IdUsuario = Convert.ToInt32(dr["IdUsuario"]), Nombres = dr["NomUsuario"].ToString() },
        //                            cantidad = Convert.ToInt32(dr["cantidad"]),
        //                            Activo = Convert.ToBoolean(dr["Activo"])
        //                        }

        //                        );
        //                }
        //            }

        //            //sb.AppendLine("select p.IdCompra,p.Nombre,p.Descripcion,");
        //            //sb.AppendLine("m.IdMarca,m.Descripcion[DesMarca],");
        //            //sb.AppendLine("c.IdCategoria,c.Descripcion[DesCategoria],");
        //            //sb.AppendLine("p.Precio,p.Stock,p.RutaImagen,p.NombreImagen,p.Activo");
        //            //sb.AppendLine("from Compra p");
        //            //sb.AppendLine("inner join MARCA m on m.IdMarca = p.IdMarca");
        //            //sb.AppendLine("inner join CATEGORIA c on c.IdCategoria = p.IdCategoria");


        //            //SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
        //            //cmd.CommandType = CommandType.Text;

        //            //oconexion.Open();

        //            //using (SqlDataReader dr = cmd.ExecuteReader())
        //            //{
        //            //    while (dr.Read())
        //            //    {
        //            //        lista.Add(new Compra()
        //            //        {
        //            //            IdCompra = Convert.ToInt32(dr["IdCompra"]),
        //            //            Nombre = dr["Nombre"].ToString(),
        //            //            Descripcion = dr["Descripcion"].ToString(),
        //            //            oMarca = new Marca() { IdMarca = Convert.ToInt32(dr["IdMarca"]), Descripcion = dr["DesMarca"].ToString() },
        //            //            oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
        //            //            Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
        //            //            Stock = Convert.ToInt32(dr["Stock"]),
        //            //            RutaImagen = dr["RutaImagen"].ToString(),
        //            //            NombreImagen = dr["NombreImagen"].ToString(),
        //            //            Activo = Convert.ToBoolean(dr["Activo"])
        //            //        });
        //            //    }
        //            //}
        //        }
        //    }
        //    catch
        //    {
        //        lista=new List<Compra>();
        //    }
        //    return lista;
        //}
    }
}
