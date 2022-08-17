using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;
namespace Persistencia.Generales
{
    public class DAOGNListaArchivos
    {

        public static int set(GNListaArchivos listaArchivos)
        {
            int idListaArchivo = 0;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNListaArchivos] ([OidGNModulo]) VALUES  (@OidGNModulo)", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNModulo", listaArchivos.IntOidGNModulo);
                command.ExecuteNonQuery();
                idListaArchivo = GetUltimo().IntOidGNListaArchivos;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return idListaArchivo;
        }

        public static GNListaArchivos GetUltimo()
        {

            GNListaArchivos listaArchivos = new GNListaArchivos();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT top 1 [OidGNListaArchivos],[OidGNModulo]  FROM[dbo].[GNListaArchivos] order by OidGNListaArchivos desc", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    listaArchivos = new GNListaArchivos
                    {
                        IntOidGNListaArchivos = Convert.ToInt32(reader["OidGNListaArchivos"].ToString())
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

            return listaArchivos;
        }
    }
}