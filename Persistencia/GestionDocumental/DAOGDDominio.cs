using Entidades.Generales;
using Entidades.GestionDocumental;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.GestionDocumental
{
    public class DAOGDDominio
    {
        public static void SetGDDominio(GDDominio dominio)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("insert into [GDDominio] (Nombre, Eliminado) values (@Nombre, 0) SELECT scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Nombre", dominio.StrNombre);
                int OidInstancia = Convert.ToInt32( command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea El dominio {dominio.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDominio"
                });
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
        public static void UpdateGDDominio(GDDominio dominio)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update   [GDDominio] set Nombre = @Nombre where OidGDDominio = @OidGDDominio", conexion.OpenConnection());
                command.Parameters.AddWithValue("@Nombre", dominio.StrNombre);
                command.Parameters.AddWithValue("@OidGDDominio", dominio.IntOidGDDominio);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = dominio.IntOidGDDominio,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actuliza el nombre del dominio {dominio.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDominio"
                });
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

        public static void DeleteDominio(int idDominio)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("update  [GDDominio] set Eliminado = 1  where OidGDDominio = @OidGDDominio", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDominio",idDominio);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idDominio,
                    intOidGNHistorico = 0,
                    strAccion = "Eliminar",
                    strDetalle = $"Se Elimina el dominio con el código {idDominio}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDocumento"
                });
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
        public static List<GDDominio> GetGDDominios()
        {
            List<GDDominio> dominios = new List<GDDominio>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDominio where isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GDDominio dominio = new GDDominio
                    {
                        IntOidGDDominio = Convert.ToInt32(reader["OidGDDominio"].ToString()),
                        StrNombre = reader["Nombre"].ToString(),
                    };
                    dominios.Add(dominio);
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
            return dominios;
        }

        public static GDDominio GetGDDominio(int idDominio)
        {
            GDDominio dominio = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDominio where OidGDDominio = @OidGDDominio and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDominio", idDominio);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dominio = new GDDominio
                    {
                        IntOidGDDominio = Convert.ToInt32(reader["OidGDDominio"].ToString()),
                        StrNombre = reader["Nombre"].ToString(),
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
            return dominio;
        }
    }
}