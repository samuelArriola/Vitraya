using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPCAPACITACION
    {
        private string      strCODIGO, 
                            strLUGAR, 
                            strUNIDADFUNCIONAL,
                            strTEMA,
                            strMODALIDAD,
                            strRESPONSABLE,
                            strLINK,
                            strESTADO,
                            strInfoMatricula;

        private int         intOidCPEJETEMA,
                            intGNCodUsu,
                            intOidListArch,
                            intOidCPCAPACITACION,
                            intOidGNArchivo,
                            intOidGDDocumento,
                            intTempFirma;

        private DateTime dtmFECHA,
                            dtmHORAINICIAL,
                            dtmHORAFINAL,
                            dtmFechaFirma;

        

        public string StrCODIGO { get => strCODIGO; set => strCODIGO = value; }
        public string StrLUGAR { get => strLUGAR; set => strLUGAR = value; }
        public string StrUNIDADFUNCIONAL { get => strUNIDADFUNCIONAL; set => strUNIDADFUNCIONAL = value; }
        public string StrTEMA { get => strTEMA; set => strTEMA = value; }
        public string StrMODALIDAD { get => strMODALIDAD; set => strMODALIDAD = value; }
        public string StrRESPONSABLE { get => strRESPONSABLE; set => strRESPONSABLE = value; }
        public string StrLINK { get => strLINK; set => strLINK = value; }
        public int IntOidCPEJETEMA { get => intOidCPEJETEMA; set => intOidCPEJETEMA = value; }
        public int IntGNCodUsu { get => intGNCodUsu; set => intGNCodUsu = value; }
        public int IntOidListArch { get => intOidListArch; set => intOidListArch = value; }
        public int IntOidCPCAPACITACION { get => intOidCPCAPACITACION; set => intOidCPCAPACITACION = value; }
        public DateTime DtmFECHA { get => dtmFECHA; set => dtmFECHA = value; }
        public DateTime DtmHORAINICIAL { get => dtmHORAINICIAL; set => dtmHORAINICIAL = value; }
        public DateTime DtmHORAFINAL { get => dtmHORAFINAL; set => dtmHORAFINAL = value; }
        public string StrESTADO { get => strESTADO; set => strESTADO = value; }
        public int IntOidGNArchivo { get => intOidGNArchivo; set => intOidGNArchivo = value; }
        public string StrInfoMatricula { get => strInfoMatricula; set => strInfoMatricula = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public int IntTempFirma { get => intTempFirma; set => intTempFirma = value;}
        public DateTime DtmFechaFirma { get => dtmFechaFirma; set => dtmFechaFirma = value; }
    }
}