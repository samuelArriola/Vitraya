
using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAODireccion
    {
        public static Direccion GetDireccion(int id)
        {
            Direccion direccion = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT [GnIdArea] ,[GnNomAra] ,[GnCdAra] ,[GnEsAre] " +
                    ",[GnSiglaDr] FROM [dbo].[Area] where GnCdAra = @GnIdArea", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GnIdArea", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    direccion = new Direccion
                    {
                        IntGnCdAra = Convert.ToInt32(reader["GnCdAra"].ToString()),
                        IntGnIdArea = Convert.ToInt32(reader["GnIdArea"].ToString()),
                        StrGnEsAre = reader["GnEsAre"].ToString(),
                        StrGnNomAra = reader["GnNomAra"].ToString(),
                        StrGnSiglaDr = reader["GnSiglaDr"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return direccion;
        }
    }
}