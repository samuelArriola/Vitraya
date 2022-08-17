using Entidades.GestionDocumental;
using Persistencia.GestionDocumental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Logica.GestionDocumental
{
    public class GestionDocumentalLogica
    {

        public static bool VerificarRevisiones(int idDocumento)
        {
            List<GDRevision> revisiones = DAOGDRevision.GetGDRevisiones(idDocumento);
            foreach(var revision in revisiones)
            {
                if (revision.IntEstado != GDRevision.APROBADO)
                    return false;
            }
            return true;
        }

        public static string ToHtml(string s)
        {
            s = HttpUtility.HtmlEncode(s);
            string[] paragraphs = s.Split(new string[] { "\n" }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            foreach (string par in paragraphs)
            {
                sb.AppendLine(par);
            }
            return sb.ToString();
        }
    }
}