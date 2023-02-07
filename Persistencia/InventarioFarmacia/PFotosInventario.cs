using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.InventarioFarmacia;

namespace Persistencia.InventarioFarmacia
{
    public class PFotosInventario
    {

        public static void GetSetFotoInventario()
        {

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT
                                        INNALMACE.OID AS 'id almacen',
                                        INNALMACE.IALCODIGO as 'cod almacen',
                                        INNALMACE.IALNOMBRE as 'nom almacen',
                                        (Case INNPRODUC.IPRTIPPRO When '0' Then 'NIN' When '1' Then 'SUM'When '2' Then 'MED' End) As 'TP PRODUCTO',
                                        INNPRODUC.OID as 'id producto',
                                        INNPRODUC.IPRBLOQUEO AS 'estado producto',
                                        INNPRODUC.IPRCODIGO as 'cod producto',
                                        INNPRODUC.IPRDESCOR as 'nom producto',
                                        INNLOTSER.OID AS 'id lote',
                                        INNLOTSER.ILSCODIGO as 'cod lote',
                                        INNLOTSER.ILSFECVEN as 'fec vto',
                                        INNFISICO.IFICANTID as 'Cant Sistema'
                                        FROM INNFISICO
                                        INNER JOIN INNPRODUC ON INNPRODUC.OID = INNFISICO.INNPRODUC
                                        INNER JOIN INNLOTSER ON INNLOTSER.OID = INNFISICO.INNLOTSER AND INNPRODUC.OID = INNLOTSER.INNPRODUC
                                        INNER JOIN INNALMACE ON INNALMACE.OID = INNFISICO.INNALMACE";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EFotosInventario1 infoFotosInventario = new EFotosInventario1
                    {

                        FT_Oid_almacen = Convert.ToInt32(reader["id almacen"].ToString()),
                        FT_Oid_producto = Convert.ToInt32(reader["id producto"].ToString()),
                        FT_Oid_lote = Convert.ToInt32(reader["id lote"].ToString()),
                        FT_Estado_producto = Convert.ToBoolean(reader["estado producto"].ToString()),
                        FT_Cod_almacen = reader["cod almacen"].ToString(),
                        FT_Nom_almacen = reader["nom almacen"].ToString(),
                        FT_Tp_producto = reader["TP PRODUCTO"].ToString(),
                        FT_Cod_producto = reader["cod producto"].ToString(),
                        FT_Nom_producto = reader["nom producto"].ToString(),
                        FT_Cod_lote = reader["cod lote"].ToString(),
                        FT_Cant_sistema = Convert.ToDecimal(reader["id almacen"].ToString()),
                        FT_FecVen_lote = Convert.ToDateTime(reader["fec vto"].ToString()),
                        FT_Fecha_foto = DateTime.Now,

                    };
                    SetInventarioVitraya(infoFotosInventario);
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

        }

        public static void SetInventarioVitraya(EFotosInventario1 infoFotosInventario)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO FAInventario (FT_Oid_almacen, FT_Cod_almacen, FT_Nom_almacen, FT_Tp_producto, FT_Oid_producto, FT_Cod_producto, FT_Nom_producto," +
                    "FT_Estado_producto, FT_Oid_lote, FT_Cod_lote, FT_FecVen_lote, FT_Cant_sistema, FT_Fecha_foto) VALUES (@FT_Oid_almacen, @FT_Cod_almacen, @FT_Nom_almacen, @FT_Tp_producto, " +
                    "@FT_Oid_producto, @FT_Cod_producto, @FT_Nom_producto, @FT_Estado_producto, @FT_Oid_lote, @FT_Cod_lote, @FT_FecVen_lote, @FT_Cant_sistema, @FT_Fecha_foto)", conexion.OpenConnection());

                command.Parameters.AddWithValue("FT_Oid_almacen", infoFotosInventario.FT_Oid_almacen);
                command.Parameters.AddWithValue("FT_Cod_almacen", infoFotosInventario.FT_Cod_almacen);
                command.Parameters.AddWithValue("FT_Nom_almacen", infoFotosInventario.FT_Nom_almacen);
                command.Parameters.AddWithValue("FT_Tp_producto", infoFotosInventario.FT_Tp_producto);
                command.Parameters.AddWithValue("FT_Oid_producto", infoFotosInventario.FT_Oid_producto);
                command.Parameters.AddWithValue("FT_Cod_producto", infoFotosInventario.FT_Cod_producto);
                command.Parameters.AddWithValue("FT_Nom_producto", infoFotosInventario.FT_Nom_producto);
                command.Parameters.AddWithValue("FT_Estado_producto", infoFotosInventario.FT_Estado_producto);
                command.Parameters.AddWithValue("FT_Oid_lote", infoFotosInventario.FT_Oid_lote);
                command.Parameters.AddWithValue("FT_Cod_lote", infoFotosInventario.FT_Cod_lote);
                command.Parameters.AddWithValue("FT_FecVen_lote", infoFotosInventario.FT_FecVen_lote);
                command.Parameters.AddWithValue("FT_Cant_sistema", infoFotosInventario.FT_Cant_sistema);
                command.Parameters.AddWithValue("FT_Fecha_foto", infoFotosInventario.FT_Fecha_foto);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

        }

        public static List<EFotosInventario1> GetFotosInvVitraya()
        {

            List<EFotosInventario1> fechasInventario = new List<EFotosInventario1>();

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT DISTINCT convert(VARCHAR, [FT_Fecha_foto], 111) AS FechasFotos FROM FAInventario ORDER BY FechasFotos ASC", conexion.OpenConnection());

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EFotosInventario1 fechaInventario = new EFotosInventario1
                    {
                        FT_Fecha_foto =  Convert.ToDateTime(reader["FechasFotos"].ToString())
                    };
                    fechasInventario.Add(fechaInventario);
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
            return fechasInventario;
        }

        public static List<EFotosInventario1> GetRegistrosFoto(string fechaFoto)
        {

            List<EFotosInventario1> registrosInventario = new List<EFotosInventario1>();

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand($@"SELECT [FT_Cod_almacen]
                                          ,[FT_Nom_almacen]
                                          ,[FT_Tp_producto]
                                          ,[FT_Cod_producto]
                                          ,[FT_Nom_producto]
                                          ,[FT_Estado_producto]
                                          ,[FT_Cod_lote]
                                          ,[FT_FecVen_lote]
                                          ,[FT_Cant_sistema]
                                      FROM [Vitraya].[dbo].[FAInventario]
                                      WHERE convert(VARCHAR, [FT_Fecha_foto], 111) = @FT_Fecha_foto", conexion.OpenConnection());

                command.Parameters.AddWithValue("FT_Fecha_foto", fechaFoto);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EFotosInventario1 registroInventario = new EFotosInventario1
                    {
                        FT_Estado_producto = Convert.ToBoolean(reader["FT_Estado_producto"].ToString()),
                        FT_Cod_almacen = reader["FT_Cod_almacen"].ToString(),
                        FT_Nom_almacen = reader["FT_Nom_almacen"].ToString(),
                        FT_Tp_producto = reader["FT_Tp_producto"].ToString(),
                        FT_Cod_producto = reader["FT_Cod_producto"].ToString(),
                        FT_Nom_producto = reader["FT_Nom_producto"].ToString(),
                        FT_Cod_lote = reader["FT_Cod_lote"].ToString(),
                        FT_Cant_sistema = Convert.ToDecimal(reader["FT_Cant_sistema"].ToString()),
                        FT_FecVen_lote = Convert.ToDateTime(reader["FT_FecVen_lote"].ToString()),
                    };
                    registrosInventario.Add(registroInventario);
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
            return registrosInventario;
        }
    }
}