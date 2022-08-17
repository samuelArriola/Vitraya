using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNEventos
    {
        private int         intOidGNEvento,
                            intOidGNModulo,
                            intOidCronograma;

        private string      strLugar,
                            strContenido;

        private DateTime    tmFechaInicio,
                            dtmFechaFinal;

        public int IntOidGNEvento { get => intOidGNEvento; set => intOidGNEvento = value; }
        public int IntOidGNModulo { get => intOidGNModulo; set => intOidGNModulo = value; }
        public string StrLugar { get => strLugar; set => strLugar = value; }
        public string StrContenido { get => strContenido; set => strContenido = value; }
        public DateTime DtmFechaInicio { get => tmFechaInicio; set => tmFechaInicio = value; }
        public DateTime DtmFechaFinal { get => dtmFechaFinal; set => dtmFechaFinal = value; }
        public int IntOidCronograma { get => intOidCronograma; set => intOidCronograma = value; }
    }
}