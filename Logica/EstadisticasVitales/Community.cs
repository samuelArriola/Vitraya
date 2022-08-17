using Entidades.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Logica.EstadisticasVitales
{
    public class Community
    {

        public static CRCodRuaf index(List<CRCodRuaf> codRuafs)
        {
            if (codRuafs.Count == 0)
                return null;

            double[] vec = new double[codRuafs.Count];

            for (int wii = 0, m = codRuafs.Count; wii < m; wii++)
                vec[wii] = Convert.ToDouble(codRuafs[wii].DoubleCRcodRuaf);

            for (int wii = 0, m = vec.Length; wii < m; wii++)
                vec[wii] = vec[wii] / 10;

            int index = 0;

            for (int wii = 0, m = vec.Length; wii < m; wii++)
            {
                if (vec[wii] == vec.Min())
                    index = wii;
            }

            return codRuafs[index];

        }

        public static void sentEmail()
        {
            //List<string> correos = new List<string>();

            //List<Usuario> usuarios = DAOUsuario.getUsuarioPorRol("Coordinador(a) de Vigilancia Epidemiológica ");
            //foreach (var usuario in usuarios)
            //{
            //    correos.Add(usuario.GNCrusu1);
            //}
            //int numCod = DAOCRCodRuaf.GetCodRuafNVVal().Count;
            //if (numCod <= 20)
            //{
            //    string mensaje = "<p><img class=\" preload-me\" style=\"display: block; margin-left: auto; margin-right: auto;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" />Hola, soy <strong>Vitraya</strong>!</p>" +
            //                    "<p>Por favor no olvide cargar c&oacute;digos al modulo de Estad&iacute;sticas Vitales, actualmente hay " + numCod + " Disponibles para registros de <strong>Nacidos Vivos</strong>.</p>" +
            //                    "<p>Cordialmente,</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>Administrador Vitraya&nbsp;</p>" +
            //                    "<p>Centro M&eacute;dico Crecer LTDA.</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>";

            //    string asunto = "Códigos Disponibles para Registro Nacidos Vivos";

            //    SendMail(correos, mensaje, asunto);
            //}

            //numCod = DAOCRCodRuaf.GetCodRuafDefVal().Count;
            //if (numCod <= 5)
            //{
            //    string mensaje = "<p><img class=\" preload-me\" style=\"display: block; margin-left: auto; margin-right: auto;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" />Hola, soy <strong>Vitraya</strong>!</p>" +
            //                    "<p>Por favor no olvide cargar c&oacute;digos al modulo de Estad&iacute;sticas Vitales, actualmente hay " + numCod + " Disponibles para registros <strong>Defunci&oacute;n</strong></p>" +
            //                    "<p>Cordialmente,</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>Administrador Vitraya&nbsp;</p>" +
            //                    "<p>Centro M&eacute;dico Crecer LTDA.</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>" +
            //                    "<p>&nbsp;</p>";
            //    string asunto = "Códigos Disponibles para Registro Defunción";

            //    SendMail(correos, mensaje, asunto);
            //}
        }



        public static bool SendMail(List<string> correos, string mensaje, string asunto)
        {
            try
            {
                MailMessage m = new MailMessage();
                SmtpClient SmtP = new SmtpClient();
                m.From = new MailAddress("notificaciones@vitrayaclinicacrecer.com");

                foreach (var correo in correos)
                {
                    try
                    {
                        m.To.Add(new MailAddress(correo));
                    }
                    catch (Exception)
                    {

                    }
                }
                m.Body = mensaje;
                m.Subject = asunto;
                m.IsBodyHtml = true;
                SmtP.Host = "mail.vitrayaclinicacrecer.com";
                SmtP.Port = 587;
                SmtP.Credentials = new NetworkCredential("notificaciones@vitrayaclinicacrecer.com", "@YVqvCYc$VKFQ)fM1R");
                SmtP.EnableSsl = false;
                SmtP.Send(m);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}