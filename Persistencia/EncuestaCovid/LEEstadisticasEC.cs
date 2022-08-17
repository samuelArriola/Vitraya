using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.EncuestaCovid;


namespace Persistencia.EncuestaCovid
{
    public class LEEstadisticasEC
    {

        public static List<EEstadisticasEC> GetEstadisticasMes(string MesI, string MesF)
        {

            string mesInicial = MesI;
            string mesFinal = MesF;

            List<EEstadisticasEC> estadisticasMes = new List<EEstadisticasEC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT YEAR(convert(date,FECHA_DIARIA,103)) ANIO, MONTH(convert(date,FECHA_DIARIA,103)) MES, COUNT( ID ) CANT " +
                "FROM CVENCUESTA_DIARIA WHERE convert(date, FECHA_DIARIA, 103) BETWEEN @MesInicial AND @MesFinal " +
                "GROUP BY YEAR(convert(date, FECHA_DIARIA, 103)), MONTH(convert(date, FECHA_DIARIA, 103)) ORDER BY ANIO, MES ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("@MesInicial", mesInicial);
                command.Parameters.AddWithValue("@MesFinal", mesFinal);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EEstadisticasEC EstadisticaMes = new EEstadisticasEC
                    {
                        IntMes = Convert.ToInt32(reader["MES"].ToString()),
                        IntCantMes = Convert.ToInt32(reader["CANT"].ToString()),
                        IntAñoMes = Convert.ToInt32(reader["ANIO"].ToString()),
                    };
                    estadisticasMes.Add(EstadisticaMes);
                }
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return estadisticasMes;
        }

        public static List<EEstadisticasEC> GetEstadisticasDias(string DiaI, string DiaF)
        {

            string diaInicial = DiaI;
            string diaFinal = DiaF;

            List<EEstadisticasEC> estadisticasDias = new List<EEstadisticasEC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT YEAR(convert(date,FECHA_DIARIA,103)) ANIO, MONTH(convert(date,FECHA_DIARIA,103)) MES, DAY(convert(date,FECHA_DIARIA,103)) DIA, COUNT(DISTINCT IDENTIFICACION) CANT FROM CVENCUESTA_DIARIA " +
                "WHERE convert(date, FECHA_DIARIA, 103) BETWEEN @DiaInicial AND @DiaFinal GROUP BY YEAR(convert(date, FECHA_DIARIA, 103)), MONTH(convert(date, FECHA_DIARIA, 103)), DAY(convert(date, FECHA_DIARIA, 103))" +
                "ORDER BY ANIO, MES, DIA ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("@DiaInicial", diaInicial);
                command.Parameters.AddWithValue("@DiaFinal", diaFinal);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EEstadisticasEC EstadisticaDia = new EEstadisticasEC
                    {
                        IntDia = Convert.ToInt32(reader["DIA"].ToString()),
                        IntCantDia = Convert.ToInt32(reader["CANT"].ToString()),
                        IntMesDia = Convert.ToInt32(reader["MES"].ToString()),
                        IntAnioMesDia = Convert.ToInt32(reader["ANIO"].ToString())
                    };
                    estadisticasDias.Add(EstadisticaDia);
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
            return estadisticasDias;
        }

        public static List<EEstadisticasEC> GetInfoDetalle(string DiaI, string DiaF)
        {

            string diaInicial = DiaI;
            string diaFinal = DiaF;

            List<EEstadisticasEC> InfoDetalles = new List<EEstadisticasEC>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT DISTINCT IDENTIFICACION, NOMBRES, COUNT(DISTINCT FECHA_DIARIA) AS CANTIDAD FROM CVENCUESTA_DIARIA E " +
                    "WHERE CONVERT(date, FECHA_DIARIA, 103) BETWEEN @DiaInicial AND @DiaFinal GROUP BY NOMBRES, IDENTIFICACION", conexion.OpenConnection());

                command.Parameters.AddWithValue("@DiaInicial", diaInicial);
                command.Parameters.AddWithValue("@DiaFinal", diaFinal);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EEstadisticasEC InfoDetalle = new EEstadisticasEC
                    {
                        StrCedula = reader["IDENTIFICACION"].ToString(),
                        StrNombres = reader["NOMBRES"].ToString(),
                        IntCantEmpleado = Convert.ToInt32(reader["CANTIDAD"].ToString()),
                    };
                    InfoDetalles.Add(InfoDetalle);
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
            return InfoDetalles;
        }
    }
}