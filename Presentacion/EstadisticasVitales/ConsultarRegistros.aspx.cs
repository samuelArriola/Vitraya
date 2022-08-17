using Entidades.EstadisticasVitales;
using Entidades.Generales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class ConsultarRegistros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string cargarRegNacViv(string dato, string opc, string fechaMin, string fechaMax)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            if (opc == "Código RUAF")
            {
                CRCodRuaf CodRuaf = DAOCRCodRuaf.getCodRuaf(dato);
                RegNacVivs = DAOCRRegNacViv.getRegNacViv(Convert.ToString(CodRuaf.IntOIdCRCodRuaf), opc);
            }
            else
                RegNacVivs = DAOCRRegNacViv.getRegNacVivs(dato, opc);

            List<double> codRuaf = new List<double>();
            foreach (CRRegNacViv NacViv in RegNacVivs)
            {
                codRuaf.Add(DAOCRCodRuaf.getCodRuafId(Convert.ToString(NacViv.IntCRCodRuaf)).DoubleCRcodRuaf);
            }

            Object[] datos = { RegNacVivs, codRuaf };
            try
            {
                string result = JsonConvert.SerializeObject(datos);
                return result;
            }
            catch
            {
                return "";
            }

        }

        [WebMethod(enableSession: true)]
        public static string cargarRegNacVivFec(string dato, string opc, string fechaMin, string fechaMax)
        {
            List<CRRegNacViv> RegNacVivs = DAOCRRegNacViv.getRegNacVivsFec(dato, opc, fechaMin, fechaMax);
            List<double> codRuaf = new List<double>();
            foreach (CRRegNacViv NacViv in RegNacVivs)
            {
                codRuaf.Add(DAOCRCodRuaf.getCodRuafId(Convert.ToString(NacViv.IntCRCodRuaf)).DoubleCRcodRuaf);
            }

            Object[] datos = { RegNacVivs, codRuaf };
            return JsonConvert.SerializeObject(datos);
        }

        [WebMethod(enableSession: true)]
        public static string cargarRegDef(string dato, string opc, string fechaMin, string fechaMax)
        {

            List<CRRegDef> RegDefs = new List<CRRegDef>();
            if (opc == "Código RUAF")
            {
                CRCodRuaf CodRuaf = DAOCRCodRuaf.getCodRuaf(dato);
                RegDefs = DAOCRRegDef.getRegDef(Convert.ToString(CodRuaf.IntOIdCRCodRuaf), opc);
            }
            else
                RegDefs = DAOCRRegDef.getRegDefs(dato, opc);

            List<double> codRuaf = new List<double>();
            foreach (CRRegDef Def in RegDefs)
            {
                codRuaf.Add(DAOCRCodRuaf.getCodRuafId(Convert.ToString(Def.IntOIdCRCodRuaf)).DoubleCRcodRuaf);
            }
            Object[] datos = { RegDefs, codRuaf };
            string result = JsonConvert.SerializeObject(datos);
            return result;
        }

        [WebMethod(enableSession: true)]
        public static string cargarRegDefFec(string dato, string opc, string fechaMin, string fechaMax)
        {
            List<CRRegDef> RegDefs = DAOCRRegDef.getRegDefsFec(dato, opc, fechaMin, fechaMax);
            List<double> codRuaf = new List<double>();
            foreach (CRRegDef Def in RegDefs)
            {
                codRuaf.Add(DAOCRCodRuaf.getCodRuafId(Convert.ToString(Def.IntOIdCRCodRuaf)).DoubleCRcodRuaf);
            }
            Object[] datos = { RegDefs, codRuaf };
            return JsonConvert.SerializeObject(datos);
        }

        // retornar a la vista los permisos de usuario para con la vista  consultar!! 
        [WebMethod(enableSession: true)]
        public static string ValidarPermisosMod()
        {
            int idUsuario = Convert.ToInt32(HttpContext.Current.Session["admin"]);
            int idRol = DAOGNRoles.buscarRolUsuario(idUsuario);

            bool[] datos = { false, false };

            List<GNPermisos> permisos = DAOGNPermisos.get(idRol);
            foreach (GNPermisos permiso in permisos)
            {
                if ((permiso.BlnConfirmar && permiso.BlnCrear && permiso.BlnEliminar && permiso.BlnModificar) &&
                  (permiso.IntOidGNOpcion == 1026))
                {
                    datos[1] = true;
                    break;
                }

            }

            foreach (GNPermisos permiso in permisos)
            {
                if (permiso.BlnModificar && permiso.IntOidGNOpcion == 1026)
                {
                    datos[0] = true;
                    break;
                }

            }

            return JsonConvert.SerializeObject(datos);
        }


        [WebMethod(enableSession: true)]
        public static string getHistoryNV(string IdMadre)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();

            if (IdMadre != "")
                RegNacVivs = DAOCRRegNacViv.getHistoryNV(IdMadre);

            return JsonConvert.SerializeObject(RegNacVivs);
        }

        [WebMethod(enableSession: true)]
        public static string getHistoryDef(string IdPaciente)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();

            if (IdPaciente != "")
                RegDefs = DAOCRRegDef.getHistoryDef(IdPaciente);

            return JsonConvert.SerializeObject(RegDefs);
        }

    }
}