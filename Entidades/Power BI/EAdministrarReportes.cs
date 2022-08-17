using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Power_BI
{
    public class EAdministrarReportes
    {

        int codigoPermisoUsu, codigoPermisoCargo, codigoPermisoUniFun, usuarioLogueado;
        String nombrePermisoUsu, nombrePermisoCargo, nombrePermisoUniFun;
        String correoPermisoUsu, correonombrePermisoCargo, correonombrePermisoUniFun;

        public int CodigoPermisoUsu { get => codigoPermisoUsu; set => codigoPermisoUsu = value; }
        public int CodigoPermisoCargo { get => codigoPermisoCargo; set => codigoPermisoCargo = value; }
        public int CodigoPermisoUniFun { get => codigoPermisoUniFun; set => codigoPermisoUniFun = value; }
        public string NombrePermisoUsu { get => nombrePermisoUsu; set => nombrePermisoUsu = value; }
        public string NombrePermisoCargo { get => nombrePermisoCargo; set => nombrePermisoCargo = value; }
        public string NombrePermisoUniFun { get => nombrePermisoUniFun; set => nombrePermisoUniFun = value; }
        public string CorreoPermisoUsu { get => correoPermisoUsu; set => correoPermisoUsu = value; }
        public string CorreonombrePermisoCargo { get => correonombrePermisoCargo; set => correonombrePermisoCargo = value; }
        public string CorreonombrePermisoUniFun { get => correonombrePermisoUniFun; set => correonombrePermisoUniFun = value; }
        public int UsuarioLogueado { get => usuarioLogueado; set => usuarioLogueado = value; }
    }
}