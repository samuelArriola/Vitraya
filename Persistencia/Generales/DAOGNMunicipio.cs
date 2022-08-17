using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNMunicipio
    {
        public static List<GNMunicipio> GetMunicipiosByNombreDepto(string nombreDepartamento, string nombreMunicipio)
        {
            List<GNMunicipio> municipios = new List<GNMunicipio>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"select M.* from GNMunicipio M
	                                            inner join GNDepartamento D on D.OidGNDepartamento = M.OidGNDepartamento
                                            where M.NombreMunicipio like '%' + @NombreMunicipio + '%' and D.NombreDepartamento = @NombreDepartamento", conexion.OpenConnection());

                command.Parameters.AddWithValue("NombreMunicipio", nombreMunicipio);
                command.Parameters.AddWithValue("NombreDepartamento", nombreDepartamento);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    municipios.Add(new GNMunicipio { 
                        IntOidGNDepartamento = Convert.ToInt32(reader["OidGNDepartamento"]),
                        IntOidGNMunicipio = Convert.ToInt32(reader["OidGNMunicipio"]),
                        StrCodigoDeptoMunicipio = reader["CodigoDeptoMunicipio"].ToString(),
                        StrCodigoMunicipio = reader["CodigoMunicipio"].ToString(),
                        StrNombreMunicipio   = reader["NombreMunicipio"].ToString(),
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

            return municipios;
        }
    }
}