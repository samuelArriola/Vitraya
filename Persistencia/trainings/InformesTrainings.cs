using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Persistencia.trainings
{
    public class InformesTrainings
    {
        public class InformeCoberturaUnidad
        {
            private string  strTema,
                            strResposable;

            private  List<InformacionInforme> informaciones = new  List<InformacionInforme>();

            public string StrTema { get => strTema; set => strTema = value; }
            public string StrResposable { get => strResposable; set => strResposable = value; }
            public  List<InformacionInforme> Informaciones { get => informaciones; set => informaciones = value; }

            

            public class InformacionInforme
            {
                private float   ftlConteoAsistencia,
                                ftlConteoMatricula;
                string          strPorcentaje, strUnidadFuncional;

                public string StrUnidadFuncional { get => strUnidadFuncional; set => strUnidadFuncional = value; }
                
                public string StrPorcentaje { get => strPorcentaje; set => strPorcentaje = value; }
                public float FtlConteoAsistencia { get => ftlConteoAsistencia; set => ftlConteoAsistencia = value; }
                public float FtlConteoMatricula { get => ftlConteoMatricula; set => ftlConteoMatricula = value; }

                public void calcularPorcentaje()
                {
                    StrPorcentaje = (int)(FtlConteoAsistencia * 100 / FtlConteoMatricula) + "%";
                }
            }

            public static InformeCoberturaUnidad BuscarAgregarInforme(List<InformeCoberturaUnidad> informes, InformeCoberturaUnidad  Informe)
            {
                foreach(var informe  in informes)
                {
                    if (informe.StrTema == Informe.StrTema)
                        return informe;
                }
                informes.Add(Informe);
                return Informe;
            }
        }

        public class InformeAsistenciaUsuario
        {
            private int intDocumento, intConteoMatricula, intConteoAsistencia;
            private string strNombreUsuario, strUnidadFuncional;
            private string strPorcentaje;

            public int IntDocumento { get => intDocumento; set => intDocumento = value; }
            public int IntConteoMatricula { get => intConteoMatricula; set => intConteoMatricula = value; }
            public int IntConteoAsistencia { get => intConteoAsistencia; set => intConteoAsistencia = value; }
            public string StrNombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
            public string StrPorcentaje { get => strPorcentaje; set => strPorcentaje = value; }
            public string StrUnidadFuncional { get => strUnidadFuncional; set => strUnidadFuncional = value; }

            public void calcularPorcentaje()
            {
                StrPorcentaje = (int)(intConteoAsistencia * 100 / IntConteoMatricula) + "%";
            }
        }

        public class InformeInasistencia
        {
            private int         intDocumento;

            private string      strNombre,
                                srtUnidadFuncional;

            List<string>        temas;

            public int IntDocumento { get => intDocumento; set => intDocumento = value; }
            public string StrNombre { get => strNombre; set => strNombre = value; }
            public List<string> Temas { get => temas; set => temas = value; }
            public string SrtUnidadFuncional { get => srtUnidadFuncional; set => srtUnidadFuncional = value; }

            public InformeInasistencia()
            {
                temas = new List<string>();
            }

            public static InformeInasistencia buscarCargarInasistencia(List<InformeInasistencia> Informes, InformeInasistencia Informe )
            {
                foreach(var informe in Informes)
                {
                    if(informe.intDocumento == Informe.intDocumento)
                    {
                        return informe;
                    }
                }
                Informes.Add(Informe);
                return Informe;
            }

        }

        public class InformeAsistencia
        {
            private string  strResponsable,
                            strTema;

            private List<InformacionInforme> informacion = new List<InformacionInforme>(); 

            public string StrResponsable { get => strResponsable; set => strResponsable = value; }
            public string StrTema { get => strTema; set => strTema = value; }
            public List<InformacionInforme> Informacion { get => informacion; set => informacion = value; }

            public class InformacionInforme
            {
                private string strNomUsuario,
                               strAsistencia,
                               strUnidadFuncional;

                public string StrNomUsuario { get => strNomUsuario; set => strNomUsuario = value; }
                public string StrAsistencia { get => strAsistencia; set => strAsistencia = value; }
                public string StrUnidadFuncional { get => strUnidadFuncional; set => strUnidadFuncional = value; }
            }

            public static InformeAsistencia BuscarCargarInforme(List<InformeAsistencia> informes, InformeAsistencia Informe)
            {
                foreach(var informe in informes)
                    if (informe.StrTema == Informe.strTema)
                        return informe;

                informes.Add(Informe);
                return Informe;
            }
        }

        public static List<InformeCoberturaUnidad> GetiformeCoverturaUnidad(DateTime t)
        {
            List<InformeCoberturaUnidad> informes = new List<InformeCoberturaUnidad>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select CPCAPACITACION.TEMA, COUNT(CPCAPACITACION.TEMA) as ConteoMatricula, ConteoAsistencia, D.GnNomDep as UnidadFuncional from CPMATRICULAS "+
                                          "  INNER JOIN CPCAPACITACION ON CPMATRICULAS.IDCAPACITACION = CPCAPACITACION.IDCAPACITACION"+
                                          "  left join Usuario as U on U.GNCodUsu = CPMATRICULAS.IDEMPLEADO"+
                                          "  left join Departamento as D ON D.GnDcDep = U.GnDcDep"+
                                          "  FULL OUTER JOIN"+
                                          "  (SELECT  CPCAPACITACION.TEMA AS TEMA, COUNT(CPCAPACITACION.TEMA) AS ConteoAsistencia, D.GnIdDep AS NOMDEP FROM CPASISTENCIA"+
                                          "  LEFT JOIN CPCAPACITACION ON CPCAPACITACION.IDCAPACITACION = CPASISTENCIA.IDCAPACITACION"+
                                          "  LEFT JOIN Usuario AS U ON U.GNCodUsu = CPASISTENCIA.IDEMPLEADO"+
                                          "  LEFT JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep"+
                                          "  GROUP BY CPCAPACITACION.TEMA, D.GnIdDep) ASI ON ASI.TEMA = CPCAPACITACION.TEMA AND ASI.NOMDEP = D.GnIdDep"+
                                          "  GROUP BY CPCAPACITACION.TEMA, ASI.ConteoAsistencia, D.GnNomDep ORDER BY D.GnNomDep", conexion.OpenConnection());
                command.Parameters.AddWithValue("@fecha", t);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["tema"].ToString() == "")
                        continue;
                    InformeCoberturaUnidad informe = new InformeCoberturaUnidad
                    {
                        StrTema = reader["TEMA"].ToString(),
                    };
                    
                    InformeCoberturaUnidad.BuscarAgregarInforme(informes, informe);
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

            return informes;
        
        }
        public static List<InformeCoberturaUnidad> getIformesCoberturaTema()
        {
            List<InformeCoberturaUnidad> informes = new List<InformeCoberturaUnidad>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select CPCAPACITACION.RESPONSABLE, CPCAPACITACION.TEMA, COUNT(CPCAPACITACION.OidCPCAPACITACION) as ConteoMatricula, ConteoAsistencia, D.GnNomDep as UnidadFuncional from CPMATRICULA"+
                                         "   INNER JOIN CPCAPACITACION ON CPMATRICULA.OidCPCAPACITACION = CPCAPACITACION.OidCPCAPACITACION"+
                                         "   left join Usuario as U on U.GNCodUsu = CPMATRICULA.GNCodUsu"+
                                         "   left join Departamento as D ON D.GnDcDep = U.GnDcDep"+
                                         "   FULL OUTER JOIN"+
                                         "   (SELECT  CPCAPACITACION.OidCPCAPACITACION AS TEMA, COUNT(CPCAPACITACION.OidCPCAPACITACION) AS ConteoAsistencia, D.GnIdDep AS NOMDEP FROM CPASISTENCIA"+
                                         "   LEFT JOIN CPMATRICULA ON CPMATRICULA.OidCPMATRICULA = CPASISTENCIA.OidCPMATRICULA"+
                                         "   LEFT JOIN CPCAPACITACION ON CPCAPACITACION.OidCPCAPACITACION = CPMATRICULA.OidCPCAPACITACION"+
                                         "   LEFT JOIN Usuario AS U ON U.GNCodUsu = CPASISTENCIA.GNCodUsu"+
                                         "   LEFT JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep"+
                                         "   GROUP BY CPCAPACITACION.OidCPCAPACITACION, D.GnIdDep) ASI ON ASI.TEMA = CPCAPACITACION.OidCPCAPACITACION AND ASI.NOMDEP = D.GnIdDep"+
                                         "   GROUP BY CPCAPACITACION.OidCPCAPACITACION, ASI.ConteoAsistencia, D.GnNomDep, CPCAPACITACION.RESPONSABLE, CPCAPACITACION.TEMA ORDER BY 2, 5", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["tema"].ToString() == "")
                        continue;
                    InformeCoberturaUnidad informe = new InformeCoberturaUnidad
                    {
                        StrTema = reader["TEMA"].ToString(),
                        StrResposable = reader["RESPONSABLE"].ToString()
                    };
                    informe = InformeCoberturaUnidad.BuscarAgregarInforme(informes, informe);
                    InformeCoberturaUnidad.InformacionInforme informacion = new InformeCoberturaUnidad.InformacionInforme()
                    {
                        FtlConteoAsistencia = reader["ConteoAsistencia"].ToString() == "" ? 0 :Convert.ToInt32(reader["ConteoAsistencia"].ToString()),
                        FtlConteoMatricula = reader["ConteoMatricula"].ToString() == "" ? 0 : Convert.ToInt32(reader["ConteoMatricula"].ToString()),
                        StrUnidadFuncional = reader["UnidadFuncional"].ToString(),
                    };
                    informacion.calcularPorcentaje();
                    informe.Informaciones.Add(informacion);
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
            return informes;
        }

        public static List<InformeAsistenciaUsuario> GetInformeAsistenciaUsuarios()
        {
            List<InformeAsistenciaUsuario> informes = new List<InformeAsistenciaUsuario>();
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT CPMATRICULA.GNCodUsu as Documento, CPMATRICULA.NOMUSUARIO as NombreUsuario, "+
                                         "   COUNT(CPMATRICULA.GNCodUsu) as ConteoMatricula, ASI.ConteoAsistencia, Departamento.GnNomDep as UnidaFunccional FROM CPMATRICULA"+
                                         "   left join Usuario on Usuario.GNCodUsu = CPMATRICULA.GNCodUsu"+
                                         "   left join Departamento on Usuario.GnDcDep = Departamento.GnDcDep"+
                                         "   FULL OUTER JOIN"+
                                         "   (SELECT CPASISTENCIA.NOMUSUARIO AS NombreUsuarion, COUNT(CPASISTENCIA.GNCodUsu)"+
                                         "   AS ConteoAsistencia FROM CPASISTENCIA GROUP BY CPASISTENCIA.GNCodUsu, CPASISTENCIA.NOMUSUARIO) ASI ON ASI.NombreUsuarion = CPMATRICULA.NOMUSUARIO"+
                                         "   GROUP BY CPMATRICULA.GNCodUsu, CPMATRICULA.NOMUSUARIO, ASI.ConteoAsistencia, Departamento.GnNomDep order by 2", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InformeAsistenciaUsuario informe = new InformeAsistenciaUsuario
                    {
                        IntConteoAsistencia = reader["ConteoAsistencia"].ToString() == "" ? 0 : Convert.ToInt32(reader["ConteoAsistencia"]),
                        IntConteoMatricula = reader["ConteoMatricula"].ToString() == "" ? 0 : Convert.ToInt32(reader["ConteoMatricula"]),
                        IntDocumento = Convert.ToInt32(reader["Documento"].ToString()),
                        StrNombreUsuario = reader["NombreUsuario"].ToString(),
                        StrUnidadFuncional = reader["UnidaFunccional"].ToString(),
                    };
                    informes.Add(informe);
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

            return informes;
        }

        public static List<InformeInasistencia> GetInformeInasistencias()
        {
            List<InformeInasistencia> informes = new List<InformeInasistencia>();
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT D.GnNomDep, CPMATRICULA.GNCodUsu as Documento, CPMATRICULA.NOMUSUARIO as Nombre, CPCAPACITACION.TEMA as Tema, D.GnNomDep as UnidadFuncional FROM CPMATRICULA"+
                                         "   LEFT JOIN CPCAPACITACION ON CPCAPACITACION.OidCPCAPACITACION = CPMATRICULA.OidCPCAPACITACION"+
                                         "   LEFT JOIN Usuario AS U ON U.GNCodUsu = CPMATRICULA.GNCodUsu"+
                                         "   LEFT JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep"+
                                         "   WHERE CPMATRICULA.GNCodUsu NOT IN"+
                                         "   (SELECT GNCodUsu FROM CPASISTENCIA WHERE"+
                                         "   CPASISTENCIA.OidCPMATRICULA = CPMATRICULA.OidCPMATRICULA)"+
                                         "   GROUP BY CPCAPACITACION.TEMA, CPMATRICULA.OidCPCAPACITACION,"+
                                         "   CPMATRICULA.GNCodUsu, CPMATRICULA.NOMUSUARIO, D.GnNomDep  ORDER BY CPMATRICULA.NOMUSUARIO", conexion.OpenConnection());

                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InformeInasistencia informeInasistencia = new InformeInasistencia
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntDocumento = Convert.ToInt32(reader["Documento"]),
                        SrtUnidadFuncional = reader["UnidadFuncional"].ToString()
                    };

                    informeInasistencia = InformeInasistencia.buscarCargarInasistencia(informes, informeInasistencia);
                    informeInasistencia.Temas.Add(reader["Tema"].ToString());

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

            return informes;
        }

        public static List<InformeInasistencia> GetAsistenciasNoFirmadas()
        {
            List<InformeInasistencia> informes = new List<InformeInasistencia>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  CPA.GNCodUsu as Documento, CPA.NOMUSUARIO as Nombre, CPCAPACITACION.TEMA As Temas, D.GnNomDep AS UnidaFuncional FROM CPASISTENCIA AS CPA"+
                                         "   LEFT JOIN CPMATRICULA ON CPMATRICULA.OidCPMATRICULA = CPA.OidCPMATRICULA"+
                                         "   LEFT JOIN CPCAPACITACION ON CPCAPACITACION.OidCPCAPACITACION = CPMATRICULA.OidCPCAPACITACION"+
                                         "   LEFT JOIN Usuario AS U ON U.GNCodUsu = CPA.GNCodUsu"+
                                         "   LEFT JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep"+
                                         "   WHERE CPA.FIRMADO = 0", conexion.OpenConnection());
                                            
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InformeInasistencia informeInasistencia = new InformeInasistencia
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntDocumento = Convert.ToInt32(reader["Documento"]),
                        SrtUnidadFuncional = reader["UnidaFuncional"].ToString()
                    };

                    informeInasistencia = InformeInasistencia.buscarCargarInasistencia(informes, informeInasistencia);
                    informeInasistencia.Temas.Add(reader["Temas"].ToString());

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

            return informes;
        }

        public static List<InformeAsistencia> GetInformeAsistencias()
        {
            List<InformeAsistencia> informes = new List<InformeAsistencia>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT C.RESPONSABLE, C.TEMA, NOMUSUARIO as NomUsuario, D.GnNomDep AS UnidadFuncional ,"+
                                         "   IIF((SELECT A.OidCPMATRICULA FROM CPASISTENCIA AS A WHERE A.OidCPMATRICULA = M.OidCPMATRICULA)IS NULL, 'NO', 'SI') AS Asistencia"+
                                         "   FROM CPMATRICULA AS M"+
                                         "   INNER JOIN CPCAPACITACION AS C ON C.OidCPCAPACITACION = M.OidCPCAPACITACION"+
                                         "   INNER JOIN Usuario AS U ON U.GNCodUsu = M.GNCodUsu"+
                                         "   INNER JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep", conexion.OpenConnection());
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    InformeAsistencia informe = new InformeAsistencia
                    {
                        StrTema = reader["Tema"].ToString(),
                        StrResponsable = reader["Responsable"].ToString(),
                    };

                    informe = InformeAsistencia.BuscarCargarInforme(informes, informe);
                    InformeAsistencia.InformacionInforme informacion = new InformeAsistencia.InformacionInforme
                    {
                        StrAsistencia = reader["Asistencia"].ToString(),
                        StrNomUsuario = reader["NomUsuario"].ToString(),
                        StrUnidadFuncional  = reader["UnidadFuncional"].ToString(),
                    };
                    informe.Informacion.Add(informacion);
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

            return informes;
        }
    }
}