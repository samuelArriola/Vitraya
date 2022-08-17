using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;
using Entidades.Servicios;
using Persistencia.Generales;

namespace Persistencia.Servicios
{
    public class PCertificadoLaboral
    {

        public static List<ECertificadoLaboral> GetDatosCertificado()
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            string identificacion = Convert.ToString(usuario.GNCodUsu1);
            DateTime fechaVacia = DateTime.Now;

            List<ECertificadoLaboral> informacionUsuario = new List<ECertificadoLaboral>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT (CASE NOMINFOLAB.ILTIPESTEM WHEN '0' THEN 'ACTIVO' WHEN '1' THEN 'RETIRADO' END) AS ESTADO, 
                    (CASE NOMINFOLAB.ILTIPCONT WHEN '0' THEN 'INDEFINIDO' WHEN '1' THEN 'FIJO' END) AS TP_CONTRATO, NOMEMPLEADO.EMPCODIGO AS DOCUMENTO, 
                    (NOMEMPLEADO.EMPNOMBRE1 + ' ' + NOMEMPLEADO.EMPNOMBRE2 + ' ' + NOMEMPLEADO.EMPAPELLI1 + ' ' + NOMEMPLEADO.EMPAPELLI2) AS NOMBRE, 
                    (DATEDIFF(YY, NOMINFOBAS.IBFECNACI, GETDATE())) AS EDAD, (CASE NOMINFOBAS.IBGENERO WHEN '0' THEN 'F' WHEN '1' THEN 'M' END) AS SEXO, 
                    CAST(NOMINFOLAB.ILFECINGRE AS date) AS 'FEC INGRESO', NOMGRUPO.GRUNOMBRE AS GRUPO, NOMSUBGRU.SUBNOMBRE AS SUBGRUPO, CO.INFINFORMA AS CORREO, 
                    TEL.TELNUMERO AS TELEFONO, DIR.DIRDIRECCION AS DIRECCION, FORMAT(NOMINFOBAS.IBFECNACI, 'MM-DD') AS CUMPLE_MES_DIA, ACA.TITULO AS TITULO, 
                    CAST(NOMINFOLAB.ILFECRETIR AS date) AS FECHA_RETIRO, CA.CANOMBRE, NOMSALARIO.SALVALOR FROM NOMEMPLEADO INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, Z.CARGO, NOMCARGO.CANOMBRE, NOMCARGO.CACODIGO AS CODIGOCARGO 
                    FROM NOMEMPLEADO LEFT JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA LEFT JOIN NOMINLACAR ON NOMINFOLAB.OID = NOMINLACAR.NOMINFOLAB 
                    LEFT JOIN NOMCARGO ON NOMCARGO.OID = NOMINLACAR.NOMCARGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGO, MAX(NOMINLACAR.OID) AS CARGO 
                    FROM NOMEMPLEADO LEFT JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA 
                    LEFT JOIN NOMINLACAR ON NOMINFOLAB.OID = NOMINLACAR.NOMINFOLAB LEFT JOIN NOMCARGO ON NOMCARGO.OID = NOMINLACAR.NOMCARGO WHERE NOMINFOLAB.ILTIPESTEM = 0 
                    GROUP BY NOMEMPLEADO.EMPCODIGO) Z ON Z.CODIGO = NOMEMPLEADO.EMPCODIGO AND Z.CARGO = NOMINLACAR.OID WHERE Z.CARGO > 1 AND NOMINFOLAB.ILTIPESTEM = 0) CA ON CA.EMPCODIGO = 
                    NOMEMPLEADO.EMPCODIGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROWEB.INFINFORMA, 
                    COMTERCEROWEB.OID, C.CORREO FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN COMTERCEROWEB ON COMTERCERO.OID = COMTERCEROWEB.COMTERCERO 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOC, MAX(COMTERCEROWEB.OID) AS CORREO 
                    FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN COMTERCEROWEB ON COMTERCERO.OID = COMTERCEROWEB.COMTERCERO 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO) C ON C.CODIGOC = NOMEMPLEADO.EMPCODIGO AND 
                    C.CORREO = COMTERCEROWEB.OID WHERE NOMINFOLAB.ILTIPESTEM = 0 AND C.CORREO > 1 GROUP BY NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, 
                    NOMEMPLEADO.EMPNOMBRE2,  COMTERCEROWEB.INFINFORMA, COMTERCEROWEB.OID, C.CORREO) CO ON CO.EMPCODIGO = NOMEMPLEADO.EMPCODIGO 
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, T.OIDTELEFONO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROTEL.TELNUMERO FROM NOMEMPLEADO 
                    INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA INNER JOIN COMTERCEROTEL ON COMTERCERO.OID = COMTERCEROTEL.COMTERCERO 
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOT, MAX(COMTERCEROTEL.OID) AS OIDTELEFONO FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA INNER JOIN COMTERCEROTEL ON COMTERCERO.OID = COMTERCEROTEL.COMTERCERO 
                    WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO) T ON T.CODIGOT = NOMEMPLEADO.EMPCODIGO AND T.OIDTELEFONO = COMTERCEROTEL.OID 
                    WHERE NOMINFOLAB.ILTIPESTEM = 0 AND T.OIDTELEFONO > 1 GROUP BY NOMEMPLEADO.EMPCODIGO, T.OIDTELEFONO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROTEL.TELNUMERO, 
                    COMTERCEROTEL.OID) TEL ON TEL.EMPCODIGO = NOMEMPLEADO.EMPCODIGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOD, MAX(COMTERCERODIR.OID) AS OIDDIRECCION, 
                    COMTERCERODIR.DIRDIRECCION FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA 
                    INNER JOIN COMTERCERODIR ON COMTERCERO.OID = COMTERCERODIR.COMTERCERO WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO, COMTERCERODIR.DIRDIRECCION, 
                    COMTERCERODIR.OID) DIR ON DIR.CODIGOD = NOMEMPLEADO.EMPCODIGO INNER JOIN NOMGRUPO ON NOMGRUPO.OID = NOMEMPLEADO.NOMGRUPO INNER JOIN NOMSUBGRU ON NOMSUBGRU.OID = NOMEMPLEADO.NOMSUBGRU 
                    INNER JOIN NOMINFOBAS ON NOMINFOBAS.OID = NOMEMPLEADO.NOMEMPLINF AND NOMEMPLEADO.OID = NOMINFOBAS.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS COD_EMPLEADO, 
                    NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, (CASE NOMINFOACA.IAMODACAD WHEN '0' THEN 'PRIMARIA' WHEN '1' THEN 'SECUNDARIA' WHEN '2' THEN 'MEDIA' WHEN '3' THEN 'TECNICA'  
                    WHEN '4' THEN 'TECNOLOGIA' WHEN '5' THEN 'TEC_ESPECIALIZADA' WHEN '6' THEN 'UNIVERSITARIA' WHEN '7' THEN 'ESPECIALIZACION' WHEN '8' THEN 'MAESTRIA' WHEN '9' THEN 'DOCTORADO' 
                    WHEN '10' THEN 'CAPACITACION' WHEN '11' THEN 'SENA' WHEN '12' THEN 'CURSOS' WHEN '13' THEN 'DIPLOMADO' WHEN '14' THEN 'EST_NO_FOMAL' WHEN '15' THEN 'SIMPOSIO' WHEN '16' THEN 'TALLERES' WHEN '17' THEN 'OTROS' END) AS TITULO, 
                    TI.OIDACA FROM NOMEMPLEADO LEFT JOIN NOMINFOAFI ON NOMEMPLEADO.OID = NOMINFOAFI.NOMEMPLEA LEFT JOIN NOMINFOACA ON NOMEMPLEADO.OID = NOMINFOACA.NOMEMPLEA 
                    INNER JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS COD_EMPLEADO, 
                    MAX(NOMINFOACA.OID) AS OIDACA FROM NOMEMPLEADO LEFT JOIN NOMINFOAFI ON NOMEMPLEADO.OID = NOMINFOAFI.NOMEMPLEA LEFT JOIN NOMINFOACA ON NOMEMPLEADO.OID = NOMINFOACA.NOMEMPLEA 
                    INNER JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA WHERE NOMINFOLAB.ILTIPESTEM = 0 
                    GROUP BY NOMEMPLEADO.EMPCODIGO) TI ON TI.COD_EMPLEADO =  NOMEMPLEADO.EMPCODIGO AND TI.OIDACA = NOMINFOACA.OID WHERE TI.OIDACA > 1 AND NOMINFOLAB.ILTIPESTEM = 0) ACA ON ACA.COD_EMPLEADO = 
                    NOMEMPLEADO.EMPCODIGO INNER JOIN NOMSALARIO ON NOMSALARIO.OID = NOMEMPLEADO.NOMSALARIO AND NOMINFOLAB.OID = NOMSALARIO.NOMINFOLAB WHERE NOMINFOLAB.ILTIPESTEM = 0 AND NOMEMPLEADO.EMPCODIGO = @identificacion 
                    GROUP BY NOMEMPLEADO.EMPCODIGO, NOMINFOLAB.ILFECINGRE, NOMGRUPO.GRUNOMBRE, NOMSUBGRU.SUBNOMBRE, CO.INFINFORMA, TEL.TELNUMERO, DIR.DIRDIRECCION, ACA.TITULO, 
                    NOMINFOLAB.ILFECRETIR, CA.CANOMBRE, NOMSALARIO.SALVALOR, NOMINFOLAB.ILTIPESTEM, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, 
                    NOMEMPLEADO.EMPAPELLI1, NOMEMPLEADO.EMPAPELLI2, NOMINFOBAS.IBFECNACI, NOMGRUPO.GRUCODIGO, NOMSUBGRU.SUBCODIGO, CA.CODIGOCARGO, NOMINFOBAS.IBGENERO, NOMINFOLAB.ILTIPCONT ORDER BY 2";

                command.Parameters.AddWithValue("@identificacion", identificacion);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral informacion = new ECertificadoLaboral
                    {
                        StrEstado = reader["ESTADO"].ToString(),
                        StrNombre = reader["NOMBRE"].ToString(),
                        StrIdentificacion = reader["DOCUMENTO"].ToString(),
                        StrCargo = reader["CANOMBRE"].ToString(),
                        StrTipoContrato = reader["TP_CONTRATO"].ToString(),
                        FloatSalario = float.Parse(reader["SALVALOR"].ToString()),
                        DtFechaVinculacion = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        DtFechaRetiro = (reader["FECHA_RETIRO"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA_RETIRO"].ToString()),
                    };
                    informacionUsuario.Add(informacion);
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
            return informacionUsuario;
        }

        public static List<ECertificadoLaboral> GetDatosCertificadoById(string id)
        {
            string identificacion = id;
            DateTime fechaVacia = DateTime.Now;

            List<ECertificadoLaboral> informacionUsuario = new List<ECertificadoLaboral>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT (CASE NOMINFOLAB.ILTIPESTEM WHEN '0' THEN 'ACTIVO' WHEN '1' THEN 'RETIRADO' END) AS ESTADO, 
                    (CASE NOMINFOLAB.ILTIPCONT WHEN '0' THEN 'INDEFINIDO' WHEN '1' THEN 'FIJO' END) AS TP_CONTRATO, NOMEMPLEADO.EMPCODIGO AS DOCUMENTO, 
                    (NOMEMPLEADO.EMPNOMBRE1 + ' ' + NOMEMPLEADO.EMPNOMBRE2 + ' ' + NOMEMPLEADO.EMPAPELLI1 + ' ' + NOMEMPLEADO.EMPAPELLI2) AS NOMBRE, 
                    (DATEDIFF(YY, NOMINFOBAS.IBFECNACI, GETDATE())) AS EDAD, (CASE NOMINFOBAS.IBGENERO WHEN '0' THEN 'F' WHEN '1' THEN 'M' END) AS SEXO, 
                    CAST(NOMINFOLAB.ILFECINGRE AS date) AS 'FEC INGRESO', NOMGRUPO.GRUNOMBRE AS GRUPO, NOMSUBGRU.SUBNOMBRE AS SUBGRUPO, CO.INFINFORMA AS CORREO, 
                    TEL.TELNUMERO AS TELEFONO, DIR.DIRDIRECCION AS DIRECCION, FORMAT(NOMINFOBAS.IBFECNACI, 'MM-DD') AS CUMPLE_MES_DIA, ACA.TITULO AS TITULO, 
                    CAST(NOMINFOLAB.ILFECRETIR AS date) AS FECHA_RETIRO, CA.CANOMBRE, NOMSALARIO.SALVALOR FROM NOMEMPLEADO INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA 
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, Z.CARGO, NOMCARGO.CANOMBRE, NOMCARGO.CACODIGO AS CODIGOCARGO 
                    FROM NOMEMPLEADO LEFT JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA LEFT JOIN NOMINLACAR ON NOMINFOLAB.OID = NOMINLACAR.NOMINFOLAB 
                    LEFT JOIN NOMCARGO ON NOMCARGO.OID = NOMINLACAR.NOMCARGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGO, MAX(NOMINLACAR.OID) AS CARGO 
                    FROM NOMEMPLEADO LEFT JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA 
                    LEFT JOIN NOMINLACAR ON NOMINFOLAB.OID = NOMINLACAR.NOMINFOLAB LEFT JOIN NOMCARGO ON NOMCARGO.OID = NOMINLACAR.NOMCARGO 
                    GROUP BY NOMEMPLEADO.EMPCODIGO) Z ON Z.CODIGO = NOMEMPLEADO.EMPCODIGO AND Z.CARGO = NOMINLACAR.OID WHERE Z.CARGO > 1) CA ON CA.EMPCODIGO = 
                    NOMEMPLEADO.EMPCODIGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROWEB.INFINFORMA, 
                    COMTERCEROWEB.OID, C.CORREO FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN COMTERCEROWEB ON COMTERCERO.OID = COMTERCEROWEB.COMTERCERO 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOC, MAX(COMTERCEROWEB.OID) AS CORREO 
                    FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN COMTERCEROWEB ON COMTERCERO.OID = COMTERCEROWEB.COMTERCERO 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO) C ON C.CODIGOC = NOMEMPLEADO.EMPCODIGO AND 
                    C.CORREO = COMTERCEROWEB.OID WHERE NOMINFOLAB.ILTIPESTEM = 0 AND C.CORREO > 1 GROUP BY NOMEMPLEADO.EMPCODIGO, NOMEMPLEADO.EMPNOMBRE1, 
                    NOMEMPLEADO.EMPNOMBRE2,  COMTERCEROWEB.INFINFORMA, COMTERCEROWEB.OID, C.CORREO) CO ON CO.EMPCODIGO = NOMEMPLEADO.EMPCODIGO 
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO, T.OIDTELEFONO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROTEL.TELNUMERO FROM NOMEMPLEADO 
                    INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA INNER JOIN COMTERCEROTEL ON COMTERCERO.OID = COMTERCEROTEL.COMTERCERO 
                    FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOT, MAX(COMTERCEROTEL.OID) AS OIDTELEFONO FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID 
                    INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA INNER JOIN COMTERCEROTEL ON COMTERCERO.OID = COMTERCEROTEL.COMTERCERO 
                    WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO) T ON T.CODIGOT = NOMEMPLEADO.EMPCODIGO AND T.OIDTELEFONO = COMTERCEROTEL.OID 
                    WHERE NOMINFOLAB.ILTIPESTEM = 0 AND T.OIDTELEFONO > 1 GROUP BY NOMEMPLEADO.EMPCODIGO, T.OIDTELEFONO, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, COMTERCEROTEL.TELNUMERO, 
                    COMTERCEROTEL.OID) TEL ON TEL.EMPCODIGO = NOMEMPLEADO.EMPCODIGO FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS CODIGOD, MAX(COMTERCERODIR.OID) AS OIDDIRECCION, 
                    COMTERCERODIR.DIRDIRECCION FROM NOMEMPLEADO INNER JOIN COMTERCERO ON NOMEMPLEADO.OID = COMTERCERO.OID INNER JOIN NOMINFOLAB ON NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA 
                    INNER JOIN COMTERCERODIR ON COMTERCERO.OID = COMTERCERODIR.COMTERCERO WHERE NOMINFOLAB.ILTIPESTEM = 0 GROUP BY NOMEMPLEADO.EMPCODIGO, COMTERCERODIR.DIRDIRECCION, 
                    COMTERCERODIR.OID) DIR ON DIR.CODIGOD = NOMEMPLEADO.EMPCODIGO INNER JOIN NOMGRUPO ON NOMGRUPO.OID = NOMEMPLEADO.NOMGRUPO INNER JOIN NOMSUBGRU ON NOMSUBGRU.OID = NOMEMPLEADO.NOMSUBGRU 
                    INNER JOIN NOMINFOBAS ON NOMINFOBAS.OID = NOMEMPLEADO.NOMEMPLINF AND NOMEMPLEADO.OID = NOMINFOBAS.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS COD_EMPLEADO, 
                    NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, (CASE NOMINFOACA.IAMODACAD WHEN '0' THEN 'PRIMARIA' WHEN '1' THEN 'SECUNDARIA' WHEN '2' THEN 'MEDIA' WHEN '3' THEN 'TECNICA'  
                    WHEN '4' THEN 'TECNOLOGIA' WHEN '5' THEN 'TEC_ESPECIALIZADA' WHEN '6' THEN 'UNIVERSITARIA' WHEN '7' THEN 'ESPECIALIZACION' WHEN '8' THEN 'MAESTRIA' WHEN '9' THEN 'DOCTORADO' 
                    WHEN '10' THEN 'CAPACITACION' WHEN '11' THEN 'SENA' WHEN '12' THEN 'CURSOS' WHEN '13' THEN 'DIPLOMADO' WHEN '14' THEN 'EST_NO_FOMAL' WHEN '15' THEN 'SIMPOSIO' WHEN '16' THEN 'TALLERES' WHEN '17' THEN 'OTROS' END) AS TITULO, 
                    TI.OIDACA FROM NOMEMPLEADO LEFT JOIN NOMINFOAFI ON NOMEMPLEADO.OID = NOMINFOAFI.NOMEMPLEA LEFT JOIN NOMINFOACA ON NOMEMPLEADO.OID = NOMINFOACA.NOMEMPLEA 
                    INNER JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA FULL OUTER JOIN(SELECT NOMEMPLEADO.EMPCODIGO AS COD_EMPLEADO, 
                    MAX(NOMINFOACA.OID) AS OIDACA FROM NOMEMPLEADO LEFT JOIN NOMINFOAFI ON NOMEMPLEADO.OID = NOMINFOAFI.NOMEMPLEA LEFT JOIN NOMINFOACA ON NOMEMPLEADO.OID = NOMINFOACA.NOMEMPLEA 
                    INNER JOIN NOMINFOLAB ON NOMINFOLAB.OID = NOMEMPLEADO.NOMINFOLAB AND NOMEMPLEADO.OID = NOMINFOLAB.NOMEMPLEA WHERE NOMINFOLAB.ILTIPESTEM = 0 
                    GROUP BY NOMEMPLEADO.EMPCODIGO) TI ON TI.COD_EMPLEADO =  NOMEMPLEADO.EMPCODIGO AND TI.OIDACA = NOMINFOACA.OID WHERE TI.OIDACA > 1 AND NOMINFOLAB.ILTIPESTEM = 0) ACA ON ACA.COD_EMPLEADO = 
                    NOMEMPLEADO.EMPCODIGO INNER JOIN NOMSALARIO ON NOMSALARIO.OID = NOMEMPLEADO.NOMSALARIO AND NOMINFOLAB.OID = NOMSALARIO.NOMINFOLAB WHERE NOMEMPLEADO.EMPCODIGO = @identificacion 
                    GROUP BY NOMEMPLEADO.EMPCODIGO, NOMINFOLAB.ILFECINGRE, NOMGRUPO.GRUNOMBRE, NOMSUBGRU.SUBNOMBRE, CO.INFINFORMA, TEL.TELNUMERO, DIR.DIRDIRECCION, ACA.TITULO, 
                    NOMINFOLAB.ILFECRETIR, CA.CANOMBRE, NOMSALARIO.SALVALOR, NOMINFOLAB.ILTIPESTEM, NOMEMPLEADO.EMPNOMBRE1, NOMEMPLEADO.EMPNOMBRE2, 
                    NOMEMPLEADO.EMPAPELLI1, NOMEMPLEADO.EMPAPELLI2, NOMINFOBAS.IBFECNACI, NOMGRUPO.GRUCODIGO, NOMSUBGRU.SUBCODIGO, CA.CODIGOCARGO, NOMINFOBAS.IBGENERO, NOMINFOLAB.ILTIPCONT ORDER BY 2";

                command.Parameters.AddWithValue("@identificacion", identificacion);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral informacion = new ECertificadoLaboral
                    {
                        StrEstado = reader["ESTADO"].ToString(),
                        StrNombre = reader["NOMBRE"].ToString(),
                        StrIdentificacion = reader["DOCUMENTO"].ToString(),
                        StrCargo = reader["CANOMBRE"].ToString(),
                        StrTipoContrato = reader["TP_CONTRATO"].ToString(),
                        FloatSalario = float.Parse(reader["SALVALOR"].ToString()),
                        DtFechaVinculacion = Convert.ToDateTime(reader["FEC INGRESO"].ToString()),
                        DtFechaRetiro = (reader["FECHA_RETIRO"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FECHA_RETIRO"].ToString()),
                    };
                    informacionUsuario.Add(informacion);
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
            return informacionUsuario;
        }

        public static List<ECertificadoLaboral> GetEmpleados()
        {

            List<ECertificadoLaboral> empleados = new List<ECertificadoLaboral>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT EMPCODIGO AS DOCUMENTO, 
                    (EMPNOMBRE1 + ' ' + EMPNOMBRE2 + ' ' + EMPAPELLI1 + ' ' + EMPAPELLI2) AS NOMBRE 
                    FROM NOMEMPLEADO";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral empleado = new ECertificadoLaboral
                    {
                        StrNombre = reader["NOMBRE"].ToString(),
                        StrIdentificacion = reader["DOCUMENTO"].ToString(),
                    };
                    empleados.Add(empleado);
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
            return empleados;
        }

        public static List<ECertificadoLaboral> GetHistorico()
        {

            List<ECertificadoLaboral> historicos = new List<ECertificadoLaboral>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(" SELECT * FROM HCertificadosLaborales ORDER BY Fecha DESC", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral historico = new ECertificadoLaboral
                    {
                        StrIdHistorico = reader["Oid_historico"].ToString(),
                        StrIdentificacion = reader["Oid_usuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrAccionHistorico = reader["Accion"].ToString(),
                        DtFechaHistorico = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
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
            return historicos;
        }

        public static List<ECertificadoLaboral> GetHistoricoFiltro1( string fechaI, string fechaF)
        {

            List<ECertificadoLaboral> historicos = new List<ECertificadoLaboral>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM HCertificadosLaborales WHERE Fecha >= @fechaInicial AND fecha < @fechaFinal ORDER BY Fecha ASC", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("fechaInicial", fechaI);
                command.Parameters.AddWithValue("fechaFinal", fechaF);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral historico = new ECertificadoLaboral
                    {
                        StrIdHistorico = reader["Oid_historico"].ToString(),
                        StrIdentificacion = reader["Oid_usuario"].ToString(),
                        StrNombre = reader["Nombre"].ToString(),
                        StrAccionHistorico = reader["Accion"].ToString(),
                        DtFechaHistorico = Convert.ToDateTime(reader["Fecha"].ToString()),
                    };
                    historicos.Add(historico);
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
            return historicos;
        }

        public static List<ECertificadoLaboral> GetHistoricoFiltro2(string fechaI, string fechaF)
        {

            List<ECertificadoLaboral> historicos = new List<ECertificadoLaboral>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT YEAR(Fecha) AS ANIO, MONTH(Fecha) AS MES, COUNT(Accion) AS TOTAL
                            FROM HCertificadosLaborales
                            WHERE Fecha >= @fechaInicial AND fecha < @fechaFinal
                            GROUP BY YEAR(Fecha), MONTH(Fecha)
                            ORDER BY YEAR(Fecha), MONTH(Fecha) ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("fechaInicial", fechaI);
                command.Parameters.AddWithValue("fechaFinal", fechaF);
                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral historico = new ECertificadoLaboral
                    {
                        StrAnio = reader["ANIO"].ToString(),
                        StrMes = reader["MES"].ToString(),
                        StrTotal = reader["TOTAL"].ToString(),
                    };
                    historicos.Add(historico);
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
            return historicos;
        }

        public static void SetHistorico(string accion)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            var identificacion = Convert.ToString(usuario.GNCodUsu1);
            var nombre = usuario.GNNomUsu1;
            var fecha = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")); 

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(" INSERT INTO HCertificadosLaborales (Oid_usuario, Nombre, Accion, Fecha) " +
                    "VALUES( @Oid_usuario, @Nombre, @Accion, @Fecha ); ", conexion.OpenConnection());

                command.Parameters.AddWithValue("Oid_usuario", identificacion);
                command.Parameters.AddWithValue("Nombre", nombre);
                command.Parameters.AddWithValue("Accion", accion);
                command.Parameters.AddWithValue("Fecha", fecha);

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

        public static List<ECertificadoLaboral> getDatosFirma()
        {

            List<ECertificadoLaboral> datosFirma = new List<ECertificadoLaboral>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM ConfigCertificadosLaborales", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral datoFirma = new ECertificadoLaboral
                    {
                        StrIdentificacion = reader["OidUsuario"].ToString(),
                        StrNombre = reader["NombreUsuario"].ToString(),
                        StrCargo = reader["CargoUsuario"].ToString(),
                        BtFirma = (reader["FirmaUsuario"].ToString() == "") ? new byte[0] : (byte[])reader["FirmaUsuario"],
                        Firmabase64 = Convert.ToBase64String((reader["FirmaUsuario"].ToString() == "") ? new byte[0] : (byte[])reader["FirmaUsuario"])
                    };
                    datosFirma.Add(datoFirma);
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
            return datosFirma;
        }

        public static List<ECertificadoLaboral> GetInfoUsuFirma(string identificacion)
        {

            List<ECertificadoLaboral> datosFirma = new List<ECertificadoLaboral>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GNNomUsu, GNCodUsu, GNFmUsu, GnCargo FROM Usuario WHERE GNCodUsu = @identificacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("identificacion", identificacion);

                reader = command.ExecuteReader();

                while (reader.Read())
                {

                    ECertificadoLaboral datoFirma = new ECertificadoLaboral
                    {
                        StrIdentificacion = reader["GNCodUsu"].ToString(),
                        StrNombre = reader["GNNomUsu"].ToString(),
                        StrCargo = reader["GnCargo"].ToString(),
                        BtFirma = (reader["GNFmUsu"] == DBNull.Value) ? new byte[0] : (byte[])reader["GNFmUsu"],
                        Firmabase64 = Convert.ToBase64String((reader["GNFmUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFmUsu"]),
                    };
                    datosFirma.Add(datoFirma);
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
            return datosFirma;
        }

        public static void UpdateUsuFirma(string identificacion, string nombre, byte[] firma, string cargo)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(" UPDATE ConfigCertificadosLaborales SET OidUsuario = @identificacion, " +
                    "NombreUsuario = @nombre, FirmaUsuario = @firma , CargoUsuario = @cargo WHERE  OidConfig = 1; ", conexion.OpenConnection());

                command.Parameters.AddWithValue("identificacion", identificacion);
                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("firma", firma);
                command.Parameters.AddWithValue("cargo", cargo);

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