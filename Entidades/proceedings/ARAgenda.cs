using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.PlanAccion
{
    public class ARAgenda
    {
        private int     oidARreunionC,
                        oidARAgenda,
                        posicion;

        private string  nombre;

        public int OidARreunionC { get => oidARreunionC; set => oidARreunionC = value; }
        public int OidARAgenda { get => oidARAgenda; set => oidARAgenda = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Posicion { get => posicion; set => posicion = value; }
    }
}