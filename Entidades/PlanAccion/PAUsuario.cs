using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Entidades.PlanAccion
{
    public class PAUsuario
    {
        public static int RESPONSABLE = 1, SEGUIMIENTO = 2, ASIGNADOR = 3;

        private int intOidPAUsuario, intOidPAPlanAccion, intOidGNUsuario;

        private string strRol, strNombre, strCargo;

        public int IntOidPAUsuario { get => intOidPAUsuario; set => intOidPAUsuario = value; }
        public int IntOidPAPlanAccion { get => intOidPAPlanAccion; set => intOidPAPlanAccion = value; }
        public int IntOidGNUsuario { get => intOidGNUsuario; set => intOidGNUsuario = value; }
        public string StrRol { get => strRol; set => strRol = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrCargo { get => strCargo; set => strCargo = value; }
    }
}