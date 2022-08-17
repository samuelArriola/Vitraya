using Entidades.Generales;
using Entidades.Procesos;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Procesos
{
    public class DAOSIPOC
    {
        public static void setSipoc(PCSIPOC sipoc)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[SIPOC]" +
                                                            " ([OIdProceso] " +
                                                            " ,[Proveedores] " +
                                                            " ,[Entrada] " +
                                                            " ,[TipoAct] " +
                                                            " ,[Actividad] " +
                                                            " ,[Clientes] " +
                                                            " ,[Responsables] " +
                                                            " ,[Salidas]) " +
                                                            " VALUES " +
                                                            " (@OIdProceso" +
                                                            " ,@Proveedores" +
                                                            " ,@Entrada" +
                                                            " ,@TipoAct" +
                                                            " ,@Actividad" +
                                                            " ,@Clientes" +
                                                            " ,@Responsables" +
                                                            " ,@Salidas) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OIdProceso", sipoc.IntOIdProceso);
                command.Parameters.AddWithValue("@Proveedores", sipoc.StrProveedores);
                command.Parameters.AddWithValue("@Entrada", sipoc.StrEntrada);
                command.Parameters.AddWithValue("@TipoAct", sipoc.StrTipoAct);
                command.Parameters.AddWithValue("@Actividad", sipoc.StrActividad);
                command.Parameters.AddWithValue("@Clientes", sipoc.StrClientes);
                command.Parameters.AddWithValue("@Responsables", sipoc.StrResponsables);
                command.Parameters.AddWithValue("@Salidas", sipoc.StrSalidas);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "SIPOC"
                });

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

        public static List<PCSIPOC> getSipocs(string OIdProceso)
        {
            List<PCSIPOC> sIPOCs = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from SIPOC where OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdProceso", OIdProceso);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                sIPOCs = new List<PCSIPOC>();
                while (reader.Read())
                {
                    PCSIPOC sipoc = new PCSIPOC
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        IntOidSipoc = Convert.ToInt32(reader["OidSipoc"].ToString()),
                        StrProveedores = reader["Proveedores"].ToString(),
                        StrEntrada = reader["Entrada"].ToString(),
                        StrTipoAct = reader["TipoAct"].ToString(),
                        StrActividad = reader["Actividad"].ToString(),
                        StrClientes = reader["Clientes"].ToString(),
                        StrSalidas = reader["Salidas"].ToString(),
                        StrResponsables = reader["Responsables"].ToString()
                    };
                    sIPOCs.Add(sipoc);
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
            return sIPOCs;
        }


        public static void DeleteSIPOCByidProc(int idProceso)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from SIPOC where OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdProceso", idProceso);

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
    }
}