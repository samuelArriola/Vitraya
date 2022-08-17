using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPAsistencia
    {
        private int         intOidCPMATRICULA, 
                            intOidCPAsistencia, 
                            intGNCodUsu;

        private string      strNOMUSUARIO, 
                            strUNIDAD, 
                            strCARGO, 
                            strEMAIL, 
                            strTELEFONO;

        private DateTime      dtmFECHAINICIO, 
                            dtmFECHAFINAL;

        private bool        isActivo, 
                            isFirmado;

        public int IntOidCPMATRICULA { get => intOidCPMATRICULA; set => intOidCPMATRICULA = value; }
        public int IntOidCPAsistencia { get => intOidCPAsistencia; set => intOidCPAsistencia = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public string StrNOMUSUARIO { get => strNOMUSUARIO; set => strNOMUSUARIO = value; }
        public string StrUNIDAD { get => strUNIDAD; set => strUNIDAD = value; }
        public string StrCARGO { get => strCARGO; set => strCARGO = value; }
        public string StrEMAIL { get => strEMAIL; set => strEMAIL = value; }
        public string StrTELEFONO { get => strTELEFONO; set => strTELEFONO = value; }
        public DateTime DtmFECHAINICIO { get => dtmFECHAINICIO; set => dtmFECHAINICIO = value; }
        public DateTime DtmFECHAFINAL { get => dtmFECHAFINAL; set => dtmFECHAFINAL = value; }
        public bool IsActivo { get => isActivo; set => isActivo = value; }
        public bool IsFirmado { get => isFirmado; set => isFirmado = value; }
    }
}