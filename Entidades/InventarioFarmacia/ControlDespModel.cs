using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.InventarioFarmacia
{
    public class ControlDespModel
    {
        private int ingreso, cantidad;
        private byte estado;
        private string oid_suministro, oid_lote, cod_lote, suministro, documento, nombre, producto, nom_producto, unidad_funsional, medico_solicita, almacen, entidad;
        private DateTime fec_ingreso, fec_solicita, fec_registra, fec_confirma;
                         
        public int INGRESO { get => ingreso; set => ingreso = value; }
        public byte ESTADO { get => estado; set => estado = value; }
        public int CANTIDAD { get => cantidad; set => cantidad = value; }
        public string OID_SUMINISTRO { get => oid_suministro; set => oid_suministro = value; }
        public string OID_LOTE { get => oid_lote; set => oid_lote = value; }
        public string SUMINISTRO { get => suministro; set => suministro = value; } 
        public string DOCUMENTO { get => documento; set => documento = value; } 
        public string NOMBRE { get => nombre; set => nombre = value; } 
        public string PRODUCTO { get => producto; set => producto = value; } 
        public string NOM_PRODUCTO { get => nom_producto; set => nom_producto = value; } 
        public string UNIDAD_FUNCIONAL { get => unidad_funsional; set => unidad_funsional = value; } 
        public string MEDICO_SOLICITA { get => medico_solicita; set => medico_solicita = value; } 
        public string ALMACEN { get => almacen; set => almacen = value; } 
        public string ENTIDAD { get => entidad; set => entidad = value; } 
        public DateTime? FEC_INGRESO { get ; set; }
        public DateTime? FEC_SOLICITA { get ; set; }
        public DateTime? FEC_REGISTRA { get ; set; }
        public DateTime? FEC_CONFIRMA { get ; set; }

    }
}