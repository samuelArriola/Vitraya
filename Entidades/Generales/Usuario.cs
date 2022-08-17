using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Generales
{
    public class Usuario
    {
        private int         GnIdUsu,
                            GNCodUsu,
                            GnDcDep,
                            GnCdAra,
                            GnDcCgo,
                            GnEpsUsu,
                            codigoR;

        private string      GNNomUsu,
                            GNConUsu,
                            GnEtUsu,
                            GNCrusu,
                            GnTlUsu,
                            GnUnfun,
                            GnCargo;

        private byte[]      GNFtUsu,
                            GNFmUsu,
                            GnFtHull;

        private DateTime GNFhUsu;
        private DateTime fechaCambioPass;

        public int GnIdUsu1 { get => GnIdUsu; set => GnIdUsu = value; }
        public int GNCodUsu1 { get => GNCodUsu; set => GNCodUsu = value; }
        public int GnDcDep1 { get => GnDcDep; set => GnDcDep = value; }
        public int GnCdAra1 { get => GnCdAra; set => GnCdAra = value; }
        public int GnDcCgo1 { get => GnDcCgo; set => GnDcCgo = value; }
        public int GnEpsUsu1 { get => GnEpsUsu; set => GnEpsUsu = value; }
        public int CodigoR { get => codigoR; set => codigoR = value; }
        public string GNNomUsu1 { get => GNNomUsu; set => GNNomUsu = value; }
        public string GNConUsu1 { get => GNConUsu; set => GNConUsu = value; }
        public string GnEtUsu1 { get => GnEtUsu; set => GnEtUsu = value; }
        public string GNCrusu1 { get => GNCrusu; set => GNCrusu = value; }
        public string GnTlUsu1 { get => GnTlUsu; set => GnTlUsu = value; }
        public string GnUnfun1 { get => GnUnfun; set => GnUnfun = value; }
        public string GnCargo1 { get => GnCargo; set => GnCargo = value; }
        public byte[] GNFtUsu1 { get => GNFtUsu; set => GNFtUsu = value; }
        public byte[] GNFmUsu1 { get => GNFmUsu; set => GNFmUsu = value; }
        public byte[] GnFtHull1 { get => GnFtHull; set => GnFtHull = value; }
        public DateTime GNFhUsu1 { get => GNFhUsu; set => GNFhUsu = value; }
        public DateTime FechaCambioPass { get => fechaCambioPass; set => fechaCambioPass = value; }
    }
}