using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;
using Persistencia.Generales;

namespace Persistencia.LinksExternos
{
    public class LEEncuestaCovid
    {

        public static void SetEncuestaCovid(string adinamia,string temperatura, string valorTemperatura, string tos, string dificultadRespiratoria, string odinofagia, string dolorLumbar, string dolorToracico, string malestarGeneral, string perdidaOlfato, string perdidaGusto, string elementosBioseguridad, string contactoEstrecho, string nombreContactoEstrecho, string idContactoEstrecho, string tipoCaso)
        {

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            string identificacionUsuario = Convert.ToString(usuario.GNCodUsu1);
            string nombreUsuario = usuario.GNNomUsu1;
            string numeroPersonal = usuario.GnTlUsu1;
            string cargoUSuario = usuario.GnCargo1;
            string fechaDiaria = DateTime.Now.ToString("dd/MM/yyyy"); 

            string vacio = "NULL";

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO CVENCUESTA_DIARIA (IDENTIFICACION, NOMBRES, FECHA_NACIMIENTO, TELEFONO_PERSONAL, TELEFONO_FAMILIAR, DIRECCION, EPS, FONDO, " +
                    "CARGO, UNIDAD, FECHA_INGRESO, SALARIO, ADINAMIA, TEMPERATURA, VTEMPERATURA, TOS, DIFIRESPIRATORIA, ODINOFAGIA, OTROSSINTOMAS, CONTACTOCON, NOMPERSONA, IDEPERSONA, TIPOCASO, " +
                    "FECHA_DIARIA, DLUMBAR, DTORACICO, MALESTARG, PERDOLFATO, PERDGUSTO, ELEMENTOBIO) VALUES (@strIdentificacion, @strNombres, @strNull, @strTelefonoPersonal, " +
                    "@strNull, @strNull, @strNull, @strNull, @strCargo, @strNull, @strNull, @strNull, @strAdinamia, @strTemperatura, @strVtemperatura, @strTos, @strDifiRespiratoria, " +
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
                command.Parameters.AddWithValue("strCargo", cargoUSuario);
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

    }
}