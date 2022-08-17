
using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;

namespace Generales_1._0.Home.dashboard.production.screens.trainings
{
    public partial class ActaCapacitacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            generarActaCapacitacion();
        }

        public void generarActaCapacitacion()
        {
            int idCapa = Convert.ToInt32(Request["idCapacitacion"].ToString());
            CPCAPACITACION Capacitacion = DAOCPCapacitacion.GetCapacitacion(idCapa);
            List<CPAgenda> agendas = DAOCPAgenda.GetAgendasByCapacitacion(idCapa);
            CPEJETEMATICO ejeTematico = DAOCPEjeTematico.GetEjeTematico(Capacitacion.IntOidCPEJETEMA);

            foreach (var agenda in agendas)
            {
                string tableContentAgenda = $@" 
                    <table style=""margin-top: 2px;"">
                        <tr>
                            <th style=""text-align:center"">Lugar</th>
                            <th style=""text-align:center"">Fecha de inicio</th>
                            <th style=""text-align:center"">Fecha de Finalización</th>
                        </tr>
                        <tr>
                            <td>{agenda.StrLugar}</td>
                            <td>{agenda.DtmFecha.ToString("dd/MM/yyyyy")}  {agenda.DtmHoraInicial.ToString("hh:mm")}</td>
                            <td>{agenda.DtmFechaFinal.ToString("dd/MM/yyyy")}  {agenda.DtmHoraFinal.ToString("hh:mm")}</td>
                        </tr>
                    </table>
                    <table style=""margin-top: 2px;"">
                        <tr>
                            <th style=""text-align:center"">Subtemas</th>
                        </tr>
                ";

                List<CPSUBTEMA> subtemas = DAOCPSUBTEMA.GetSubtemasByAgenda(agenda.IntOidCPAgenda);
                foreach (var subtema in subtemas)
                {
                    tableContentAgenda += $@"
                        <tr>
                            <td>{subtema.StrSUBTEMA}</td>
                        </tr>
                    ";
                }

                tableContentAgenda += $@"
                    <table style=""margin-top: 2px;"">
                        <thead>
                            <tr><td colspan=""4"" class=""text-center"">Asistencia</td></tr>
                            <tr>
                                <th class=""text-center"">Identificacion</th>
                                <th class=""text-center"">Nombre</th>
                                <th class=""text-center"">Cargo</th>
                                <th class=""text-center"">Firma</th>
                            </tr>
                        </thead>
                        <tbody>
                ";

                List<CPMatricula> matriculas = DAOCPMatricula.GetMatriculasFirmadasByAgenda(agenda.IntOidCPAgenda);
                foreach (var matricula in matriculas)
                {
                    Usuario usuario = DAOUsuario.getInstance().GetUsuario(matricula.IntGNCodUsu);
                    tableContentAgenda += $@"
                        <tr>
                            <td>{matricula.IntGNCodUsu}</td>
                            <td>{usuario.GNNomUsu1}</td>
                            <td>{usuario.GnCargo1}</td>
                            <td><img src=""data:image/gif;base64,{Convert.ToBase64String(usuario.GNFmUsu1)}"" width=""100"" /></td>
                        </tr>
                    ";
                }

                tableContentAgenda += "</table></tbody>";
                contenidoActa.InnerHtml += tableContentAgenda;
            }

            List<CPSUBTEMA> temas = DAOCPSUBTEMA.GetSubtemas(idCapa);
            lbLugar.InnerText = Capacitacion.StrLUGAR;
            lbHoraFinal.InnerText = Capacitacion.DtmHORAFINAL.ToString("t");
            lbHoraInicial.InnerText = Capacitacion.DtmHORAINICIAL.ToString("t");
            lbCodigo.InnerText = Capacitacion.StrCODIGO;


            lbFecha.InnerText = Capacitacion.DtmFECHA.ToString("d");

            lbNombre.InnerText = ("Acta de Asistencia de " + Capacitacion.StrTEMA).ToUpper();


        }

    }
}