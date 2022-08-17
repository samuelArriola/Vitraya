using Comunes;
using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.PlanAccion
{
    public partial class MisPlanes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// metodo que retorna un listado de los planes de accion que estan asignado a los usuarios
        /// </summary>

        [WebMethod]

        public static List<PAPlanAccion> GetPlanesAccionByUsu()
        {
            //se obtiene el usuario que se encuentra logueado
            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            //se retornan los planes asignados al susuarios que se encuentra logueado
            return DAOPAPlanAccion.GetPlanesByUsu(idUsuario);
        }


        /// <summary>
        /// Metodo que envía el un plan de acción a revisión por el Oid del plan de accion
        /// </summary>
        /// <param name="idPlanAccion"></param>
        [WebMethod]
        public static void CerrarPlan(int idPlanAccion)
        {
            //se consulta el plan de accion por su id
            PAPlanAccion planAccion = DAOPAPlanAccion.Get(idPlanAccion);

            //Se consulta el usuario que realiza el seguimiento al plan de acción
            Usuario responsableSeguimiento = DAOUsuario.getInstance().GetUsuario(planAccion.IntcodUsuSegui);
            Usuario responsable = DAOUsuario.getInstance().GetUsuario(planAccion.IntGNCodUsu);

            List<string> correos = new List<string> { responsableSeguimiento.GNCrusu1 };


            string mensaje = $@"
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
                                                    <span style=""color: #ffffff;""><br />{DateTime.Now.ToString("MMMM, dd yyyy | hh:mm tt")}</span><br>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr style=""height: 183px;"">
                                <td style=""padding: 45px; color: #153643; height: 183px;"">
                                    <h2 style=""text-align: center;""><strong>Seguimiento a acción de mejora</strong></h2>
                                    <p>Sr(a). {responsableSeguimiento.GNNomUsu1}</p>
                                    <p>Cordial saludo,&nbsp;</p>
                                    <p>
                                        Se le informa que {responsable.GNNomUsu1} ha enviado acciones de mejora a evaluación
                                        para realizar el correspondiente seguimiento, por favor ingrese a la opción <strong>seguimiento a planes de acción</strong
                                        de la plataforma Vitraya para realizar las evaluaciones.
                                     </p>
                                </td>
                            </tr>
                            <tr style=""height: 19px;"">
                                <td style=""padding: 40px; background: #f0f0f0;"">
                                    <table style=""border-collapse: collapse; width: 100%;"">
                                        <tbody>
                                            <tr>
                                                <td style=""width: 49.9359%;"">
                                                    <img src=""http://190.242.128.206:81/Images/Logos.png"" alt="""" width=""260"" height=""59"" />
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

            Email.SendMail(correos, mensaje, "Seguimiento a planes de acción");

            //se cambia el estado del plan de accion a evaluación
            planAccion.IntEstAct = PAPlanAccion.ESTADO.EVALUACION;

            //se actualiza el plan de accion
            DAOPAPlanAccion.UpdateCompromiso(planAccion);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = planAccion.IntOidPAPlanAccion,
                strAccion = "Modificar",
                strDetalle = "",
                strEntidad = "Se cambia el estado del plan de acción de En Proceso a En Evaluación"
            });
        }
    }
}