using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPMatricula
    {
        public class DatosMatricula
        {
            public int metodoMatr { get; set; }
            public int valorMetodoMetr { get; set; }
            public string nombreMetodoMatr { get; set; }
        }

        private int intOidCPMatricula,
                                intOidCPCAPACITACION,
                                intGNCodUsu,
                                intMetodoMatri,
                                intValorMetodoMatr,
                                intOidCPAgenda;



        private string          strNOMUSUARIO,
                                strUNIDAD,
                                strCARGO,
                                strTELEFONO,
                                strEMAIL,
                                strNombreMetodoMatr;


        private DateTime        dtmFECHA;

        private bool            blnESTADO,
                                blnFirmado,
                                blnAsistido;


        public int IntOidCPMatricula { get => intOidCPMatricula; set => intOidCPMatricula = value; }
        public int IntOidCPCAPACITACION { get => intOidCPCAPACITACION; set => intOidCPCAPACITACION = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public string StrNOMUSUARIO { get => strNOMUSUARIO; set => strNOMUSUARIO = value; }
        public string StrUNIDAD { get => strUNIDAD; set => strUNIDAD = value; }
        public string StrCARGO { get => strCARGO; set => strCARGO = value; }
        public string StrTELEFONO { get => strTELEFONO; set => strTELEFONO = value; }
        public string StrEMAIL { get => strEMAIL; set => strEMAIL = value; }
        public DateTime DtmFECHA { get => dtmFECHA; set => dtmFECHA = value; }
        public bool BlnESTADO { get => blnESTADO; set => blnESTADO = value; }
        public bool BlnFirmado { get => blnFirmado; set => blnFirmado = value; }
        public bool BlnAsistido { get => blnAsistido; set => blnAsistido = value; }
        public int IntMetodoMatri { get => intMetodoMatri; set => intMetodoMatri = value; }
        public int IntValorMetodoMatr { get => intValorMetodoMatr; set => intValorMetodoMatr = value; }
        public string StrNombreMetodoMatr { get => strNombreMetodoMatr; set => strNombreMetodoMatr = value; }
        public int IntOidCPAgenda { get => intOidCPAgenda; set => intOidCPAgenda = value; }



        /*MetodoMatri int, ValorMetodoMatr int, NombreMetodoMatr*/
    }
}