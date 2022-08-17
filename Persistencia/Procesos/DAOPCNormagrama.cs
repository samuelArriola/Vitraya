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
    public class DAOPCNormagrama
    {

        public static PCNormagrama GetNormagrama(int idNormagrama)
        {
            PCNormagrama normagrama = null;

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCNormagrama where OidPCNormagrama = @OidPCNormagrama", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidPCNormagrama", idNormagrama);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    normagrama = new PCNormagrama
                    {
                        DtmFecEmision = Convert.ToDateTime(reader["FecEmision"].ToString()),
                        IntNumNorma = Convert.ToInt32(reader["NumNorma"].ToString()),
                        IntOidPCNormagrama = Convert.ToInt32(reader["OidPCNormagrama"].ToString()),
                        StrDescripcion = reader["Descripcion"].ToString(),
                        StrEmision = reader["Emision"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrUrl = reader["Url"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                    };
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

            return normagrama;
        }

        public static void SetNormagrama(PCNormagrama normagrama)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[PCNormagrama]"+
                                         "          ([NumNorma]"+
                                         "          ,[FecEmision]"+
                                         "          ,[Emision]"+
                                         "          ,[Descripcion]"+
                                         "          ,[Estado]"+
                                         "          ,[Url]" +
                                         "          ,[Tipo])"+
                                         "    VALUES"+
                                         "          (@NumNorma"+
                                         "          ,@FecEmision"+
                                         "          ,@Emision"+
                                         "          ,@Descripcion"+
                                         "          ,@Estado"+
                                         "          ,@Url" +
                                         "          ,@Tipo) select scope_indentity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@NumNorma", normagrama.IntNumNorma);
                command.Parameters.AddWithValue("@FecEmision", normagrama.DtmFecEmision);
                command.Parameters.AddWithValue("@Emision", normagrama.StrEmision);
                command.Parameters.AddWithValue("@Descripcion", normagrama.StrDescripcion);
                command.Parameters.AddWithValue("@Estado", normagrama.StrEstado);
                command.Parameters.AddWithValue("@Url", normagrama.StrUrl);
                command.Parameters.AddWithValue("@Tipo", normagrama.StrTipo);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"",
                    strEntidad = "PCNormagrama"
                });
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

           
        }

        public static List<PCNormagrama> GetNormagramas()
        {
            List<PCNormagrama> normagramas = new List<PCNormagrama>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from PCNormagrama", conexion.OpenConnection());
                
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    PCNormagrama normagrama = new PCNormagrama
                    {
                        DtmFecEmision = Convert.ToDateTime(reader["FecEmision"].ToString()),
                        IntNumNorma = Convert.ToInt32(reader["NumNorma"].ToString()),
                        IntOidPCNormagrama = Convert.ToInt32(reader["OidPCNormagrama"].ToString()),
                        StrDescripcion = reader["Descripcion"].ToString(),
                        StrEmision = reader["Emision"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrUrl = reader["Url"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),

                    };
                    normagramas.Add(normagrama);
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

            return normagramas;
        }

        public static void UpdateNormagrama(PCNormagrama normagrama)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[PCNormagrama]"+
                                         "      SET[NumNorma] = @NumNorma"+
                                         "         ,[FecEmision] = @FecEmision"+
                                         "         ,[Emision] = @Emision"+
                                         "         ,[Descripcion] = @Descripcion"+
                                         "         ,[Estado] = @Estado"+
                                         "         ,[Url] = @Url" +
                                         "         ,[Tipo] = @Tipo" +
                                         "    WHERE OidPCNormagrama = @OidPCNormagrama", conexion.OpenConnection());

                command.Parameters.AddWithValue("@NumNorma", normagrama.IntNumNorma);
                command.Parameters.AddWithValue(",@FecEmision", normagrama.DtmFecEmision);
                command.Parameters.AddWithValue("@Emision", normagrama.StrEmision);
                command.Parameters.AddWithValue("@Descripcion", normagrama.StrDescripcion);
                command.Parameters.AddWithValue("@Estado", normagrama.StrEstado);
                command.Parameters.AddWithValue("@Url", normagrama.StrUrl);
                command.Parameters.AddWithValue("@Tipo", normagrama.StrTipo);
                command.Parameters.AddWithValue("@OidPCNormagrama", normagrama.IntOidPCNormagrama);

                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = normagrama.IntOidPCNormagrama,
                    strAccion = "Modificar",
                    strDetalle = $"",
                    strEntidad = "PCNormagrama"
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
    }
}