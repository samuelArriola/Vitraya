using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.InventarioFarmacia
{
    public class ControlDespModel
    {
        private long consecutivo;
        private float   cantidad;
        private double dosis;
        private string oid_suministro, oid_lote, documento, nombre, producto, via_admin, almacen, frecuencia, cama, codigo, posologia, area_servicio, unidad_medida;
        private DateTime fec_registra;
                         
      
        public long CONSECUTIVO { get => consecutivo; set => consecutivo = value; }
        public string OID_SUMINISTRO { get => oid_suministro; set => oid_suministro = value; }
        public DateTime? FEC_DOCUMENTO { get ; set; }
        public string DOCUMENTO_PAC { get => documento; set => documento = value; } 
        public string NOMBRE_PAC { get => nombre; set => nombre = value; } 
        public string ALMACEN { get => almacen; set => almacen = value; } 
        public string CAMA { get => cama; set => cama = value; }
        public string POSOLOGIA { get => posologia; set => posologia = value; }
        public string AREA_SERVICIO { get => area_servicio; set => area_servicio = value; }

        //MED : Medicamento
        public string CODIGO_MED { get => codigo; set => codigo = value; } 
        public string DESCRIP_MED { get => producto; set => producto = value; } 
        public string UNIDAD_DE_MEDIDA { get => unidad_medida; set => unidad_medida = value; } 
        public double DOSIS_MED { get => dosis; set => dosis = value; }  
        public string FECUENCIA_MED { get => frecuencia; set => frecuencia = value; } 
        public string VIA_ADMIN_MED { get => via_admin; set => via_admin = value; } 
        public string OID_LOTE { get => oid_lote; set => oid_lote = value; }
        public float CANTIDAD { get => cantidad; set => cantidad = value; }
       
    }
}