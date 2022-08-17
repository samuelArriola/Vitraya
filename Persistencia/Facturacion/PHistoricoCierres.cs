using Entidades.Facturacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Facturacion
{
    public class PHistoricoCierres
    {

        public static List<EHistoricoCierres> getInfoHistorico()
        {

            List<EHistoricoCierres> infoHistorico = new List<EHistoricoCierres>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM UsuariosCierreAut";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EHistoricoCierres infoHistoricoI = new EHistoricoCierres
                    {
                        NumIngreso = reader["NumIngreso"].ToString(),
                        UsuarioCierre = reader["UsuarioAccion"].ToString(),
                        EstadoCierre = reader["Estado"].ToString(),
                        MotivoCierre = reader["Motivo"].ToString(),
                        FechaCierre = Convert.ToDateTime(reader["Fecha"].ToString())
                    };
                    infoHistorico.Add(infoHistoricoI);
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
            return infoHistorico;
        }

        public static List<EHistoricoCierres> getfiltro1(string numeroIngreso, string nombreCierre)
        {

            List<EHistoricoCierres> infoHistorico = new List<EHistoricoCierres>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM UsuariosCierreAut WHERE NumIngreso LIKE '%'+ @numeroIngreso +'%' AND UsuarioAccion LIKE '%'+ @nombreCierre+'%'";

                command.Parameters.AddWithValue("numeroIngreso", numeroIngreso);
                command.Parameters.AddWithValue("nombreCierre", nombreCierre);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EHistoricoCierres infoHistoricoI = new EHistoricoCierres
                    {
                        NumIngreso = reader["NumIngreso"].ToString(),
                        UsuarioCierre = reader["UsuarioAccion"].ToString(),
                        EstadoCierre = reader["Estado"].ToString(),
                        MotivoCierre = reader["Motivo"].ToString(),
                        FechaCierre = Convert.ToDateTime(reader["Fecha"].ToString())
                    };
                    infoHistorico.Add(infoHistoricoI);
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
            return infoHistorico;
        }

        public static List<EHistoricoCierres> getfiltro2(string fechaI, string fechaF)
        {

            List<EHistoricoCierres> infoHistorico = new List<EHistoricoCierres>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM UsuariosCierreAut WHERE FORMAT(Fecha, 'yyyy-MM-dd') >= @fechaI AND FORMAT(Fecha, 'yyyy-MM-dd') <= @fechaF";

                command.Parameters.AddWithValue("fechaI", fechaI);
                command.Parameters.AddWithValue("fechaF", fechaF);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EHistoricoCierres infoHistoricoI = new EHistoricoCierres
                    {
                        NumIngreso = reader["NumIngreso"].ToString(),
                        UsuarioCierre = reader["UsuarioAccion"].ToString(),
                        EstadoCierre = reader["Estado"].ToString(),
                        MotivoCierre = reader["Motivo"].ToString(),
                        FechaCierre = Convert.ToDateTime(reader["Fecha"].ToString())
                    };
                    infoHistorico.Add(infoHistoricoI);
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
            return infoHistorico;
        }

    }
}