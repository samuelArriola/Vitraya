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
    public class DAOGDSolicitud
    {

        public static void SetSolicitud(GDSolicitud solicitud)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDSolicitud]  " +
                                        "           ([TipoSol]  " +
                                        "           ,[FechaSol] " +
                                        "           ,[NomDoc] " +
                                        "           ,[NomUsu] " +
                                        "           ,[CarUsu] " +
                                        "           ,[CodUsu] " +
                                        "           ,[JusSol] " +
                                        "           ,[DesSol] " +
                                        "           ,[TipoDoc]  " +
                                        "           ,[OidGNProceso]" +
                                        "           ,[Estado]" +
                                        "           ,[Incidencia]" +
                                        "           ,[UnidadFuncional]" +
                                        "           ,[OidGDDocE]" +
                                        "           ,[GnDcDep])  " +
                                        "     VALUES  " +
                                        "           (@TipoSol " +
                                        "           ,@FechaSol  " +
                                        "           ,@NomDoc " +
                                        "           ,@NomUsu " +
                                        "           ,@CarUsu " +
                                        "           ,@CodUsu " +
                                        "           ,@JusSol  " +
                                        "           ,@DesSol  " +
                                        "           ,@TipoDoc " +
                                        "           ,@OidGNProceso" +
                                        "           ,@Estado" +
                                        "           ,@Incidencia" +
                                        "           ,@UnidadFuncional" +
                                        "           ,@OidGDDocE" +
                                        "           ,@GnDcDep) select scope_identity()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@TipoSol", solicitud.StrTipoSol);
                command.Parameters.AddWithValue("@FechaSol", solicitud.DtmFechaSol);
                command.Parameters.AddWithValue("@NomDoc", solicitud.StrNomDoc);
                command.Parameters.AddWithValue("@NomUsu", solicitud.StrNomUsu);
                command.Parameters.AddWithValue("@CarUsu", solicitud.StrCarUsu);
                command.Parameters.AddWithValue("@CodUsu", solicitud.DblCodUsu);
                command.Parameters.AddWithValue("@JusSol", solicitud.StrJusSol);
                command.Parameters.AddWithValue("@DesSol", solicitud.StrDesSol);
                command.Parameters.AddWithValue("@TipoDoc", solicitud.StrTipoDoc);
                command.Parameters.AddWithValue("@OidGNProceso", solicitud.IntOidGNProceso);
                command.Parameters.AddWithValue("@Estado", solicitud.StrEstado);
                command.Parameters.AddWithValue("@Incidencia", solicitud.StrIncidencia);
                command.Parameters.AddWithValue("@UnidadFuncional", solicitud.StrUnidadFuncional);
                command.Parameters.AddWithValue("@GnDcDep", solicitud.IntGnDcDep);
                command.Parameters.AddWithValue("@OidGDDocE", solicitud.IntOidGDDocE);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se crea solicitud de creación de documento: {solicitud.StrNomDoc}",
                    strEntidad = "GDSolicitud"
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

        public static void SetUpdate(GDSolicitud solicitud) 
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update [dbo].[GDSolicitud] set "+
                                        "           [FechaSol] = @FechaSol " +
                                        "           ,[NomDoc] = @NomDoc" +
                                        "           ,[JusSol] = @JusSol" +
                                        "           ,[DesSol] = @DesSol" +
                                        "           ,[Estado] = @Estado" +
                                        "           ,[Incidencia] = @Incidencia" +
                                        "           ,[GnDcDep] = @GnDcDep" +
                                        "           ,[OidGDDocE] = @OidGDDocE" +
                                        "   where OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());

               
                command.Parameters.AddWithValue("@FechaSol", solicitud.DtmFechaSol);
                command.Parameters.AddWithValue("@NomDoc", solicitud.StrNomDoc);
                command.Parameters.AddWithValue("@JusSol", solicitud.StrJusSol);
                command.Parameters.AddWithValue("@DesSol", solicitud.StrDesSol);
                command.Parameters.AddWithValue("@Estado", solicitud.StrEstado);
                command.Parameters.AddWithValue("@Incidencia", solicitud.StrIncidencia);
                command.Parameters.AddWithValue("@OidGDSolicitud", solicitud.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@GnDcDep", solicitud.IntGnDcDep);
                command.Parameters.AddWithValue("@OidGDDocE", solicitud.IntOidGDDocE);

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

        public static List<GDSolicitud> GetSolisitudes(string nombre, string tipoSol, DateTime fecha, string tipoDoc, string estado)
        {
            List<GDSolicitud> solicitudes = new List<GDSolicitud>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT *  FROM GDSolicitud
                                            where TipoSol like  '%' + @TipoSol + '%' and NomDoc like '%' + @NomDoc + '%' 
                                            and Estado like '%' + @Estado + '%' and TipoDoc like '%' + @TipoDoc + '%' and FechaSol < @FechaSol and OidGDSolicitud not in (select OidGDSolicitud from GDDocumento where estado <> 0)", conexion.OpenConnection());
                command.Parameters.AddWithValue("@TipoSol", tipoSol);
                command.Parameters.AddWithValue("@NomDoc", nombre);
                command.Parameters.AddWithValue("@TipoDoc", tipoDoc);
                command.Parameters.AddWithValue("@FechaSol", fecha);
                command.Parameters.AddWithValue("@Estado", estado);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var t = reader["GnDcDep"].ToString();
                    GDSolicitud solicitud = new GDSolicitud
                    {
                        DblCodUsu = Convert.ToDouble(reader["CodUsu"].ToString()),
                        DtmFechaSol = Convert.ToDateTime(reader["FechaSol"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntOidGNProceso = Convert.ToInt32(reader["OidGNProceso"].ToString()),
                        StrCarUsu = reader["CarUsu"].ToString(),
                        StrDesSol = reader["DesSol"].ToString(),
                        StrJusSol = reader["JusSol"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomUsu = reader["NomUsu"].ToString(),
                        StrTipoDoc = reader["TipoDoc"].ToString(),
                        StrTipoSol = reader["TipoSol"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                        StrIncidencia= reader["Incidencia"].ToString(),
                        IntGnDcDep = reader["GnDcDep"].ToString() == "" ? 0 : Convert.ToInt32(reader["GnDcDep"].ToString()),
                        IntOidGDDocE = Convert.ToInt32(reader["OidGDDocE"])
                    };
                    solicitudes.Add(solicitud);
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

            return solicitudes;
        }

        public static GDSolicitud GetUltSolicitud()
        {
            GDSolicitud solicitud = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT top(1) *  FROM GDSolicitud order by OidGDSolicitud desc ", conexion.OpenConnection());
                
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    solicitud = new GDSolicitud
                    {
                        DblCodUsu = Convert.ToDouble(reader["CodUsu"].ToString()),
                        DtmFechaSol = Convert.ToDateTime(reader["FechaSol"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntOidGNProceso = Convert.ToInt32(reader["OidGNProceso"].ToString()),
                        StrCarUsu = reader["CarUsu"].ToString(),
                        StrDesSol = reader["DesSol"].ToString(),
                        StrJusSol = reader["JusSol"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomUsu = reader["NomUsu"].ToString(),
                        StrTipoDoc = reader["TipoDoc"].ToString(),
                        StrTipoSol = reader["TipoSol"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                        IntGnDcDep = Convert.ToInt32(reader["GnDcDep"]),
                        IntOidGDDocE = Convert.ToInt32(reader["OidGDDocE"])


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

            return solicitud;

        }

        public static GDSolicitud GetSolicitud(string OidGDSolicitud)
        {
            GDSolicitud solicitud = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GDSolicitud where OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDSolicitud", OidGDSolicitud);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    solicitud = new GDSolicitud
                    {
                        DblCodUsu = Convert.ToDouble(reader["CodUsu"].ToString()),
                        DtmFechaSol = Convert.ToDateTime(reader["FechaSol"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntOidGNProceso = Convert.ToInt32(reader["OidGNProceso"].ToString()),
                        StrCarUsu = reader["CarUsu"].ToString(),
                        StrDesSol = reader["DesSol"].ToString(),
                        StrJusSol = reader["JusSol"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomUsu = reader["NomUsu"].ToString(),
                        StrTipoDoc = reader["TipoDoc"].ToString(),
                        StrTipoSol = reader["TipoSol"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                        IntGnDcDep = Convert.ToInt32(reader["GnDcDep"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IntOidGDDocE = Convert.ToInt32(reader["OidGDDocE"])

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

            return solicitud;
        }
    }
}