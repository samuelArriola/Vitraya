using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace Persistencia.trainings
{
    public partial class Archivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int intTipoInforme = Convert.ToInt32(Request["informe"].ToString());

            switch (intTipoInforme)
            {
                case 1:
                    {
                        GetInformeCoverturaTemas();
                        break;
                    }
                case 2:
                    {
                        GetInformePendiantesPorFirmar();
                        break;
                    }
                case 3:
                    {
                        GetInformeAsistencia();
                        break;
                    }
                case 4:
                    {
                        GetInformeInAsistencia();
                        break;
                    }
            }
        }

        public void GetInformeCoverturaTemas()
        {
            List<InformesTrainings.InformeCoberturaUnidad> informes = InformesTrainings.getIformesCoberturaTema();

            DataTable dt = new DataTable();
            dt.Columns.Add("Responsable");
            dt.Columns.Add("Tema");
            dt.Columns.Add("Unidad Funcional");
            dt.Columns.Add("N° de Invitados");
            dt.Columns.Add("N° Asistentes");
            dt.Columns.Add("Procentaje de  Cumplimiento");
            foreach (var informe in informes)
            {
                foreach (var datos in informe.Informaciones)
                {
                    dt.Rows.Add(new Object[] { informe.StrResposable, informe.StrTema, datos.StrUnidadFuncional, datos.FtlConteoMatricula, datos.FtlConteoAsistencia, datos.StrPorcentaje });
                }
            }

            InformeAExcel(dt, "Informe de Cobertura Por Temas");
        }

        public void GetInformePendiantesPorFirmar()
        {
            List<InformesTrainings.InformeInasistencia> informes = InformesTrainings.GetAsistenciasNoFirmadas();

            DataTable dt = new DataTable();

            dt.Columns.Add("Documento");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Unidad Funcional");
            dt.Columns.Add("Tema");

            foreach (var informe in informes)
            {
                foreach (var tema in informe.Temas)
                {
                    dt.Rows.Add(new object[] { informe.IntDocumento, informe.StrNombre, informe.SrtUnidadFuncional, tema });
                }
            }

            InformeAExcel(dt, "informe de Actas Pendiantes Por Firmar");
        }

        public void GetInformeAsistencia()
        {
            List<InformesTrainings.InformeAsistencia> informes = InformesTrainings.GetInformeAsistencias();

            DataTable dt = new DataTable();

            dt.Columns.Add("Responsable");
            dt.Columns.Add("Tema");
            dt.Columns.Add("Trabajador");
            dt.Columns.Add("Unidad Funcional");
            dt.Columns.Add("Asistencia");

            foreach (var informe in informes)
            {
                foreach (var datos in informe.Informacion)
                {
                    dt.Rows.Add(new object[] { informe.StrResponsable, informe.StrTema, datos.StrNomUsuario, datos.StrUnidadFuncional, datos.StrAsistencia });
                }
            }

            InformeAExcel(dt, "Informe de Asistencia");
        }

        public void InformeAExcel(DataTable dt, string nombre)
        {
            try
            {
                XSSFWorkbook book = new XSSFWorkbook();
                MemoryStream s = new MemoryStream();
                XSSFSheet sheet = (XSSFSheet)book.CreateSheet("hoja 1");
                XSSFRow HeaderRow = (XSSFRow)sheet.CreateRow(1);
                foreach (DataColumn column in dt.Columns)
                {
                    HeaderRow.CreateCell(column.Ordinal + 1).SetCellValue(column.ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    XSSFRow row = (XSSFRow)sheet.CreateRow(i + 2);
                    foreach (DataColumn column in dt.Columns)
                    {
                        row.CreateCell(column.Ordinal + 1).SetCellValue(dt.Rows[i][column].ToString());
                    }
                }

                book.Write(s);
                s.Flush();

                MemoryStream ms = new MemoryStream();
                ms.Write(s.ToArray(), 0, s.ToArray().Length);
                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode(nombre) + ".xlsx"));
                HttpContext.Current.Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.OutputStream.Close();


            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

            }
        }
        public void GetInformeInAsistencia()
        {
            List<InformesTrainings.InformeInasistencia> informes = InformesTrainings.GetInformeInasistencias();

            DataTable dt = new DataTable();

            dt.Columns.Add("Documento");
            dt.Columns.Add("Nombre");
            dt.Columns.Add("Unidad Funcional");
            dt.Columns.Add("Tema");
            foreach (var informe in informes)
            {
                foreach (var tema in informe.Temas)
                {
                    dt.Rows.Add(new object[] { informe.IntDocumento, informe.StrNombre, informe.SrtUnidadFuncional, tema });
                }
            }
            InformeAExcel(dt, "Informe de inasistencia");
        }
    }
}