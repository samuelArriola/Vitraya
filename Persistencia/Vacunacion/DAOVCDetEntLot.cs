using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAOVCDetEntLot
    {
        public static int SerDetEntLot(VCDetEntLot DetalleEntradaLote)
        {
            int idDetalleEntradaLote = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[VCDetEntLot]
                                                   ([FechaEntrada]
                                                   ,[FechaRegistro]
                                                   ,[OidUsuario])
                                             VALUES
                                                   (@FechaEntrada
                                                   ,@FechaRegistro
                                                   ,@OidUsuario)
                                            SELECT CAST(SCOPE_IDENTITY() AS INT)", conexion.OpenConnection());

                command.Parameters.AddWithValue("FechaEntrada", DetalleEntradaLote.DtmFechaEntrada);
                command.Parameters.AddWithValue("FechaRegistro", DetalleEntradaLote.DtmFechaRegistro);
                command.Parameters.AddWithValue("OidUsuario", DetalleEntradaLote.IntOidUsuario);

                idDetalleEntradaLote = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return idDetalleEntradaLote;
        }

        public static VCDetEntLot GetDetalleEntradaLote(int idDetalle)
        {
            VCDetEntLot Detalle = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM VCDetEntLot WHERE OidVCDetEntLot = @OidVCDetEntLot", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidVCDetEntLot", idDetalle);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Detalle = new VCDetEntLot
                    {
                        DtmFechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                        DtmFechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        IntOidUsuario = Convert.ToInt32(reader["OidUsuario"]),
                        IntOidVCDetEntLot = Convert.ToInt32(reader["OidVCDetEntLot"]),
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

            return Detalle;
        }

        public static List<VCDetEntLot> GetDetallesEntradas()
        {
            List<VCDetEntLot> detalles = new List<VCDetEntLot>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("SELECT * FROM VCDetEntLot", conexion.OpenConnection());

                reader = command.ExecuteReader();
                {
                    detalles.Add(new VCDetEntLot
                    {
                        DtmFechaEntrada = Convert.ToDateTime(reader["FechaEntrada"]),
                        DtmFechaRegistro = Convert.ToDateTime(reader["FechaRegistro"]),
                        IntOidUsuario = Convert.ToInt32(reader["OidUsuario"]),
                        IntOidVCDetEntLot = Convert.ToInt32(reader["OidVCDetEntLot"])
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

            return detalles;
        }
    }
}