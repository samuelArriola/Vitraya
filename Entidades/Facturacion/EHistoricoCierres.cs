using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Facturacion
{
    public class EHistoricoCierres
    {

        string numIngreso, usuarioCierre, estadoCierre, motivoCierre;
        DateTime fechaCierre;

        public string NumIngreso { get => numIngreso; set => numIngreso = value; }
        public string UsuarioCierre { get => usuarioCierre; set => usuarioCierre = value; }
        public string EstadoCierre { get => estadoCierre; set => estadoCierre = value; }
        public string MotivoCierre { get => motivoCierre; set => motivoCierre = value; }
        public DateTime FechaCierre { get => fechaCierre; set => fechaCierre = value; }
    }
}