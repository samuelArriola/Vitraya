using Entidades.GestionDocumental;
using Entidades.Procesos;
using Persistencia.GestionDocumental;
using Persistencia.procesos;
using Persistencia.Procesos;
using System;

namespace Presentacion.Procesos
{
    public partial class PrintProceso : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos()
        {
            int OIdProceso = Convert.ToInt32(Request["OIdProceso"]);
            PCProceso proceso = DAOProceso.BuscarProceso(OIdProceso);

            GDDocumento documento = DAOGDDocumento.GetDocumento(proceso.IntOidGDDocumento);

            proceso.SIPOCs = DAOSIPOC.getSipocs(Convert.ToString(OIdProceso));

            codigo.InnerText = documento.StrCodigoDoc;
            NomPro1.InnerText = documento.StrNomDoc;
            version.InnerText = documento.IntVersion + "";
            fecha.InnerText = documento.DtFechaE.ToString("d");
            tipo.InnerText = proceso.StrTipo;
            proPadre.InnerText = proceso.StrProcesoPadre == "0" ? "No Aplica" : proceso.StrProcesoPadre;
            prefijo.InnerText = proceso.StrPrefijo;
            LiderPro.InnerText = proceso.StrLideresProceso + "";
            Alcance.InnerText = proceso.StrAlcance;
            Objetivo.InnerText = proceso.StrObjetivo;
            Normativa.InnerText = proceso.StrNormas;
            TalHumano.InnerText = proceso.StrRecHumanos;
            financieros.InnerText = proceso.StrRecFinancieros;
            Fisicos.InnerText = proceso.StrRecursosFis;
            informaticos.InnerText = proceso.StrRecursosInfo;
            tecnologicos.InnerText = proceso.StrRecursosTec;
            medicamentos.InnerText = proceso.StrRecursosMed;



            string sipocs = "<table class=\"table-b\">" +
                            "<tr>" +
                            "    <td><strong>PROVEEDOR</strong></td>" +
                            "    <td><strong>ENTRADA</strong></td>" +
                            "    <td colspan=\"2\"><strong>GESTÓN PHVA</strong></td>" +
                            "    <td><strong>RESPONSABLE</strong></td>" +
                            "    <td><strong>SALIDA</strong></td>" +
                            "    <td><strong>CLIENTE</strong></td>" +
                            "</tr>";

            foreach (var Sipoc in proceso.SIPOCs)
            {
                if (Sipoc.StrTipoAct[0].ToString().ToUpper() == "P")
                    sipocs += "<tr>" +
                              "      <td>" + Sipoc.StrProveedores + "</td>" +
                              "      <td>" + Sipoc.StrEntrada + "</td>" +
                              "      <td style=\"padding:0 25px; font-weight: 900\">" + ConvertTextToVertical(Sipoc.StrTipoAct.ToUpper()) + "</td>" +
                              "      <td>" + Sipoc.StrActividad + "</td>" +
                              "      <td>" + Sipoc.StrResponsables + "</td>" +
                              "      <td>" + Sipoc.StrSalidas + "</td>" +
                              "      <td>" + Sipoc.StrClientes + "</td>" +
                              "</tr>";

            }
            foreach (var Sipoc in proceso.SIPOCs)
            {
                if (Sipoc.StrTipoAct[0].ToString().ToUpper() == "H")
                    sipocs += "<tr>" +
                              "      <td>" + Sipoc.StrProveedores + "</td>" +
                              "      <td>" + Sipoc.StrEntrada + "</td>" +
                              "      <td style=\"padding:0 25px; font-weight: 900\">" + ConvertTextToVertical(Sipoc.StrTipoAct.ToUpper()) + "</td>" +
                              "      <td>" + Sipoc.StrActividad + "</td>" +
                              "      <td>" + Sipoc.StrResponsables + "</td>" +
                              "      <td>" + Sipoc.StrSalidas + "</td>" +
                              "      <td>" + Sipoc.StrClientes + "</td>" +
                              "</tr>";

            }
            foreach (var Sipoc in proceso.SIPOCs)
            {
                if (Sipoc.StrTipoAct[0].ToString().ToUpper() == "V")
                    sipocs += "<tr>" +
                              "      <td>" + Sipoc.StrProveedores + "</td>" +
                              "      <td>" + Sipoc.StrEntrada + "</td>" +
                              "      <td style=\"padding:0 25px; font-weight: 900\">" + ConvertTextToVertical(Sipoc.StrTipoAct.ToUpper()) + "</td>" +
                              "      <td>" + Sipoc.StrActividad + "</td>" +
                              "      <td>" + Sipoc.StrResponsables + "</td>" +
                              "      <td>" + Sipoc.StrSalidas + "</td>" +
                              "      <td>" + Sipoc.StrClientes + "</td>" +
                              "</tr>";

            }
            foreach (var Sipoc in proceso.SIPOCs)
            {
                if (Sipoc.StrTipoAct[0].ToString().ToUpper() == "A")
                    sipocs += "<tr>" +
                              "      <td>" + Sipoc.StrProveedores + "</td>" +
                              "      <td>" + Sipoc.StrEntrada + "</td>" +
                              "      <td style=\"padding:0 25px; font-weight: 900\">" + ConvertTextToVertical(Sipoc.StrTipoAct.ToUpper()) + "</td>" +
                              "      <td>" + Sipoc.StrActividad + "</td>" +
                              "      <td>" + Sipoc.StrResponsables + "</td>" +
                              "      <td>" + Sipoc.StrSalidas + "</td>" +
                              "      <td>" + Sipoc.StrClientes + "</td>" +
                              "</tr>";

            }
            sipocs += "</table>";
            tblSipoc.InnerHtml = sipocs;
        }


        public string ConvertTextToVertical(string text)
        {
            string stringAux = "";

            for (int i = 0; i < text.Length; i++)
                stringAux += text[i] + "<br />";
            return stringAux;
        }
    }
}