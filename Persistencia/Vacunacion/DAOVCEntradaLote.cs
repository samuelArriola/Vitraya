using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAOVCEntradaLote
    {
        public static int SetEntradaLote(VCEntradaLote entradaLote)
        {
            int idEntradaLote = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[VCEntradaLote]
                                                   ([Cantidad]
                                                   ,[OidVCDetEntLot]
                                                   ,[OidVCLote])
                                             VALUES
                                                   (@Cantidad
                                                   ,@OidVCDetEntLot
                                                   ,@OidVCLote)
                                            SELECT CAST(SCOPE_IDENTITY() AS INT)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Cantidad", entradaLote.FltCantidad);
                command.Parameters.AddWithValue("OidVCDetEntLot", entradaLote.IntOidVCDetEntLot);
                command.Parameters.AddWithValue("OidVCLote", entradaLote.IntOidVCLote);

                idEntradaLote = (int)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return idEntradaLote;
        }

        public static VCEntradaLote GetEntradaLote(int idEntrada)
        {
            VCEntradaLote entradaLote = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM VCEntradaLote WHERE OidVCEntradaLote = @OidVCEntradaLote", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidVCEntradaLote", idEntrada);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    entradaLote = new VCEntradaLote
                    {
                        FltCantidad = float.Parse(reader["Cantidad"].ToString()),
                        IntOidVCDetEntLot = Convert.ToInt32(reader["OidVCDetEntLot"]),
                        IntOidVCEntradaLote = Convert.ToInt32(reader["OidVCEntradaLote"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
                        
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
            return entradaLote;
        }
        public static List<VCEntradaLote> GetEntradasLotes() {
            List<VCEntradaLote> entradas = new List<VCEntradaLote>();
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM VCEntradaLote", conexion.OpenConnection());
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    entradas.Add(new VCEntradaLote
                    {
                        FltCantidad = float.Parse(reader["Cantidad"].ToString()),
                        IntOidVCDetEntLot = Convert.ToInt32(reader["OidVCDetEntLot"]),
                        IntOidVCEntradaLote = Convert.ToInt32(reader["OidVCEntradaLote"]),
                        IntOidVCLote = Convert.ToInt32(reader["OidVCLote"]),
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

            return entradas;
        }
    }
}