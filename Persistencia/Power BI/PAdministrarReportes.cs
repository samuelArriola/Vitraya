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
    public class PAdministrarReportes
    {

        public static List<ELIstaReportes> GetReportes()
        {

            List<ELIstaReportes> infoReportes = new List<ELIstaReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM PBReportes ORDER BY OidReportePB DESC", conexion.OpenConnection());

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

        public static List<ELIstaReportes> GetDetallesReportes(int idReporte)
        {

            List<ELIstaReportes> infoReportes = new List<ELIstaReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM PBReportes WHERE OidReportePB = @idReporte ORDER BY OidReportePB DESC", conexion.OpenConnection());

                command.Parameters.AddWithValue("idReporte", idReporte);
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

        public static List<EAdministrarReportes> GetUsuarios()
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            int identificacionUsuario = usuario.GNCodUsu1;

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GnIdUsu, GNCodUsu, GNNomUsu, GNCrusu FROM Usuario WHERE GnEtUsu = 'Activo' ORDER BY GNNomUsu ASC", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        NombrePermisoUsu = reader["GNNomUsu"].ToString(),
                        CorreoPermisoUsu = reader["GNCrusu"].ToString(),
                        UsuarioLogueado = identificacionUsuario
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

        public static List<EAdministrarReportes> GetCargos()
        {

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GnDcCgo, GnNomCgo FROM Cargo WHERE GnEsCgo = 'Activo' ORDER BY GnNomCgo ASC", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoCargo = Convert.ToInt32(reader["GnDcCgo"].ToString()),
                        NombrePermisoCargo = reader["GnNomCgo"].ToString()
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

        public static List<EAdministrarReportes> GetUnidadesFuncionales()
        {

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GnDcDep, GnNomDep FROM Departamento WHERE GnEsDep = 'Activo' ORDER BY GnNomDep ASC", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoUniFun = Convert.ToInt32(reader["GnDcDep"].ToString()),
                        NombrePermisoUniFun = reader["GnNomDep"].ToString()
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

        public static List<EAdministrarReportes> GetUsuariosPorCargo(int idCargo)
        {

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GnIdUsu, GNCodUsu, GNNomUsu, GNCrusu FROM Usuario WHERE GnEtUsu = 'Activo' AND GnDcCgo = @idCargo ORDER BY GNNomUsu ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("idCargo", idCargo);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        NombrePermisoUsu = reader["GNNomUsu"].ToString(),
                        CorreonombrePermisoCargo = reader["GNCrusu"].ToString(),
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

        public static List<EAdministrarReportes> GetUsuariosPorUnidadF(int idUnidadF)
        {

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GnIdUsu, GNCodUsu, GNNomUsu, GNCrusu FROM Usuario WHERE GnEtUsu = 'Activo' AND GnDcDep = @idUF ORDER BY GNNomUsu ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("idUF", idUnidadF);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoUsu = Convert.ToInt32(reader["GNCodUsu"].ToString()),
                        NombrePermisoUsu = reader["GNNomUsu"].ToString(),
                        CorreonombrePermisoUniFun = reader["GNCrusu"].ToString(),
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

        public static List<ELIstaReportes> SetReporte(string nombre, int estado, string descripcion, string tipo, string enlace)
        {

            List<ELIstaReportes> infoReportes = new List<ELIstaReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO PBReportes (Nombre, Enlace, TipoR, DescripcionG, Estado ) VALUES (@nombre, @enlace, @tipo, @descripcion, @estado) SELECT SCOPE_IDENTITY() AS 'ID'", conexion.OpenConnection());

                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("estado", estado);
                command.Parameters.AddWithValue("descripcion", descripcion);
                command.Parameters.AddWithValue("tipo", tipo);
                command.Parameters.AddWithValue("enlace", enlace);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ELIstaReportes infoReporte = new ELIstaReportes
                    {
                        OidReportePB1 = Convert.ToInt32(reader["ID"].ToString())
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

        public static void SetPermisosUsuarios(int idR, string identificacionU, string nombreU)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO PBPermisos (OidReportePB, IdentificacionU, NombreU) " +
                "VALUES(@idR, @identificacionU, @nombreU)", conexion.OpenConnection());

                command.Parameters.AddWithValue("idR", idR);
                command.Parameters.AddWithValue("identificacionU", identificacionU);
                command.Parameters.AddWithValue("nombreU", nombreU);

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

        public static void UpdateReporte(int codigo, string nombre, int estado, string descripcion, string tipo, string enlace)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE PBReportes SET Nombre = @nombre, Enlace = @enlace, TipoR = @tipo, DescripcionG = @descripcion, Estado = @estado WHERE OidReportePB = @codigo ", conexion.OpenConnection());

                command.Parameters.AddWithValue("codigo", codigo);
                command.Parameters.AddWithValue("nombre", nombre);
                command.Parameters.AddWithValue("estado", estado);
                command.Parameters.AddWithValue("descripcion", descripcion);
                command.Parameters.AddWithValue("tipo", tipo);
                command.Parameters.AddWithValue("enlace", enlace);

                reader = command.ExecuteReader();

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

        public static void DeletePermisos(int codigo)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM PBPermisos WHERE OidReportePB = @codigo", conexion.OpenConnection());

                command.Parameters.AddWithValue("codigo", codigo);

                reader = command.ExecuteReader();

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

        public static List<EAdministrarReportes> GetPermisosBD(int idReporte)
        {

            List<EAdministrarReportes> infoReportes = new List<EAdministrarReportes>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT IdentificacionU, NombreU FROM PBPermisos WHERE OidReportePB = @idReporte ORDER BY NombreU ASC", conexion.OpenConnection());

                command.Parameters.AddWithValue("idReporte", idReporte);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EAdministrarReportes infoReporte = new EAdministrarReportes
                    {
                        CodigoPermisoUsu = Convert.ToInt32(reader["IdentificacionU"].ToString()),
                        NombrePermisoUsu = reader["NombreU"].ToString()
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
                command = new SqlCommand("SELECT * FROM PBReportes WHERE Nombre LIKE '%' + @nombreReporte + '%' ORDER BY Nombre ASC", conexion.OpenConnection());

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