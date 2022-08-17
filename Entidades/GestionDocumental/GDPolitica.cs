using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDPolitica
    {
        private int         intOidGDPolitica,
                            intOidGDDocumento,
                            intOidGDProceso;

        private string strIntroduccion,
                            strObjetivos,
                            strObjetivosEsp,
                            strAlcance,
                            strMarcoLegal,
                            strDesarrollo,
                            strGlosario,
                            strAnexos,
                            strNombre;
        

        public int IntOidGDPolitica { get => intOidGDPolitica; set => intOidGDPolitica = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public string StrIntroduccion { get => strIntroduccion; set => strIntroduccion = value; }
        public string StrObjetivos { get => strObjetivos; set => strObjetivos = value; }
        public string StrObjetivosEsp { get => strObjetivosEsp; set => strObjetivosEsp = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public string StrMarcoLegal { get => strMarcoLegal; set => strMarcoLegal = value; }
        public string StrDesarrollo { get => strDesarrollo; set => strDesarrollo = value; }
        public string StrGlosario { get => strGlosario; set => strGlosario = value; }
        public string StrAnexos { get => strAnexos; set => strAnexos = value; }
        public int IntOidGDProceso { get => intOidGDProceso; set => intOidGDProceso = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
    }
}