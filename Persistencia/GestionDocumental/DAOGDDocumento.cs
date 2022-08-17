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
    public class DAOGDDocumento
    {
        public static void SetDocumento(GDDocumento Documento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDDocumento]"+
                                               " ([OidGDSolicitud]"+
                                               " ,[NomDoc]"+
                                               " ,[UniFunSolicitante]"+
                                               " ,[FechaE]"+
                                               " ,[NomSolicitante]"+
                                               " ,[CodigoDoc]"+
                                               " ,[TipDoc]"+
                                               " ,[OidPCProceso]" +
                                               " ,[Version]" +
                                               " ,[Consecutivo]" +
                                               " ,[Estado])" +
                                        " VALUES"+
                                            "   (@OidGDSolicitud"+
                                            "   ,@NomDoc"+
                                            "   ,@UniFunSolicitante"+
                                            "   ,@FechaE"+
                                            "   ,@NomSolicitante"+
                                            "   ,@CodigoDoc"+
                                            "   ,@TipDoc"+
                                            "   ,@OidPCProceso" +
                                            "   ,@Version" +
                                            "   ,@Consecutivo" +
                                            "   ,@Estado) select SCOPE_IDENTITY()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDSolicitud", Documento.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@NomDoc", Documento.StrNomDoc);
                command.Parameters.AddWithValue("@UniFunSolicitante", Documento.StrUniFunSolicitante);
                command.Parameters.AddWithValue("@FechaE", Documento.DtFechaE);
                command.Parameters.AddWithValue("@NomSolicitante", Documento.StrNomSolicitante);
                command.Parameters.AddWithValue("@CodigoDoc", Documento.StrCodigoDoc);
                command.Parameters.AddWithValue("@TipDoc", Documento.StrTipDoc);
                command.Parameters.AddWithValue("@Version", Documento.IntVersion);
                command.Parameters.AddWithValue("@Estado", Documento.IntEstado);
                command.Parameters.AddWithValue("@OidPCProceso", Documento.IntOidPCProceso);
                command.Parameters.AddWithValue("@Consecutivo", Documento.IntConsecutivo);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea la cabecera para el documento {Documento.StrNomDoc}",
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

        public static List<GDDocumento> GetDocumentos(string nomDoc, string tipo, string estado)
        {

            List<GDDocumento> documentos = new List<GDDocumento>(); ;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDocumento WHERE  NomDoc like '%'+@NomDoc+'%' and TipDoc like '%' +@TipDoc+ '%' and Estado like '%'+ @Estado +'%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomDoc", nomDoc);
                command.Parameters.AddWithValue("@TipDoc", tipo);
                command.Parameters.AddWithValue("@Estado", estado);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDDocumento documento = new GDDocumento();

                    documento.DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString());
                    documento.IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString());
                    documento.IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString());
                    documento.IntVersion = Convert.ToInt32(reader["Version"].ToString());
                    documento.StrCodigoDoc = reader["CodigoDoc"].ToString();
                    documento.StrNomDoc = reader["NomDoc"].ToString();
                    documento.StrNomSolicitante = reader["NomSolicitante"].ToString();
                    documento.StrTipDoc = reader["TipDoc"].ToString();
                    documento.StrUniFunSolicitante = reader["UniFunSolicitante"].ToString();
                    documento.IntEstado = reader["Estado"].ToString() == "" ? 0 : Convert.ToInt32(reader["Estado"]);
                    documento.IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString());
                    documento.IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]);
                    documentos.Add(documento);
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

            return documentos;
        }

        public static List<GDDocumento> GetDocumentosVigentes(string nomDoc, string tipo)
        {

            List<GDDocumento> documentos = new List<GDDocumento>(); ;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDocumento where OidGDDocumento in  (select MAX(OidGDDocumento)  from GDDocumento where estado = 4 group by CodigoDoc)"+
                                            "and NomDoc like '%' + @NomDoc + '%' and TipDoc like '%' + @TipDoc + '%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomDoc", nomDoc);
                command.Parameters.AddWithValue("@TipDoc", tipo);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDDocumento documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntEstado = reader["Estado"].ToString() == "" ? 0 : Convert.ToInt32(reader["Estado"]),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"])

                    };
                    documentos.Add(documento);
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

            return documentos;
        }


        public static void SetUpdate(GDDocumento Documento)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update [dbo].[GDDocumento] set "  +
                                               " [OidGDSolicitud] = @OidGDSolicitud" +
                                               " ,[NomDoc] = @NomDoc" +
                                               " ,[UniFunSolicitante] = @UniFunSolicitante" +
                                               " ,[FechaE] = @FechaE" +
                                               " ,[NomSolicitante] = @NomSolicitante" +
                                               " ,[CodigoDoc] = @CodigoDoc" +
                                               " ,[TipDoc] = @TipDoc" +
                                               " ,[Version] = @Version" +
                                               " ,[Estado] = @Estado" +
                                               " ,[Consecutivo] = @Consecutivo" +
                                               " ,[OidPCProceso] = @OidPCProceso" +
                                               " where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDSolicitud", Documento.IntOidGDSolicitud);
                command.Parameters.AddWithValue("@NomDoc", Documento.StrNomDoc);
                command.Parameters.AddWithValue("@UniFunSolicitante", Documento.StrUniFunSolicitante);
                command.Parameters.AddWithValue("@FechaE", Documento.DtFechaE);
                command.Parameters.AddWithValue("@NomSolicitante", Documento.StrNomSolicitante);
                command.Parameters.AddWithValue("@CodigoDoc", Documento.StrCodigoDoc);
                command.Parameters.AddWithValue("@TipDoc", Documento.StrTipDoc);
                command.Parameters.AddWithValue("@Version", Documento.IntVersion);
                command.Parameters.AddWithValue("@OidGDDocumento", Documento.IntOidGDDocumento);
                command.Parameters.AddWithValue("@Estado", Documento.IntEstado);
                command.Parameters.AddWithValue("@OidPCProceso", Documento.IntOidPCProceso);
                command.Parameters.AddWithValue("@Consecutivo", Documento.IntConsecutivo);
                command.ExecuteNonQuery();


                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Documento.IntOidGDDocumento,
                    intOidGNHistorico = 0,
                    strAccion = "Editar",
                    strDetalle = $"Se actualiza la informacion del documento con el nombre {Documento.StrNomDoc}",
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

        public static GDDocumento GetDocumento(int OidGDDocumento)
        {
            GDDocumento documento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDocumento WHERE  OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", OidGDDocumento);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante= reader["UniFunSolicitante"].ToString(),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]),
                        IntEstado = Convert.ToInt32(reader["Estado"])
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

            return documento;
        }

        /// <summary>
        /// obtener documento por el id de la solicitud que lo genero.
        /// </summary>
        /// <param name="OidGDSolicitud"></param>
        /// <returns></returns>
        public static GDDocumento GetDocumentoSol(int OidGDSolicitud)
        {
            GDDocumento documento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from GDDocumento WHERE  OidGDSolicitud = @OidGDSolicitud", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDSolicitud", OidGDSolicitud);

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntEstado = reader["Estado"].ToString() == "" ? 0 : Convert.ToInt32(reader["Estado"]),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"]),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]),
                        
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

            return documento;
        }

        public static GDDocumento GetUltDocumento()
        {
            GDDocumento documento = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select top(1) * from GDDocumento where Estado < 5 order by OidGDDocumento desc", conexion.OpenConnection());

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]),
                        
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

            return documento;
        }


        public static List<GDDocumento> GetDDocumentosByIdRev(int idRevisor, string NomDoc, string Tipo, string Estado)
        {
            List<GDDocumento> documentos = new List<GDDocumento>(); ;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT  D.* FROM[dbo].[GDDocumento] as D
                                              left join GDRevision as R on r.OidGDDocumento = D.OidGDDocumento
                                              where R.OidRevisor = @OidRevisor
                                              and D.[NomDoc] like '%' + @NomDoc + '%' and  D.TipDoc like '%' + @TipDoc + '%' and D.Estado like '%' + @Estado + '%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidRevisor", idRevisor);
                command.Parameters.AddWithValue("@NomDoc", NomDoc);
                command.Parameters.AddWithValue("@TipDoc", Tipo);
                command.Parameters.AddWithValue("@Estado", Estado);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDDocumento documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntEstado = reader["Estado"].ToString() == "" ? 0 : Convert.ToInt32(reader["Estado"]),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]),
                      
                    };
                    documentos.Add(documento);
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

            return documentos;
        }


        public static List<GDDocumento> GetDDocumentosByIdApro(int idRevisor, string NomDoc, string Tipo, string Estado)
        {
            List<GDDocumento> documentos = new List<GDDocumento>(); ;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT D.* FROM[dbo].[GDDocumento] D
                                              LEFT JOIN GDAprobacion A ON A.OidGDDocumento = D.OidGDDocumento
                                              where A.OidRevisor = @OidRevisor
                                              and D.[NomDoc] like '%' + @NomDoc + '%' and  D.TipDoc like '%' + @TipDoc + '%' and D.Estado like '%' + @Estado + '%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidRevisor", idRevisor);
                command.Parameters.AddWithValue("@NomDoc", NomDoc);
                command.Parameters.AddWithValue("@TipDoc", Tipo);
                command.Parameters.AddWithValue("@Estado", Estado);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDDocumento documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntEstado = reader["Estado"].ToString() == "" ? 0 : Convert.ToInt32(reader["Estado"]),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"]),
                      
                    };
                    documentos.Add(documento);
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

            return documentos;
        }

        public static int GetCodigoDocumento(string siglaCodigo)
        {
            int codigo = 1;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select ISNULL(max(Consecutivo), 0) +1 from GDDocumento WHERE CodigoDoc LIKE '' + @CodigoDoc + '%' and Estado < 5", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CodigoDoc", siglaCodigo);
                codigo = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return codigo;
        }

        public static List<GDDocumento> GetDocumentoByCod(string codigo, int Consecutivo, int Version)
        {
            List<GDDocumento> documentos = new List<GDDocumento>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("Select * from GDDocumento where CodigoDoc like '%' + @CodigoDoc + '%' AND Consecutivo = @Consecutivo and Estado < 5 and Version <=  @Version order by Version", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CodigoDoc", codigo);
                command.Parameters.AddWithValue("@Consecutivo", Consecutivo);
                command.Parameters.AddWithValue("@Version", Version);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GDDocumento documento = new GDDocumento
                    {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"])
                    };
                    documentos.Add(documento);
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

            return documentos;
        }

        public static List<GDDocumento> GetDocumentosByTipo(string tipo)
        {
            List<GDDocumento> documentos = new List<GDDocumento>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from GDDocumento where OidGDDocumento in (select MAX(OidGDDocumento) from GDDocumento where TipDoc  = @TipDoc and Estado = 4  group by CodigoDoc) and TipDoc  = @TipDoc and Estado = 4", conexion.OpenConnection());

                command.Parameters.AddWithValue("TipDoc", tipo);

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    documentos.Add(new GDDocumento {
                        DtFechaE = Convert.ToDateTime(reader["FechaE"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidGDSolicitud = Convert.ToInt32(reader["OidGDSolicitud"].ToString()),
                        IntVersion = Convert.ToInt32(reader["Version"].ToString()),
                        StrCodigoDoc = reader["CodigoDoc"].ToString(),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrNomSolicitante = reader["NomSolicitante"].ToString(),
                        StrTipDoc = reader["TipDoc"].ToString(),
                        StrUniFunSolicitante = reader["UniFunSolicitante"].ToString(),
                        IntOidPCProceso = Convert.ToInt32(reader["OidPCProceso"].ToString()),
                        IntConsecutivo = Convert.ToInt32(reader["Consecutivo"])

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

            return documentos;
        }

        
        public static List<dynamic> GetListadoMaestro(string proceso, string codigo, string nombre, string tipo, string version )
        {
            List<dynamic> listadoMaestro = new List<dynamic>();

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select P.NomPro Proceso, (D.CodigoDoc + '-' + RIGHT('0000' + Ltrim(Rtrim(D.Consecutivo)),4)) Codigo,
	                                        D.NomDoc Nombre, D.TipDoc Tipo, Dir.NomDir Direccion, D.Version, 'Publicado' Estado, S.JusSol Cambio,
	                                        D.FechaE FechaElaboracion, S.NomUsu Elaborador,
	                                        (select stuff((
		                                        select ',' + U.GNNomUsu from GDRevision R
		                                        inner join Usuario U on U.GNCodUsu = R.OidRevisor
		                                        where R.OidGDDocumento = D.OidGDDocumento
		                                        FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(4000)'
	                                        ),1,1,'')) Revisores,
	                                        (select stuff((
		                                        select ',' + format(R.Fecha, 'dd/MM/yyyy') from GDRevision R
		                                        where R.OidGDDocumento = D.OidGDDocumento
		                                        FOR XML PATH(''), TYPE).value('.[1]', 'nvarchar(4000)'
	                                        ),1,1,'')) FechaRevision,
	                                        Usr.GNNomUsu Aprobador, FORMAT(A.Fecha, 'dd/MM/yyyy') FechaAprobacion,
	                                        (case D.TipDoc
		                                        when 'Indicador'
		                                        then (select I.OIdGDDocIndicador from GDDocIndicador I where I.OidGDDocumento = D.OidGDDocumento)
		                                        when 'Procedimiento'
		                                        then (select P.OIdGdDocprocedimiento from GdDocProcedimiento P where P.OidGDDocumento = D.OidGDDocumento)
		                                        when 'Protocolo'
		                                        then (select OidGDProtocolo from GDProtocolo where OidGDDocumento = D.OidGDDocumento)
		                                        when 'Manual'
		                                        then (select OidGDManual from GDManual where OidGDDocumento = D.OidGDDocumento)
		                                        when 'Politica'
		                                        then (select OidGDPolitica from GDPolitica where OidGDDocumento = D.OidGDDocumento)
	                                        end) IdDocumento
                                        from GDDocumento D
	                                        inner join PCProceso P on P.OIdProceso = D.OidPCProceso
	                                        inner join Departamento U on U.GnDcDep = P.GnDcDep
	                                        inner join GNDireccion Dir on Dir.OidGNDir = U.OidGnDir
	                                        inner join GDSolicitud S on S.OidGDSolicitud = D.OidGDSolicitud
	                                        inner join GDAprobacion A on A.OidGDDocumento = D.OidGDDocumento
	                                        inner join Usuario Usr on Usr.GNCodUsu = A.OidRevisor
                                        where D.Estado = 4 and P.NomPro like '%'+ @proceso +'%' 
	                                        and  (D.CodigoDoc + '-' + RIGHT('0000' + Ltrim(Rtrim(D.Consecutivo)),4)) like '%'+@coidgo+'%'
	                                        and D.NomDoc like '%'+@nombre+'%' and D.TipDoc like '%'+@tipo+'%' and D.[Version] like '%'+@version+'%'", conexion.OpenConnection());

                command.Parameters.AddWithValue("proceso", proceso);
                command.Parameters.AddWithValue("coidgo", codigo);
                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("tipo", tipo);
                command.Parameters.AddWithValue("version", version);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listadoMaestro.Add(new
                        {
                            Proceso = reader["Proceso"].ToString(),
                            Codigo = reader["Codigo"].ToString(),
                            Nombre = reader["Nombre"].ToString(),
                            Tipo = reader["Tipo"].ToString(),
                            Direccion = reader["Direccion"].ToString(),
                            Version = reader["Version"].ToString(),
                            Estado = reader["Estado"].ToString(),
                            Cambio = reader["Cambio"].ToString(),
                            FechaElaboracion = reader["FechaElaboracion"].ToString(),
                            Elaborador = reader["Elaborador"].ToString(),
                            Revisores = reader["Revisores"].ToString(),
                            FechaRevision = reader["FechaRevision"].ToString(),
                            Aprobador = reader["Aprobador"].ToString(),
                            FechaAprobacion = reader["FechaAprobacion"].ToString(),
                            IdDocumento = reader["IdDocumento"].ToString(),
                        }) ;
                    }
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

            return listadoMaestro;
        }
    }
}