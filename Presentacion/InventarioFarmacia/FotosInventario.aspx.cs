﻿using Entidades.InventarioFarmacia;
using Persistencia.InventarioFarmacia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.InventarioFarmacia
{
    public partial class FotosInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void GetSetFoto()
        {
            PFotosInventario.GetSetFotoInventario();
        }

        [WebMethod]
        public static List<EFotosInventario1> GetFotosInvVitraya()
        {
            return PFotosInventario.GetFotosInvVitraya();
        }
    }
}