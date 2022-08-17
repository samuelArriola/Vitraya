using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDListadoMaestro
    {
        private string observaciones;
        private string proceso;
        private string codigo;
        private string estandar;
        private string nombre;
        private string tipo;
        private string direccion;
        private string version;
        private string clasificacion;
        private string estado;
        private string cambio;
        private string fechaElaboracion;
        private string elaborador;
        private string fechaRevision;
        private string revisor;
        private string fechaAprobacion;
        private string aprobador;
        private string fechaEmision;
        private string enlace;
        private string fechaActualizacion;
        private string responsableActualizacion;

        public string Proceso { get => proceso; set => proceso = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Estandar { get => estandar; set => estandar = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Direccion { get => direccion; set => direccion = value; }
        public string Version { get => version; set => version = value; }
        public string Clasificacion { get => clasificacion; set => clasificacion = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Cambio { get => cambio; set => cambio = value; }
        public string FechaElaboracion { get => fechaElaboracion; set => fechaElaboracion = value; }
        public string Elaborador { get => elaborador; set => elaborador = value; }
        public string FechaRevision { get => fechaRevision; set => fechaRevision = value; }
        public string Revisor { get => revisor; set => revisor = value; }
        public string FechaAprobacion { get => fechaAprobacion; set => fechaAprobacion = value; }
        public string Aprobador { get => aprobador; set => aprobador = value; }
        public string FechaEmision { get => fechaEmision; set => fechaEmision = value; }
        public string Enlace { get => enlace; set => enlace = value; }
        public string FechaActualizacion { get => fechaActualizacion; set => fechaActualizacion = value; }
        public string ResponsableActualizacion { get => responsableActualizacion; set => responsableActualizacion = value; }
        public string Observaciones { get => observaciones; set => observaciones = value; }
    }
}