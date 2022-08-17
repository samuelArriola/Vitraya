using System.Web.Mvc;

namespace Presentacion.Controllers
{
    public class GenDocumentController : Controller
    {
        public ActionResult Cabecera(string nomDocument, string codigo, string fecha, string version)
        {
            return View("Cabecera2", new object[] { nomDocument, codigo, fecha, version });
        }

        public ActionResult PiePagina()
        {
            return View("PiePagina2");
        }

        public ActionResult Portada(string t)
        {
            return View(new object[] { t });
        }
    }
}