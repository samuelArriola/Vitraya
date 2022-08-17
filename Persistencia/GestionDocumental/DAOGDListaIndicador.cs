using Entidades.GestionDocumental;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.GestionDocumental
{
    public class DAOGDListaIndicador
    {
        public static void  SetListaIndicador(GDListaIndicador listaIndicador)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDListaIndicador]"+
                                         "          ([OIdGDDocumento]"+
                                         "          ,[OIdGDDocIndicador])" +
                                         "    VALUES"+
                                         "          (@OIdGDDocumento"+
                                         "          ,@OIdGDDocIndicador)", conexion.OpenConnection());

                command.Parameters.AddWithValue("OIdGDDocumento", listaIndicador.IntOIdGDDocumento);
                command.Parameters.AddWithValue("OIdGDDocIndicador", listaIndicador.IntOIdGDDocIndicador);
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

        public static List<GDListaIndicador> GetListaIndicadores()
        {
            List<GDListaIndicador> lista = new List<GDListaIndicador>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDListaIndicador", conexion.OpenConnection());

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GDListaIndicador Lista = new GDListaIndicador
                    {
                        IntOIdGDDocIndicador = Convert.ToInt32(reader["OIdGDDocIndicador"]),
                        IntOIdGDDocumento = Convert.ToInt32(reader["OIdGDDocumento"]),
                        IntOidGDListaIndicador = Convert.ToInt32(reader["OidGDListaIndicador"]),
                    };
                    lista.Add(Lista);
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
            return lista;
        }
        public static List<GDListaIndicador> GetListaIndicadores(int IdDocumento)
        {
            List<GDListaIndicador> lista = new List<GDListaIndicador>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDListaIndicador where OIdGDDocumento = @OIdGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OIdGDDocumento", IdDocumento);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new GDListaIndicador
                    {
                        IntOIdGDDocIndicador = Convert.ToInt32(reader["OIdGDDocIndicador"]),
                        IntOIdGDDocumento = Convert.ToInt32(reader["OIdGDDocumento"]),
                        IntOidGDListaIndicador = Convert.ToInt32(reader["OidGDListaIndicador"]),
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
            return lista;
        }

        public static void Delete(int idProcdimiento) 
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from GDListaIndicador where OIdGDDocumento = @OIdGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OIdGDDocumento", idProcdimiento);
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
    }
}