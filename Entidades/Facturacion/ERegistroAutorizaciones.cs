using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Facturacion
{
    public class ERegistroAutorizaciones
    {

        string nombreProfesional, numServicio, numId, numContratoPrestacion, nombres, origenAtencion, tipoServiciosSolicitados, prioridadAtencion, ubicacionPaciente, contratoPrestacion, servicio, numeroCama, numDiagnosticoPrincipal, diagnosticoPrincipal, NombreCUPS, DescripCUPS, ClasificacionCUPS, numeroIngreso, tipoId;
        DateTime fechaHoraIngreso;

        public string NumId { get => numId; set => numId = value; }
        public string Nombres { get => nombres; set => nombres = value; }
        public string OrigenAtencion { get => origenAtencion; set => origenAtencion = value; }
        public string TipoServiciosSolicitados { get => tipoServiciosSolicitados; set => tipoServiciosSolicitados = value; }
        public string PrioridadAtencion { get => prioridadAtencion; set => prioridadAtencion = value; }
        public string UbicacionPaciente { get => ubicacionPaciente; set => ubicacionPaciente = value; }
        public string ContratoPrestacion { get => contratoPrestacion; set => contratoPrestacion = value; }
        public string Servicio { get => servicio; set => servicio = value; }
        public string NumeroCama { get => numeroCama; set => numeroCama = value; }
        public string DiagnosticoPrincipal { get => diagnosticoPrincipal; set => diagnosticoPrincipal = value; }
        public DateTime FechaHoraIngreso { get => fechaHoraIngreso; set => fechaHoraIngreso = value; }
        public string NumContratoPrestacion { get => numContratoPrestacion; set => numContratoPrestacion = value; }
        public string NumServicio { get => numServicio; set => numServicio = value; }
        public string NumDiagnosticoPrincipal { get => numDiagnosticoPrincipal; set => numDiagnosticoPrincipal = value; }
        public string NombreCUPS1 { get => NombreCUPS; set => NombreCUPS = value; }
        public string DescripCUPS1 { get => DescripCUPS; set => DescripCUPS = value; }
        public string ClasificacionCUPS1 { get => ClasificacionCUPS; set => ClasificacionCUPS = value; }
        public string NumeroIngreso { get => numeroIngreso; set => numeroIngreso = value; }
        public string TipoId { get => tipoId; set => tipoId = value; }
        public string NombreProfesional { get => nombreProfesional; set => nombreProfesional = value; }
    }
}