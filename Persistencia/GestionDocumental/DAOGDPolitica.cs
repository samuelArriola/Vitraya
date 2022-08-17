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
    public class DAOGDPolitica
    {
        public static void SetPolitica(GDPolitica politica)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDPolitica]"+
                                         "          ([OidGDDocumento]"+
                                         "          ,[Introduccion]"+
                                         "          ,[Objetivos]"+
                                         "          ,[ObjetivosEsp]"+
                                         "          ,[Alcance]"+
                                         "          ,[MarcoLegal]"+
                                         "          ,[Desarrollo]"+
                                         "          ,[Glosario]"+
                                         "          ,[Anexos]"+
                                         "          ,[OidGDProceso]" +
                                         "          ,[Nombre])"+
                                         "    VALUES"+
                                         "          (@OidGDDocumento"+
                                         "          , @Introduccion"+
                                         "          , @Objetivos"+
                                         "          , @ObjetivosEsp"+
                                         "          , @Alcance"+
                                         "          , @MarcoLegal"+
                                         "          , @Desarrollo"+
                                         "          , @Glosario"+
                                         "          , @Anexos"+
                                         "          , @OidGDProceso" +
                                         "          , @Nombre)" +
                                         "  Select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidGDDocumento", politica.IntOidGDDocumento);
                command.Parameters.AddWithValue("Introduccion", politica.StrIntroduccion);
                command.Parameters.AddWithValue("Objetivos", politica.StrObjetivos);
                command.Parameters.AddWithValue("ObjetivosEsp", politica.StrObjetivosEsp);
                command.Parameters.AddWithValue("Alcance", politica.StrAlcance);
                command.Parameters.AddWithValue("MarcoLegal", politica.StrMarcoLegal);
                command.Parameters.AddWithValue("Desarrollo", politica.StrDesarrollo);
                command.Parameters.AddWithValue("Glosario", politica.StrGlosario);
                command.Parameters.AddWithValue("Anexos", politica.StrAnexos);
                command.Parameters.AddWithValue("OidGDProceso", politica.IntOidGDProceso);
                command.Parameters.AddWithValue("Nombre", politica.StrNombre);
                int OidInstancia = Convert.ToInt32( command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el documento tipo politica: {politica.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDPolitica"
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

        public static List<GDPolitica> GetPoliticas()
        {
            List<GDPolitica> politicas = new List<GDPolitica>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GDPolitica", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    GDPolitica politica = new GDPolitica
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDPolitica = Convert.ToInt32(reader["OidGDPolitica"].ToString()),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrObjetivosEsp = reader["ObjetivosEsp"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
                    };
                    politicas.Add(politica);
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

            return politicas;
        }

        public static GDPolitica GetPolitica(int idPolitica)
        {
            GDPolitica politica = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GDPolitica where OidGDPolitica = @OidGDPolitica", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDPolitica", idPolitica);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    politica = new GDPolitica
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDPolitica = Convert.ToInt32(reader["OidGDPolitica"].ToString()),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrObjetivosEsp = reader["ObjetivosEsp"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
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

            return politica;
        }

        public static GDPolitica GetPoliticaByIdDoc(int idDocumento)
        {
            GDPolitica politica = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GDPolitica where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDDocumento", idDocumento);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    politica = new GDPolitica
                    {
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDPolitica = Convert.ToInt32(reader["OidGDPolitica"].ToString()),
                        IntOidGDProceso = Convert.ToInt32(reader["OidGDProceso"].ToString()),
                        StrAlcance = reader["Alcance"].ToString(),
                        StrAnexos = reader["Anexos"].ToString(),
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrGlosario = reader["Glosario"].ToString(),
                        StrIntroduccion = reader["Introduccion"].ToString(),
                        StrMarcoLegal = reader["MarcoLegal"].ToString(),
                        StrObjetivos = reader["Objetivos"].ToString(),
                        StrObjetivosEsp = reader["ObjetivosEsp"].ToString(),
                        StrNombre = reader["Nombre"].ToString()
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

            return politica;
        }

        public static void UpdatePolitica(GDPolitica politica)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GDPolitica]"+
                                         "      SET[OidGDDocumento] = @OidGDDocumento"+
                                         "         ,[Introduccion] = @Introduccion"+
                                         "         ,[Objetivos] = @Objetivos"+
                                         "         ,[ObjetivosEsp] = @ObjetivosEsp"+
                                         "         ,[Alcance] = @Alcance"+
                                         "         ,[MarcoLegal] = @MarcoLegal"+
                                         "         ,[Desarrollo] = @Desarrollo"+
                                         "         ,[Glosario] = @Glosario"+
                                         "         ,[Anexos] = @Anexos"+
                                         "         ,[OidGDProceso] = @OidGDProceso"+
                                         "         ,[Nombre] = @Nombre" +
                                         "    WHERE OidGDPolitica = @OidGDPolitica", conexion.OpenConnection());

                command.Parameters.AddWithValue("OidGDDocumento", politica.IntOidGDDocumento);
                command.Parameters.AddWithValue("Introduccion", politica.StrIntroduccion);
                command.Parameters.AddWithValue("Objetivos", politica.StrObjetivos);
                command.Parameters.AddWithValue("ObjetivosEsp", politica.StrObjetivosEsp);
                command.Parameters.AddWithValue("Alcance", politica.StrAlcance);
                command.Parameters.AddWithValue("MarcoLegal", politica.StrMarcoLegal);
                command.Parameters.AddWithValue("Desarrollo", politica.StrDesarrollo);
                command.Parameters.AddWithValue("Glosario", politica.StrGlosario);
                command.Parameters.AddWithValue("Anexos", politica.StrAnexos);
                command.Parameters.AddWithValue("OidGDProceso", politica.IntOidGDProceso);
                command.Parameters.AddWithValue("OidGDPolitica", politica.IntOidGDPolitica);
                command.Parameters.AddWithValue("Nombre", politica.StrNombre);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = politica.IntOidGDPolitica,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza la información del documento {politica.StrNombre}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDPolitica"
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