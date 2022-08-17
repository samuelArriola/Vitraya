using Entidades.Generales;
using Entidades.Procesos;
using Newtonsoft.Json;
using Persistencia.Generales;
using Persistencia.procesos;
using Persistencia.Procesos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.Procesos
{
    public partial class CrearProceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            GuardarFlujograma();
            cargarddl();
        }

        public bool verificarDoc()
        {
            int IntOidGDSolicitud = Convert.ToInt32(HttpContext.Current.Request["OIdSolicitud"].ToString()); //obtener id de la solicitud
            try
            {
                return DAOProceso.GetProcesoByOidSol(IntOidGDSolicitud) == null;
            }
            catch (Exception ex)
            {
                return true;
            }

        }


        //public void crearDocumento()
        //{
        //    GNListaArchivos listaArchivos = new GNListaArchivos
        //    {
        //        IntOidGNModulo = 8,
        //    };

        //    DAOGNListaArchivos.set(listaArchivos);
        //    listaArchivos = DAOGNListaArchivos.GetUltimo();

        //    int idSolicitud = Convert.ToInt32(Request["OIdSolicitud"]);

        //    GDSolicitud solicitud = DAOGDSolicitud.GetSolicitud(idSolicitud+"");

        //    PCProceso procesoAux = DAOProceso.BuscarProceso(solicitud.IntOidGNProceso);


        //    GDDocumento documento = new GDDocumento
        //    {
        //        DtFechaE = Convert.ToDateTime("01/01/1800"),
        //        IntEstado = 0,
        //        IntOidGDSolicitud = solicitud.IntOidGDSolicitud,
        //        IntVersion = 1,
        //        StrCodigoDoc = "",
        //        StrNomDoc = solicitud.StrNomDoc,
        //        StrNomSolicitante = solicitud.StrNomUsu,
        //        StrTipDoc = solicitud.StrTipoDoc,
        //        StrUniFunSolicitante = solicitud.StrUnidadFuncional,
        //    };

        //    DAOGDDocumento.SetDocumento(documento);

        //    documento = DAOGDDocumento.GetUltDocumento();


        //    PCProceso proceso = new PCProceso
        //    {
        //        StrTipo = "",
        //        StrRiesgos = "",
        //        StrLideresProceso = "",
        //        DtFecha = DateTime.Now,
        //        IntOidGNListaArchivo = listaArchivos.IntOidGNListaArchivos,
        //        IntVersion = 1,
        //        StrAlcance = "",
        //        StrDocRelacionados = "",
        //        StrEstado = "1",
        //        StrNomPro = solicitud.StrNomDoc,
        //        StrNormas = "",
        //        StrObjetivo = "",
        //        StrPrefijo = "",
        //        StrProcesoPadre = (procesoAux == null ? "" : procesoAux.StrNomPro),
        //        StrRecFinancieros = "",
        //        StrRecHumanos = "",
        //        IntOidGDSolicitud = idSolicitud,
        //        StrRecursosTec = "",
        //        StrRecursosInfo = "",
        //        StrRecursosFis = "",
        //        IntOidGDDocumento = documento.IntOidGDDocumento,
        //        StrFlujoGrama = "",
        //        StrRecursosMed = ""
        //    };
        //    DAOProceso.setProceso(proceso);
        //}

        public void cargarddl()
        {
            List<PCNormagrama> normagramas = DAOPCNormagrama.GetNormagramas();
            List<Cargo> cargos = DAOCargo.GetCargos();
            List<UnidadFuncional> unidadFuncionales = DAOUnidadFuncional.GetInstance().listar();
            List<PCProceso> procesos = DAOProceso.GetProcesosActivos();
            ddlResponsables.Items.Clear();
            ddlRecHumanos.Items.Clear();
            ddlLideres.Items.Clear();
            ddlUnidades.Items.Clear();
            txtNormas.Items.Clear();
            ddlProcesoPadre.Items.Clear();

            ddlProcesoPadre.Items.Add(new ListItem("Seleccione", "-1"));
            ddlLideres.Items.Add(new ListItem("Seleccione", "-1"));
            ddlRecHumanos.Items.Add(new ListItem("Seleccione", "-1"));
            ddlResponsables.Items.Add(new ListItem("Seleccione", "-1"));
            ddlUnidades.Items.Add(new ListItem("Seleccione", "-1"));
            ddlTipPro.Items.Add(new ListItem("Seleccione", "-1"));

            ddlProcesoPadre.Items.Add(new ListItem("No Aplica", "0"));

            foreach (var cargo in cargos)
            {
                ListItem item = new ListItem(cargo.StrGnNomCgo, cargo.IntGnDcCgo.ToString());
                ddlResponsables.Items.Add(item);
                ddlRecHumanos.Items.Add(item);
                ddlLideres.Items.Add(item);
            }

            foreach (var normagrama in normagramas)
            {
                txtNormas.Items.Add(new ListItem(normagrama.StrTipo + " " + normagrama.IntNumNorma + " de " + normagrama.DtmFecEmision.ToString("d"), normagrama.StrTipo + " " + normagrama.IntNumNorma + " de " + normagrama.DtmFecEmision.ToString("d")));
            }
            foreach (var unidad in unidadFuncionales)
            {
                ddlUnidades.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }

            foreach (var proceso in procesos)
            {
                ddlProcesoPadre.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso + ""));
            }
        }


        [WebMethod]
        public static void SetProceso(PCProceso proceso)
        {
            PCProceso procesoAux = DAOProceso.BuscarProceso(proceso.IntOIdProceso);

            proceso.IntOidGNListaArchivo = procesoAux.IntOidGNListaArchivo;
            proceso.IntVersion = procesoAux.IntVersion;
            proceso.DtFecha = DateTime.Now;
            proceso.IntOidGDDocumento = procesoAux.IntOidGDDocumento;
            DAOProceso.setUpProceso(proceso);


            DAOSIPOC.DeleteSIPOCByidProc(proceso.IntOIdProceso);
            foreach (var sipoc in proceso.SIPOCs)
            {
                sipoc.IntOIdProceso = proceso.IntOIdProceso;
                DAOSIPOC.setSipoc(sipoc);
            }

            HttpContext.Current.Session["IntOidGNListaArchivo"] = DAOProceso.GetProcesoUlt().IntOidGNListaArchivo;
        }

        [WebMethod]
        public static void realizarSolicitud(int idProceso)
        {
            PCProceso proceso = DAOProceso.BuscarProceso(idProceso);
            proceso.StrEstado = "1";
            DAOProceso.setUpProceso(proceso);
        }

        public void GuardarFlujograma()
        {
            int IntOidGNListaArchivo = 0;
            try
            {
                IntOidGNListaArchivo = (int)Session["IntOidGNListaArchivo"];
            }
            catch (Exception ex)
            {
                return;
            }

            if (fuImageFlujo.HasFile)
            {
                HttpPostedFile file = fuImageFlujo.PostedFile;
                //se obtiene el nombre del archivo
                string nombre = file.FileName.Substring(0, file.FileName.LastIndexOf("."));

                //se obtiene la extencion del archivo
                string ext = file.FileName;
                ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();

                // Tipo de contenido
                string contentType = file.ContentType;
                // se obtine el array de bit del archivo
                byte[] imagen = new byte[file.InputStream.Length];


                file.InputStream.Read(imagen, 0, imagen.Length);

                List<GNArchivo> archivos = DAOGNArchivo.Listar(IntOidGNListaArchivo);


                GNArchivo archivo = new GNArchivo
                {
                    IntOidGNListaArchivos = IntOidGNListaArchivo,
                    AbteArchivo = imagen,
                    StrContenido = contentType,
                    StrExt = ext,
                    StrNombre = nombre,
                };

                if (archivos.Count == 0)
                {
                    DAOGNArchivo.set(archivo);
                }
                else
                {
                    DAOGNArchivo.deleteArchivo(archivos[0].IntOidGNArchivo);
                    DAOGNArchivo.set(archivo);
                }
            }
        }
        [WebMethod]
        public static string GetProceso(int idSolicitud)
        {
            PCProceso proceso = DAOProceso.BuscarProceso(idSolicitud);
            List<PCSIPOC> Sipocs = DAOSIPOC.getSipocs(proceso.IntOIdProceso + "");
            proceso.SIPOCs = Sipocs;
            return JsonConvert.SerializeObject(proceso);
        }
    }
}