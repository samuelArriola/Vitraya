using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAOVCInsumo
    {
        public static VCInsumo GetInsumo(int idInsumo)
        {
            VCInsumo insumo = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from VCInsumo where OidVCInsumo = @OidVCInsumo", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidVCInsumo", idInsumo);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    insumo = new VCInsumo
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        StrNombre = reader["Nombre"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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
            return insumo;
        }

        public static List<VCInsumo> GetInsumos()
        {
            List<VCInsumo> insumos = new List<VCInsumo>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from VCInsumo", conexion.OpenConnection());

                
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    insumos.Add(new VCInsumo
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        StrNombre = reader["Nombre"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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

            return insumos;
        }

        public static List<VCInsumo> GetInsumos(string tipo)
        {
            List<VCInsumo> insumos = new List<VCInsumo>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from VCInsumo where Tipo = @Tipo", conexion.OpenConnection());
                command.Parameters.AddWithValue("Tipo", tipo);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    insumos.Add(new VCInsumo
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        StrNombre = reader["Nombre"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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

            return insumos;
        }
        public static List<VCInsumo> GetInsumosPorNombre(string nombre)
        {
            List<VCInsumo> insumos = new List<VCInsumo>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from VCInsumo where Nombre LIKE '%' + @Nombre +'%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("Nombre", nombre);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    insumos.Add(new VCInsumo
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        StrNombre = reader["Nombre"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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

            return insumos;
        }
        public static List<VCInsumo> GetInsumosPorTipo(string tipo)
        {
            List<VCInsumo> insumos = new List<VCInsumo>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from VCInsumo where Tipo = @Tipo", conexion.OpenConnection());
                command.Parameters.AddWithValue("Tipo", tipo);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    insumos.Add(new VCInsumo
                    {
                        IntOidVCInsumo = Convert.ToInt32(reader["OidVCInsumo"]),
                        StrNombre = reader["Nombre"].ToString(),
                        StrTipo = reader["Tipo"].ToString()
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

            return insumos;

        }
    }
}