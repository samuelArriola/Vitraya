using Entidades.EstadisticasVitales;
using Logica.EstadisticasVitales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class ModificarValidarCodigo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validarCodigosDisponibles();
        }

        public void validarCodigosDisponibles()
        {
            if (DAOCRCodRuaf.GetCodRuafDefVal().Count == 0)
            {
                lbCodDispDef.InnerHtml = "" + "No hay códigos para Defunción. esperar que admin los suministre al sistema";
                TipValDef.Disabled = true;
            }
            else
            {
                //lbCodDispDef.InnerHtml = "" + "Còdigo RUAF Disponibles Defunción: " + "<strong>" + DAOCRCodRuaf.GetCodRuafDefVal().Count + "</strong>";
                TipValDef.Disabled = false;
            }


            if (DAOCRCodRuaf.GetCodRuafNVVal().Count == 0)
            {
                lbCodDispNacViv.InnerHtml = "" + "No hay códigos para Registros nacidos vivo. esperar que admin los suministre al sistema";
                TipValNacViv.Disabled = true;
            }
            else
            {
                //lbCodDispNacViv.InnerHtml = "" + "Còdigo RUAF Disponibles Nacidos Vivos: " + "<strong>" + DAOCRCodRuaf.GetCodRuafNVVal().Count + "</strong>";
                TipValNacViv.Disabled = false;
            }

        }

        [WebMethod(enableSession: true)]
        public static string cargarValRegNacViv(string CodRuaf)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            List<double> cod = new List<double>();
            cod.Add(DAOCRCodRuaf.getCodRuaf(CodRuaf).DoubleCRcodRuaf);
            RegNacVivs = DAOCRRegNacViv.getRegNacViv((DAOCRCodRuaf.getCodRuaf(CodRuaf).IntOIdCRCodRuaf).ToString(), "Código RUAF");
            Object[] datos = { RegNacVivs, cod };
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod(enableSession: true)]
        public static string cargarValRegDef(string CodRuaf)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();
            List<double> cod = new List<double>();
            cod.Add(DAOCRCodRuaf.getCodRuaf(CodRuaf).DoubleCRcodRuaf);
            RegDefs = DAOCRRegDef.getRegDef((DAOCRCodRuaf.getCodRuaf(CodRuaf).IntOIdCRCodRuaf).ToString(), "Código RUAF");
            Object[] datos = { RegDefs, cod };
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod(enableSession: true)]
        public static string actRegIncidenciaNacViv(string incidencia, string OIdCRCodRuaf, string tipoReg)
        {
            if (tipoReg == "NacViv")
            {
                CRCodRuaf codRuaf = Community.index(DAOCRCodRuaf.GetCodRuafNVVal());
                DAOCRCodRuaf.SetIncidencia(Convert.ToInt32(DAOCRCodRuaf.getCodRuaf(OIdCRCodRuaf).IntOIdCRCodRuaf), incidencia);
                DAOCRRegNacViv.setUpdate(Convert.ToString(codRuaf.IntOIdCRCodRuaf), "Código ruaf", (DAOCRCodRuaf.getCodRuaf(OIdCRCodRuaf).IntOIdCRCodRuaf).ToString());
                DAOCRCodRuaf.cambEstCodRuaf(codRuaf.IntOIdCRCodRuaf);
                Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.
            }

            return JsonConvert.SerializeObject("");
        }

        [WebMethod(enableSession: true)]
        public static string actRegIncidenciaDef(string incidencia, string OIdCRCodRuaf, string tipoReg)
        {
            if (tipoReg == "Defunción")
            {
                CRCodRuaf codRuaf = Community.index(DAOCRCodRuaf.GetCodRuafDefVal());
                DAOCRCodRuaf.SetIncidencia(Convert.ToInt32(DAOCRCodRuaf.getCodRuaf(OIdCRCodRuaf).IntOIdCRCodRuaf), incidencia);
                DAOCRRegDef.setUpdate(Convert.ToString(codRuaf.IntOIdCRCodRuaf), "Código ruaf", (DAOCRCodRuaf.getCodRuaf(OIdCRCodRuaf).IntOIdCRCodRuaf).ToString());
                DAOCRCodRuaf.cambEstCodRuaf(codRuaf.IntOIdCRCodRuaf);
                Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.
            }

            return JsonConvert.SerializeObject("");
        }
    }
}