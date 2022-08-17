using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class Cargo
    {

        private string  strGnNomCgo,
                        strGnEsCgo,
                        strGnDcDep,
                        strNomDep;
                        

        private int     intGnIdCgo,
                        intGnDcCgo;

        public string StrGnNomCgo { get => strGnNomCgo; set => strGnNomCgo = value; }
        public string StrGnEsCgo { get => strGnEsCgo; set => strGnEsCgo = value; }
        public string StrGnDcDep { get => strGnDcDep; set => strGnDcDep = value; }
        public int IntGnIdCgo { get => intGnIdCgo; set => intGnIdCgo = value; }
        public int IntGnDcCgo { get => intGnDcCgo; set => intGnDcCgo = value; }
        public string StrNomDep { get => strNomDep; set => strNomDep = value; }
    }
}