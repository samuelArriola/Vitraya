using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Entidades.Procesos
{
    public class PCSIPOC
    {
        private int         intOidSipoc,
                            intOIdProceso;

        private string      strProveedores,
                            strEntrada,
                            strTipoAct,
                            strActividad,
                            strClientes,
                            strSalidas,
                            strResponsables;

        public int IntOidSipoc { get => intOidSipoc; set => intOidSipoc = value; }
        public string StrProveedores { get => strProveedores; set => strProveedores = value; }
        public string StrEntrada { get => strEntrada; set => strEntrada = value; }
        public string StrTipoAct { get => strTipoAct; set => strTipoAct = value; }
        public string StrActividad { get => strActividad; set => strActividad = value; }
        public string StrClientes { get => strClientes; set => strClientes = value; }
        public string StrSalidas { get => strSalidas; set => strSalidas = value; }
        public int IntOIdProceso { get => intOIdProceso; set => intOIdProceso = value; }
        public string StrResponsables { get => strResponsables; set => strResponsables = value; }
    }
}