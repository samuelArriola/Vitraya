using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDSolicitud
    {
        private double                  dblCodUsu;

        private int intOidGNProceso,
                                        intOidGDSolicitud,
                                        intGnDcDep,
                                        intOidGDDocE;

        private string                  strTipoSol,
                                        strNomDoc,
                                        strNomUsu,
                                        strCarUsu,
                                        strJusSol,
                                        strDesSol,
                                        strTipoDoc,
                                        strEstado,
                                        strIncidencia,
                                        strUnidadFuncional;


        private DateTime                dtmFechaSol;

        public double DblCodUsu { get => dblCodUsu; set => dblCodUsu = value; }
        public int IntOidGNProceso { get => intOidGNProceso; set => intOidGNProceso = value; }
        public int IntOidGDSolicitud { get => intOidGDSolicitud; set => intOidGDSolicitud = value; }
        public string StrTipoSol { get => strTipoSol; set => strTipoSol = value; }
        public string StrNomDoc { get => strNomDoc; set => strNomDoc = value; }
        public string StrNomUsu { get => strNomUsu; set => strNomUsu = value; }
        public string StrCarUsu { get => strCarUsu; set => strCarUsu = value; }
        public string StrJusSol { get => strJusSol; set => strJusSol = value; }
        public string StrDesSol { get => strDesSol; set => strDesSol = value; }
        public string StrTipoDoc { get => strTipoDoc; set => strTipoDoc = value; }
        public DateTime DtmFechaSol { get => dtmFechaSol; set => dtmFechaSol = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string StrIncidencia { get => strIncidencia; set => strIncidencia = value; }
        public string StrUnidadFuncional { get => strUnidadFuncional; set => strUnidadFuncional = value; }
        public int IntGnDcDep { get => intGnDcDep; set => intGnDcDep = value; }
        public int IntOidGDDocE { get => intOidGDDocE; set => intOidGDDocE = value; }
    }
}