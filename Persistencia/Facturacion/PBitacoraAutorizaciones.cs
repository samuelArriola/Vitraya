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
    public class PBitacoraAutorizaciones
    {

        public static List<EBitacoraAutorizaciones> GetAutorizaciones()
        {

            List<EBitacoraAutorizaciones> infoGeneAut = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM RegistroAutorizaciones ORDER BY OidRegAutorizacion DESC";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoGeneAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        TipoIdentificacion1 = reader["TipoIdentificacion"].ToString(),
                        Identificacion1 = reader["Identificacion"].ToString(),
                        Nombres1 = reader["Nombres"].ToString(),
                        NumSolicitud1 = reader["NumSolicitud"].ToString(),
                        FechaSolicitud1 = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                        OrigenAtencion1 = reader["OrigenAtencion"].ToString(),
                        TipoServicio1 = reader["TipoServicio"].ToString(),
                        PrioridadAtencion1 = reader["PrioridadAtencion"].ToString(),
                        UbicacionPaciente1 = reader["UbicacionPaciente"].ToString(),
                        ContratoPrestacion1 = reader["ContratoPrestacion"].ToString(),
                        Servicio1 = reader["Servicio"].ToString(),
                        FechaIngreso1 = Convert.ToDateTime(reader["FechaIngreso"].ToString()),
                        NumCama1 = reader["NumCama"].ToString(),
                        DiagPrincipal1 = reader["DiagPrincipal"].ToString(),
                        DiagRel11 = reader["DiagRel1"].ToString(),
                        DiagRel21 = reader["DiagRel2"].ToString(),
                        NombreIPS1 = reader["NombreIPS"].ToString(),
                        DireccionIPS1 = reader["DireccionIPS"].ToString(),
                        JustificacionClinica1 = reader["JustificacionClinica"].ToString(),
                        ProfesionalSolicita1 = reader["ProfesionalSolicita"].ToString(),
                        CargoProfesional1 = reader["CargoProfesional"].ToString(),
                        Estado1 = reader["Estado"].ToString(),
                        ClasificacionTecnologia = reader["ClasifTecnologia"].ToString(),
                        NombreTecnologia = reader["NomTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["CantTecnologia"].ToString()),
                        FechaAprobacion1 = (reader["FechaAprobacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        NumAutorizacion1 = reader["NumAutorizacion"].ToString(),
                        FechaAnulacion1 = (reader["FechaAnulacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAnulacion"].ToString()),
                        MotivoAnulacion1 = reader["MotivoAnulacion"].ToString(),
                        NumIngreso1 = reader["NumIngreso"].ToString()
                    };
                    infoGeneAut.Add(infoGeneAuti);
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
            return infoGeneAut;
        }

        public static List<EBitacoraAutorizaciones> GetDetalles(int id)
        {

            List<EBitacoraAutorizaciones> infoDetAut = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM RegistroAutorizaciones WHERE OidRegAutorizacion = @idAutorizacion";

                command.Parameters.AddWithValue("idAutorizacion", id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoDetaAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        TipoIdentificacion1 = reader["TipoIdentificacion"].ToString(),
                        Identificacion1 = reader["Identificacion"].ToString(),
                        Nombres1 = reader["Nombres"].ToString(),
                        NumSolicitud1 = reader["NumSolicitud"].ToString(),
                        FechaSolicitud1 = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                        OrigenAtencion1 = reader["OrigenAtencion"].ToString(),
                        TipoServicio1 = reader["TipoServicio"].ToString(),
                        PrioridadAtencion1 = reader["PrioridadAtencion"].ToString(),
                        UbicacionPaciente1 = reader["UbicacionPaciente"].ToString(),
                        ContratoPrestacion1 = reader["ContratoPrestacion"].ToString(),
                        Servicio1 = reader["Servicio"].ToString(),
                        FechaIngreso1 = Convert.ToDateTime( reader["FechaIngreso"].ToString() ),
                        NumCama1 = reader["NumCama"].ToString() ,
                        DiagPrincipal1 = reader["DiagPrincipal"].ToString(),
                        DiagRel11 = reader["DiagRel1"].ToString(),
                        DiagRel21 = reader["DiagRel2"].ToString(),
                        NombreIPS1 = reader["NombreIPS"].ToString(),
                        DireccionIPS1 = reader["DireccionIPS"].ToString(),
                        JustificacionClinica1 = reader["JustificacionClinica"].ToString(),
                        ProfesionalSolicita1 = reader["ProfesionalSolicita"].ToString(),
                        CargoProfesional1 = reader["CargoProfesional"].ToString(),
                        Estado1 = reader["Estado"].ToString(),
                        ClasificacionTecnologia = reader["ClasifTecnologia"].ToString(),
                        NombreTecnologia = reader["NomTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["CantTecnologia"].ToString()),
                        //FechaAprobacion1 = Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        FechaAprobacion1 = (reader["FechaAprobacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        NumAutorizacion1 = reader["NumAutorizacion"].ToString(),
                        FechaAnulacion1 = (reader["FechaAnulacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAnulacion"].ToString()),
                        MotivoAnulacion1 = reader["MotivoAnulacion"].ToString(),
                        NumIngreso1 = reader["NumIngreso"].ToString()
                    };
                    infoDetAut.Add(infoDetaAuti);
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
            return infoDetAut;
        }

        public static List<EBitacoraAutorizaciones> GetDetallesTecno(int id)
        {

            List<EBitacoraAutorizaciones> infoDetAut = new List<EBitacoraAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM RegistroTecnologiasAutorizaciones where OidRegAutorizacion = @idAutorizacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("idAutorizacion", id);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoDetaAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        OidTecnologia1 = Convert.ToInt32(reader["OidTecnologia"].ToString()),
                        ClasificacionTecnologia = reader["Clasificacion"].ToString(),
                        NombreTecnologia = reader["NombreTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["Cantidad"].ToString())
                    };
                    infoDetAut.Add(infoDetaAuti);
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
            return infoDetAut;
        }

        public static void SetAprobarAutorizacion(int idAutorizacion, DateTime fechaAprobacion, string numAutorizacion)
        {

            string estado = "Aprobado";

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"UPDATE RegistroAutorizaciones SET Estado = @estado, FechaAprobacion = @fechaAprobacion, NumAutorizacion = @numAutorizacion " +
                "WHERE OidRegAutorizacion = @idAutorizacion";

                command.Parameters.AddWithValue("idAutorizacion", idAutorizacion);
                command.Parameters.AddWithValue("fechaAprobacion", fechaAprobacion);
                command.Parameters.AddWithValue("numAutorizacion", numAutorizacion);
                command.Parameters.AddWithValue("estado", estado);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }

        }

        public static void SetAnularAutorizacion(int idAutorizacion, DateTime fechaAnulacion, string motivoAnulacion)
        {

            string estado = "Anulado";

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"UPDATE RegistroAutorizaciones SET Estado = @estado, FechaAnulacion = @fechaAnulacion, MotivoAnulacion = @motivoAnulacion " +
                "WHERE OidRegAutorizacion = @idAutorizacion";

                command.Parameters.AddWithValue("idAutorizacion", idAutorizacion);
                command.Parameters.AddWithValue("fechaAnulacion", fechaAnulacion);
                command.Parameters.AddWithValue("motivoAnulacion", motivoAnulacion);
                command.Parameters.AddWithValue("estado", estado);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }

        }

        public static List<EBitacoraAutorizaciones> GetArchivosAut(int id)
        {

            List<EBitacoraAutorizaciones> infoArchivosAut = new List<EBitacoraAutorizaciones>();

            var tipo = "Orden";

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GNArchivosFact where OidRegAutorizacion = @idAutorizacion AND Tipo = @tipo", conexion.OpenConnection());

                command.Parameters.AddWithValue("idAutorizacion", id);
                command.Parameters.AddWithValue("tipo", tipo);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoArchivoaAuti = new EBitacoraAutorizaciones
                    {
                        OidGNArchivo1 = Convert.ToInt32(reader["OidGNArchivo"].ToString()),
                        ArchivoNombre1 = reader["Nombre"].ToString(),
                        ArchivoExt1 = reader["Ext"].ToString(),
                        ArchivoContenido1 = reader["Contenido"].ToString(),
                        Archivo1 = reader["Archivo"].ToString(),
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString())
                        //OidGNListaArchivos1 = Convert.ToInt32(reader["OidGNListaArchivos"].ToString())
                        
                    };
                    infoArchivosAut.Add(infoArchivoaAuti);
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
            return infoArchivosAut;
        }

        public static void SetRegistroDocumentoAprobAut(string Nombre, string Archivo, string Contenido, string Extension, int OidRegAutorizacion)
        {
            var tipo = "Autorizacion";
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

        public static void setObservacionAut(int idAutorizacion, string observacion)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string usuarioObser = usuario.GNNomUsu1;
            string fechaDiaria = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO RegAutObservaciones (OidRegAutorizacion, NomAutorizador, FechaObser, DescripcionObser) " +
                "VALUES(@idAutorizacion, @usuarioObser, @fechaDiaria, @observacion)", conexion.OpenConnection());

                command.Parameters.AddWithValue("idAutorizacion", idAutorizacion);
                command.Parameters.AddWithValue("usuarioObser", usuarioObser);
                command.Parameters.AddWithValue("fechaDiaria", fechaDiaria);
                command.Parameters.AddWithValue("observacion", observacion);

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

        public static List<EBitacoraAutorizaciones> getObservaciones(int AutId)
        {

            List<EBitacoraAutorizaciones> InfoObservaciones = new List<EBitacoraAutorizaciones>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM RegAutObservaciones WHERE OidRegAutorizacion = @AutId", conexion.OpenConnection());

                command.Parameters.AddWithValue("AutId", AutId);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones InfoObservacionesi = new EBitacoraAutorizaciones
                    {
                        ProfesionalSolicita1 = reader["NomAutorizador"].ToString(),
                        FechaObservacion = Convert.ToDateTime(reader["FechaObser"].ToString()),
                        DescripcionObservacion1 = reader["DescripcionObser"].ToString()

                    };
                    InfoObservaciones.Add(InfoObservacionesi);
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
            return InfoObservaciones;
        }

        public static List<EBitacoraAutorizaciones> GetArchivosAprobAut(int id)
        {

            List<EBitacoraAutorizaciones> infoArchivosAut = new List<EBitacoraAutorizaciones>();

            var tipo = "Autorizacion";

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM GNArchivosFact where OidRegAutorizacion = @idAutorizacion AND Tipo = @tipo", conexion.OpenConnection());

                command.Parameters.AddWithValue("idAutorizacion", id);
                command.Parameters.AddWithValue("tipo", tipo);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoArchivoaAuti = new EBitacoraAutorizaciones
                    {
                        OidGNArchivo1 = Convert.ToInt32(reader["OidGNArchivo"].ToString()),
                        ArchivoNombre1 = reader["Nombre"].ToString(),
                        ArchivoExt1 = reader["Ext"].ToString(),
                        ArchivoContenido1 = reader["Contenido"].ToString(),
                        Archivo1 = reader["Archivo"].ToString(),
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString())
                        //OidGNListaArchivos1 = Convert.ToInt32(reader["OidGNListaArchivos"].ToString())

                    };
                    infoArchivosAut.Add(infoArchivoaAuti);
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
            return infoArchivosAut;
        }

        public static List<EBitacoraAutorizaciones> filtro1(string numId, string NomPacien, string NumSolic, string NumIngreso, string NumAut, string EstAut)
        {

            List<EBitacoraAutorizaciones> infoGeneAut = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM RegistroAutorizaciones WHERE Identificacion LIKE '%' + @numId + '%'  AND Nombres LIKE '%' + @NomPacien + '%'  AND [NumSolicitud] LIKE '%'+@NumSolic+'%' AND [NumIngreso] LIKE '%'+@NumIngreso+'%' " +
                "AND ISNULL([NumAutorizacion], '') LIKE '%'+@NumAut+'%' AND [Estado] LIKE '%'+@EstAut+'%' ORDER BY OidRegAutorizacion DESC";

                command.Parameters.AddWithValue("numId", numId);
                command.Parameters.AddWithValue("NomPacien", NomPacien);
                command.Parameters.AddWithValue("NumSolic", NumSolic);
                command.Parameters.AddWithValue("NumIngreso", NumIngreso);
                command.Parameters.AddWithValue("NumAut", NumAut);
                command.Parameters.AddWithValue("EstAut", EstAut);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoGeneAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        TipoIdentificacion1 = reader["TipoIdentificacion"].ToString(),
                        Identificacion1 = reader["Identificacion"].ToString(),
                        Nombres1 = reader["Nombres"].ToString(),
                        NumSolicitud1 = reader["NumSolicitud"].ToString(),
                        FechaSolicitud1 = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                        OrigenAtencion1 = reader["OrigenAtencion"].ToString(),
                        TipoServicio1 = reader["TipoServicio"].ToString(),
                        PrioridadAtencion1 = reader["PrioridadAtencion"].ToString(),
                        UbicacionPaciente1 = reader["UbicacionPaciente"].ToString(),
                        ContratoPrestacion1 = reader["ContratoPrestacion"].ToString(),
                        Servicio1 = reader["Servicio"].ToString(),
                        FechaIngreso1 = Convert.ToDateTime(reader["FechaIngreso"].ToString()),
                        NumCama1 = reader["NumCama"].ToString(),
                        DiagPrincipal1 = reader["DiagPrincipal"].ToString(),
                        DiagRel11 = reader["DiagRel1"].ToString(),
                        DiagRel21 = reader["DiagRel2"].ToString(),
                        NombreIPS1 = reader["NombreIPS"].ToString(),
                        DireccionIPS1 = reader["DireccionIPS"].ToString(),
                        JustificacionClinica1 = reader["JustificacionClinica"].ToString(),
                        ProfesionalSolicita1 = reader["ProfesionalSolicita"].ToString(),
                        CargoProfesional1 = reader["CargoProfesional"].ToString(),
                        Estado1 = reader["Estado"].ToString(),
                        ClasificacionTecnologia = reader["ClasifTecnologia"].ToString(),
                        NombreTecnologia = reader["NomTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["CantTecnologia"].ToString()),
                        FechaAprobacion1 = (reader["FechaAprobacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        NumAutorizacion1 = reader["NumAutorizacion"].ToString(),
                        FechaAnulacion1 = (reader["FechaAnulacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAnulacion"].ToString()),
                        MotivoAnulacion1 = reader["MotivoAnulacion"].ToString(),
                        NumIngreso1 = reader["NumIngreso"].ToString()
                    };
                    infoGeneAut.Add(infoGeneAuti);
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
            return infoGeneAut;
        }

        public static List<EBitacoraAutorizaciones> filtro2(DateTime fechaI, DateTime fechaF)
        {

            List<EBitacoraAutorizaciones> infoGeneAut = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM RegistroAutorizaciones WHERE FORMAT(FechaSolicitud, 'yyyy/MM/dd') >= @fechaI AND FORMAT(FechaSolicitud, 'yyyy/MM/dd') <= @fechaF ORDER BY OidRegAutorizacion DESC";

                command.Parameters.AddWithValue("fechaI", fechaI);
                command.Parameters.AddWithValue("fechaF", fechaF);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoGeneAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        TipoIdentificacion1 = reader["TipoIdentificacion"].ToString(),
                        Identificacion1 = reader["Identificacion"].ToString(),
                        Nombres1 = reader["Nombres"].ToString(),
                        NumSolicitud1 = reader["NumSolicitud"].ToString(),
                        FechaSolicitud1 = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                        OrigenAtencion1 = reader["OrigenAtencion"].ToString(),
                        TipoServicio1 = reader["TipoServicio"].ToString(),
                        PrioridadAtencion1 = reader["PrioridadAtencion"].ToString(),
                        UbicacionPaciente1 = reader["UbicacionPaciente"].ToString(),
                        ContratoPrestacion1 = reader["ContratoPrestacion"].ToString(),
                        Servicio1 = reader["Servicio"].ToString(),
                        FechaIngreso1 = Convert.ToDateTime(reader["FechaIngreso"].ToString()),
                        NumCama1 = reader["NumCama"].ToString(),
                        DiagPrincipal1 = reader["DiagPrincipal"].ToString(),
                        DiagRel11 = reader["DiagRel1"].ToString(),
                        DiagRel21 = reader["DiagRel2"].ToString(),
                        NombreIPS1 = reader["NombreIPS"].ToString(),
                        DireccionIPS1 = reader["DireccionIPS"].ToString(),
                        JustificacionClinica1 = reader["JustificacionClinica"].ToString(),
                        ProfesionalSolicita1 = reader["ProfesionalSolicita"].ToString(),
                        CargoProfesional1 = reader["CargoProfesional"].ToString(),
                        Estado1 = reader["Estado"].ToString(),
                        ClasificacionTecnologia = reader["ClasifTecnologia"].ToString(),
                        NombreTecnologia = reader["NomTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["CantTecnologia"].ToString()),
                        FechaAprobacion1 = (reader["FechaAprobacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        NumAutorizacion1 = reader["NumAutorizacion"].ToString(),
                        FechaAnulacion1 = (reader["FechaAnulacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAnulacion"].ToString()),
                        MotivoAnulacion1 = reader["MotivoAnulacion"].ToString(),
                        NumIngreso1 = reader["NumIngreso"].ToString()
                    };
                    infoGeneAut.Add(infoGeneAuti);
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
            return infoGeneAut;
        }

        public static List<EBitacoraAutorizaciones> filtro3(DateTime fechaI, DateTime fechaF)
        {

            List<EBitacoraAutorizaciones> infoGeneAut = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT * FROM RegistroAutorizaciones WHERE FORMAT(FechaAprobacion, 'yyyy/MM/dd') >= @fechaI AND FORMAT(FechaAprobacion, 'yyyy/MM/dd') <= @fechaF ORDER BY OidRegAutorizacion DESC";

                command.Parameters.AddWithValue("fechaI", fechaI);
                command.Parameters.AddWithValue("fechaF", fechaF);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoGeneAuti = new EBitacoraAutorizaciones
                    {
                        OidRegAutorizacion1 = Convert.ToInt32(reader["OidRegAutorizacion"].ToString()),
                        TipoIdentificacion1 = reader["TipoIdentificacion"].ToString(),
                        Identificacion1 = reader["Identificacion"].ToString(),
                        Nombres1 = reader["Nombres"].ToString(),
                        NumSolicitud1 = reader["NumSolicitud"].ToString(),
                        FechaSolicitud1 = Convert.ToDateTime(reader["FechaSolicitud"].ToString()),
                        OrigenAtencion1 = reader["OrigenAtencion"].ToString(),
                        TipoServicio1 = reader["TipoServicio"].ToString(),
                        PrioridadAtencion1 = reader["PrioridadAtencion"].ToString(),
                        UbicacionPaciente1 = reader["UbicacionPaciente"].ToString(),
                        ContratoPrestacion1 = reader["ContratoPrestacion"].ToString(),
                        Servicio1 = reader["Servicio"].ToString(),
                        FechaIngreso1 = Convert.ToDateTime(reader["FechaIngreso"].ToString()),
                        NumCama1 = reader["NumCama"].ToString(),
                        DiagPrincipal1 = reader["DiagPrincipal"].ToString(),
                        DiagRel11 = reader["DiagRel1"].ToString(),
                        DiagRel21 = reader["DiagRel2"].ToString(),
                        NombreIPS1 = reader["NombreIPS"].ToString(),
                        DireccionIPS1 = reader["DireccionIPS"].ToString(),
                        JustificacionClinica1 = reader["JustificacionClinica"].ToString(),
                        ProfesionalSolicita1 = reader["ProfesionalSolicita"].ToString(),
                        CargoProfesional1 = reader["CargoProfesional"].ToString(),
                        Estado1 = reader["Estado"].ToString(),
                        ClasificacionTecnologia = reader["ClasifTecnologia"].ToString(),
                        NombreTecnologia = reader["NomTecnologia"].ToString(),
                        CantidadTecnologia = Convert.ToInt32(reader["CantTecnologia"].ToString()),
                        FechaAprobacion1 = (reader["FechaAprobacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAprobacion"].ToString()),
                        NumAutorizacion1 = reader["NumAutorizacion"].ToString(),
                        FechaAnulacion1 = (reader["FechaAnulacion"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FechaAnulacion"].ToString()),
                        MotivoAnulacion1 = reader["MotivoAnulacion"].ToString(),
                        NumIngreso1 = reader["NumIngreso"].ToString()
                    };
                    infoGeneAut.Add(infoGeneAuti);
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
            return infoGeneAut;
        }

        public static void updateInformacion(int AutId, string tipoId, string numId, string nombres, string numSolicitud, DateTime fechaSolicitud, string ubicacion, string NumIngreso, DateTime fechaIngreso, string servicio, string numCama, string diagPrincipal, string clasificacionT, string tecnologiaT, string cantidadT)
        {

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"UPDATE RegistroAutorizaciones SET TipoIdentificacion = @tipoId, Identificacion = @numId, Nombres = @nombres,  NumSolicitud = @numSolicitud, FechaSolicitud = @fechaSolicitud," +
                "UbicacionPaciente = @ubicacion, NumIngreso = @NumIngreso, FechaIngreso = @fechaIngreso, Servicio = @servicio, NumCama = @numCama, DiagPrincipal = @diagPrincipal, ClasifTecnologia = @clasificacionT," +
                "NomTecnologia = @tecnologiaT, CantTecnologia = @cantidadT WHERE OidRegAutorizacion = @AutId";

                command.Parameters.AddWithValue("AutId", AutId);
                command.Parameters.AddWithValue("tipoId", tipoId);
                command.Parameters.AddWithValue("numId", numId);
                command.Parameters.AddWithValue("nombres", nombres);
                command.Parameters.AddWithValue("numSolicitud", numSolicitud);
                command.Parameters.AddWithValue("fechaSolicitud", fechaSolicitud);
                command.Parameters.AddWithValue("ubicacion", ubicacion);
                command.Parameters.AddWithValue("NumIngreso", NumIngreso);
                command.Parameters.AddWithValue("fechaIngreso", fechaIngreso);
                command.Parameters.AddWithValue("servicio", servicio);
                command.Parameters.AddWithValue("numCama", numCama);
                command.Parameters.AddWithValue("diagPrincipal", diagPrincipal);
                command.Parameters.AddWithValue("clasificacionT", clasificacionT);
                command.Parameters.AddWithValue("tecnologiaT", tecnologiaT);
                command.Parameters.AddWithValue("cantidadT", cantidadT);

                command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }

        }

        public static List<EBitacoraAutorizaciones> validadorAutRepetidas(string NumeroIngreso, string NumAutorizacion)
        {

            List<EBitacoraAutorizaciones> infoValidacion = new List<EBitacoraAutorizaciones>();

            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT count(*) AS 'Resultado' FROM RegistroAutorizaciones WHERE NumIngreso != @NumeroIngreso AND NumAutorizacion = @NumAutorizacion";

                command.Parameters.AddWithValue("NumAutorizacion", NumAutorizacion);
                command.Parameters.AddWithValue("NumeroIngreso", NumeroIngreso);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EBitacoraAutorizaciones infoValidacionI = new EBitacoraAutorizaciones
                    {
                        ResultadoAutRepetida = Convert.ToInt32(reader["Resultado"].ToString())
                    };
                    infoValidacion.Add(infoValidacionI);
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
            return infoValidacion;
        }

    }
}