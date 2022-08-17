using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNDepartamento
    {
        public static List<GNDepartamento> GetDeptoByNombre(string nombre)
        {
            List<GNDepartamento> departamentos = new List<GNDepartamento>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GNDepartamento WHERE NombreDepartamento like '%' + @NombreDepartamento + '%'",conexion.OpenConnection());
                command.Parameters.AddWithValue("NombreDepartamento", nombre);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    departamentos.Add(new GNDepartamento
                    {
                        IntOidGNDepartamento = Convert.ToInt32(reader["OidGNDepartamento"]),
                        StrCodigoDepartamento = reader["CodigoDepartamento"].ToString(),
                        StrNombreDepartamento = reader["NombreDepartamento"].ToString()
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

            return departamentos;
        }
    }
}