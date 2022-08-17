using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Servicios
{
    public class EDesprendibles
    {

        int oid_Config;
        DateTime dtFechaNomina;
        string strEmpleado, strIdentificacion, strCodigoConcepto, strNombreConcepto, strCargo, strGrado, nombre_Config, estadoValorConfig;
        float  floatCantidad, floatDevengado, floatDeduccion, floatSueldo;

        public DateTime DtFechaNomina { get => dtFechaNomina; set => dtFechaNomina = value; }
        public string StrEmpleado { get => strEmpleado; set => strEmpleado = value; }
        public string StrIdentificacion { get => strIdentificacion; set => strIdentificacion = value; }
        public string StrCodigoConcepto { get => strCodigoConcepto; set => strCodigoConcepto = value; }
        public string StrNombreConcepto { get => strNombreConcepto; set => strNombreConcepto = value; }
        public string StrCargo { get => strCargo; set => strCargo = value; }
        public string StrGrado { get => strGrado; set => strGrado = value; }
        public float FloatCantidad { get => floatCantidad; set => floatCantidad = value; }
        public float FloatDevengado { get => floatDevengado; set => floatDevengado = value; }
        public float FloatDeduccion { get => floatDeduccion; set => floatDeduccion = value; }
        public float FloatSueldo { get => floatSueldo; set => floatSueldo = value; }
        public int Oid_Config { get => oid_Config; set => oid_Config = value; }
        public string Nombre_Config { get => nombre_Config; set => nombre_Config = value; }
        public string EstadoValorConfig { get => estadoValorConfig; set => estadoValorConfig = value; }
    }
}