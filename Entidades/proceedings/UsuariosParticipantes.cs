using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class UsuariosParticipantes
    {
        private int     OidAReunionC,
                        OidAReunionD,
                        GNCodUsu,
                        TipoUsuario,
                        EstUsuario;
        
        private string  TpNomUsu,
                        TpNomEst,
                        nombreUsuario;

        public int OidAReunionC1 { get => OidAReunionC; set => OidAReunionC = value; }
        public int GNCodUsu1 { get => GNCodUsu; set => GNCodUsu = value; }
        public int TipoUsuario1 { get => TipoUsuario; set => TipoUsuario = value; }
        public int EstUsuario1 { get => EstUsuario; set => EstUsuario = value; }
        public string TpNomUsu1 { get => TpNomUsu; set => TpNomUsu = value; }
        public string TpNomEst1 { get => TpNomEst; set => TpNomEst = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public int OidAReunionD1 { get => OidAReunionD; set => OidAReunionD = value; }
      
    }
}