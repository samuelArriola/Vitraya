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
    public partial class RevisarPlanAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Metodo que retorna un listado de los avances de un plan de accion en evaluación por el id de este
        /// </summary>
        /// <param name="idPlanAccion"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<object[]> GetAvancesByIdPlan(int idPlanAccion)
        {
            //se consulta un listdo de los avances por el id del plan de accion
            List<PAAvance> avances = DAOPAAvance.GetAvencesByIdPlan(idPlanAccion);

            //listado donde se guardaran todos los datos a retornarse
            List<object[]> datos = new List<object[]>();

            foreach (var avance in avances)
            {
                //se consulta el listado de archivos cargados para el plan de accion
                List<GNArchivo> archivos = DAOGNArchivo.Listar(avance.IntOidListaArch);
                foreach (var archivo in archivos)
                {
                    //se borra la informacion del archivo para evitar retornar datos muy pesados  
                    archivo.AbteArchivo = null;
                }

                //se agreban todos los datos a la lista a retornar
                datos.Add(new object[] { avance, archivos });
            }

            return datos;
        }


        /// <summary>
        /// Metodo que cambia el estao del plan de acción a terminado o en proceso de acuerdo a lo enviado por el usuario
        /// </summary>
        /// <param name="idPlanAccion"></param>
        /// <param name="revision"></param>
        [WebMethod]
        public static void SetRevisarPlanAccion(int idPlanAccion, bool revision, string notas)
        {
            PAPlanAccion plan = DAOPAPlanAccion.Get(idPlanAccion);
            plan.IntEstAct = revision ? PAPlanAccion.ESTADO.TERMINADO : PAPlanAccion.ESTADO.PROCESO;

            if (!revision)
            {
                //Se consulta el usuario que realiza el seguimiento al plan de acción
                Usuario responsableSeguimiento = DAOUsuario.getInstance().GetUsuario(plan.IntcodUsuSegui);
                Usuario responsable = DAOUsuario.getInstance().GetUsuario(plan.IntGNCodUsu);

                List<string> correos = new List<string> { responsable.GNCrusu1 };

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
                                                        <span style=""color: #ffffff;""><br />{DateTime.Now.ToString("MMMM, dd yyyy | hh:mm tt")}</span><br>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                                <tr style=""height: 183px;"">
                                    <td style=""padding: 45px; color: #153643; height: 183px;"">
                                        <h2 style=""text-align: center;""><strong>Rechazo de acción de menjora</strong></h2>
                                        <p>Señor(a). {responsable.GNNomUsu1}</p>
                                        <p>Cordial saludo,&nbsp;</p>
                                        <p>
                                            Se le informa que {responsableSeguimiento.GNNomUsu1} ha rechasado los avances a un plan de acción que usted envió a evaluación, 
                                            generando la siguientes notas:
                                         </p>
                                         <p style:""padding-left: 10px"">
                                            {notas}
                                         </p>
                                         <p>
                                             Por favor ingrese a la plataforma de vitraya a la opción de <strong>Mis Planes de acción</strong> y realice las correcciones y avances
                                             correspondientes.
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
                Email.SendMail(correos, mensaje, "Evaluación de plan de acción");
            }
            DAOPAPlanAccion.UpdateCompromiso(plan);
            DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = plan.IntOidPAPlanAccion,
                    strAccion = "Modificar",
                    strDetalle = $"Se Cambia el estado del plan de acción de En Evaluación a {(revision ? "Teminado" : "En proceso")}",
                    strEntidad = "PAPlanAccion"
                });
        }
    }
}