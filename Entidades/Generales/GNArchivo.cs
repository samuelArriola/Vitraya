using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNArchivo
    {
        private int         intOidGNArchivo,
                            intOidGNListaArchivos;

        private string      strNombre, 
                            strContenido, 
                            strExt;

        private byte[]      abteArchivo;

        public int IntOidGNArchivo { get => intOidGNArchivo; set => intOidGNArchivo = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrContenido { get => strContenido; set => strContenido = value; }
        public string StrExt { get => strExt; set => strExt = value; }
        public byte[] AbteArchivo { get => abteArchivo; set => abteArchivo = value; }
        public int IntOidGNListaArchivos { get => intOidGNListaArchivos; set => intOidGNListaArchivos = value; }
    }
}