using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.EncuestaCovid;
using Entidades.Generales;
using Persistencia.Generales;

namespace Persistencia.EncuestaCovid
{
    public class PEncuestaCovid
    {

        public static string GetEPS()
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string identificacionUsuario = Convert.ToString(usuario.GNCodUsu1);
            string getEPS = "";

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT GNEps.NomEps FROM Usuario INNER JOIN GNEps ON Usuario.GnEpsUsu = GNEps.OidGNEps WHERE GNCodUsu = @identificacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("identificacion", identificacionUsuario);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    getEPS = reader["NomEps"].ToString();
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return getEPS;
        }

        public static void SetEncuestaCovid(string eps, string adinamia,string temperatura, string valorTemperatura, string tos, string dificultadRespiratoria, string odinofagia, string dolorLumbar, string dolorToracico, string malestarGeneral, string perdidaOlfato, string perdidaGusto, string elementosBioseguridad, string contactoEstrecho, string nombreContactoEstrecho, string idContactoEstrecho, string tipoCaso)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string identificacionUsuario = Convert.ToString(usuario.GNCodUsu1);
            string nombreUsuario = usuario.GNNomUsu1;
            string numeroPersonal = usuario.GnTlUsu1;
            string cargoUSuario = usuario.GnCargo1;
            string unidadUsuario = usuario.GnUnfun1;
            string fechaDiaria = DateTime.Now.ToString("dd/MM/yyyy");
                

            string vacio = "NULL";

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO CVENCUESTA_DIARIA (IDENTIFICACION, NOMBRES, FECHA_NACIMIENTO, TELEFONO_PERSONAL, TELEFONO_FAMILIAR, DIRECCION, EPS, FONDO, " +
                    "CARGO, UNIDAD, FECHA_INGRESO, SALARIO, ADINAMIA, TEMPERATURA, VTEMPERATURA, TOS, DIFIRESPIRATORIA, ODINOFAGIA, OTROSSINTOMAS, CONTACTOCON, NOMPERSONA, IDEPERSONA, TIPOCASO, " +
                    "FECHA_DIARIA, DLUMBAR, DTORACICO, MALESTARG, PERDOLFATO, PERDGUSTO, ELEMENTOBIO) VALUES (@strIdentificacion, @strNombres, @strNull, @strTelefonoPersonal, " +
                    "@strNull, @strNull, @strEps, @strNull, @strCargo, @strUnidad, @strNull, @strNull, @strAdinamia, @strTemperatura, @strVtemperatura, @strTos, @strDifiRespiratoria, " +
                    "@strOdinofagia, @strNull, @strContactoCon, @strNomPersona, @strIdePersona, @strTipoCaso, @strFechaDiaria, @strDLumbar, @strDtoracico, @strMalestarG, " +
                    "@strPerdOlfato, @strPerdGusto, @strElementoBio)", conexion.OpenConnection());

                command.Parameters.AddWithValue("strAdinamia", adinamia);
                command.Parameters.AddWithValue("strTemperatura", temperatura);
                command.Parameters.AddWithValue("strVtemperatura", valorTemperatura);
                command.Parameters.AddWithValue("strTos", tos);
                command.Parameters.AddWithValue("strDifiRespiratoria", dificultadRespiratoria);
                command.Parameters.AddWithValue("strOdinofagia", odinofagia);
                command.Parameters.AddWithValue("strDLumbar", dolorLumbar);
                command.Parameters.AddWithValue("strDtoracico", dolorToracico);
                command.Parameters.AddWithValue("strMalestarG", malestarGeneral);
                command.Parameters.AddWithValue("strPerdOlfato", perdidaOlfato);
                command.Parameters.AddWithValue("strPerdGusto", perdidaGusto);
                command.Parameters.AddWithValue("strElementoBio", elementosBioseguridad);
                command.Parameters.AddWithValue("strContactoCon", contactoEstrecho);
                command.Parameters.AddWithValue("strNomPersona", nombreContactoEstrecho);
                command.Parameters.AddWithValue("strIdePersona", idContactoEstrecho);
                command.Parameters.AddWithValue("strTipoCaso", tipoCaso);
                command.Parameters.AddWithValue("strIdentificacion", identificacionUsuario);
                command.Parameters.AddWithValue("strNombres", nombreUsuario);
                command.Parameters.AddWithValue("strTelefonoPersonal", numeroPersonal);
                command.Parameters.AddWithValue("strEps", eps);
                command.Parameters.AddWithValue("strCargo", cargoUSuario);
                command.Parameters.AddWithValue("strUnidad", unidadUsuario);
                command.Parameters.AddWithValue("strNull", vacio);
                command.Parameters.AddWithValue("strFechaDiaria", fechaDiaria);

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

        public static List<EEncuestaCovid> GetReporte()
        {
            string fechaDiaria = DateTime.Now.ToString("dd/MM/yyyy");

            List<EEncuestaCovid> infoReporte = new List<EEncuestaCovid>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT DISTINCT(IDENTIFICACION), NOMBRES, TELEFONO_PERSONAL, EPS, CARGO, UNIDAD ,ADINAMIA, TEMPERATURA, VTEMPERATURA,TOS,DIFIRESPIRATORIA, " +
                    "ODINOFAGIA, CONTACTOCON, NOMPERSONA, IDEPERSONA, TIPOCASO, FECHA_DIARIA, COMENTARIO, DLUMBAR, DTORACICO, MALESTARG, PERDOLFATO, PERDGUSTO," +
                    "ELEMENTOBIO FROM CVENCUESTA_DIARIA WHERE FECHA_DIARIA = @fechaActual", conexion.OpenConnection());

                command.Parameters.AddWithValue("fechaActual", fechaDiaria);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EEncuestaCovid info = new EEncuestaCovid
                    {
                        StrIdentificacion = reader["IDENTIFICACION"].ToString(),
                        StrNombres = reader["NOMBRES"].ToString(),
                        StrTelefonoPersonal = reader["TELEFONO_PERSONAL"].ToString(),
                        StrEps = reader["EPS"].ToString(),
                        StrCargo = reader["CARGO"].ToString(),
                        StrUnidad = reader["UNIDAD"].ToString(),
                        StrAdinamia = reader["ADINAMIA"].ToString(),
                        StrTemperatura = reader["TEMPERATURA"].ToString(),
                        StrVtemperatura = reader["VTEMPERATURA"].ToString(),
                        StrTos = reader["TOS"].ToString(),
                        StrDifiRespiratoria = reader["DIFIRESPIRATORIA"].ToString(),
                        StrOdinofagia = reader["ODINOFAGIA"].ToString(),
                        StrContactoCon = reader["CONTACTOCON"].ToString(),
                        StrNomPersona = reader["NOMPERSONA"].ToString(),
                        StrIdePersona = reader["IDEPERSONA"].ToString(),
                        StrTipoCaso = reader["TIPOCASO"].ToString(),
                        StrFechaDiaria = reader["FECHA_DIARIA"].ToString(),
                        StrDLumbar = reader["DLUMBAR"].ToString(),
                        StrDtoracico = reader["DTORACICO"].ToString(),
                        StrMalestarG = reader["MALESTARG"].ToString(),
                        StrPerdOlfato = reader["PERDOLFATO"].ToString(),
                        StrPerdGusto = reader["PERDGUSTO"].ToString(),
                        StrElementoBio = reader["ELEMENTOBIO"].ToString(),
                    };
                    infoReporte.Add(info);
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
            return infoReporte;
        }

        public static List<EEncuestaCovid> GetFiltroReporte(string fechaI, string fechaF)
        {

            List<EEncuestaCovid> infoReporte = new List<EEncuestaCovid>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT DISTINCT IDENTIFICACION, NOMBRES, TELEFONO_PERSONAL, EPS, CARGO, UNIDAD ,ADINAMIA, TEMPERATURA, VTEMPERATURA,TOS,DIFIRESPIRATORIA,
                    ODINOFAGIA, OTROSSINTOMAS, CONTACTOCON, NOMPERSONA, IDEPERSONA, TIPOCASO, FECHA_DIARIA, COMENTARIO, DLUMBAR, DTORACICO, MALESTARG, PERDOLFATO, PERDGUSTO
                    , ELEMENTOBIO FROM CVENCUESTA_DIARIA WHERE convert(date, FECHA_DIARIA, 103) >= @fechaI AND convert(date, FECHA_DIARIA, 103) <= @fechaF
                    GROUP BY FECHA_DIARIA, IDENTIFICACION, NOMBRES, TELEFONO_PERSONAL, EPS, CARGO, UNIDAD, ADINAMIA, TEMPERATURA, VTEMPERATURA, TOS, DIFIRESPIRATORIA,
                    ODINOFAGIA, OTROSSINTOMAS, CONTACTOCON, NOMPERSONA, IDEPERSONA, TIPOCASO, COMENTARIO, DLUMBAR, DTORACICO, MALESTARG, PERDOLFATO, PERDGUSTO, ELEMENTOBIO", conexion.OpenConnection());

                command.Parameters.AddWithValue("fechaI", fechaI);
                command.Parameters.AddWithValue("fechaF", fechaF);

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    EEncuestaCovid info = new EEncuestaCovid
                    {
                        StrIdentificacion = reader["IDENTIFICACION"].ToString(),
                        StrNombres = reader["NOMBRES"].ToString(),
                        StrTelefonoPersonal = reader["TELEFONO_PERSONAL"].ToString(),
                        StrEps = reader["EPS"].ToString(),
                        StrCargo = reader["CARGO"].ToString(),
                        StrUnidad = reader["UNIDAD"].ToString(),
                        StrAdinamia = reader["ADINAMIA"].ToString(),
                        StrTemperatura = reader["TEMPERATURA"].ToString(),
                        StrVtemperatura = reader["VTEMPERATURA"].ToString(),
                        StrTos = reader["TOS"].ToString(),
                        StrDifiRespiratoria = reader["DIFIRESPIRATORIA"].ToString(),
                        StrOdinofagia = reader["ODINOFAGIA"].ToString(),
                        StrContactoCon = reader["CONTACTOCON"].ToString(),
                        StrNomPersona = reader["NOMPERSONA"].ToString(),
                        StrIdePersona = reader["IDEPERSONA"].ToString(),
                        StrTipoCaso = reader["TIPOCASO"].ToString(),
                        StrFechaDiaria = reader["FECHA_DIARIA"].ToString(),
                        StrDLumbar = reader["DLUMBAR"].ToString(),
                        StrDtoracico = reader["DTORACICO"].ToString(),
                        StrMalestarG = reader["MALESTARG"].ToString(),
                        StrPerdOlfato = reader["PERDOLFATO"].ToString(),
                        StrPerdGusto = reader["PERDGUSTO"].ToString(),
                        StrElementoBio = reader["ELEMENTOBIO"].ToString(),
                    };
                    infoReporte.Add(info);
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
            return infoReporte;
        }

    }
}