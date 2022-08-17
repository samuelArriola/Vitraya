using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNCtrlCambioPass
    {
        public static void UpdateCtrlCambioPass(string IdCambioPass)
        {
            Conexion conexion = new Conexion();
            SqlCommand command;

            try
            {
                command = new SqlCommand("update GNCtrlCambioPass set Cambiada = 1 where OidGNCtrlCambioPass = @OidGNCtrlCambioPass", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGNCtrlCambioPass", IdCambioPass);
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
        public static GNCtrlCambioPass GetCtrlCambioPass(string uid)
        {
            GNCtrlCambioPass cambioPass = null;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GNCtrlCambioPass where OidGNCtrlCambioPass = @OidGNCtrlCambioPass", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGNCtrlCambioPass", uid);
               
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cambioPass = new GNCtrlCambioPass
                        {
                            BlnCambiada = Convert.ToBoolean(reader["Cambiada"]),
                            DblGNCodUSu = Convert.ToDouble(reader["GNCodUSu"]),
                            DtmFecha = DateTime.Parse(reader["Fecha"].ToString()),
                            StrOidGNCtrlCambioPass = reader["OidGNCtrlCambioPass"].ToString()
                        };
                    }
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
            return cambioPass;
        }

        public static string SetCtrlCambioPass(GNCtrlCambioPass cambioPass)
        {
            Conexion conexion = new Conexion();
            SqlCommand command;

            try
            {
                command = new SqlCommand(@"declare @uuid uniqueidentifier =  newid()
                                            INSERT INTO [dbo].[GNCtrlCambioPass]
                                                       (OidGNCtrlCambioPass
		                                               ,[GNCodUSu]
                                                       ,[Cambiada]
                                                       ,[Fecha])
                                                 VALUES
                                                       (@uuid
		                                               ,@GNCodUSu
                                                       ,@Cambiada
                                                       ,@Fecha)
                                            select @uuid", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNCodUSu", cambioPass.DblGNCodUSu);
                command.Parameters.AddWithValue("Cambiada", cambioPass.BlnCambiada);
                command.Parameters.AddWithValue("Fecha", cambioPass.DtmFecha);

                return command.ExecuteScalar().ToString();
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}