using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Facturacion
{
    public class EConsultaAutorizaciones
    {

        int intIdHistorico, intIdUsuario;
        string strNombreUsuario, strEstadoAut, strNumeroAut;
        DateTime dtfechaConsulta;

        public int IntIdHistorico { get => intIdHistorico; set => intIdHistorico = value; }
        public int IntIdUsuario { get => intIdUsuario; set => intIdUsuario = value; }
        public string StrNombreUsuario { get => strNombreUsuario; set => strNombreUsuario = value; }
        public string StrEstadoAut { get => strEstadoAut; set => strEstadoAut = value; }
        public string StrNumeroAut { get => strNumeroAut; set => strNumeroAut = value; }
        public DateTime DtfechaConsulta { get => dtfechaConsulta; set => dtfechaConsulta = value; }
    }
}