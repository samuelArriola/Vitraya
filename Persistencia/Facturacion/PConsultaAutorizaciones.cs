using Entidades.Facturacion;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Facturacion
{
    public class PConsultaAutorizaciones
    {

        public static List<EConsultaAutorizaciones> GetHistorico()
        {

            List<EConsultaAutorizaciones> historicos = new List<EConsultaAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM HValidacionAutorizaciones ORDER BY Fecha DESC", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    EConsultaAutorizaciones historico = new EConsultaAutorizaciones
                    {
                        IntIdUsuario = Convert.ToInt32(reader["OidUsuario"].ToString()),
                        StrNombreUsuario = reader["Nombre"].ToString(),
                        StrNumeroAut = reader["NumeroAutorizacion"].ToString(),
                        StrEstadoAut = reader["EstadoAutorizacion"].ToString(),
                        DtfechaConsulta = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return historicos;
        }

        public static List<EConsultaAutorizaciones> ConsultaAut(string numeroAutorizacion)
        {

            List<EConsultaAutorizaciones> Autorizaciones = new List<EConsultaAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT SLNSERPRO.SERAUTORI AS AUTORIZACION FROM ADNINGRESO
                    INNER JOIN SLNSERPRO ON ADNINGRESO.OID = SLNSERPRO.ADNINGRES1 WHERE SLNSERPRO.SERAUTORI = @numeroAutorizacion
                    GROUP BY SLNSERPRO.SERAUTORI UNION SELECT ADNINGRESO.AINNUMAUT AS AUTORIZACION FROM ADNINGRESO
                    INNER JOIN SLNSERPRO ON ADNINGRESO.OID = SLNSERPRO.ADNINGRES1 WHERE SLNSERPRO.SERAUTORI = @numeroAutorizacion
                    GROUP BY ADNINGRESO.AINNUMAUT";

                command.Parameters.AddWithValue("numeroAutorizacion", numeroAutorizacion);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    EConsultaAutorizaciones Autorizacion = new EConsultaAutorizaciones
                    {
                        StrNumeroAut = reader["AUTORIZACION"].ToString(),
                    };
                    Autorizaciones.Add(Autorizacion);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return Autorizaciones;
        }

        public static void SetHistorico(string numeroAutorizacion, string estadoAutorizacion)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            var identificacion = Convert.ToString(usuario.GNCodUsu1);
            var nombre = usuario.GNNomUsu1;
            var fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO HValidacionAutorizaciones (OidUsuario, Nombre, NumeroAutorizacion, EstadoAutorizacion, Fecha) " +
                    "VALUES(@Oid_usuario, @Nombre, @NumeroAutorizacion, @EstadoAutorizacion, @Fecha) ", conexion.OpenConnection());

                command.Parameters.AddWithValue("Oid_usuario", identificacion);
                command.Parameters.AddWithValue("Nombre", nombre);
                command.Parameters.AddWithValue("NumeroAutorizacion", numeroAutorizacion);
                command.Parameters.AddWithValue("EstadoAutorizacion", estadoAutorizacion);
                command.Parameters.AddWithValue("Fecha", fecha);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static List<EConsultaAutorizaciones> getFiltro1(string fecha1, string fecha2)
        {

            List<EConsultaAutorizaciones> historicos = new List<EConsultaAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM HValidacionAutorizaciones WHERE Fecha >= @fechaI AND Fecha < @fechaF ORDER BY Fecha DESC", conexion.OpenConnection());
                command.Parameters.AddWithValue("fechaI", fecha1);
                command.Parameters.AddWithValue("fechaF", fecha2);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    EConsultaAutorizaciones historico = new EConsultaAutorizaciones
                    {
                        IntIdUsuario = Convert.ToInt32(reader["OidUsuario"].ToString()),
                        StrNombreUsuario = reader["Nombre"].ToString(),
                        StrNumeroAut = reader["NumeroAutorizacion"].ToString(),
                        StrEstadoAut = reader["EstadoAutorizacion"].ToString(),
                        DtfechaConsulta = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return historicos;
        }

        public static List<EConsultaAutorizaciones> getFiltro2(string fecha1, string fecha2)
        {

            List<EConsultaAutorizaciones> historicos = new List<EConsultaAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM HValidacionAutorizaciones WHERE Fecha >= @fechaI AND Fecha < @fechaF AND EstadoAutorizacion = 'Repetido' ORDER BY Fecha DESC", conexion.OpenConnection());
                command.Parameters.AddWithValue("fechaI", fecha1);
                command.Parameters.AddWithValue("fechaF", fecha2);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    EConsultaAutorizaciones historico = new EConsultaAutorizaciones
                    {
                        IntIdUsuario = Convert.ToInt32(reader["OidUsuario"].ToString()),
                        StrNombreUsuario = reader["Nombre"].ToString(),
                        StrNumeroAut = reader["NumeroAutorizacion"].ToString(),
                        StrEstadoAut = reader["EstadoAutorizacion"].ToString(),
                        DtfechaConsulta = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return historicos;
        }

        public static List<EConsultaAutorizaciones> getFiltro3(string fecha1, string fecha2)
        {

            List<EConsultaAutorizaciones> historicos = new List<EConsultaAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM HValidacionAutorizaciones WHERE Fecha >= @fechaI AND Fecha < @fechaF AND EstadoAutorizacion = 'No repetido' ORDER BY Fecha DESC", conexion.OpenConnection());
                command.Parameters.AddWithValue("fechaI", fecha1);
                command.Parameters.AddWithValue("fechaF", fecha2);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    EConsultaAutorizaciones historico = new EConsultaAutorizaciones
                    {
                        IntIdUsuario = Convert.ToInt32(reader["OidUsuario"].ToString()),
                        StrNombreUsuario = reader["Nombre"].ToString(),
                        StrNumeroAut = reader["NumeroAutorizacion"].ToString(),
                        StrEstadoAut = reader["EstadoAutorizacion"].ToString(),
                        DtfechaConsulta = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return historicos;
        }
    }
}