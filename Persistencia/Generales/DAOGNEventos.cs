using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;

namespace Persistencia.Generales
{
    public class DAOGNEventos
    {

        public static void set(GNEventos data)
        {
            Conexion conexion = new Conexion();
            SqlCommand command;

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNEventos] "+
                                               "([OidGNModulo]"+
                                               ",[FechaInicio]"+
                                               ",[FechaFinal]"+
                                               ",[Lugar]"+
                                               ",[Contenido]" +
                                               ",[OidCronograma])" +
                                        " VALUES "+
                                               "(@OidGNModulo,"+
                                               "@FechaInicio,"+
                                               "@FechaFinal,"+
                                               "@Lugar,"+
                                               "@Contenido," +
                                               "@OidCronograma)", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGNModulo", data.IntOidGNModulo);
                command.Parameters.AddWithValue("@FechaInicio", data.DtmFechaInicio);
                command.Parameters.AddWithValue("@FechaFinal", data.DtmFechaFinal);
                command.Parameters.AddWithValue("@Lugar", data.StrLugar);
                command.Parameters.AddWithValue("@Contenido", data.StrContenido);
                command.Parameters.AddWithValue("@OidCronograma", data.IntOidCronograma);
                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

        }
        public static List<GNEventos> GetEventos(int id)
        {
            List<GNEventos> eventos = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  [OidGNEvento]"+
                                              ",[OidGNModulo]"+
                                              ",[FechaInicio]"+
                                              ",[FechaFinal]"+
                                              ",[Lugar]"+
                                              ",[Contenido]" +
                                              ",[OidCronograma]" +
                                          " FROM[dbo].[GNEventos] WHERE OidGNModulo = @OidGNModulo", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNModulo", id);
                reader = command.ExecuteReader();

                eventos = new List<GNEventos>();
                while (reader.Read())
                {
                    GNEventos evento = new GNEventos();

                    evento.DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"].ToString());
                    evento.DtmFechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                    evento.IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString());
                    evento.IntOidGNEvento = Convert.ToInt32(reader["OidGNEvento"].ToString());
                    evento.StrContenido = reader["Contenido"].ToString();
                    evento.StrLugar = reader["Lugar"].ToString();
                    evento.IntOidCronograma = Convert.ToInt32(reader["OidCronograma"].ToString());

                    eventos.Add(evento);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return eventos;
        }

        public static GNEventos GetUltimo()
        {
            GNEventos evento = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select top 1 * from GNEventos order by OidGNEvento", conexion.OpenConnection());
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    evento = new GNEventos();
                    evento.DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"].ToString());
                    evento.DtmFechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                    evento.IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString());
                    evento.IntOidGNEvento = Convert.ToInt32(reader["OidGNEvento"].ToString());
                    evento.StrContenido = reader["Contenido"].ToString();
                    evento.StrLugar = reader["Lugar"].ToString();
                    evento.IntOidCronograma = Convert.ToInt32(reader["OidCronograma"].ToString());
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return evento;
        }

        public static GNEventos Get(int id)
        {
            GNEventos evento = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNEventos where OidGNEvento = @OidGNEvento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNEvento", id);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    evento = new GNEventos();
                    evento.DtmFechaFinal = Convert.ToDateTime(reader["FechaFinal"].ToString());
                    evento.DtmFechaInicio = Convert.ToDateTime(reader["FechaInicio"].ToString());
                    evento.IntOidGNModulo = Convert.ToInt32(reader["OidGNModulo"].ToString());
                    evento.IntOidGNEvento = Convert.ToInt32(reader["OidGNEvento"].ToString());
                    evento.IntOidCronograma = Convert.ToInt32(reader["OidCronograma"].ToString());
                    evento.StrContenido = reader["Contenido"].ToString();
                    evento.StrLugar = reader["Lugar"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return evento;
        }

        public static GNEventos update(GNEventos evento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GNEventos]"+
                                           "SET [FechaInicio] =  @FechaInicio"+
                                              ",[FechaFinal] =  @FechaFinal"+
                                              ",[Lugar] = @Lugar"+
                                              ",[Contenido] =  @Contenido"+
                                              ",[OidCronograma] =  @OidCronograma" +
                                        " WHERE OidGNEvento = @OidGNEvento ", conexion.OpenConnection());

                command.Parameters.AddWithValue("@FechaInicio", evento.DtmFechaInicio);
                command.Parameters.AddWithValue("@FechaFinal", evento.DtmFechaFinal);
                command.Parameters.AddWithValue("@Lugar", evento.StrLugar);
                command.Parameters.AddWithValue("@Contenido", evento.StrContenido);
                command.Parameters.AddWithValue("@OidGNEvento", evento.IntOidGNEvento);
                command.Parameters.AddWithValue("@OidCronograma", evento.IntOidCronograma);
                command.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                conexion.CloseConnection();
            }

            return Get(evento.IntOidGNEvento);

        }

        public static void delete(int id)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("delete from GNEventos where OidGNEvento = @id", conexion.OpenConnection());

                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}