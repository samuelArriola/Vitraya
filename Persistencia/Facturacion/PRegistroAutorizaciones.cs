
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
    public class PRegistroAutorizaciones
    {

        public static List<EBitacoraAutorizaciones> SetRegistroAutorizacion(string tipoIdentificacion, string numeroIdentificacion, string nombres, string numeroSolicitud, DateTime fechaSolicitud, string origenAtencion, string ubicacionPaciente, 
            string tipoServicio, DateTime fechaIngreso, string numIngreso, string prioridadAtencion,   string contratoPrestacion, string servicio, string numeroCama, string diagPrincipal, string diag1, string diag2, string IPS, 
            string direccion, string justificacionClinica, string clasificacionT, string cantidadT, string tecnologiaT, string profesionalSalud, string cargoProfesional)
        {

            List<EBitacoraAutorizaciones> infoGeneAut = new List<EBitacoraAutorizaciones>();
            string estado = "Pendiente";

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"INSERT INTO RegistroAutorizaciones([TipoIdentificacion], [Identificacion], [Nombres], [NumSolicitud], [FechaSolicitud], [OrigenAtencion], [TipoServicio], [PrioridadAtencion], [UbicacionPaciente]" +
                ",[FechaIngreso], [NumIngreso], [ContratoPrestacion], [Servicio], [NumCama], [DiagPrincipal], [DiagRel1], [DiagRel2], [NombreIPS], [DireccionIPS], [JustificacionClinica], [ProfesionalSolicita], [CargoProfesional], [Estado], [ClasifTecnologia], [NomTecnologia], [CantTecnologia])" +
                "VALUES(@tipoIdentificacion, @numeroIdentificacion, @nombres, @numeroSolicitud, @fechaSolicitud, @origenAtencion, @tipoServicio, @prioridadAtencion, @ubicacionPaciente, @fechaIngreso, @numIngreso, @contratoPrestacion," +
                "@servicio, @numeroCama, @diagPrincipal, @diag1, @diag2, @IPS, @direccion, @justificacionClinica, @profesionalSalud, @cargoProfesional, @estado, @clasificacionTecnologia, @nombreTecnologia, @cantidadTecnologia);" +
                "SELECT SCOPE_IDENTITY() AS 'ID'";

                command.Parameters.AddWithValue("tipoIdentificacion", tipoIdentificacion);
                command.Parameters.AddWithValue("numeroIdentificacion", numeroIdentificacion);
                command.Parameters.AddWithValue("nombres", nombres);
                command.Parameters.AddWithValue("numeroSolicitud", numeroSolicitud);
                command.Parameters.AddWithValue("fechaSolicitud", fechaSolicitud);
                command.Parameters.AddWithValue("origenAtencion", origenAtencion);
                command.Parameters.AddWithValue("tipoServicio", tipoServicio);
                command.Parameters.AddWithValue("prioridadAtencion", prioridadAtencion);
                command.Parameters.AddWithValue("ubicacionPaciente", ubicacionPaciente);
                command.Parameters.AddWithValue("fechaIngreso", fechaIngreso);
                command.Parameters.AddWithValue("numIngreso", numIngreso);
                command.Parameters.AddWithValue("contratoPrestacion", contratoPrestacion);
                command.Parameters.AddWithValue("servicio", servicio);
                command.Parameters.AddWithValue("numeroCama", numeroCama);
                command.Parameters.AddWithValue("diagPrincipal", diagPrincipal);
                command.Parameters.AddWithValue("diag1", diag1);
                command.Parameters.AddWithValue("diag2", diag2);
                command.Parameters.AddWithValue("IPS", IPS);
                command.Parameters.AddWithValue("direccion", direccion);
                command.Parameters.AddWithValue("justificacionClinica", justificacionClinica);
                command.Parameters.AddWithValue("profesionalSalud", profesionalSalud);
                command.Parameters.AddWithValue("cargoProfesional", cargoProfesional);
                command.Parameters.AddWithValue("estado", estado);
                command.Parameters.AddWithValue("clasificacionTecnologia", clasificacionT);
                command.Parameters.AddWithValue("nombreTecnologia", cantidadT);
                command.Parameters.AddWithValue("cantidadTecnologia", tecnologiaT);

                //command.ExecuteNonQuery();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoGeneAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["ID"].ToString())
                    };
                    infoGeneAut.Add(infoGeneAuti);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return infoGeneAut;
        }

        public static void SetRegistroTecnologias(int id, string clasificacion, string nombre, int cantidad)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO RegistroTecnologiasAutorizaciones (OidRegAutorizacion, Clasificacion, NombreTecnologia, Cantidad) " +
                "VALUES(@id, @clasificacion, @nombre, @cantidad)", conexion.OpenConnection());

                command.Parameters.AddWithValue("id", id);
                command.Parameters.AddWithValue("clasificacion", clasificacion);
                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("cantidad", cantidad);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static void SetRegistroDocumentoOrdenM(string Nombre, string Archivo, string Contenido, string Extension, int OidRegAutorizacion)
        {
            var tipo = "Orden";
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO GNArchivosFact (Nombre, Ext, Contenido, Archivo, OidRegAutorizacion, Tipo) " +
                "VALUES(@Nombre, @Extension, @Contenido, @Archivo, @OidRegAutorizacion, @Tipo)", conexion.OpenConnection());

                command.Parameters.AddWithValue("Nombre", Nombre);
                command.Parameters.AddWithValue("Archivo", Archivo);
                command.Parameters.AddWithValue("Contenido", Contenido);
                command.Parameters.AddWithValue("Extension", Extension);
                command.Parameters.AddWithValue("OidRegAutorizacion", OidRegAutorizacion);
                command.Parameters.AddWithValue("Tipo", tipo);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static List<ERegistroAutorizaciones> GetDatosDinamica(string numId)
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
                WHERE GENPACIEN.PACNUMDOC = @idPaciente AND ADNINGRESO.AINESTADO IN (0,1,3)
                GROUP BY GENPACIEN.PACNUMDOC,
                HPNGRUPOS.HGRNOMBRE,ADNINGRESO.AINFECING, GENSERIPS.SIPCODCUP, HPNESTANC.ADNINGRES,GENDIAGNO.DIACODIGO,
                GENDIAGNO.DIANOMBRE,GENPACIEN.PACTIPDOC,GENPACIEN.PACPRINOM, GENPACIEN.PACSEGNOM,GENPACIEN.PACPRIAPE,
                GENPACIEN.PACSEGAPE, HPNGRUPOS.OID,HPNDEFCAM.HCAOBSHOS,
                ADNINGRESO.AINCONSEC,HPNDEFCAM.HCACODIGO, GENCONTRA.GECCONTRA, GENCONTRA.GECNOMENT, NOTA19941.CLASIFICACION, BIPGPCLASI.NOMCLASIFICACION,
                HPNDEFCAM1.HCACODIGO, GENSERIPS.SIPCODCUP, GENSERIPS.SIPDESCUP,GENDIAGNO.DIACODIGO,GENDIAGNO.DIANOMBRE, NOTA19941.CUPS,NOTA19941.DESCRIPCUPS,
                GENPACIEN.GPAFECNAC, ADNINGRESO.AINFECING, GENPACIEN.GPASEXPAC, GENDETCON.GDECODIGO,GENDETCON.GDENOMBRE";

                command.Parameters.AddWithValue("idPaciente", numId);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ERegistroAutorizaciones infoItemDinamica = new ERegistroAutorizaciones
                    {
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

        public static List<ERegistroAutorizaciones> GetDatosDinamica2(string idPaciente, string idCups, string unidadF)
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
                    NOTA19941.CLASIFICACION as 'servicio',
                    BIPGPCLASI.NOMCLASIFICACION as 'nom servicio',
                    HPNDEFCAM1.HCACODIGO AS 'NUMERO CAMA',
                    GENSERIPS.SIPCODCUP AS CUPS,
                    GENSERIPS.SIPNOMBRE AS NOMBRECUPS,
                    GENSERIPS.SIPDESCUP AS 'SERVICIO',
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
                    WHERE GENPACIEN.PACNUMDOC = @idPaciente AND GENSERIPS.SIPCODCUP = @idCups AND HPNGRUPOS.HGRNOMBRE = @unidadF AND ADNINGRESO.AINESTADO IN (0,3) 
                    GROUP BY GENPACIEN.PACNUMDOC,
                    HPNGRUPOS.HGRNOMBRE,ADNINGRESO.AINFECING, GENSERIPS.SIPCODCUP, HPNESTANC.ADNINGRES,GENDIAGNO.DIACODIGO,
                    GENDIAGNO.DIANOMBRE,GENPACIEN.PACTIPDOC,GENPACIEN.PACPRINOM, GENPACIEN.PACSEGNOM,GENPACIEN.PACPRIAPE,
                    GENPACIEN.PACSEGAPE, HPNGRUPOS.OID,HPNDEFCAM.HCAOBSHOS,
                    ADNINGRESO.AINCONSEC,HPNDEFCAM.HCACODIGO, GENCONTRA.GECCONTRA, GENCONTRA.GECNOMENT, NOTA19941.CLASIFICACION, BIPGPCLASI.NOMCLASIFICACION,
                    HPNDEFCAM1.HCACODIGO, GENSERIPS.SIPCODCUP, GENSERIPS.SIPDESCUP,GENDIAGNO.DIACODIGO,GENDIAGNO.DIANOMBRE, NOTA19941.CUPS,NOTA19941.DESCRIPCUPS,
                    GENPACIEN.GPAFECNAC, ADNINGRESO.AINFECING, GENPACIEN.GPASEXPAC, GENDETCON.GDECODIGO,GENDETCON.GDENOMBRE, GENSERIPS.SIPNOMBRE
                    ORDER BY GENPACIEN.PACNUMDOC";

                command.Parameters.AddWithValue("idPaciente", idPaciente);
                command.Parameters.AddWithValue("idCups", idCups);
                command.Parameters.AddWithValue("unidadF", unidadF);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ERegistroAutorizaciones infoItemDinamica = new ERegistroAutorizaciones
                    {
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

        public static List<ERegistroAutorizaciones> GetListaCups()
        {

            List<ERegistroAutorizaciones> listaCups = new List<ERegistroAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT [CUPS], [DescripCUPS], [Clasificacion] FROM[dgempres01].[dbo].[Nota19941]";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ERegistroAutorizaciones InfoCup = new ERegistroAutorizaciones
                    {
                        NombreCUPS1 = reader["CUPS"].ToString(),
                        DescripCUPS1 = reader["DescripCUPS"].ToString(),
                        ClasificacionCUPS1 = reader["Clasificacion"].ToString()
                    };
                    listaCups.Add(InfoCup);
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
            return listaCups;
        }
    }
}