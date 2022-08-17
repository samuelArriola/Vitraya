using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Vacunacion
{
    public class RegistroDiarioVac
    {

		public int IntOidRegistroDiarioVac { get; set; }
		public int IntEdad{ get; set; }
		public int? intOidRegistrador { get; set; }
		public int? IntOidVCLoteJeringa { get; set; }
		public int? IntOidVCLoteBiologico { get; set; }
		public string StrLugarRegistro { get; set; }
		public string StrTipoDocumento { get; set; }
		public string StrDocumento { get; set; }
		public string StrSexo { get; set; }
		public string StrPrimerApellido { get; set; }
		public string StrSegundoApellido { get; set; }
		public string StrNombres { get; set; }
		public string StrRegimen { get; set; }
		public string StrEps { get; set; }
		public string StrDeptoResidencia { get; set; }
		public string StrMunicipioResidencia { get; set; }
		public string StrAreaResidencia { get; set; }
		public string StrBarrio { get; set; }
		public string StrDireccion { get; set; }
		public string StrTelefono { get; set; }
		public string StrGrupoEtnico { get; set; }
		public string StrDesplazamiento { get; set; }
		public string StrDiscapacidad { get; set; }
		public string StrEmail { get; set; }
		public string StrGestacion { get; set; }
		public string StrEtapaVacunacion { get; set; }
		public string StrTipoPoblacion { get; set; }
		public string StrDosis { get; set; }
		public string StrBiologico { get; set; }
		public string StrLote { get; set; }
		public string StrJeringa { get; set; }
		public string StrNombreVacunador { get; set; }
		public string StrNombreIps { get; set; }
		public string StrTipoDocumentoAC { get; set; }
		public string StrDocumentoAC { get; set; }
		public string StrParentescoAC { get; set; }
		public string StrNombresAC { get; set; }
		public string StrRegimenAC { get; set; }
		public string StrEpsAC { get; set; }
		public string StrGrupoEtnicoAC { get; set; }
		public string StrDesplazamientoAC { get; set; }
		public string StrDiscapacidadAC { get; set; }
		public string StrEmailAC { get; set; }
		public string StrTelefonoAC { get; set; }
		public string StrLoteJeringa { get; set; }
		public DateTime DtmFechaVacunacion { get; set; }
		public DateTime DtmFechaNacimiento { get; set; }
		public string StrFechaProbableParto { get; set; }
		public DateTime? DtmFechaRetgistro { get; set; }
		public bool BlnEstado { get; set; }
		public string MotivoEliminacion { get; set; }

		public string StrNombreMadre { get; set; }
		public string StrSemanasMenor { get; set; }
		public string StrPesoMenor { get; set; }
		public string StrTipoDocumentoMA { get; set; }
		public string StrLugarParto { get; set; }
		public string StrNombreLugarParto { get; set; }

		public string StrDeptoNacimiento { get; set; }
		public string StrMunicipioNacimiento { get; set; }

	}
}