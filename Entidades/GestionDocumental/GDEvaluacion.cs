using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDEvaluacion
    {


        private int intOIdGDEvaluacion;
        private string strEstado;
        private string srtInsidencia;
        private int intOidGDSolicitud;
        private string strTipo;

        public int IntOIdGDEvaluacion { get => intOIdGDEvaluacion; set => intOIdGDEvaluacion = value; }
        public string StrEstado { get => strEstado; set => strEstado = value; }
        public string SrtInsidencia { get => srtInsidencia; set => srtInsidencia = value; }
        public int IntOidGDSolicitud { get => intOidGDSolicitud; set => intOidGDSolicitud = value; }
        public string StrTipo { get => strTipo; set => strTipo = value; }
    }
}