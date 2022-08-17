
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Auditorias
{
    public class AuditoriaExterna
    {
        private int         intOIdAuditoriaExterna,
                            intOidListaArchivos,
                            intOidUsuarioCreador;

        private string      strEnteAuditor,
                            strAuditores,
                            strObjetivo,
                            strProcesos,
                            strHallasgos,
                            strAlcance;
        
        private DateTime    dtmFecha;

        public int IntOIdAuditoriaExterna { get => intOIdAuditoriaExterna; set => intOIdAuditoriaExterna = value; }
        public int IntOidListaArchivos { get => intOidListaArchivos; set => intOidListaArchivos = value; }
        public string StrEnteAuditor { get => strEnteAuditor; set => strEnteAuditor = value; }
        public string StrAuditores { get => strAuditores; set => strAuditores = value; }
        public string StrObjetivo { get => strObjetivo; set => strObjetivo = value; }
        public string StrProcesos { get => strProcesos; set => strProcesos = value; }
        public string StrHallasgos { get => strHallasgos; set => strHallasgos = value; }
        public DateTime DtmFecha { get => dtmFecha; set => dtmFecha = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public int IntOidUsuarioCreador { get => intOidUsuarioCreador; set => intOidUsuarioCreador = value; }
    }
}