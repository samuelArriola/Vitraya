using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class Direccion
    {
        private int         intGnIdArea,
                            intGnCdAra;

        private string      strGnNomAra,
                            strGnEsAre,
                            strGnSiglaDr;

        public int IntGnIdArea { get => intGnIdArea; set => intGnIdArea = value; }
        public int IntGnCdAra { get => intGnCdAra; set => intGnCdAra = value; }
        public string StrGnNomAra { get => strGnNomAra; set => strGnNomAra = value; }
        public string StrGnEsAre { get => strGnEsAre; set => strGnEsAre = value; }
        public string StrGnSiglaDr { get => strGnSiglaDr; set => strGnSiglaDr = value; }
    }
}