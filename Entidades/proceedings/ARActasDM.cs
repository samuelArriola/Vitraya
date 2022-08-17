using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class ARActasDM
    {
        private int         intGNCodUsu,
                            intEstUsuario, 
                            intOidARActasDM,
                            intOidARActasC;

        private string      strTipoUsuario,
                            strNombre;

        private bool        blnFirmado;

        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public int IntEstUsuario { get => intEstUsuario; set => intEstUsuario = value; }
        public int IntOidARActasDM { get => intOidARActasDM; set => intOidARActasDM = value; }
        public int IntOidARActasC { get => intOidARActasC; set => intOidARActasC = value; }
        public string StrTipoUsuario { get => strTipoUsuario; set => strTipoUsuario = value; }
        public bool BlnFirmado { get => blnFirmado; set => blnFirmado = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
    }
}