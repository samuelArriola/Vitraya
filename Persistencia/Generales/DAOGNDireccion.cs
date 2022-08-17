using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Generales
{
    public class DAOGNDireccion
    {

        public static void SetGNDireccion(GNDireccion direccion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNDireccion] ([NomDir],[CdDir],[Estado],[SiglaDir], Eliminado)"+
                                          "  VALUES(@NomDir, @CdDir,  @Estado, @SiglaDir, 0) select SCOPE_IDENTITY()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@NomDir", direccion.StrNomDir);
                command.Parameters.AddWithValue("@Estado", direccion.StrEstado);
                command.Parameters.AddWithValue("@CdDir", direccion.IntCdDir);
                command.Parameters.AddWithValue("@SiglaDir", direccion.StrSiglaDir);
                
                int OidInstancia =  Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea ladiraccion {direccion.StrNomDir}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNDireccion"
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

        //
        public static GNDireccion GetGNDireccion(int idDireccion)
        {
            GNDireccion direccion = null;
            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("SELECT * FROM GNDireccion WHERE OidGNDir = @OidGNDir and isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGNDir", idDireccion);
                reader = command.ExecuteReader();
                
                if(reader.Read())
                    direccion = new GNDireccion
                    {
                        IntCdDir = Convert.ToInt32(reader["CdDir"].ToString()),
                        IntOidGNDir = Convert.ToInt32(reader["OidGNDir"].ToString()),
                        StrEstado = reader["Estado"].ToString(),
                        StrNomDir = reader["NomDir"].ToString(),
                        StrSiglaDir = reader["SiglaDir"].ToString()
                    };
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return direccion;
        }
        

        public static List<GNDireccion> GetDirecciones()
        {
            List<GNDireccion> direcciones = new List<GNDireccion>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GNDireccion where isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNDireccion direccion = new GNDireccion
                    {
                        IntCdDir = Convert.ToInt32(reader["CdDir"].ToString()),
                        IntOidGNDir = Convert.ToInt32(reader["OidGNDir"].ToString()),
                        StrEstado = reader["Estado"].ToString(),
                        StrNomDir = reader["NomDir"].ToString(),
                        StrSiglaDir = reader["SiglaDir"].ToString()
                    };
                    direcciones.Add(direccion);
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

            return direcciones;
        }
        public static List<GNDireccion> GetDirecciones(string nomDireccion)
        {
            List<GNDireccion> direcciones = new List<GNDireccion>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand($@"SELECT * FROM GNDireccion where NomDir like '%{nomDireccion}%' and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GNDireccion direccion = new GNDireccion
                    {
                        IntCdDir = Convert.ToInt32(reader["CdDir"].ToString()),
                        IntOidGNDir = Convert.ToInt32(reader["OidGNDir"].ToString()),
                        StrEstado = reader["Estado"].ToString(),
                        StrNomDir = reader["NomDir"].ToString(),
                        StrSiglaDir = reader["SiglaDir"].ToString()
                    };
                    direcciones.Add(direccion);
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

            return direcciones;
        }

        public static bool DeleteDireccion(int idDireccion)
        {

            bool isDeleted = true;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GNDireccion WHERE GNDireccion.OidGNDir = @OidDireccion " +
                    " update GNDireccion set Eliminado = 1 WHERE GNDireccion.OidGNDir = @OidDireccion", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidDireccion", idDireccion);
                reader = command.ExecuteReader();

                if (reader.Read()) {
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = idDireccion,
                        intOidGNHistorico = 0,
                        strAccion = "Eliminar",
                        strDetalle = $"Se elimina la dirección {reader["NomDir"].ToString()}",
                        dtmFecha = DateTime.Now,
                        strEntidad = "GNDireccion"
                    });
                }
            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isDeleted;
        }
        public static void UpdateGNDireccion(GNDireccion direccion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[GNDireccion] SET [NomDir] = @NomDir, [CdDir] = @CdDir, "+
                                         "   [Estado] = @Estado,[SiglaDir] = @SiglaDir WHERE OidGNDir = @OidGNDir and isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomDir", direccion.StrNomDir);
                command.Parameters.AddWithValue("@CdDir", direccion.IntCdDir);
                command.Parameters.AddWithValue("@Estado", direccion.StrEstado);
                command.Parameters.AddWithValue("@SiglaDir", direccion.StrSiglaDir);
                command.Parameters.AddWithValue("@OidGNDir", direccion.IntOidGNDir);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = direccion.IntOidGNDir,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se Modifica la dirección {direccion.StrNomDir}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNDireccion"
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
    }

}