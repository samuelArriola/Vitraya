using Entidades.Generales;
using Entidades.PlanAccion;
using Entidades.Power_BI;
using Logica.proceedings;
using Persistencia.Generales;
using Persistencia.Power_BI;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.proceedings
{
    public partial class Convocatorias2 : System.Web.UI.Page
    {

        public static int idActa = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["idActa"] = 0;
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuarios()
        {
            return PAdministrarReportes.GetUsuarios();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getCargos()
        {
            return PAdministrarReportes.GetCargos();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUnidadesFuncionales()
        {
            return PAdministrarReportes.GetUnidadesFuncionales();
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuariosPorCargo(int idCargo)
        {
            return PAdministrarReportes.GetUsuariosPorCargo(idCargo);
        }

        [WebMethod]
        public static List<EAdministrarReportes> getUsuariosPorUF(int idUnidadF)
        {
            return PAdministrarReportes.GetUsuariosPorUnidadF(idUnidadF);
        }

        [WebMethod]
        public static List<ARActasC> getComitesProgramados()
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            int idUsuario = usuario.GNCodUsu1;

            return DAOARActasC.ListarActasProgramadas(idUsuario);
        }

        [WebMethod]
        public static List<ARActasC> getComitesConvocados()
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            int idUsuario = usuario.GNCodUsu1;

            return DAOARActasC.GetActasConvocadas(idUsuario);
        }

        [WebMethod]
        public static List<UsuariosParticipantes> getMiembrosC(int idComite)
        {
            return DAOAReunionD.GetUsuariosParticipantes(idComite);
        }

        [WebMethod]
        public static List<ARAgenda> getTemasC(int idComite)
        {
            return DAOARAgenda.GetInstance().listar(idComite);
        }

        [WebMethod]
        public static void setConvocatoriaRG(string nombreReunion, string unidadFuncionalReunion, string coordinadorReunion, string fechaReunion, string lugarReunion, string linkReunion)
        {
            UnidadFuncional unidadFuncional = DAOUnidadFuncional.GetUnidadFuncional(Convert.ToInt32(unidadFuncionalReunion));
            GNDireccion direccion = DAOGNDireccion.GetGNDireccion(unidadFuncional.GnCdAra1);

            string sigla = "ACT-" + direccion.StrSiglaDir + "-" + unidadFuncional.GnSiglaUnf1 + "-";

            ActasReunionLogica logica = new ActasReunionLogica();

            AReunionC comite = logica.GetAReunionC(6017);

            ARActasC acta = new ARActasC
            {
                DtmFecFinal = Convert.ToDateTime(fechaReunion),
                DtmFechEditable = Convert.ToDateTime(fechaReunion),
                DtmFecInicio = Convert.ToDateTime(fechaReunion),
                DtmFecSistema = Convert.ToDateTime(fechaReunion),
                IntCodigo = DAOARActasC.GetCodigo(sigla),
                IntEstado = 1,
                IntGNCodUsu = Convert.ToInt32(coordinadorReunion),
                IntOidAReunionC = 6017,
                StrLink = linkReunion,
                StrLugarReun = lugarReunion,
                StrNombre = nombreReunion,
                StrObjetivo = "",
                StrSigla = sigla,
            };
            //logica.CargarActa(acta);
            idActa = logica.CargarActa2(acta);

            acta = logica.GetARActasCUltima(6017);

            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(coordinadorReunion));

            ARActasDM miembro = new ARActasDM
            {
                BlnFirmado = false,
                IntEstUsuario = 1,
                IntGNCodUsu = usuario.GNCodUsu1,
                //IntOidARActasC = acta.IntOidARActas,
                IntOidARActasC = idActa,
                StrNombre = usuario.GNNomUsu1,
                StrTipoUsuario = "Coordinador",
            };

            DAOARactasDM.set(miembro);
        }

        [WebMethod]
        public static void setMiembros(int identificacionU, string nombreU, string tipoU)
        {
            ActasReunionLogica logica = new ActasReunionLogica();
            ARActasC acta = new ARActasC();

            acta = logica.GetARActasCUltima(6017);

            DAOARactasDM.set(new ARActasDM
            {
                BlnFirmado = false,
                IntEstUsuario = 1,
                IntGNCodUsu = identificacionU,
                //IntOidARActasC = acta.IntOidARActas,
                IntOidARActasC = idActa,
                StrNombre = nombreU,
                StrTipoUsuario = tipoU,
            });
        }

        [WebMethod]
        public static void setTemas(string nombreTema)
        {
            ActasReunionLogica logica = new ActasReunionLogica();
            ARActasC acta = new ARActasC();

            acta = logica.GetARActasCUltima(6017);

            logica.CargarTema(new ARActasTemas
            {
                //IntOidARActasC = acta.IntOidARActas,
                IntOidARActasC = idActa,
                StrAdjuntar = "",
                StrDesarrollo = "",
                StrNomTema = nombreTema
            });
        }

        [WebMethod]
        public static void setConvocatoriaCP(int idActaComite, string fechaReunion, string lugarReunion)
        {
            idActa = idActaComite;
            int idActaC = Convert.ToInt32(idActaComite);
            ARActasC acta = DAOARActasC.get(idActaC);
            acta.StrLugarReun = lugarReunion;
            acta.DtmFecInicio = Convert.ToDateTime(fechaReunion);
            acta.DtmFecFinal = Convert.ToDateTime(fechaReunion);
            acta.IntEstado = 1;
            acta.IntCodigo = DAOARActasC.GetCodigo(acta.StrSigla);

            //CargarTemaCompromisos(acta);

            DAOARActasC.update(acta);
           
        }

        [WebMethod]
        public static void intoDesarrollo(int idActa)
        {
            
            //Session["idActa"] = idActa;
            HttpContext.Current.Session["idActa"] = idActa;

            //Response.Redirect("RecordMinutes.aspx");

        }

    }
}