using System;
using System.Collections.Generic;

namespace Entidades.PlanAccion
{
    public class PAPlanAccion
    {

        //Variables para predefinir el contexto del plan de accion
        public static class CONTEXTO
        {
            public const int ACTAREUNION = 2, PLANACCION = 1, AUDITORIA_EXTERNA = 3;
        }

        public static class ESTADO{
            public const int ASIGNADO = 1, PROCESO = 2, EVALUACION = 3, TERMINADO = 4;
        }

        private int         intOidPlanAccion,
                            intOidARActasC,
                            intGNCodUsu,
                            intcodUsuSegui,
                            intEstAct,
                            intCodUsuApr,
                            intOidInstancia,
                            intContexto;

        private string      strActividad,
                            strNomEst,
                            strSoporte,
                            strNombreUsuarioAprueba,
                            strNombreUsuarioSeguimiento,
                            strNombreUsuarioResponsable,
                            strNombreUsuarioCreador,
                            strComo,
                            strDonde,
                            strPorQue,
                            strCuanto,
                            strProceso,
                            strFuente,
                            strDescriptcion,
                            strOrigen;

        private DateTime    dtmFecIniActa,
                            dtmFecFinalActa;

        private List<PAUsuario> usuarios;

        public int IntOidPAPlanAccion { get => intOidPlanAccion; set => intOidPlanAccion = value; }
        public int IntOidARActasC { get => intOidARActasC; set => intOidARActasC = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public int IntcodUsuSegui { get => intcodUsuSegui; set => intcodUsuSegui = value; }
        public int IntEstAct { get => intEstAct; set => intEstAct = value; }
        public int IntCodUsuApr { get => intCodUsuApr; set => intCodUsuApr = value; }
        public string StrActividad { get => strActividad; set => strActividad = value; }
        public string StrNomEst { get => strNomEst; set => strNomEst = value; }
        public DateTime DtmFecIniActa { get => dtmFecIniActa; set => dtmFecIniActa = value; }
        public DateTime DtmFecFinalActa { get => dtmFecFinalActa; set => dtmFecFinalActa = value; }
        public string StrSoporte { get => strSoporte; set => strSoporte = value; }
        public string StrNombreUsuarioAprueba { get => strNombreUsuarioAprueba; set => strNombreUsuarioAprueba = value; }
        public string StrNombreUsuarioSeguimiento { get => strNombreUsuarioSeguimiento; set => strNombreUsuarioSeguimiento = value; }
        public string StrNombreUsuarioResponsable { get => strNombreUsuarioResponsable; set => strNombreUsuarioResponsable = value; }
        public int IntOidInstancia { get => intOidInstancia; set => intOidInstancia = value; }
        public string StrComo { get => strComo; set => strComo = value; }
        public string StrDonde { get => strDonde; set => strDonde = value; }
        public string StrPorQue { get => strPorQue; set => strPorQue = value; }
        public string StrCuanto { get => strCuanto; set => strCuanto = value; }
        public string StrProceso { get => strProceso; set => strProceso = value; }
        public string StrFuente { get => strFuente; set => strFuente = value; }
        public string StrDescriptcion { get => strDescriptcion; set => strDescriptcion = value; }
        public int IntContexto { get => intContexto; set => intContexto = value; }
        public string StrOrigen { get => strOrigen; set => strOrigen = value; }
        public List<PAUsuario> Usuarios { get => usuarios; set => usuarios = value; }
        public string StrNombreUsuarioCreador { get => strNombreUsuarioCreador; set => strNombreUsuarioCreador = value; }
    }
}