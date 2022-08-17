using Entidades.Generales;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;

namespace Presentacion.Procesos
{
    public partial class VistaProcesos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<PCProceso> GetProcesos(string nombreProceso, string prefijo, string tipo, string nomProcesoPadre, string estado)
        {
            return DAOProceso.GetPCProcesos(nombreProceso, prefijo, tipo, nomProcesoPadre, estado);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

            GNListaArchivos listaArchivos = new GNListaArchivos
            {
                IntOidGNModulo = 7
            };

            DAOGNListaArchivos.set(listaArchivos);

            listaArchivos = DAOGNListaArchivos.GetUltimo();

            PCProceso proceso = new PCProceso
            {
                StrTipo = "",
                StrRiesgos = "",
                StrLideresProceso = "",
                DtFecha = DateTime.Now,
                IntOidGNListaArchivo = listaArchivos.IntOidGNListaArchivos,
                IntVersion = 1,
                StrAlcance = "",
                StrDocRelacionados = "",
                StrEstado = "0",
                StrNomPro = txtNomProceso.Text,
                StrNormas = "",
                StrObjetivo = "",
                StrPrefijo = "",
                StrProcesoPadre = "",
                StrRecFinancieros = "",
                StrRecHumanos = "",
                IntOidGDSolicitud = 0,
                StrRecursosTec = "",
                StrRecursosInfo = "",
                StrRecursosFis = "",
                IntOidGDDocumento = 0,
                StrFlujoGrama = "",
                StrRecursosMed = ""
            };
            DAOProceso.setProceso(proceso);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "GetProcesos", "GetProcesos();", true);
        }
    }
}