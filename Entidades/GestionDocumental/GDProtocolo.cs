using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.GestionDocumental
{
    public class GDProtocolo
    {
        private int         intOidGDProtocolo,
                            intOidGDDocumento,
                            intOidGDProceso;

        private string      strNombre,
                            strAlcance,
                            strObjetivo,
                            strRecursos,
                            strDefiniciones,
                            strRecomendaciones,
                            strRefNorm,
                            strResponsable,
                            strAnexos,
                            strActividad,
                            strRecHumanos,
                            strRecEquiposBiomedicos,
                            strIndicadores,
                            strRecInformaticos,
                            strRecMedicamentos;

       
        public int IntOidGDProtocolo { get => intOidGDProtocolo; set => intOidGDProtocolo = value; }
        public int IntOidGDDocumento { get => intOidGDDocumento; set => intOidGDDocumento = value; }
        public string StrNombre { get => strNombre; set => strNombre = value; }
        public string StrAlcance { get => strAlcance; set => strAlcance = value; }
        public string StrObjetivo { get => strObjetivo; set => strObjetivo = value; }
        public string StrRecursos { get => strRecursos; set => strRecursos = value; }
        public string StrDefiniciones { get => strDefiniciones; set => strDefiniciones = value; }
        public string StrRecomendaciones { get => strRecomendaciones; set => strRecomendaciones = value; }
        public string StrRefNorm { get => strRefNorm; set => strRefNorm = value; }
        public string StrResponsable { get => strResponsable; set => strResponsable = value; }
        public string StrAnexos { get => strAnexos; set => strAnexos = value; }
        public int IntOidGDProceso { get => intOidGDProceso; set => intOidGDProceso = value; }
        public string StrRecHumanos { get => strRecHumanos; set => strRecHumanos = value; }
        public string StrRecEquiposBiomedicos { get => strRecEquiposBiomedicos; set => strRecEquiposBiomedicos = value; }
        public string StrRecInformaticos { get => strRecInformaticos; set => strRecInformaticos = value; }
        public string StrRecMedicamentos { get => strRecMedicamentos; set => strRecMedicamentos = value; }
        public string StrActividad { get => strActividad; set => strActividad = value; }
        public string StrIndicadores { get => strIndicadores; set => strIndicadores = value; }
    }
}