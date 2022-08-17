using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class GestionDocumentalController : Controller
    {

        private string CrearTablaControlDocumentos(GDSolicitud solicitud, List<GDRevision> revisiones, List<GDAprobacion> aprobaciones, bool estado)
        {
            //Se consulta al usuario creador del documento
            Usuario creador = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(solicitud.DblCodUsu));

            //Se consulta el usuario aprobador del documento
            var Aprobador = DAOUsuario.getInstance().GetUsuario(aprobaciones.Count == 0 ? 0 : aprobaciones[0].IntOidRevisor);
            string tbAprobacion = "";
            for (int i = 0, count = revisiones.Count; i < count; i++)
            {
                Usuario revisor = DAOUsuario.getInstance().GetUsuario(revisiones[i].IntOidRevisor);
                if (estado)
                    if (i == 0)
                    {
                        if (i == count - 1)
                        {
                            tbAprobacion += $@"
                            <tr>
                                <td style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(creador.GNFmUsu1)}"" style=""max-width:100px"" />
                                    <p style=""text-align:center"">{creador.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{solicitud.StrCarUsu}</p>
                                </td>
                                <td style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(revisor.GNFmUsu1)}"" style=""max-width:100px"" />
                                    <p  style=""text-align:center"">{revisor.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{revisiones[i].StrCargo}</p>
                                </td>
                                <td style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(Aprobador.GNFmUsu1)}"" style=""max-width:100px"" />
                                    <p  style=""text-align:center"">{Aprobador.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{aprobaciones[0].StrCargo}</p>
                                </td>
                            </tr>
                            <tr>
                                <td style=""text-align:center"">Fecha: {solicitud.DtmFechaSol.ToString("dd/MM/yyyy")}</td>
                                <td style=""text-align:center"">Fecha: {revisiones[i].DtmFecha.ToString("dd/MM/yyyy")}</td>
                                <td style=""text-align:center"">Fecha: {aprobaciones[0].DtmFecha.ToString("dd/MM/yyyy")}</td>
                            </tr>
                        ";
                        }
                        else
                        {
                            tbAprobacion += $@"
                            <tr>
                                <td rowspan=""{count * 2 - 1}"" style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(creador.GNFmUsu1)}"" style=""width:100px"" />
                                    <p  style=""text-align:center"">{creador.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{solicitud.StrCarUsu}</p>
                                </td>
                                <td style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(revisor.GNFmUsu1)}"" style=""width:100px"" />
                                    <p  style=""text-align:center"">{revisor.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{revisiones[i].StrCargo}</p>
                                </td>
                                <td rowspan=""{count * 2 - 1}"" style=""text-align:center"">
                                    <img src=""data:image/jpg;base64,{Convert.ToBase64String(Aprobador.GNFmUsu1)}"" style=""width:100px"" />
                                    <p  style=""text-align:center"">{Aprobador.GNNomUsu1}</p>
                                    <p  style=""text-align:center"">{aprobaciones[0].StrCargo}</p>
                                </td>
                            </tr>
                            <tr>
                                <td style=""text-align:center"">Fecha: {revisiones[i].DtmFecha.ToString("dd/MM/yyyy")}</td>
                            </tr>
                        ";
                        }
                    }
                    else if (i == count - 1)
                    {
                        tbAprobacion += $@"
                        <tr>
                            <td style=""text-align:center"">
                                <img src=""data:image/jpg;base64,{Convert.ToBase64String(revisor.GNFmUsu1)}"" style=""width:100px"" />
                                <p  style=""text-align:center"">{revisor.GNNomUsu1}</p>
                                <p  style=""text-align:center"">{revisiones[i].StrCargo}</p>
                            </td>
                        </tr>
                        <tr>
                            <td style=""text-align:center"">Fecha: {solicitud.DtmFechaSol.ToString("dd/MM/yyyy")}</td>
                            <td style=""text-align:center"">Fecha: {revisiones[i].DtmFecha.ToString("dd/MM/yyyy")}</td>
                            <td style=""text-align:center"">Fecha: {aprobaciones[0].DtmFecha.ToString("dd/MM/yyyy")}</td>
                        </tr>";
                    }
                    else
                    {
                        tbAprobacion += $@"
                        <tr>
                            <td style=""text-align:center"">
                                <img src=""data:image/jpg;base64,{Convert.ToBase64String(revisor.GNFmUsu1)}"" style=""width:100px"" />
                                <p  style=""text-align:center"">{revisor.GNNomUsu1}</p>
                                <p  style=""text-align:center"">{solicitud.StrCarUsu}</p>
                            </td>
                        </tr>
                        <tr>
                            <td>Fecha: {revisiones[i].DtmFecha.ToString("dd/MM/yyyy")}</td>
                        </tr>";
                    }
            }
            return tbAprobacion;
        }

        public string crearTablaControlCambios(List<GDDocumento> docsContCam)
        {

            //se crea una tabla con la informacion del control de cambios
            string tbControlCam = "";
            foreach (var documentoA in docsContCam)
            {
                GDSolicitud solicitudAux = DAOGDSolicitud.GetSolicitud(documentoA.IntOidGDSolicitud + "");
                tbControlCam += "" +
                    "<tr>" +
                    "   <td>" + documentoA.IntVersion + "</td>" +
                    "   <td>" + solicitudAux.DtmFechaSol.ToString("dd-MM-yyyy") + "</td>" +
                    "   <td colspan=\"2\">" + solicitudAux.StrJusSol + "</td>" +
                    "   <td>" + solicitudAux.StrNomUsu + "<br/>" + solicitudAux.StrCarUsu + "</td>" +
                    "</tr>";
            }

            return tbControlCam;
        }


        public ActionResult Procedimiento(int id)
        {
            //Se consulta el procedimiento de la base de datos por su id
            GdDocProcedimiento procedimiento = DAOGdDocProcedimiento.getProcedimientoID(id + "");

            //se consulta la informacion general del documento
            GDDocumento documento = DAOGDDocumento.GetDocumento(procedimiento.IntOidGDDocumento);

            //se consulta la solicitud que se hizo para el documento
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(documento.IntOidGDSolicitud + "");

            //se consulta el estdo de la aprobacion del documento
            List<GDAprobacion> aprobaciones = DAOGDAprobacion.GetGDAprobaciones(documento.IntOidGDDocumento);

            //se consulta el estado de la revision de docuemto
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);

            //se consultan las versiones anteriores
            List<GDDocumento> docsContCam = DAOGDDocumento.GetDocumentoByCod(documento.StrCodigoDoc, documento.IntConsecutivo, documento.IntVersion);
            

            //se crea una tabla con la informacion del control de cambios
            string tbControlCam = crearTablaControlCambios(docsContCam);

            //se crea una tabla con la informacion del estado de la aprobacion del documento
            string tbAprobacion = CrearTablaControlDocumentos(solicitud,revisiones,aprobaciones,documento.IntEstado > 3);

            object[] datos = { procedimiento, documento,documento.IntEstado >= 4, "", "", tbControlCam, tbAprobacion };


            //string _headerUrl = Url.Action("Cabecera", "GenDocument", new { nomDocument = documento.StrNomDoc, codigo = documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0'), fecha = documento.DtFechaE.ToString("dd/MM/yyyy"), version = documento.IntVersion + "" }, "http");
            string ConvertName = Uri.EscapeDataString(documento.StrNomDoc);
            string _headerUrl = "http://localhost/GenDocument/Cabecera?nomDocument=" + ConvertName + "&codigo=" + documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0') + "&fecha=" + documento.DtFechaE.ToString("dd/MM/yyyy") + "&version="+ documento.IntVersion;

            //string _footerUrl = Url.Action("PiePagina", "GenDocument", null, "http");
            string _footerUrl = "http://localhost/GenDocument/PiePagina";

            //return View(datos);

            return new ViewAsPdf("Procedimiento", datos)
            {
                CustomSwitches = string.Format("--footer-html {0} --footer-spacing  \"5\" --header-html {1} --header-spacing \"5\"", _footerUrl, _headerUrl),
                PageMargins = new Rotativa.Options.Margins(35, 10, 15, 10)
            };
        }

        public ActionResult Indicador(int id)
        {

            //Se consulta el indicador en la base de datos
            GDDocIndicador indicador = DAOGDDocIndicador.GetIndicador(id);

            ///se consulta el proceso desde la base de datos
            PCProceso proceso = DAOProceso.BuscarProceso(indicador.IntOidProceso);

            //Se consulta la informacion genral del documento
            GDDocumento documento = DAOGDDocumento.GetDocumento(indicador.IntOidGDDocumento);

            //Se consulta la infomacion de la solicitud para el presente documuiento
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(documento.IntOidGDSolicitud + "");

            //se obtiene el listado de la aprobaciones del documento
            List<GDAprobacion> aprobaciones = DAOGDAprobacion.GetGDAprobaciones(documento.IntOidGDDocumento);

            //se obtine un listado de las revisiones del presente documento
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);

            //se obtiene un listado de los documentos realacionados a este
            List<GDDocumento> docsContCam = DAOGDDocumento.GetDocumentoByCod(documento.StrCodigoDoc, documento.IntConsecutivo, documento.IntVersion);

            string tbControlCam = crearTablaControlCambios(docsContCam);


            string tbAprobacion = CrearTablaControlDocumentos(solicitud,revisiones,aprobaciones, documento.IntEstado > 3);


            object[] datos = { indicador, proceso, documento, solicitud, aprobaciones, tbAprobacion, tbControlCam, documento.IntEstado >= 4 };

            //return View(datos);

            //string _headerUrl = Url.Action("Cabecera", "GenDocument", new { nomDocument = documento.StrNomDoc, codigo = documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0'), fecha = documento.DtFechaE.ToString("dd/MM/yyyy"), version = documento.IntVersion + "" }, "http");
            string ConvertName = Uri.EscapeDataString(documento.StrNomDoc);
            string _headerUrl = "http://localhost/GenDocument/Cabecera?nomDocument=" + ConvertName + "&codigo=" + documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0') + "&fecha=" + documento.DtFechaE.ToString("dd/MM/yyyy") + "&version=" + documento.IntVersion;

            //string _footerUrl = Url.Action("PiePagina", "GenDocument", null, "http");
            string _footerUrl = "http://localhost/GenDocument/PiePagina";

            return new ViewAsPdf("Indicador", datos)
            {

                CustomSwitches = string.Format("--footer-html {0} --footer-spacing \"5\" --header-html {1} --header-spacing \"5\"", _footerUrl, _headerUrl),
                PageMargins = new Rotativa.Options.Margins(35, 10, 15, 10)
            };
        }

        public ActionResult Protocolo(int id)
        {
            //se consulta el protocolo desde la base de datos
            GDProtocolo protocolo = DAOGDProtocolo.getProtocolo(id);

            //se consulta la informacion general del documento
            GDDocumento documento = DAOGDDocumento.GetDocumento(protocolo.IntOidGDDocumento);

            //se consulta el listado de los indicadores a los que hace refencia el documento
            List<GDListaIndicador> listaIndicadores = DAOGDListaIndicador.GetListaIndicadores(protocolo.IntOidGDDocumento);
            List<GDDocIndicador> indicadores = new List<GDDocIndicador>();
            foreach (var idIndicador in listaIndicadores)
            {
                indicadores.Add(DAOGDDocIndicador.GetIndicador(idIndicador.IntOIdGDDocIndicador));
            }

            //se consulta la solicitud realizado para el protocolo
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(documento.IntOidGDSolicitud + "");

            //se consulta el estdo de las aprobacion del documento
            List<GDAprobacion> aprobaciones = DAOGDAprobacion.GetGDAprobaciones(documento.IntOidGDDocumento);

            //se consulta el estado de la revision del documento
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);

            //se consulta las versiones anteriores al precente documento
            List<GDDocumento> docsContCam = DAOGDDocumento.GetDocumentoByCod(documento.StrCodigoDoc, documento.IntConsecutivo, documento.IntVersion);

            

            string tbIndic = "";
            foreach (var indicador in indicadores)
            {
                GDDocumento documento1 = DAOGDDocumento.GetDocumento(indicador.IntOidGDDocumento);
                tbIndic += "" +
                    "<tr>" +
                    "   <td>" + documento1.StrCodigoDoc + "</td>" +
                    "   <td>" + documento1.StrNomDoc + "</td>" +
                    "   <td>" + indicador.StrFormulaCalc + "</td>" +
                    "   <td>" + indicador.StrEstandar + "</td>" +
                    "</tr>";
            }

            string tbControlCam = crearTablaControlCambios(docsContCam);


            string tbAprobacion = CrearTablaControlDocumentos(solicitud,revisiones,aprobaciones,documento.IntEstado > 3);

            object[] datos = { protocolo, documento, tbIndic, solicitud, documento.IntEstado >= 4, tbControlCam, tbAprobacion };


            //string _headerUrl = Url.Action("Cabecera", "GenDocument", new { nomDocument = documento.StrNomDoc, codigo = documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0'), fecha = documento.DtFechaE.ToString("dd/MM/yyyy"), version = documento.IntVersion + "" }, "http");
            string ConvertName = Uri.EscapeDataString(documento.StrNomDoc);
            string _headerUrl = "http://localhost/GenDocument/Cabecera?nomDocument=" + ConvertName + "&codigo=" + documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0') + "&fecha=" + documento.DtFechaE.ToString("dd/MM/yyyy") + "&version=" + documento.IntVersion;

            //string _footerUrl = Url.Action("PiePagina", "GenDocument", null, "http");
            string _footerUrl = "http://localhost/GenDocument/PiePagina";

            return new ViewAsPdf("Protocolo", datos)
            {
                CustomSwitches = string.Format("--footer-html {0} --footer-spacing \"5\" --header-html {1} --header-spacing \"5\"", _footerUrl, _headerUrl),
                PageMargins = new Rotativa.Options.Margins(35, 10, 15, 10)
            };
        }

        public ActionResult Manual(int id)
        {
            //Se consulta la información del desde la base de datos 
            GDManual manual = DAOGDManual.GetManual(id);

            //se consulta la informacion general del documeneto desde la base de datos
            GDDocumento documento = DAOGDDocumento.GetDocumento(manual.IntOidGDDocumento);

            //se consulta la solicitud que se hizo para el documento
            GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(documento.IntOidGDSolicitud + "");

            //se consulta el estado de la aprobacion del documento
            List<GDAprobacion> aprobaciones = DAOGDAprobacion.GetGDAprobaciones(documento.IntOidGDDocumento);

            //se consulta el estado de la revision del documento
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(documento.IntOidGDDocumento);

            //se consulta las versiones anteriores al precente documento
            List<GDDocumento> docsContCam = DAOGDDocumento.GetDocumentoByCod(documento.StrCodigoDoc, documento.IntConsecutivo, documento.IntVersion);


            string tbAprobaciones = CrearTablaControlDocumentos(solicitud, revisiones, aprobaciones, documento.IntEstado > 3);

            string tbControlCam = crearTablaControlCambios(docsContCam);

            object[] datos = { manual, documento, tbAprobaciones, tbControlCam, documento.IntEstado >= 4 };

            //return View(datos);

            //string _headerUrl = Url.Action("Cabecera", "GenDocument", new { nomDocument = documento.StrNomDoc, codigo = documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0'), fecha = documento.DtFechaE.ToString("dd/MM/yyyy"), version = documento.IntVersion + "" }, "http");
            string ConvertName = Uri.EscapeDataString(documento.StrNomDoc);
            string _headerUrl = "http://localhost/GenDocument/Cabecera?nomDocument=" + ConvertName + "&codigo=" + documento.StrCodigoDoc + "-" + (documento.IntConsecutivo + "").PadLeft(4, '0') + "&fecha=" + documento.DtFechaE.ToString("dd/MM/yyyy") + "&version=" + documento.IntVersion;

            //string _footerUrl = Url.Action("PiePagina", "GenDocument", null, "http");
            string _footerUrl = "http://localhost/GenDocument/PiePagina";

            //string _portadaUrl = Url.Action("Portada", "GenDocument", new { t = documento.StrNomDoc}, "http");
            string _portadaUrl = "http://localhost/GenDocument/Portada?t="+ ConvertName;

            return new ViewAsPdf("Manual", datos)
            {
                CustomSwitches = string.Format("--header-spacing \"5\" --footer-spacing \"5\" --footer-html {0} --header-html {1} cover {2} toc   --xsl-style-sheet tc.xsl", _footerUrl, _headerUrl, _portadaUrl),
                PageMargins = new Rotativa.Options.Margins(25, 30, 25, 30)
            };
        }
      
    }
}