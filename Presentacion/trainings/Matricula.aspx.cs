using Comunes;
using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.trainings
{
    public partial class Matricula : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static dynamic GetUsuariosMatriculados(int idAgenda, string documento, string nombre, string cargo, int idCapacitacion)
        {
            List<dynamic> datos = new List<dynamic>();

            List<Usuario> usuarios = DAOCPMatricula.GetMatriculasByAgenda(idAgenda, documento, nombre, cargo);

            foreach (var usuario in usuarios)
            {
                datos.Add(new { usuario = usuario, foto = Convert.ToBase64String(usuario.GNFtUsu1) });
            }

            return new
            {
                matriculas = datos,
                metodos = DAOCPMatricula.GetDatosMatriculas(idAgenda),
                capacitacion = DAOCPCapacitacion.GetCapacitacion(idCapacitacion)
            };
        }
        [WebMethod]
        public static List<dynamic> GetUsers(string name)
        {
            List<dynamic> data = new List<dynamic>();
            DAOUsuario.GetListadoUsuarios(name).ForEach(usuario => data.Add(new { text = usuario.GNNomUsu1, value = usuario.GNCodUsu1 }));
            return data;
        }

        [WebMethod]
        public static List<dynamic> GetCargos(string name)
        {
            List<dynamic> data = new List<dynamic>();
            DAOCargo.GetCargos(name).ForEach(cargo => data.Add(new { text = cargo.StrGnNomCgo, value = cargo.IntGnDcCgo }));
            return data;
        }

        [WebMethod]
        public static List<dynamic> GetUnidadesFuncionales(string name)
        {
            List<dynamic> datos = new List<dynamic>();
            DAOUnidadFuncional.GetUnidadesFuncionales(name).ForEach(unidad => datos.Add(new { text = unidad.GnNomDep1, value = unidad.GnDcDep1 }));
            return datos;
        }


        [WebMethod]
        public static void SetMatricula(List<CPMatricula.DatosMatricula> datos, int idAgenda, int idCapacitacion)
        {
            string[] metodos = new string[] { "unitario", "cargo", "unidad" };

            List<CPMatricula.DatosMatricula> matriculasEliminar = new List<CPMatricula.DatosMatricula>();
            List<CPMatricula.DatosMatricula> matriculasCrear = new List<CPMatricula.DatosMatricula>();
            List<CPMatricula.DatosMatricula> matriculasBd = DAOCPMatricula.GetDatosMatriculas(idAgenda);

            if (matriculasBd.Count == 0)
            {
                datos.ForEach(item =>
                {
                    DAOCPMatricula.matricularUsuarios(
                        item.valorMetodoMetr + "",
                        metodos[item.metodoMatr],
                        idCapacitacion,
                        DateTime.Now,
                        item.metodoMatr,
                        item.valorMetodoMetr,
                        item.nombreMetodoMatr,
                        idAgenda
                    );
                });
            }
            else
            {
                for (int i = 0; i < datos.Count; i++)
                {
                    bool flag = true;
                    for (int j = 0; j < matriculasBd.Count; j++)
                    {
                        if (datos[i].metodoMatr == matriculasBd[j].metodoMatr && datos[i].valorMetodoMetr == matriculasBd[j].valorMetodoMetr)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        matriculasCrear.Add(datos[i]);
                    }
                }

                for (int i = 0; i < matriculasBd.Count; i++)
                {
                    bool flag = true;

                    for (int j = 0; j < datos.Count; j++)
                    {
                        if (datos[j].metodoMatr == matriculasBd[i].metodoMatr && datos[j].valorMetodoMetr == matriculasBd[i].valorMetodoMetr)
                        {
                            flag = false;
                            break;
                        }
                    }

                    if (flag)
                    {
                        matriculasEliminar.Add(matriculasBd[i]);
                    }
                }

                matriculasEliminar.ForEach(matricula => DAOCPMatricula.DeleteMatriculaByMethod(idAgenda, matricula.metodoMatr, matricula.valorMetodoMetr));

                matriculasCrear.ForEach(matricula =>
                {
                    DAOCPMatricula.matricularUsuarios(
                        matricula.valorMetodoMetr + "",
                        metodos[matricula.metodoMatr],
                        idCapacitacion,
                        DateTime.Now,
                        matricula.metodoMatr,
                        matricula.valorMetodoMetr,
                        matricula.nombreMetodoMatr,
                        idAgenda
                    );
                });
            }
        }
        [WebMethod]
        public static void DeleteMatricula(int idUsuario, int idAgenda)
        {
            DAOCPMatricula.DeleteMatriculaByUsuario(idUsuario, idAgenda);
        }

        protected void btnEnviarNotificacion_Click(object sender, EventArgs e)
        {
            var idAgenda = Convert.ToInt32(Request["idAgenda"]);
            List<CPMatricula> matriculas = DAOCPMatricula.GetMatriculasByAgenda(idAgenda);
            List<string> correos = new List<string>();

            CPCAPACITACION capacitacion = DAOCPCapacitacion.GetCapacitacionByAgenda(idAgenda);

            var Agenda = DAOCPAgenda.GetAgenda(idAgenda);

            matriculas.ForEach(matricula =>
            {
                correos.Add(DAOUsuario.getInstance().GetUsuario(matricula.IntGNCodUsu).GNCrusu1);
            });

            var mensaje = $@"
                
                <!DOCTYPE html>
                <html lang=""en"" xmlns=""https://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width,initial-scale=1"">
                    <meta name=""x-apple-disable-message-reformatting"">
                    <title></title>
                    <!--[if mso]>
                    <noscript>
                        <xml>
                            <o:OfficeDocumentSettings>
                                <o:PixelsPerInch>96</o:PixelsPerInch>
                            </o:OfficeDocumentSettings>
                        </xml>
                    </noscript>
                    <![endif]-->
                    <style>
                        table,td,div, h1,p {"{font - family: Arial, sans-serif;}"}
                    </style>
                </head>

                <body style=""margin: 0;padding: 0;"">
                    <p>&nbsp;</p>
   
                    <table
                        style=""width: 640px; border-collapse: collapse; border: 0px; border-spacing: 0px; text-align: left; margin: 0px auto; font-family: Arial, sans-serif; height: 272px;""
                        role=""presentation"">
                        <tbody>
                            <tr style=""height: 70px;"">
                                <td style=""padding: 40px; background: #2874A6; height: 70px;"" align=""center"">
                                    <table style=""border-collapse: collapse; width: 700px; margin-left: auto; margin-right: auto;""
                                        role=""presentation"" cellspacing=""0px"">
                                        <tbody>
                                            <tr>
                                                <td style=""padding-right: 30px; width: 434.219px;"" align=""right"">
                                                    <img style=""float: left;"" src=""http://190.242.128.206:81/Images/LogoVitraya2.png"" alt=""""  width=""121"" height=""116"" />
                                                </td>
                                                <td style=""padding-left: 30px; padding-top: 21px; text-align: right; width: 264.781px;"">

                                                    <img src=""http://190.242.128.206:81/Images/calendario-blanco.png"" alt="""" width=""47"" height=""50"" /><br />
                                                    <span style=""color: #ffffff;"">Notificación de capacitación</span><br>
                                                    <span style=""color: #ffffff;""><br />{DateTime.Now.ToString("MMMM, dd yyyy | hh:mm tt")}</span><br>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr style=""height: 183px;"">
                                <td style=""padding: 45px; color: #153643; height: 183px;"">
                                    <h2 style=""text-align: center;""><strong>{capacitacion.StrTEMA}</strong></h2>
                                    <p>Señor(a).</p>
                                    <p>Cordial saludo,&nbsp;</p>
                                    <p>
                                        Se le informa que ha sido programado para la capacitación: {capacitacion.StrTEMA}, que se realizara el dia {Agenda.DtmFecha.ToString("dd-MM-yyyy")} 
                                        a las {Agenda.DtmHoraInicial.ToString("HH:mm tt")}.
                                     </p>
                                     <table style=""width: 100%; height: 53px;"">
                                        <tbody>
                                            <tr>
                                                <td align=""center"">
                                                    <a href=""{Agenda.StrLinkReunion}""><img src=""http://190.242.128.206:81/Images/BotonReunion.png""  width=""244"" height=""55"" /></a>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr style=""height: 19px;"">
                                <td style=""padding: 40px; background: #f0f0f0;"">
                                    <table style=""border-collapse: collapse; width: 100%;"">
                                        <tbody>
                                            <tr>
                                                <td style=""width: 49.9359%;"">
                                                    <img src=""http://190.242.128.206:81/Images/Logos.png"" width=""260"" height=""59"" />
                                                </td>
                                                <td style=""width: 49.9359%; text-align: right;"">
                                                    Notificación realizada desde Vitraya Clinca Crecer 
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </body>
                </html>
            ";
            Email.SendMail(correos, mensaje, $"invitación a la capacitacion: {capacitacion.StrTEMA}");
        }

    }
}