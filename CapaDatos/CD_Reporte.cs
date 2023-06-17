using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Runtime.InteropServices.ComTypes;

namespace CapaDatos
{
    public class CD_Reporte
    {

        public List<Reporte> Ventas(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Reporte> lista = new List<Reporte>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {


                    SqlCommand cmd = new SqlCommand("sp_ReporteVentas", oconexion);
                    cmd.Parameters.AddWithValue("fechainicio", fechainicio);
                    cmd.Parameters.AddWithValue("fechafin", fechafin);
                    cmd.Parameters.AddWithValue("idtransaccion", idtransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            lista.Add(
                                new Reporte()
                                {
                                    FechaVenta = dr["FechaVenta"].ToString(),
                                    Cliente = dr["Cliente"].ToString(),
                                    Producto = dr["Producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["Precio"], new CultureInfo("es-PE")),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"].ToString()),
                                    Total = Convert.ToDecimal(dr["Total"], new CultureInfo("es-PE")),
                                    IdTransaccion = dr["IdTransaccion"].ToString()
                                });
                        }
                    }
                }

            }
            catch
            {
                lista = new List<Reporte>();

            }


            return lista;


        }



        public DashBoard VerDashBoard()
        {

            DashBoard objeto = new DashBoard();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            objeto = new DashBoard()
                            {
                                TotalCliente = Convert.ToInt32(dr["TotalCliente"]),
                                TotalVenta = Convert.ToInt32(dr["TotalVenta"]),
                                TotalProducto = Convert.ToInt32(dr["TotalProducto"]),

                            };
                        }
                    }
                }

            }
            catch
            {
                objeto = new  DashBoard();

            }


            return objeto;


        }

        //public List<Top5Productos> ObtenerTopProductos()
        //{
        //    List<Top5Productos> topProductos = new List<Top5Productos>();

        //    using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //    {
        //        oconexion.Open();
        //        using (var command = new SqlCommand())
        //        {
        //            SqlDataReader reader;
        //            command.Connection = oconexion;
        //            // Obtener los 5 productos principales
        //            command.CommandText = @"select top 5 P.Nombre, sum(DETALLE_VENTA.Cantidad) as Q
        //                            from DETALLE_VENTA
        //                            inner join PRODUCTO P on P.IdProducto = DETALLE_VENTA.IdProducto
        //                            inner join VENTA V on V.IdVenta = DETALLE_VENTA.IdVenta
        //                            group by P.Nombre
        //                            order by Q desc";

        //            reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                topProductos.Add(new Top5Productos
        //                {
        //                    Nombres = reader[0].ToString(),
        //                    Cantidad = (int)reader[1]
        //                });
        //            }

        //            reader.Close();
        //        }
        //    }

        //    return topProductos;
        //}
        public List<Top5Productos> ObtenerTopProductos()
        {
            List<Top5Productos> topProductos = new List<Top5Productos>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    oconexion.Open();
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = oconexion;
                        command.CommandText = @"SELECT TOP 5 P.Nombre, SUM(DETALLE_VENTA.Cantidad) AS Q
                                    FROM DETALLE_VENTA
                                    INNER JOIN PRODUCTO P ON P.IdProducto = DETALLE_VENTA.IdProducto
                                    INNER JOIN VENTA V ON V.IdVenta = DETALLE_VENTA.IdVenta
                                    GROUP BY P.Nombre
                                    ORDER BY Q DESC";

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Top5Productos producto = new Top5Productos
                                {
                                    Nombres = reader.GetString(0),
                                    Cantidad = reader.GetInt32(1)
                                };

                                topProductos.Add(producto);
                            }
                        }
                    }
                }
            }
            catch
            {
                topProductos = new List<Top5Productos>();
            }

            return topProductos;
        }

        static decimal Indicador(int numDias)
        {
            string connectionString = "Data Source=34.227.55.226;Initial Catalog=workduck_misdb;User Id=MisUser;Password=MisDb123zzs;";
            // Obtener la fecha actual y la fecha hace X días
            DateTime fechaActual = DateTime.Now.Date;
            DateTime fechaAnterior = fechaActual.AddDays(-numDias);



            // Consulta SQL para obtener el total de ganancias en el periodo especificado
            string query = $@"
                SELECT 
                    SUM(TotalAmount) AS Ganancias
                FROM 
                    [Order]
                WHERE 
                    OrderDate >= @FechaAnterior AND OrderDate <= @FechaActual";



            // Variables para almacenar los resultados
            decimal gananciasActual = 0;
            decimal gananciasAnterior = 0;
            decimal porcentajeGananciaPerdida = 0;



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();



                // Obtener ganancias del periodo actual
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FechaAnterior", fechaAnterior);
                command.Parameters.AddWithValue("@FechaActual", fechaActual);
                SqlDataReader reader = command.ExecuteReader();



                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        gananciasActual = reader.GetDecimal(0);
                    }
                }
                reader.Close();



                // Obtener ganancias del periodo anterior
                command.Parameters["@FechaAnterior"].Value = fechaAnterior.AddDays(-numDias);
                command.Parameters["@FechaActual"].Value = fechaActual.AddDays(-numDias);
                reader = command.ExecuteReader();



                if (reader.Read())
                {
                    if (!reader.IsDBNull(0))
                    {
                        gananciasAnterior = reader.GetDecimal(0);
                    }
                }
                reader.Close();



                // Calcular el porcentaje de ganancia o pérdida
                if (gananciasAnterior != 0)
                {
                    porcentajeGananciaPerdida = ((gananciasActual - gananciasAnterior) / gananciasAnterior) * 100;
                }



                connection.Close();
            }



            return porcentajeGananciaPerdida;
        }
    }
}
