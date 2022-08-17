using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace  Persistencia.proceedings
{
    public class DAOARAgenda
    {

        private static DAOARAgenda DAOAgenda;

        private DAOARAgenda() { }

        public  static DAOARAgenda GetInstance()
        {
            if (DAOAgenda == null) DAOAgenda = new DAOARAgenda();

            return DAOAgenda;
        }

        public static void Set(ARAgenda data)
        {
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[ARAgenda]"+
                                                   "([nombre],"+
                                                   "[OidAReunionC]," +
                                                   "[Eliminado]," +
                                                   "[Posicion])" +
                                            " VALUES"+
                                                   "(@nombre,"+
                                                   "@OidAReunionC," +
                                                   "0," +
                                                   "(select isnull(MAX(Posicion), 0)  +1 from [ARAgenda] where [OidAReunionC] =  @OidAReunionC ))" +
                                                   " select scope_identity() instancia, NomReunion from AReunionC where OidAReunionC = @OidAReunionC", conexion.OpenConnection());

                command.Parameters.AddWithValue("@nombre", data.Nombre);
                command.Parameters.AddWithValue("@OidAReunionC", data.OidARreunionC);

                reader = command.ExecuteReader();

                reader.Read();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(reader["instancia"]),
                    strAccion = "Crear",
                    strDetalle = $"Se agrega el tema {data.Nombre} a la agenda de la reunion  {reader["NomReunion"].ToString()}",
                    strEntidad = "ARAgenda"
                });

            }
            catch (SqlException ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public List<ARAgenda> listar(int id)
        {
            List<ARAgenda> agenda = null;
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;

            try
            {

                command = new SqlCommand("SELECT *  FROM ARAgenda where OidAReunionC  = @id and isnull(Eliminado, 0) = 0 order by Posicion", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id",id);
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();

                agenda = new List<ARAgenda>();

                while (reader.Read())
                {
                    ARAgenda tema = new ARAgenda();
                    tema.Nombre = reader["nombre"].ToString();
                    tema.OidARreunionC = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    tema.OidARAgenda = Convert.ToInt32(reader["OidARAgenda"].ToString());
                    tema.Posicion = Convert.ToInt32(reader["Posicion"].ToString());
                    agenda.Add(tema);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return agenda;
        }
        
        public void delete(int id)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[ARAgenda] SET Eliminado = 1  WHERE OidARAgenda = @id", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = id,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "ARAgenda"
                });
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
        public static void update(ARAgenda agenda)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[ARAgenda] "+
                                           "SET[nombre] = @tema, " +
                                           "[Posicion] = @Posicion "+
                                         "WHERE OidARAgenda = @id",conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", agenda.OidARAgenda);
                command.Parameters.AddWithValue("@tema", agenda.Nombre);
                command.Parameters.AddWithValue("@Posicion", agenda.Posicion);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = agenda.OidARAgenda,
                    strAccion = "Modificar",
                    strDetalle = $"",
                    strEntidad = "ARAgenda"
                });
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static ARAgenda GetAgenda(int idAgenda)
        {
            ARAgenda tema = null;
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;

            try
            {

                command = new SqlCommand("SELECT *  FROM ARAgenda where OidARAgenda = @id and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", idAgenda);
                command.ExecuteNonQuery();

                reader = command.ExecuteReader();

               

                if (reader.Read())
                {
                    tema = new ARAgenda();
                    tema.Nombre = reader["nombre"].ToString();
                    tema.OidARreunionC = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    tema.OidARAgenda = Convert.ToInt32(reader["OidARAgenda"].ToString());
                    tema.Posicion = Convert.ToInt32(reader["Posicion"].ToString());
                    
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return tema;
        }
    }
}