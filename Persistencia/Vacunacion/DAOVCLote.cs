using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAOVCLote
    {
        public static int SetLote(VCLote lote)
        {
            int idLote = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand(@" INSERT INTO [dbo].[VCLote]
                                                   ([OidVCInsumo]
                                                   ,[NumLote]
                                                   ,[Diluyente]
                                                   ,[TotalIngresado])
                                             VALUES
                                                   (@OidVCInsumo
                                                   ,@NumLote
                                                   ,@Diluyente
                                                   ,@TotalIngresado)
                                             select CAST(SCOPE_IDENTITY() as int)", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidVCInsumo", lote.IntOidVCInsumo);
                command.Parameters.AddWithValue("NumLote", lote.StrNumLote);
                command.Parameters.AddWithValue("TotalIngresado", lote.IntTotalIngresado);
                command.Parameters.AddWithValue("Diluyente", lote.StrDiluyente);

                idLote = (int)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return idLote;
        }

        public static VCLote GetLote(int idLote)
        {
            VCLote lote = null;
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select  L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote ) Existencias, L.*
                                            from VCLote L WHERE L.OidVCLote = @OidVCLote", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidVCLote", idLote);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lote = new VCLote
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        IntTotalIngresado = Convert.ToInt32(reader["TotalIngresado"]),
                        IntExistencias = Convert.ToInt32(reader["Existencias"]),
                        StrNumLote = reader["NumLote"].ToString(),
                        StrDiluyente = reader["Diluyente"].ToString(),
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

            return lote;
        }

        public static VCLote GetLote(int idInsumo, string numLote)
        {
            VCLote lote = null;
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select  L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote) Existencias, L.*
                                            from VCLote L where L.NumLote = @NumLote and L.OidVCInsumo = @OidVCInsumo", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidVCInsumo", idInsumo);
                command.Parameters.AddWithValue("NumLote", numLote);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lote = new VCLote
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        IntTotalIngresado = Convert.ToInt32(reader["TotalIngresado"]),
                        StrNumLote = reader["NumLote"].ToString(),
                        StrDiluyente = reader["Diluyente"].ToString(),
                        IntExistencias = Convert.ToInt32(reader["Existencias"])
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

            return lote;
        }
        public static List<VCLote> GetLotes()
        {
            List<VCLote> lotes = new List<VCLote>();
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select  L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote) Existencias, L.*
                                            from VCLote L");
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lotes.Add(new VCLote
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        IntTotalIngresado = Convert.ToInt32(reader["TotalIngresado"]),
                        StrNumLote = reader["NumLote"].ToString(),
                        IntExistencias = Convert.ToInt32(reader["Existencias"]),
                        StrDiluyente = reader["Diluyente"].ToString(),
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

            return lotes;
        }

        public static List<dynamic> GetLotes(string nombre)
        {
            List<dynamic> lotes = new List<dynamic> { new { text = "Nuevo lote", value = 0 } };
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select  L.*, i.Nombre from VCLote L 
	                                            inner join VCInsumo  I on I.OidVCInsumo = L.OidVCInsumo
                                            where CONCAT(NumLote,' - ', Nombre) like '%' + @Nombre + '%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("Nombre", nombre);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lotes.Add(new
                    {
                        text = reader["NumLote"].ToString() + " - " + reader["NOmbre"],
                        value = reader["OidVCLote"].ToString()
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

            return lotes;
        }

        public static dynamic GetLoteWidthInfoInsumo(int idLote)
        {
            dynamic lote = new { };

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote) Existencias, * from VCLote L
	                                            inner join VCInsumo I on I.OidVCInsumo = L.OidVCInsumo
                                            where l.OidVCLote = @OidVCLote", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidVCLote", idLote);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lote = new
                    {
                        Tipo = reader["Tipo"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        NumLote = reader["NumLote"].ToString(),
                        Existencias = Convert.ToInt32(reader["Existencias"]),
                        OidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        OidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        Diluyente = reader["Diluyente"].ToString(),
                        TotalIngresado = Convert.ToInt32(reader["TotalIngresado"])
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


            return lote;
        }

        public static void updateLote(VCLote lote)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"UPDATE [dbo].[VCLote]
                                               SET [OidVCInsumo] = @OidVCInsumo
                                                  ,[NumLote] = @NumLote
                                                  ,[Diluyente] = @Diluyente
                                                  ,[TotalIngresado] = @TotalIngresado
                                             WHERE OidVCLote = @OidVCLote", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidVCInsumo", lote.IntOidVCInsumo);
                command.Parameters.AddWithValue("NumLote", lote.StrNumLote);
                command.Parameters.AddWithValue("TotalIngresado", lote.IntTotalIngresado);
                command.Parameters.AddWithValue("OidVCLote", lote.IntOidVCLote);
                command.Parameters.AddWithValue("Diluyente", lote.StrDiluyente);
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

        public static List<VCLote> GetLotes(int idInsumo)
        {
            List<VCLote> lotes = new List<VCLote>();
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select  L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote) Existencias, L.*
                                            from VCLote L WHERE OidVCInsumo = @OidVCInsumo and L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico  = L.OidVCLote) > 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidVCInsumo", idInsumo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lotes.Add(new VCLote
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        IntTotalIngresado = Convert.ToInt32(reader["TotalIngresado"]),
                        StrNumLote = reader["NumLote"].ToString(),
                        StrDiluyente = reader["Diluyente"].ToString(),
                        IntExistencias = Convert.ToInt32(reader["Existencias"])
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

            return lotes;
        }

        public static List<VCLote> GetLotesByIdInsumo(string idInsumo)
        {
            List<VCLote> lotes = new List<VCLote>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select  L.TotalIngresado - (select COUNT(*) from RegistroDiarioVac where OidVCLoteBiologico = L.OidVCLote or OidVCLoteJeringa = L.OidVCLote) Existencias, L.*, I.Nombre
                                            from VCLote L
                                                INNER JOIN VCInsumo I ON I. OidVCInsumo = L.OidVCInsumo
                                            WHERE L.OidVCInsumo like '%' + @OidVCInsumo + '%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidVCInsumo", idInsumo);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lotes.Add(new VCLote {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        IntTotalIngresado = Convert.ToInt32(reader["TotalIngresado"]),
                        StrNumLote = reader["NumLote"].ToString(),
                        StrDiluyente = reader["Diluyente"].ToString(),
                        IntExistencias = Convert.ToInt32(reader["Existencias"]),
                        StrNombreInsumo = reader["Nombre"].ToString()
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

            return lotes;
        }
    }
}