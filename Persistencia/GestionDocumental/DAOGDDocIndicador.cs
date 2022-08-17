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
    public class DAOGDDocIndicador
    {
        public static void set(GDDocIndicador indicador)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GDDocIndicador]"+
                                         "          ([OidGDDocumento]"+
                                         "          ,[OidProceso]"+
                                         "          ,[NomDoc]"+
                                         "          ,[Tipo]"+
                                         "          ,[Justificacion]"+
                                         "          ,[CodSOGC]"+
                                         "          ,[DescNum]"+
                                         "          ,[OrInfoNum]"+
                                         "          ,[FuentPrimNum]"+
                                         "          ,[DescDen]"+
                                         "          ,[OrInfoDen]"+
                                         "          ,[FuentPrimDen]"+
                                         "          ,[UniMedicion]"+
                                         "          ,[Tasa]"+
                                         "          ,[Factor]"+
                                         "          ,[Periodicidad]"+
                                         "          ,[Responsable]"+
                                         "          ,[FormulaCalc]"+
                                         "          ,[Estandar]"+
                                         "          ,[Tendencia]"+
                                         "          ,[TipGrafica]"+
                                         "          ,[Interpretacion]"+
                                         "          ,[ResponsableMed]"+
                                         "          ,[ResponsableAna]"+
                                         "          ,[Actores]"+
                                         "          ,[Vigilancia]"+
                                         "          ,[FechaRevision]"+
                                         "          ,[FechaAprovacion]"+
                                         "          ,[OidRevisor]"+
                                         "          ,[NomRevisor]"+
                                         "          ,[OidAprovador]"+
                                         "          ,[NomAprovador]"+
                                         "          ,[Fecha]" +
                                         "          ,[Dominio])" +
                                         "    VALUES"+
                                         "          ( @OidGDDocumento" +
                                         "          , @OidProceso" +
                                         "          , @NomDoc" +
                                         "          , @Tipo" +
                                         "          , @Justificacion" +
                                         "          , @CodSOGC" +
                                         "          , @DescNum" +
                                         "          , @OrInfoNum" +
                                         "          , @FuentPrimNum" +
                                         "          , @DescDen" +
                                         "          , @OrInfoDen" +
                                         "          , @FuentPrimDen" +
                                         "          , @UniMedicion" +
                                         "          , @Tasa" +
                                         "          , @Factor" +
                                         "          , @Periodicidad" +
                                         "          , @Responsable" +
                                         "          , @FormulaCalc" +
                                         "          , @Estandar" +
                                         "          , @Tendencia" +
                                         "          , @TipGrafica" +
                                         "          , @Interpretacion" +
                                         "          , @ResponsableMed" +
                                         "          , @ResponsableAna" +
                                         "          , @Actores" +
                                         "          , @Vigilancia" +
                                         "          , @FechaRevision" +
                                         "          , @FechaAprovacion" +
                                         "          , @OidRevisor" +
                                         "          , @NomRevisor" +
                                         "          , @OidAprovador" +
                                         "          , @NomAprovador" +
                                         "          , @Fecha" +
                                         "          , @Dominio) select scope_identity", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", indicador.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidProceso", indicador.IntOidProceso);
                command.Parameters.AddWithValue("@NomDoc", indicador.StrNomDoc);
                command.Parameters.AddWithValue("@Justificacion", indicador.StrJustificacion);
                command.Parameters.AddWithValue("@CodSOGC", indicador.StrCodSOGC);
                command.Parameters.AddWithValue("@DescNum", indicador.StrDescNum);
                command.Parameters.AddWithValue("@OrInfoNum", indicador.StrOrInfoNum);
                command.Parameters.AddWithValue("@FuentPrimNum", indicador.StrFuentPrimNum);
                command.Parameters.AddWithValue("@DescDen", indicador.StrDescDen);
                command.Parameters.AddWithValue("@OrInfoDen", indicador.StrOrInfoDen);
                command.Parameters.AddWithValue("@FuentPrimDen", indicador.StrFuentPrimDen);
                command.Parameters.AddWithValue("@UniMedicion", indicador.StrUniMedicion);
                command.Parameters.AddWithValue("@Factor", indicador.StrFactor);
                command.Parameters.AddWithValue("@Periodicidad", indicador.StrPeriodicidad);
                command.Parameters.AddWithValue("@Responsable", indicador.StrResponsable);
                command.Parameters.AddWithValue("@FormulaCalc", indicador.StrFormulaCalc);
                command.Parameters.AddWithValue("@Estandar", indicador.StrEstandar);
                command.Parameters.AddWithValue("@Tendencia", indicador.StrTendencia);
                command.Parameters.AddWithValue("@TipGrafica", indicador.StrTipGrafica);
                command.Parameters.AddWithValue("@Interpretacion", indicador.StrInterpretacion);
                command.Parameters.AddWithValue("@ResponsableMed", indicador.StrResponsableMed);
                command.Parameters.AddWithValue("@ResponsableAna", indicador.StrResponsableAna);
                command.Parameters.AddWithValue("@Actores", indicador.StrActores);
                command.Parameters.AddWithValue("@Vigilancia", indicador.StrVigilancia);
                command.Parameters.AddWithValue("@FechaRevision", indicador.DtmFechaRevision);
                command.Parameters.AddWithValue("@FechaAprovacion", indicador.DtmFechaAprovacion);
                command.Parameters.AddWithValue("@OidRevisor", indicador.IntOidRevisor);
                command.Parameters.AddWithValue("@NomRevisor", indicador.StrNomRevisor);
                command.Parameters.AddWithValue("@OidAprovador", indicador.IntOidAprovador);
                command.Parameters.AddWithValue("@NomAprovador", indicador.StrNomAprovador);
                command.Parameters.AddWithValue("@Fecha", indicador.DtmFecha);
                command.Parameters.AddWithValue("@Tasa", indicador.StrTasa);
                command.Parameters.AddWithValue("@Tipo", indicador.StrTipo);
                command.Parameters.AddWithValue("@Dominio", indicador.StrDominio);

                int  OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el documento tipo indicador: {indicador.StrNomDoc}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDocIndicador"
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

        public static List<GDDocIndicador> GetIndicadores()
        {
            List<GDDocIndicador> indicadores = new List<GDDocIndicador>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GDDocIndicador", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GDDocIndicador indicador = new GDDocIndicador
                    {
                        IntOIdGDDocIndicador = Convert.ToInt32(reader["OidGDDocIndicador"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidProceso = Convert.ToInt32(reader["OidProceso"].ToString()),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"].ToString()),
                        IntOidAprovador = Convert.ToInt32(reader["OidAprovador"].ToString()),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrJustificacion = reader["Justificacion"].ToString(),
                        StrCodSOGC = reader["CodSOGC"].ToString(),
                        StrDescNum = reader["DescNum"].ToString(),
                        StrOrInfoNum = reader["OrInfoNum"].ToString(),
                        StrFuentPrimNum = reader["FuentPrimNum"].ToString(),
                        StrDescDen = reader["DescDen"].ToString(),
                        StrOrInfoDen = reader["OrInfoDen"].ToString(),
                        StrFuentPrimDen = reader["FuentPrimDen"].ToString(),
                        StrUniMedicion = reader["UniMedicion"].ToString(),
                        StrFactor = reader["Factor"].ToString(),
                        StrPeriodicidad = reader["Periodicidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrFormulaCalc = reader["FormulaCalc"].ToString(),
                        StrEstandar = reader["Estandar"].ToString(),
                        StrTendencia = reader["Tendencia"].ToString(),
                        StrTipGrafica = reader["TipGrafica"].ToString(),
                        StrInterpretacion = reader["Interpretacion"].ToString(),
                        StrResponsableMed = reader["ResponsableMed"].ToString(),
                        StrResponsableAna = reader["ResponsableAna"].ToString(),
                        StrActores = reader["Actores"].ToString(),
                        StrVigilancia = reader["Vigilancia"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrTasa = reader["Tasa"].ToString(),
                        StrNomAprovador = reader["NomAprovador"].ToString(),
                        DtmFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtmFechaAprovacion = Convert.ToDateTime(reader["FechaAprovacion"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        StrDominio = reader["Dominio"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                    };
                    indicadores.Add(indicador);
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
            return indicadores;
        }

        public static GDDocIndicador GetIndicador(int idIndicador)
        {

            GDDocIndicador indicador = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GDDocIndicador where OidGDDocIndicador = @IdGDDocIndicador", conexion.OpenConnection());
                command.Parameters.AddWithValue("@IdGDDocIndicador", idIndicador);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    indicador = new GDDocIndicador
                    {
                        IntOIdGDDocIndicador = Convert.ToInt32(reader["OidGDDocIndicador"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidProceso = Convert.ToInt32(reader["OidProceso"].ToString()),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"].ToString()),
                        IntOidAprovador = Convert.ToInt32(reader["OidAprovador"].ToString()),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrJustificacion = reader["Justificacion"].ToString(),
                        StrCodSOGC = reader["CodSOGC"].ToString(),
                        StrDescNum = reader["DescNum"].ToString(),
                        StrOrInfoNum = reader["OrInfoNum"].ToString(),
                        StrFuentPrimNum = reader["FuentPrimNum"].ToString(),
                        StrDescDen = reader["DescDen"].ToString(),
                        StrOrInfoDen = reader["OrInfoDen"].ToString(),
                        StrFuentPrimDen = reader["FuentPrimDen"].ToString(),
                        StrUniMedicion = reader["UniMedicion"].ToString(),
                        StrFactor = reader["Factor"].ToString(),
                        StrPeriodicidad = reader["Periodicidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrFormulaCalc = reader["FormulaCalc"].ToString(),
                        StrEstandar = reader["Estandar"].ToString(),
                        StrTendencia = reader["Tendencia"].ToString(),
                        StrTipGrafica = reader["TipGrafica"].ToString(),
                        StrInterpretacion = reader["Interpretacion"].ToString(),
                        StrResponsableMed = reader["ResponsableMed"].ToString(),
                        StrResponsableAna = reader["ResponsableAna"].ToString(),
                        StrActores = reader["Actores"].ToString(),
                        StrVigilancia = reader["Vigilancia"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrTasa = reader["Tasa"].ToString(),
                        StrNomAprovador = reader["NomAprovador"].ToString(),
                        DtmFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtmFechaAprovacion = Convert.ToDateTime(reader["FechaAprovacion"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        StrDominio = reader["Dominio"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
                        IntOidGDProceso =Convert.ToInt32(reader["OidProceso"]),
                        
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
            return indicador;
        }

        public static GDDocIndicador GetIndicadorByIdDoc(int idDoc)
        {

            GDDocIndicador indicador = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT top(1) * FROM GDDocIndicador where OidGDDocumento = @OidGDDocumento order by OIdGDDocIndicador desc ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidGDDocumento", idDoc);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    indicador = new GDDocIndicador
                    {
                        IntOIdGDDocIndicador = Convert.ToInt32(reader["OidGDDocIndicador"].ToString()),
                        IntOidGDDocumento = Convert.ToInt32(reader["OidGDDocumento"].ToString()),
                        IntOidProceso = Convert.ToInt32(reader["OidProceso"].ToString()),
                        IntOidRevisor = Convert.ToInt32(reader["OidRevisor"].ToString()),
                        IntOidAprovador = Convert.ToInt32(reader["OidAprovador"].ToString()),
                        StrNomDoc = reader["NomDoc"].ToString(),
                        StrJustificacion = reader["Justificacion"].ToString(),
                        StrCodSOGC = reader["CodSOGC"].ToString(),
                        StrDescNum = reader["DescNum"].ToString(),
                        StrOrInfoNum = reader["OrInfoNum"].ToString(),
                        StrFuentPrimNum = reader["FuentPrimNum"].ToString(),
                        StrDescDen = reader["DescDen"].ToString(),
                        StrOrInfoDen = reader["OrInfoDen"].ToString(),
                        StrFuentPrimDen = reader["FuentPrimDen"].ToString(),
                        StrUniMedicion = reader["UniMedicion"].ToString(),
                        StrFactor = reader["Factor"].ToString(),
                        StrPeriodicidad = reader["Periodicidad"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                        StrFormulaCalc = reader["FormulaCalc"].ToString(),
                        StrEstandar = reader["Estandar"].ToString(),
                        StrTendencia = reader["Tendencia"].ToString(),
                        StrTipGrafica = reader["TipGrafica"].ToString(),
                        StrInterpretacion = reader["Interpretacion"].ToString(),
                        StrResponsableMed = reader["ResponsableMed"].ToString(),
                        StrResponsableAna = reader["ResponsableAna"].ToString(),
                        StrActores = reader["Actores"].ToString(),
                        StrVigilancia = reader["Vigilancia"].ToString(),
                        StrNomRevisor = reader["NomRevisor"].ToString(),
                        StrTasa = reader["Tasa"].ToString(),
                        StrNomAprovador = reader["NomAprovador"].ToString(),
                        DtmFechaRevision = Convert.ToDateTime(reader["FechaRevision"]),
                        DtmFechaAprovacion = Convert.ToDateTime(reader["FechaAprovacion"]),
                        DtmFecha = Convert.ToDateTime(reader["Fecha"]),
                        StrDominio = reader["Dominio"].ToString(),
                        StrTipo = reader["Tipo"].ToString(),
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
            return indicador;
        }

        public static void UpdateIndicador(GDDocIndicador indicador)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [GDDocIndicador]"+
                                         "  SET[OidGDDocumento] = @OidGDDocumento"+
                                         "     ,[OidProceso] = @OidProceso"+
                                         "     ,[NomDoc] = @NomDoc"+
                                         "     ,[Justificacion] = @Justificacion"+
                                         "     ,[CodSOGC] = @CodSOGC"+
                                         "     ,[DescNum] = @DescNum"+
                                         "     ,[OrInfoNum] = @OrInfoNum"+
                                         "     ,[FuentPrimNum] = @FuentPrimNum"+
                                         "     ,[DescDen] = @DescDen"+
                                         "     ,[OrInfoDen] = @OrInfoDen"+
                                         "     ,[FuentPrimDen] = @FuentPrimDen"+
                                         "     ,[UniMedicion] = @UniMedicion"+
                                         "     ,[Factor] = @Factor"+
                                         "     ,[Periodicidad] = @Periodicidad"+
                                         "     ,[Responsable] = @Responsable"+
                                         "     ,[FormulaCalc] = @FormulaCalc"+
                                         "     ,[Estandar] = @Estandar"+
                                         "     ,[Tendencia] = @Tendencia"+
                                         "     ,[TipGrafica] = @TipGrafica"+
                                         "     ,[Interpretacion] = @Interpretacion"+
                                         "     ,[ResponsableMed] = @ResponsableMed"+
                                         "     ,[ResponsableAna] = @ResponsableAna"+
                                         "     ,[Actores] = @Actores"+
                                         "     ,[Vigilancia] = @Vigilancia"+
                                         "     ,[FechaRevision] = @FechaRevision"+
                                         "     ,[FechaAprovacion] = @FechaAprovacion"+
                                         "     ,[OidRevisor] = @OidRevisor"+
                                         "     ,[NomRevisor] = @NomRevisor"+
                                         "     ,[OidAprovador] = @OidAprovador"+
                                         "     ,[NomAprovador] = @NomAprovador"+
                                         "     ,[Fecha] = @Fecha"+
                                         "     ,[Tasa] = @Tasa" +
                                         "     ,[Tipo] = @Tipo" +
                                         "     ,[Dominio] = @Dominio" +
                                         " WHERE  OIdGDDocIndicador = @OIdGDDocIndicador", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidGDDocumento", indicador.IntOidGDDocumento);
                command.Parameters.AddWithValue("@OidProceso", indicador.IntOidProceso);
                command.Parameters.AddWithValue("@NomDoc", indicador.StrNomDoc);
                command.Parameters.AddWithValue("@Justificacion", indicador.StrJustificacion);
                command.Parameters.AddWithValue("@CodSOGC", indicador.StrCodSOGC);
                command.Parameters.AddWithValue("@DescNum", indicador.StrDescNum);
                command.Parameters.AddWithValue("@OrInfoNum", indicador.StrOrInfoNum);
                command.Parameters.AddWithValue("@FuentPrimNum", indicador.StrFuentPrimNum);
                command.Parameters.AddWithValue("@DescDen", indicador.StrDescDen);
                command.Parameters.AddWithValue("@OrInfoDen", indicador.StrOrInfoDen);
                command.Parameters.AddWithValue("@FuentPrimDen", indicador.StrFuentPrimDen);
                command.Parameters.AddWithValue("@UniMedicion", indicador.StrUniMedicion);
                command.Parameters.AddWithValue("@Factor", indicador.StrFactor);
                command.Parameters.AddWithValue("@Periodicidad", indicador.StrPeriodicidad);
                command.Parameters.AddWithValue("@Responsable", indicador.StrResponsable);
                command.Parameters.AddWithValue("@FormulaCalc", indicador.StrFormulaCalc);
                command.Parameters.AddWithValue("@Estandar", indicador.StrEstandar);
                command.Parameters.AddWithValue("@Tendencia", indicador.StrTendencia);
                command.Parameters.AddWithValue("@TipGrafica", indicador.StrTipGrafica);
                command.Parameters.AddWithValue("@Interpretacion", indicador.StrInterpretacion);
                command.Parameters.AddWithValue("@ResponsableMed", indicador.StrResponsableMed);
                command.Parameters.AddWithValue("@ResponsableAna", indicador.StrResponsableAna);
                command.Parameters.AddWithValue("@Actores", indicador.StrActores);
                command.Parameters.AddWithValue("@Vigilancia", indicador.StrVigilancia);
                command.Parameters.AddWithValue("@FechaRevision", indicador.DtmFechaRevision);
                command.Parameters.AddWithValue("@FechaAprovacion", indicador.DtmFechaAprovacion);
                command.Parameters.AddWithValue("@OidRevisor", indicador.IntOidRevisor);
                command.Parameters.AddWithValue("@NomRevisor", indicador.StrNomRevisor);
                command.Parameters.AddWithValue("@OidAprovador", indicador.IntOidAprovador);
                command.Parameters.AddWithValue("@NomAprovador", indicador.StrNomAprovador);
                command.Parameters.AddWithValue("@Fecha", indicador.DtmFecha);
                command.Parameters.AddWithValue("@OIdGDDocIndicador", indicador.IntOIdGDDocIndicador);
                command.Parameters.AddWithValue("@Tasa", indicador.StrTasa);
                command.Parameters.AddWithValue("@Tipo", indicador.StrTipo);
                command.Parameters.AddWithValue("@Dominio", indicador.StrDominio);

                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = indicador.IntOIdGDDocIndicador,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza la informacion del documento {indicador.StrNomDoc}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GDDocIndicador"
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

        public static void DeleleteIndicadoByIdDoc(int idDocumanto)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Delete from GDDocIndicador where OidGDDocumento = @OidGDDocumento", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidGDDocumento", idDocumanto);
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