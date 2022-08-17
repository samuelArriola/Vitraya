using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDDominio
    {
        private int intOidGDDominio;
        private string strNombre;

        public int IntOidGDDominio { get => intOidGDDominio; set => intOidGDDominio = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
    }
}