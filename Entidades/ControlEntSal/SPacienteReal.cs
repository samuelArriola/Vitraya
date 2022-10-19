using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ControlEntSal
{
    public class SPacienteReal
    {
        private int oid;
        private string  documento, ordensalida, gnidusu, nombre_completo, adningres1;
        private DateTime fecsalida;



        public int OID { get => oid; set => oid = value; }
        public string DOCUMENTO { get => documento; set => documento = value; }
        public string ADNINGRES1 { get => adningres1; set => adningres1 = value; }
        public string ORDENSALIDA { get => ordensalida; set => ordensalida = value; }
        public string GnIdUsu { get => gnidusu; set => gnidusu = value; }
        public string NOMBRE_COMPLETO { get => nombre_completo; set => nombre_completo = value; }
        public DateTime FECSALIDA { get => fecsalida; set => fecsalida = value; }
      
    }
}