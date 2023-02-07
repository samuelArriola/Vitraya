using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.InventarioFarmacia
{
    public class INNSumEntregado
    {
        long consecutivo, usuarioFirma, oid, usuarioLog;
        string oidSuministro,obsPro , doc_pac;
        char cpac, ccant, cviaadmin, cdosis;
        DateTime fecfirma;
        public long OID { get => oid; set => oid = value; }
        public long CONSECUTIVO { get => consecutivo; set => consecutivo = value; }
        public long USUARIOLOG { get => usuarioLog; set => usuarioLog = value; }
        public long USUARIOFIRMA { get => usuarioFirma; set => usuarioFirma = value; }

        public string DOCUMENTO_PAC { get => doc_pac; set => doc_pac = value; }
        public string OIDSUMINISTRO { get => oidSuministro; set => oidSuministro = value; }
        public string OBSPRO { get => obsPro; set => obsPro = value; }

        public char CPAC { get => cpac; set => cpac = value; }
        public char CCANT { get => ccant; set => ccant = value; }
        public char CVIAADMIN { get => cviaadmin; set => cviaadmin = value; }
        public char CDOSIS { get => cdosis; set => cdosis = value; }

        public DateTime FECFIRMA { get => fecfirma; set => fecfirma = value; }




    }
}