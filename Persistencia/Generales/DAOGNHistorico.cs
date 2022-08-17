using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNHistorico
    {
        public static void SetHistorico(GNHistorico historico)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"insert into GNHistorico (GNCodUsu, Accion, Entidad, Instancia, Detalle, Fecha)
                                            values(@GNCodUsu, @Accion, @Entidad, @Instancia, @Detalle, @Fecha)", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUsu", historico.intGNCodUsu);
                command.Parameters.AddWithValue("Accion", historico.strAccion);
                command.Parameters.AddWithValue("Entidad", historico.strEntidad);
                command.Parameters.AddWithValue("Instancia", historico.intInstancia);
                command.Parameters.AddWithValue("Detalle", historico.strDetalle);
                command.Parameters.AddWithValue("Fecha", historico.dtmFecha);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally {
                conexion.CloseConnection();
            }
        }

        public static List<GNHistorico> GetHistoricos()
        {
            List<GNHistorico> historicos = new List<GNHistorico>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT * FROM GNHistorico", conexion.OpenConnection());

                reader = command.ExecuteReader();
                while (reader.Read()) {
                    historicos.Add(new GNHistorico { 
                        intOidGNHistorico = Convert.ToInt32(reader["OidGNHistorico"]),
                        intGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        intInstancia = Convert.ToInt32(reader["Instancia"]),
                        strAccion = reader["Accion"].ToString(),
                        strDetalle = reader["Detalle"].ToString(),
                        strEntidad   = reader["Entidad"].ToString(),
                        dtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    });
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

        public static GNHistorico GetHistorico(int idHistorico)
        {
            GNHistorico historico = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GNHistorico WHERE OidGNHistorico = @OidGNHistorico", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidGNHistorico", idHistorico);

                reader = command.ExecuteReader();
                while (reader.Read()) {
                    historico = new GNHistorico
                    {
                        intOidGNHistorico = Convert.ToInt32(reader["OidGNHistorico"]),
                        intGNCodUsu = Convert.ToInt32(reader["GNCodUsu"]),
                        intInstancia = Convert.ToInt32(reader["Instancia"]),
                        strAccion = reader["Accion"].ToString(),
                        strDetalle = reader["Detalle"].ToString(),
                        strEntidad = reader["Entidad"].ToString(),
                        dtmFecha = Convert.ToDateTime(reader["Fecha"].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally {
                conexion.CloseConnection();
                
            }
            return historico;
        }
    }
}