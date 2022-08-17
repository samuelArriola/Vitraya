using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOEps
    {

        public static List<Eps> ListaEps()
        {
            List<Eps> ListaEps = new List<Eps>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from Eps", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Eps eps = new Eps
                    {
                        IntGnIdEps = Convert.ToInt32(reader["GnIdEps"].ToString()),
                        StrGnCodEps = reader["GnCodEps"].ToString(),
                        StrGnEstEps = reader["GnEstEps"].ToString(),
                        StrGnNomEps  = reader["GnNomEps"].ToString()
                    };
                    ListaEps.Add(eps);
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

            return ListaEps;
        }
    }
}