using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Generales_1._0.Home.dashboard.production.screens.proceedings
{
    public partial class Acta : System.Web.UI.Page

    {

        protected void Page_Load(object sender, EventArgs e)
        {
            generarActa();
        }

        public void generarActa()
        {
            try
            {
                // se crea una instancia de la logica
                ActasReunionLogica actasReunionLogica = new ActasReunionLogica();

                //se consulta el el acta de la reunion por el id de la reunion
                ARActasC acta = actasReunionLogica.getActasC(Convert.ToInt32(Request["idActa"] ?? Session["idActa"]));

                //se consulta una lista de los usuario que han firmado el acta
                List<ARActasDM> usuarios = DAOARactasDM.GerAsistentes(acta.IntOidARActas);

                //se consulta un listado de los tamas tratados en el acta
                List<ARActasTemas> temas = actasReunionLogica.GetARActasTemas(acta.IntOidARActas);

                //se construye el codigo del acta
                string CodigoActa = "";
                for (int i = 0; i < 4 - acta.IntCodigo.ToString().Length; i++)
                {
                    CodigoActa += "0";
                }

                CodigoActa += acta.IntCodigo;


                //se pasa la informacion que se va a imprimir
                codigoActa.InnerText = acta.StrSigla + CodigoActa;
                lugar.InnerText = acta.StrLugarReun;
                fecha.InnerText = acta.DtmFechEditable.ToString();
                horInicio.InnerText = acta.DtmFecInicio.ToString("t");
                horaFinal.InnerText = acta.DtmFecFinal.ToString("t");
                txtNombre1.InnerText = acta.StrNombre.ToUpper();
                txtNombre.InnerText = acta.StrNombre;
                txtNombre2.InnerText = acta.StrNombre;
                txtFecha.InnerText = acta.DtmFechEditable.ToString("D");
                lbFecha1.InnerText = acta.DtmFechEditable.ToString("d");
                txtHora.InnerText = acta.DtmFecInicio.ToString("t");
                txtlugar.InnerText = acta.StrLugarReun;
                txtMes.InnerText = acta.DtmFechEditable.ToString("MMMM");

                //variable donde se guradara la tabla de los miembros e invitados
                string participantes = "";

                //se consulta un listado de los mienbros del acta
                List<ARActasDM> paraticipantes = DAOARactasDM.getParticipantes(acta.IntOidARActas);

                //se crea la tabla de los miembros del acta
                foreach (ARActasDM participante in paraticipantes)
                {
                    //se consulta el usuario para utilisar los datos 
                    Usuario usuario = DAOUsuario.getInstance().GetUsuario(participante.IntGNCodUsu);

                    //se crean la filas pra la tabla de la asistencia
                    participantes += $"" +
                        $"<tr>" +
                        $"  <td>{usuario.GNNomUsu1}</td>" +
                        $"  <td>{usuario.GnCargo1}</td>" +
                        $"  <td>{(participante.IntEstUsuario == 1 ? "Presente" : "No Presente")}</td>" +
                        $"</tr>";
                }
                //se pasa la informacion de los participante a la tabla de la asitencia
                tableAsistencia.InnerHtml = participantes;


                //variable donde se va a almacenar el contenido de los temas  tratados
                string contenidoTemas = "";

                //
                string lstOrden = "";

                foreach (ARActasTemas tema in temas)
                {

                    contenidoTemas += "<div class=\"sec\"><div class='tema'><h4 class=\"text-center \">" + tema.StrNomTema + "</h4><br><div>" + tema.StrDesarrollo + "</div></div></div>";

                    string anexos = "";

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


                ordenDia.InnerHtml += lstOrden;

                var compromisos = DAOPAPlanAccion.GetCompromisosActa(acta.IntOidARActas);

                string Scompromisos = "" +
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

                foreach (PAPlanAccion compromiso in compromisos)
                {
                    Scompromisos += "" +
                    "<tr>" +
                    "   <td>" + compromiso.StrNombreUsuarioResponsable + "</td>" +
                    "   <td>" + compromiso.StrActividad + "</td>" +
                    "   <td>" + compromiso.StrComo + "</td>" +
                    "   <td>" + compromiso.DtmFecFinalActa.ToString("MM/dd/yyyy") + "</td>" +
                    "</tr>";
                }
                Scompromisos += "</tbody></table>";

                planes.InnerHtml = Scompromisos;

                desarrollo.InnerHtml = contenidoTemas;

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


                Usuario usuarioCreador = DAOUsuario.getInstance().GetUsuario(acta.IntUsuarioCreador);

                participantesFirma.InnerHtml = asistentes;

                try
                {
                    participantesFirma.InnerHtml += $"<br/> Acta elaborada por: {usuarioCreador.GNNomUsu1}, {usuarioCreador.GnCargo1}";
                }
                catch (Exception)
                {

                }
            }

            catch (Exception ex)
            {
                
            }
        }
    }
}