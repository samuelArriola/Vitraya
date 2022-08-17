using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class AReunionC
    {
        private string strCodReunion,
                        strNomReunion,
                        strTipo,
                        strSigla,
                        strNomUnidadFuncional;
                        

        private int     intOidAReunionC,
                        intEstadoReu,
                        intPeriodicidad,
                        intGnCdArea,
                        intGNCodUsu;

        public string StrCodReunion { get => strCodReunion; set => strCodReunion = value; }
        public string StrNomReunion { get => strNomReunion; set => strNomReunion = value; }
        public string StrTipo { get => strTipo; set => strTipo = value; }
        public string StrSigla { get => strSigla; set => strSigla = value; }
        public string StrNomUnidadFuncional { get => strNomUnidadFuncional; set => strNomUnidadFuncional = value; }
        public int IntOidAReunionC { get => intOidAReunionC; set => intOidAReunionC = value; }
        public int IntEstadoReu { get => intEstadoReu; set => intEstadoReu = value; }
        public int IntPeriodicidad { get => intPeriodicidad; set => intPeriodicidad = value; }
        public int IntGnCdArea { get => intGnCdArea; set => intGnCdArea = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
    }
}