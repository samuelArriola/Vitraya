using Entidades.Generales;
using Entidades.Power_BI;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Power_BI
{
    public class PListaReportes
    {

        public static List<ELIstaReportes> GetReportes()
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            int idUsuario = usuario.GNCodUsu1;

            List<ELIstaReportes> infoReportes = new List<ELIstaReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT DISTINCT p.OidReportePB, p.IdentificacionU, r.Nombre, r.Enlace, r.DescripcionG, r.Estado, r.TipoR FROM PBPermisos AS p " +
                    "INNER JOIN PBReportes AS r ON p.OidReportePB = r.OidReportePB WHERE p.IdentificacionU = @idUsuario AND r.Estado = 1", conexion.OpenConnection());

                command.Parameters.AddWithValue("idUsuario", idUsuario);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ELIstaReportes infoReporte = new ELIstaReportes
                    {
                        OidReportePB1 = Convert.ToInt32(reader["OidReportePB"].ToString()),
                        Estado1 = Convert.ToInt32(reader["Estado"].ToString()),
                        Nombre1 = reader["Nombre"].ToString(),
                        Enlace1 = reader["Enlace"].ToString(),
                        Descripcion1 = reader["DescripcionG"].ToString(),
                        Tipo1 = reader["TipoR"].ToString()
                    };
                    infoReportes.Add(infoReporte);
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
            return infoReportes;
        }

        public static List<ELIstaReportes> FiltroNombreR(string nombreReporte)
        {

            List<ELIstaReportes> infoReportes = new List<ELIstaReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM PBReportes WHERE Estado = 1 AND Nombre LIKE '%' + @nombreReporte + '%' ORDER BY Nombre ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("nombreReporte", nombreReporte);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ELIstaReportes infoReporte = new ELIstaReportes
                    {
                        OidReportePB1 = Convert.ToInt32(reader["OidReportePB"].ToString()),
                        Estado1 = Convert.ToInt32(reader["Estado"].ToString()),
                        Nombre1 = reader["Nombre"].ToString(),
                        Enlace1 = reader["Enlace"].ToString(),
                        Descripcion1 = reader["DescripcionG"].ToString(),
                        Tipo1 = reader["TipoR"].ToString()
                    };
                    infoReportes.Add(infoReporte);
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
            return infoReportes;
        }

    }
}