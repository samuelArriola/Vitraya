using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.ControlEntSal
{
    public class Censo
    {
        private int ingreso;
        private string cod_grupo, nom_grupo, cod_subgrupo, nom_subgrupo, documento, nom_pac, cod_cama, nom_cama;
        private string visita;
        public int Ingreso { get => ingreso; set => ingreso = value; }

        public string Cod_Grupo { get => cod_grupo; set => cod_grupo = value; }
        public string Nom_Grupo { get => nom_grupo; set => nom_grupo = value; }
        public string Cod_Subgrupo { get => cod_subgrupo; set => cod_subgrupo = value; }
        public string Nom_Subgrupo { get => nom_subgrupo; set => nom_subgrupo = value; }
        public string Documento { get => documento; set => documento = value; }
        public string NOM_PAC { get => nom_pac; set => nom_pac = value; }
        public string Cod_Cama { get => cod_cama; set => cod_cama = value; }
        public string Nom_Cama { get => nom_cama; set => nom_cama = value; }
        public string VISITA { get => visita; set => visita = value; }
    }
}