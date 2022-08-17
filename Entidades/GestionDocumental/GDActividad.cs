using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDActividad
    {
        private int     intOidActividad,
                        intOidGDDocumento;

        private string  strResponsable,
                        strDescripcion,
                        strNomActividad;

        public int IntOidActividad { get => intOidActividad; set => intOidActividad = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public string StrResponsable { get => strResponsable; set => strResponsable = value; }
        public string StrDescripcion { get => strDescripcion; set => strDescripcion = value; }
        public string StrNomActividad { get => strNomActividad; set => strNomActividad = value; }
    }
}