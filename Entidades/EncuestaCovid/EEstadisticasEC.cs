using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.EncuestaCovid
{
    public class EEstadisticasEC
    {

        private int intMes, intCantMes, intAñoMes, intDia, intCantDia, intMesDia, intAnioMesDia, intCantEmpleado;
        private string strCedula, strNombres;

        public int IntMes { get => intMes; set => intMes = value; }
        public int IntCantMes { get => intCantMes; set => intCantMes = value; }
        public int IntDia { get => intDia; set => intDia = value; }
        public int IntCantDia { get => intCantDia; set => intCantDia = value; }
        public int IntAñoMes { get => intAñoMes; set => intAñoMes = value; }
        public int IntMesDia { get => intMesDia; set => intMesDia = value; }
        public int IntAnioMesDia { get => intAnioMesDia; set => intAnioMesDia = value; }
        public int IntCantEmpleado { get => intCantEmpleado; set => intCantEmpleado = value; }
        public string StrCedula { get => strCedula; set => strCedula = value; }
        public string StrNombres { get => strNombres; set => strNombres = value; }
    }
}