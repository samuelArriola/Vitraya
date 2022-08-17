using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class ARActasTemas
    {
        private string      strDesarrollo,
                            strAdjuntar,
                            strNomTema;
        
        int                 intOidARActasTemas,
                            intOidARActasC,
                            intOidGNListaArchivos,
                            intPosicion;

        public string StrDesarrollo { get => strDesarrollo; set => strDesarrollo = value; }
        public string StrAdjuntar { get => strAdjuntar; set => strAdjuntar = value; }
        public string StrNomTema { get => strNomTema; set => strNomTema = value; }
        public int IntOidARActasTemas { get => intOidARActasTemas; set => intOidARActasTemas = value; }
        public int IntOidARActasC { get => intOidARActasC; set => intOidARActasC = value; }
        public int IntOidGNListaArchivos { get => intOidGNListaArchivos; set => intOidGNListaArchivos = value; }
        public int IntPosicion { get => intPosicion; set => intPosicion = value; }
    }
}