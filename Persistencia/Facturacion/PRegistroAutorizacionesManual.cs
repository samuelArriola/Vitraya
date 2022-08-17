using Entidades.Facturacion;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Facturacion
{
    public class PRegistroAutorizacionesManual
    {

        public static List<ERegistroAutorizaciones> GetDatosDinamica(string numIngreso)
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string nombreUsuario = usuario.GNNomUsu1;

            List<ERegistroAutorizaciones> infoDinamica = new List<ERegistroAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT(CASE GENPACIEN.PACTIPDOC WHEN '1' THEN 'CC' WHEN '2' THEN 'CE'
                WHEN '3' THEN 'TI' WHEN '4' THEN 'RC' WHEN '5' THEN 'PA' WHEN '6' THEN 'ASI'
                WHEN '7' THEN 'MSI' WHEN '8' THEN 'CNV' WHEN '9' THEN 'CC'
                WHEN '10' THEN 'CNV' WHEN '11' THEN 'SC' WHEN '12' THEN 'PEP'
                WHEN '13' THEN 'PE' END) AS DOC_TIPO,
                GENPACIEN.PACNUMDOC AS DOCUMENTO,
                (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                ' ' + GENPACIEN.PACSEGAPE) AS NOMPAC,
                'ENFERMEDAD GENERAL' AS 'ORIGEN ATENCIÓN',
                (CASE HPNGRUPOS.OID WHEN '1' THEN 'SERVICIO ELECTIVO'
                WHEN '3' THEN 'SERVICIOS ELECTIVOS' WHEN '3' THEN 'TI'
                WHEN '4' THEN 'SERVICIOS ELECTIVOS' WHEN '5' THEN 'POSTERIOR URGENCIAS'
                WHEN '6' THEN 'SERVICIOS ELECTIVOS' END) AS 'TIPO SERVICIO SOLICITADO',
                'PRIORITARIO' AS 'PRIORIDAD ATENCION',
                HPNGRUPOS.HGRNOMBRE AS 'UNIDAD FUNCIONAL',
                (CASE HPNDEFCAM.HCAOBSHOS WHEN '2' THEN 'URGENCIAS'
                WHEN '1' THEN 'HOSPITALIZACION' END) AS 'UBICACION PCIENTE',
                (ADNINGRESO.AINCONSEC)AS ADMISION,
                ADNINGRESO.AINFECING AS 'FEC INGRESO',
                GENCONTRA.GECCONTRA AS 'NRO CONTRATO',
                GENCONTRA.GECNOMENT AS 'CONTRATO PRESTACIONNOM CONTRATO',
                NOTA19941.CLASIFICACION as servicio,
                BIPGPCLASI.NOMCLASIFICACION as 'nom servicio',
                HPNDEFCAM1.HCACODIGO AS 'NUMERO CAMA',
                GENSERIPS.SIPCODCUP AS CUPS,
                GENSERIPS.SIPDESCUP AS 'NOMBRECUPS',
                GENDIAGNO.DIACODIGO AS 'DX PRINCIPAL',
                GENDIAGNO.DIANOMBRE AS 'NOM DIAGNOSTICO',
                NOTA19941.CUPS AS 'COD TECNOLOGIA',
                NOTA19941.DESCRIPCUPS AS 'TECNOLOGIAS EN SALUD',
                (DATEDIFF(YY, GENPACIEN.GPAFECNAC, ADNINGRESO.AINFECING)) AS EDAD,
                (CASE GENPACIEN.GPASEXPAC WHEN '1' THEN 'M' WHEN '2' THEN 'F' END) AS SEXO,
                GENDETCON.GDECODIGO AS 'PLAN BENEFICIO',
                GENDETCON.GDENOMBRE AS 'NOM PALN BENEFICIO',
                GENSERIPS.SIPCODCUP, HPNESTANC.ADNINGRES,GENDIAGNO.DIACODIGO,GENDIAGNO.DIANOMBRE,
                HPNDEFCAM.HCACODIGO,
                ADNINGRESO.AINCONSEC AS 'NUMERO DE INGRESO'
                FROM HPNESTANC
                INNER JOIN HPNDEFCAM ON HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM
                INNER JOIN ADNINGRESO ON ADNINGRESO.OID = HPNESTANC.ADNINGRES
                INNER JOIN GENDETCON ON GENDETCON.OID = ADNINGRESO.GENDETCON
                INNER JOIN GENPLAPRO ON GENPLAPRO.OID = GENDETCON.GENPLAPRO1
                INNER JOIN HPNMANUALCAM ON HPNDEFCAM.OID = HPNMANUALCAM.HPNDEFCAM AND
                GENPLAPRO.OID = HPNMANUALCAM.GENPLAPRO
                INNER JOIN GENSERIPS ON GENSERIPS.OID = HPNMANUALCAM.GENSERIPS
                FULL OUTER JOIN(SELECT MAX(HCNDIAPAC.GENDIAGNO) AS DXMAX,
                HCNFOLIO.ADNINGRESO AS OIDINGRESO
                FROM HCNDIAPAC
                INNER JOIN HCNFOLIO ON HCNFOLIO.OID = HCNDIAPAC.HCNFOLIO
                GROUP BY HCNFOLIO.ADNINGRESO) DX ON DX.OIDINGRESO = ADNINGRESO.OID
                INNER JOIN GENDIAGNO ON GENDIAGNO.OID = DX.DXMAX
                INNER JOIN GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                INNER JOIN HPNGRUPOS ON HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS
                INNER JOIN HPNSUBGRU ON HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU
                INNER JOIN GENCONTRA ON GENCONTRA.OID = GENDETCON.GENCONTRA1
                LEFT JOIN NOTA19941 ON NOTA19941.CUPS = GENSERIPS.SIPCODIGO
                LEFT JOIN BIPGPCLASI ON BIPGPCLASI.CODIGO = NOTA19941.CLASIFICACION
                LEFT JOIN HPNDEFCAM HPNDEFCAM1 ON HPNDEFCAM1.OID = ADNINGRESO.HPNDEFCAM
                WHERE ADNINGRESO.AINCONSEC = @numIngreso AND ADNINGRESO.AINESTADO IN (0,1,2,3,4)
                GROUP BY GENPACIEN.PACNUMDOC,
                HPNGRUPOS.HGRNOMBRE,ADNINGRESO.AINFECING, GENSERIPS.SIPCODCUP, HPNESTANC.ADNINGRES,GENDIAGNO.DIACODIGO,
                GENDIAGNO.DIANOMBRE,GENPACIEN.PACTIPDOC,GENPACIEN.PACPRINOM, GENPACIEN.PACSEGNOM,GENPACIEN.PACPRIAPE,
                GENPACIEN.PACSEGAPE, HPNGRUPOS.OID,HPNDEFCAM.HCAOBSHOS,
                ADNINGRESO.AINCONSEC,HPNDEFCAM.HCACODIGO, GENCONTRA.GECCONTRA, GENCONTRA.GECNOMENT, NOTA19941.CLASIFICACION, BIPGPCLASI.NOMCLASIFICACION,
                HPNDEFCAM1.HCACODIGO, GENSERIPS.SIPCODCUP, GENSERIPS.SIPDESCUP,GENDIAGNO.DIACODIGO,GENDIAGNO.DIANOMBRE, NOTA19941.CUPS,NOTA19941.DESCRIPCUPS,
                GENPACIEN.GPAFECNAC, ADNINGRESO.AINFECING, GENPACIEN.GPASEXPAC, GENDETCON.GDECODIGO,GENDETCON.GDENOMBRE";

                command.Parameters.AddWithValue("numIngreso", numIngreso);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ERegistroAutorizaciones infoItemDinamica = new ERegistroAutorizaciones
                    {
                        NumId = reader["DOCUMENTO"].ToString(),
                        TipoId = reader["DOC_TIPO"].ToString(),
                        Nombres = reader["NOMPAC"].ToString(),
                        OrigenAtencion = reader["ORIGEN ATENCIÓN"].ToString(),
                        TipoServiciosSolicitados = reader["TIPO SERVICIO SOLICITADO"].ToString(),
                        PrioridadAtencion = reader["PRIORIDAD ATENCION"].ToString(),
                        UbicacionPaciente = reader["UNIDAD FUNCIONAL"].ToString(),
                        NumContratoPrestacion = reader["NRO CONTRATO"].ToString(),
                        ContratoPrestacion = reader["CONTRATO PRESTACIONNOM CONTRATO"].ToString(),
                        NumServicio = reader["servicio"].ToString(),
                        Servicio = reader["nom servicio"].ToString(),
                        NumeroCama = reader["NUMERO CAMA"].ToString(),
                        NumDiagnosticoPrincipal = reader["DX PRINCIPAL"].ToString(),
                        DiagnosticoPrincipal = reader["NOM DIAGNOSTICO"].ToString(),
                        FechaHoraIngreso = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        NombreCUPS1 = reader["CUPS"].ToString(),
                        DescripCUPS1 = reader["NOMBRECUPS"].ToString(),
                        ClasificacionCUPS1 = reader["servicio"].ToString(),
                        NumeroIngreso = reader["NUMERO DE INGRESO"].ToString(),
                        NombreProfesional = nombreUsuario

                    };
                    infoDinamica.Add(infoItemDinamica);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoDinamica;
        }

    }
}