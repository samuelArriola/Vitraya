using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.trainings
{
    public class CPEXAMEN
    {
        private int intOidCPEXAMEN, intNumApro, intOidInstancia, intContexto;

        private string strNombre;
                       

        private List<CPPREGUNTA> preguntas;

        public int IntOidCPEXAMEN { get => intOidCPEXAMEN; set => intOidCPEXAMEN = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public List<CPPREGUNTA> Preguntas { get => preguntas; set => preguntas = value; }
        public int IntNumApro { get => intNumApro; set => intNumApro = value; }
        public int IntOidInstancia { get => intOidInstancia; set => intOidInstancia = value; }
        public int IntContexto { get => intContexto; set => intContexto = value; }
    }
}