using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Servicios;
using Entidades.Generales;
using Persistencia.Generales;

namespace Persistencia.Servicios
{
    public class PDesprendibles
    {

        public static List<EDesprendibles> GetListaDesprendibles(DateTime fechaI, DateTime fechaF)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            string identificacion = Convert.ToString(usuario.GNCodUsu1);

            List<EDesprendibles> DesprendiblesMes = new List<EDesprendibles>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@" SELECT DISTINCT (NOMNOMINA.NOMFECHA ) AS 'FECHA NOMINA' 
                     FROM NOMNOMINAEC INNER JOIN NOMNOMINAE ON NOMNOMINAE.OID = NOMNOMINAEC.NOMNOMINAE INNER JOIN NOMNOMINA ON NOMNOMINA.OID = NOMNOMINAE.NOMNOMINA 
                     INNER JOIN NOMEMPLEADO ON NOMEMPLEADO.OID = NOMNOMINAE.NOMEMPLEA INNER JOIN NOMCARGO ON NOMCARGO.OID = NOMNOMINAE.NOMCARGO INNER JOIN NOMGRUPO ON NOMGRUPO.OID = NOMNOMINA.NOMGRUPO
                     INNER JOIN NOMSUBGRU ON NOMSUBGRU.OID = NOMNOMINAE.NOMSUBGRU LEFT JOIN NOMGRAPRO ON NOMGRAPRO.OID = NOMNOMINAE.NOMGRAPRO 
                     WHERE NOMNOMINA.NOMFECHA >= @fechaI AND NOMNOMINA.NOMFECHA <= @fechaF AND NOMEMPLEADO.EMPCODIGO = @identificacion";

                command.Parameters.AddWithValue("@fechaI", fechaI);
                command.Parameters.AddWithValue("@fechaF", fechaF);
                command.Parameters.AddWithValue("@identificacion", identificacion);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EDesprendibles DesprendibleMes = new EDesprendibles
                    {
                        DtFechaNomina = Convert.ToDateTime( reader["FECHA NOMINA"].ToString()),
                    };
                    DesprendiblesMes.Add(DesprendibleMes);
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
            return DesprendiblesMes;
        }

        public static List<EDesprendibles> GetDesprendibleByFecha(string fecha)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));
            string identificacion = Convert.ToString(usuario.GNCodUsu1);

            List<EDesprendibles> InfoDesprendible = new List<EDesprendibles>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {

                command.Connection = conexion2;
                command.CommandText = $@"SELECT NOMNOMINA.NOMFECHA AS 'FECHA NOMINA', NOMEMPLEADO.EMPCODIGO AS DOCUMENTO, " +
                    "(NOMEMPLEADO.EMPNOMBRE1 + ' ' + NOMEMPLEADO.EMPNOMBRE2 + ' ' + NOMEMPLEADO.EMPAPELLI1 + ' ' + NOMEMPLEADO.EMPAPELLI2) AS NOMBRE, NOMNOMINAEC.CONCODIGO AS CONCEPTO, " +
                    "NOMNOMINAEC.CONNOMBRE AS 'NOMBRE CONCEPTO', NOMNOMINAEC.CONCANTIDA AS CANTIDAD, NOMNOMINAEC.CONVALDEV AS DEVENGADO, " +
                    "NOMNOMINAEC.CONVALDED AS DEDUCCION, NOMCARGO.CANOMBRE AS CARGO, NOMNOMINAE.NEMSUELBAS AS 'SUELDO BASICO', NOMGRUPO.GRUNOMBRE AS GRUPO, " +
                    "NOMSUBGRU.SUBNOMBRE AS SUBGRUPO, NOMGRAPRO.GPNOMBRE AS 'GRADO PROFESIONAL' " +
                    "FROM NOMNOMINAEC INNER JOIN NOMNOMINAE ON NOMNOMINAE.OID = NOMNOMINAEC.NOMNOMINAE INNER JOIN NOMNOMINA ON NOMNOMINA.OID = NOMNOMINAE.NOMNOMINA " +
                    "INNER JOIN NOMEMPLEADO ON NOMEMPLEADO.OID = NOMNOMINAE.NOMEMPLEA INNER JOIN NOMCARGO ON NOMCARGO.OID = NOMNOMINAE.NOMCARGO " +
                    "INNER JOIN NOMGRUPO ON NOMGRUPO.OID = NOMNOMINA.NOMGRUPO INNER JOIN NOMSUBGRU ON NOMSUBGRU.OID = NOMNOMINAE.NOMSUBGRU " +
                    "LEFT JOIN NOMGRAPRO ON NOMGRAPRO.OID = NOMNOMINAE.NOMGRAPRO WHERE CAST(NOMNOMINA.NOMFECHA AS date) = @fecha " +
                    "AND NOMEMPLEADO.EMPCODIGO = @identificacion";

                command.Parameters.AddWithValue("@fecha", fecha);
                command.Parameters.AddWithValue("@identificacion", identificacion);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EDesprendibles desprendible = new EDesprendibles()
                    {
                        DtFechaNomina = Convert.ToDateTime( reader["FECHA NOMINA"]),
                        StrEmpleado = reader["NOMBRE"].ToString(),
                        StrIdentificacion = reader["DOCUMENTO"].ToString(),
                        StrCodigoConcepto = reader["CONCEPTO"].ToString(),
                        StrNombreConcepto = reader["NOMBRE CONCEPTO"].ToString(),
                        FloatDevengado = float.Parse(reader["DEVENGADO"].ToString()),
                        FloatDeduccion = float.Parse(reader["DEDUCCION"].ToString()),
                        StrCargo = reader["CARGO"].ToString(),
                        FloatSueldo = float.Parse(reader["SUELDO BASICO"].ToString()),
                        FloatCantidad = float.Parse(reader["CANTIDAD"].ToString()),
                        StrGrado = reader["GRADO PROFESIONAL"].ToString(),

                    };
                    InfoDesprendible.Add(desprendible);
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
            return InfoDesprendible;
        }

        public static List<EDesprendibles> GetVisiOpcion()
        {

            List<EDesprendibles> infoValidaciones = new List<EDesprendibles>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT Oid_Config, Nombre_Config, EstadoValor FROM ConfiguracionesGenerales WHERE Oid_Config = 1", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EDesprendibles infoValidacion = new EDesprendibles
                    {
                        Oid_Config = Convert.ToInt32(reader["Oid_Config"].ToString()),
                        Nombre_Config = reader["Nombre_Config"].ToString(),
                        EstadoValorConfig = reader["EstadoValor"].ToString()
                    };
                    infoValidaciones.Add(infoValidacion);
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
            return infoValidaciones;
        }

        public static void SetActivacion()
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update GNPermisos set Eliminar = 0, Crear = 0, Confirmar = 1, Modificar = 0 where OidGNOpcion = 4046 AND (OidRol <> 1 AND OidRol <> 4037);" +
                                         "update ConfiguracionesGenerales set EstadoValor = '1' where Oid_Config = 1;", conexion.OpenConnection());

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

        public static void SetInactivacion()
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update GNPermisos set Eliminar = 0, Crear = 0, Confirmar = 0, Modificar = 0 where OidGNOpcion = 4046 AND (OidRol <> 1 AND OidRol <> 4037);" +
                                         "update ConfiguracionesGenerales set EstadoValor = '0' where Oid_Config = 1;", conexion.OpenConnection());

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

    }
}