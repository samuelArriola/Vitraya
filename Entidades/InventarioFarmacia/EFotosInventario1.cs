using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.InventarioFarmacia
{
    public class EFotosInventario1
    {

        int fT_Oid_almacen, fT_Oid_producto, fT_Oid_lote;
        string fT_Cod_almacen, fT_Nom_almacen, fT_Tp_producto, fT_Cod_producto, fT_Nom_producto, fT_Cod_lote;
        Decimal fT_Cant_sistema;
        DateTime fT_FecVen_lote, fT_Fecha_foto;
        Boolean fT_Estado_producto;

        public int FT_Oid_almacen { get => fT_Oid_almacen; set => fT_Oid_almacen = value; }
        public int FT_Oid_producto { get => fT_Oid_producto; set => fT_Oid_producto = value; }
        public int FT_Oid_lote { get => fT_Oid_lote; set => fT_Oid_lote = value; }
        public Boolean FT_Estado_producto { get => fT_Estado_producto; set => fT_Estado_producto = value; }
        public string FT_Cod_almacen { get => fT_Cod_almacen; set => fT_Cod_almacen = value; }
        public string FT_Nom_almacen { get => fT_Nom_almacen; set => fT_Nom_almacen = value; }
        public string FT_Tp_producto { get => fT_Tp_producto; set => fT_Tp_producto = value; }
        public string FT_Cod_producto { get => fT_Cod_producto; set => fT_Cod_producto = value; }
        public string FT_Nom_producto { get => fT_Nom_producto; set => fT_Nom_producto = value; }
        public string FT_Cod_lote { get => fT_Cod_lote; set => fT_Cod_lote = value; }
        public Decimal FT_Cant_sistema { get => fT_Cant_sistema; set => fT_Cant_sistema = value; }
        public DateTime FT_FecVen_lote { get => fT_FecVen_lote; set => fT_FecVen_lote = value; }
        public DateTime FT_Fecha_foto { get => fT_Fecha_foto; set => fT_Fecha_foto = value; }
    }
}