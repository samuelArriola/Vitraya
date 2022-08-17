using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.proceedings;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class ActasReunionController : Controller
    {
        public ActionResult Acta(int id)
        {
            CultureInfo.CurrentCulture = new CultureInfo("es-ES");
            CultureInfo.CurrentUICulture = new CultureInfo("es-ES");

            // se crea una instancia de la logica
            ActasReunionLogica actasReunionLogica = new ActasReunionLogica();

            //se consulta el el acta de la reunion por el id de la reunion
            ARActasC acta = actasReunionLogica.getActasC(id);

            //se consulta una lista de los usuario que han firmado el acta
            List<ARActasDM> usuarios = DAOARactasDM.GerAsistentes(acta.IntOidARActas);

            //se consulta un listado de los tamas tratados en el acta
            List<ARActasTemas> temas = actasReunionLogica.GetARActasTemas(acta.IntOidARActas);

            //se consulta un listado de los mienbros del acta
            List<ARActasDM> participantes = DAOARactasDM.getParticipantes(acta.IntOidARActas);

            string dtParticipantes = "";

            //se crea la tabla de los miembros del acta
            foreach (ARActasDM participante in participantes)
            {
                //se consulta el usuario para utilisar los datos 
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(participante.IntGNCodUsu);

                //se crean la filas pra la tabla de la asistencia
                dtParticipantes += $"" +
                    $"<tr>" +
                    $"  <td>{usuario.GNNomUsu1}</td>" +
                    $"  <td>{usuario.GnCargo1}</td>" +
                    $"  <td>{(participante.IntEstUsuario == 1 ? "Presente" : "No Presente")}</td>" +
                    $"</tr>";

            }

            //variable donde se va a almacenar el contenido de los temas  tratados
            string contenidoTemas = "";

            //varible en la cual se almacenara la informacion del orden del dia
            string lstOrden = "";

            //se crea el codigo html para mostrar la informacion de los temas que pertenecen al acta
            foreach (ARActasTemas tema in temas)
            {

                contenidoTemas += "<div class=\"sec\"><div class='tema'><h4 class=\"text-center \">" + tema.StrNomTema + "</h4><br><div>" + tema.StrDesarrollo + "</div></div></div>";

                string anexos = "";

                //por cada archivo anexo al tema se agrega un link en el cual descargar la informacion 
                List<GNArchivo> archivos = DAOGNArchivo.Listar(tema.IntOidGNListaArchivos);
                if (archivos.Count > 0)
                {
                    anexos = "<h5 class=\"text-center\">Anexos al Tema</h5><ul>";
                }

                foreach (GNArchivo archivo in archivos)
                {
                    anexos += $@"<a href=""{Request.Url.GetLeftPart(UriPartial.Authority)}/proceedings/Archivos.aspx?id={archivo.IntOidGNArchivo}"">" + archivo.StrNombre + "</a>";
                }
                anexos += "</ul>";

                contenidoTemas += anexos;


                lstOrden += $"<li>{tema.StrNomTema}</li>";
            }

            lstOrden += "<li>Aprobación del acta.</li>";

            var compromisos = DAOPAPlanAccion.GetCompromisosActa(acta.IntOidARActas);

            //se crea la tabla html para mostrar la informacion de los compromisos en la reunion
            string tbCompromisos = "" +
            "<table class='table-border'>" +
            "   <thead>" +
            "       <tr >" +
            "           <td>¿Quién?</td>" +
            "           <td>¿Qué?</td>" +
            "           <td>¿Cómo?</td>" +
            "           <td>¿Cuándo?</td>" +
            "       </tr>" +
            "   </thead>" +
            "   <tbody>";

            //se carga cada fila de los compromisos
            foreach (PAPlanAccion compromiso in compromisos)
            {
                tbCompromisos += "" +
                "<tr>" +
                "   <td>" + compromiso.StrNombreUsuarioResponsable + "</td>" +
                "   <td>" + compromiso.StrActividad + "</td>" +
                "   <td>" + compromiso.StrComo + "</td>" +
                "   <td>" + compromiso.DtmFecFinalActa.ToString("MM/dd/yyyy") + "</td>" +
                "</tr>";
            }
            tbCompromisos += "</tbody></table>";


            //se crea una tabla html con el contido de los asistente con sus respectiva firma
            string asistentes = "" +
                "<table class='table-border'>" +
                "   <thead>" +
                "       <tr >" +
                "           <td>Nombre</td>" +
                "           <td>Cargo</td>" +
                "           <td>Roles</td>" +
                "           <td>Firma</td>" +
                "       </tr>" +
                "   </thead>" +
                "   <tbody>";

            foreach (var asistente in usuarios)
            {
                Usuario usuario = DAOUsuario.getInstance().GetUsuario(asistente.IntGNCodUsu);


                asistentes += "" +
                "<tr>" +
                "   <td>" + usuario.GNNomUsu1 + "</td>" +
                "   <td>" + usuario.GnCargo1 + "</td>" +
                $"   <td>{asistente.StrTipoUsuario}</td>" +
                "   <td><img src=\"data:image/png;base64," + Convert.ToBase64String(usuario.GNFmUsu1) + "\" width=\"100\" /></td>" +
                "</tr>";
            }

            asistentes += "</tbody></table>";


            //arreglo en el cual se guradara toda la informacion de del acta la cual sera mostrada en la vista 
            object[] datos = { acta, usuarios, contenidoTemas, dtParticipantes, lstOrden, tbCompromisos, asistentes };


            // cabecera del acta
            //string _headerUrl = Url.Action("Cabecera", "GenDocument", new { nomDocument = acta.StrNombre, codigo = acta.StrSigla + (acta.IntCodigo + "").PadLeft(4, '0'), fecha = acta.DtmFechEditable.ToString("dd/MM/yyyy"), version = "1" }, "Http");
            string ConvertName = Uri.EscapeDataString(acta.StrNombre);
            string _headerUrl = "http://localhost/GenDocument/Cabecera?nomDocument=" + ConvertName + "&codigo=" + acta.StrSigla + (acta.IntCodigo + "").PadLeft(4, '0') + "&fecha=" + acta.DtmFechEditable.ToString("dd/MM/yyyy") + "&version=1";

            // pie de paagina del aca
            //string _footerUrl = Url.Action("PiePagina", "GenDocument", null, "Http");
            string _footerUrl = "http://localhost/GenDocument/PiePagina";

            //return View(datos); 

            return new ViewAsPdf("Acta", datos)
            {
                CustomSwitches = string.Format("--footer-html {0} --footer-spacing  \"5\" --header-html {1} --header-spacing \"5\"", _footerUrl, _headerUrl),
                PageMargins = new Rotativa.Options.Margins(40, 10, 25, 10)

            };
        }
    }
}