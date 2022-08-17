using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Facturacion
{
    public class EBitacoraAutorizaciones
    {

        int OidRegAutorizacion, OidTecnologia, cantidadTecnologia, OidGNArchivo, OidGNListaArchivos, resultadoAutRepetida;
        string NumSolicitud, Identificacion, TipoIdentificacion, Nombres , NumCama, OrigenAtencion, TipoServicio, PrioridadAtencion, UbicacionPaciente, ContratoPrestacion, Servicio, DiagPrincipal, DiagRel1, DiagRel2, NombreIPS, DireccionIPS, JustificacionClinica, ProfesionalSolicita, CargoProfesional, Estado, NumAutorizacion, clasificacionTecnologia, nombreTecnologia, ArchivoNombre, ArchivoContenido, ArchivoExt, Archivo, MotivoAnulacion, NumIngreso, DescripcionObservacion;
        DateTime FechaSolicitud, FechaIngreso, FechaAprobacion, FechaAnulacion, fechaObservacion;

        public int OidRegAutorizacion1 { get => OidRegAutorizacion; set => OidRegAutorizacion = value; }
        public string Identificacion1 { get => Identificacion; set => Identificacion = value; }
        public string NumSolicitud1 { get => NumSolicitud; set => NumSolicitud = value; }
        public string NumCama1 { get => NumCama; set => NumCama = value; }
        public string TipoIdentificacion1 { get => TipoIdentificacion; set => TipoIdentificacion = value; }
        public string Nombres1 { get => Nombres; set => Nombres = value; }
        public string OrigenAtencion1 { get => OrigenAtencion; set => OrigenAtencion = value; }
        public string TipoServicio1 { get => TipoServicio; set => TipoServicio = value; }
        public string PrioridadAtencion1 { get => PrioridadAtencion; set => PrioridadAtencion = value; }
        public string UbicacionPaciente1 { get => UbicacionPaciente; set => UbicacionPaciente = value; }
        public string ContratoPrestacion1 { get => ContratoPrestacion; set => ContratoPrestacion = value; }
        public string Servicio1 { get => Servicio; set => Servicio = value; }
        public string DiagPrincipal1 { get => DiagPrincipal; set => DiagPrincipal = value; }
        public string DiagRel11 { get => DiagRel1; set => DiagRel1 = value; }
        public string DiagRel21 { get => DiagRel2; set => DiagRel2 = value; }
        public string NombreIPS1 { get => NombreIPS; set => NombreIPS = value; }
        public string DireccionIPS1 { get => DireccionIPS; set => DireccionIPS = value; }
        public string JustificacionClinica1 { get => JustificacionClinica; set => JustificacionClinica = value; }
        public string ProfesionalSolicita1 { get => ProfesionalSolicita; set => ProfesionalSolicita = value; }
        public string CargoProfesional1 { get => CargoProfesional; set => CargoProfesional = value; }
        public DateTime FechaSolicitud1 { get => FechaSolicitud; set => FechaSolicitud = value; }
        public DateTime FechaIngreso1 { get => FechaIngreso; set => FechaIngreso = value; }
        public DateTime? FechaAprobacion1 { get; set; }
        public string Estado1 { get => Estado; set => Estado = value; }
        public string NumAutorizacion1 { get => NumAutorizacion; set => NumAutorizacion = value; }
        public int OidTecnologia1 { get => OidTecnologia; set => OidTecnologia = value; }
        public int CantidadTecnologia { get => cantidadTecnologia; set => cantidadTecnologia = value; }
        public string ClasificacionTecnologia { get => clasificacionTecnologia; set => clasificacionTecnologia = value; }
        public string NombreTecnologia { get => nombreTecnologia; set => nombreTecnologia = value; }
        public int OidGNArchivo1 { get => OidGNArchivo; set => OidGNArchivo = value; }
        public int OidGNListaArchivos1 { get => OidGNListaArchivos; set => OidGNListaArchivos = value; }
        public string ArchivoNombre1 { get => ArchivoNombre; set => ArchivoNombre = value; }
        public string ArchivoContenido1 { get => ArchivoContenido; set => ArchivoContenido = value; }
        public string ArchivoExt1 { get => ArchivoExt; set => ArchivoExt = value; }
        public string Archivo1 { get => Archivo; set => Archivo = value; }
        public DateTime? FechaAnulacion1 { get; set; }
        public string MotivoAnulacion1 { get => MotivoAnulacion; set => MotivoAnulacion = value; }
        public string NumIngreso1 { get => NumIngreso; set => NumIngreso = value; }
        public DateTime FechaObservacion { get => fechaObservacion; set => fechaObservacion = value; }
        public string DescripcionObservacion1 { get => DescripcionObservacion; set => DescripcionObservacion = value; }
        public int ResultadoAutRepetida { get => resultadoAutRepetida; set => resultadoAutRepetida = value; }
    }
}