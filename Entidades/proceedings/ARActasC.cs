using System;

namespace Entidades.PlanAccion
{
    public class ARActasC
    {

        private int         intOidARActas,
                            intOidAReunionC,
                            intGNCodUsu,
                            intEstado,
                            intCodigo,
                            intUsuarioCreador;


        private string      strLugarReun,
                            strObjetivo,
                            strSigla,
                            strNombre,
                            strLink,
                            strCoordinador,
                            strSecretario;
                            

        private DateTime    dtmFecInicio,
                            dtmFecFinal,
                            dtmFechEditable,
                            dtmFecSistema;

        public int IntOidARActas { get => intOidARActas; set => intOidARActas = value; }
        public int IntOidAReunionC { get => intOidAReunionC; set => intOidAReunionC = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public int IntEstado { get => intEstado; set => intEstado = value; }
       
        public string StrLugarReun { get => strLugarReun; set => strLugarReun = value; }
        public string StrObjetivo { get => strObjetivo; set => strObjetivo = value; }
        public DateTime DtmFecInicio { get => dtmFecInicio; set => dtmFecInicio = value; }
        public DateTime DtmFecFinal { get => dtmFecFinal; set => dtmFecFinal = value; }
        public DateTime DtmFechEditable { get => dtmFechEditable; set => dtmFechEditable = value; }
        public DateTime DtmFecSistema { get => dtmFecSistema; set => dtmFecSistema = value; }
        public int IntCodigo { get => intCodigo; set => intCodigo = value; }
        public string StrSigla { get => strSigla; set => strSigla = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrLink { get => strLink; set => strLink = value; }
        public string StrCoordinador { get => strCoordinador; set => strCoordinador = value; }
        public string StrSecretario { get => strSecretario; set => strSecretario = value; }
        public int IntUsuarioCreador { get => intUsuarioCreador; set => intUsuarioCreador = value; }
    }
}