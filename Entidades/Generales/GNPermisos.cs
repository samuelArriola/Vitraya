using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class GNPermisos
    {
        private int     intOidGNPermiso,
                        intOidGNOpcion,
                        itnOidRol;

        private bool    blnCrear,
                        blnEliminar,
                        blnConfirmar,
                        blnModificar;
        private string  strNombre;

        public int IntOidGNPermiso { get => intOidGNPermiso; set => intOidGNPermiso = value; }
        public int IntOidGNOpcion { get => intOidGNOpcion; set => intOidGNOpcion = value; }
        public int ItnOidRol { get => itnOidRol; set => itnOidRol = value; }
        public bool BlnCrear { get => blnCrear; set => blnCrear = value; }
        public bool BlnEliminar { get => blnEliminar; set => blnEliminar = value; }
        public bool BlnConfirmar { get => blnConfirmar; set => blnConfirmar = value; }
        public bool BlnModificar { get => blnModificar; set => blnModificar = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
    }
}