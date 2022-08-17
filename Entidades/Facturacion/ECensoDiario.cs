using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Facturacion
{
    public class ECensoDiario
    {

        string tipoIdentificacion, numeroIdentificacion, nombresApellidos, tipoServicio, unidadFuncional, unidadFuncionalSubgrupo, cups, nombreCups, cama, numeroIngreso, tieneEgreso;
        DateTime fechaIngreso, fechaEgreso;
        int resultado, resultadoCierre;

        public string NumeroIdentificacion { get => numeroIdentificacion; set => numeroIdentificacion = value; }
        public string NombresApellidos { get => nombresApellidos; set => nombresApellidos = value; }
        public DateTime FechaIngreso { get => fechaIngreso; set => fechaIngreso = value; }
        public string TipoServicio { get => tipoServicio; set => tipoServicio = value; }
        public string UnidadFuncional { get => unidadFuncional; set => unidadFuncional = value; }
        public string Cups { get => cups; set => cups = value; }
        public string NombreCups { get => nombreCups; set => nombreCups = value; }
        public string Cama { get => cama; set => cama = value; }
        public string TipoIdentificacion { get => tipoIdentificacion; set => tipoIdentificacion = value; }
        public string UnidadFuncionalSubgrupo { get => unidadFuncionalSubgrupo; set => unidadFuncionalSubgrupo = value; }
        public int Resultado { get => resultado; set => resultado = value; }
        public string NumeroIngreso { get => numeroIngreso; set => numeroIngreso = value; }
        public string TieneEgreso { get => tieneEgreso; set => tieneEgreso = value; }
        public DateTime? FechaEgreso { get; set; }
        public int ResultadoCierre { get => resultadoCierre; set => resultadoCierre = value; }
    }
}