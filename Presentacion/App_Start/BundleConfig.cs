using System.Web.Optimization;

namespace Presentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkID=303951

        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/bundles/WebformsCSS").Include(
                   "~/vendors/bootstrap/dist/css/bootstrap.min.css",
                   "~/vendors/font-awesome/css/font-awesome.min.css",
                   "~/vendors/nprogress/nprogress.css",
                   "~/vendors/iCheck/skins/flat/green.css",
                   "~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                   "~/vendors/jqvmap/dist/jqvmap.min.css",
                   "~/vendors/bootstrap-daterangepicker/daterangepicker.css",
                   "~/build/css/custom.min.css",
                   "~/vendors/bootstrap-daterangepicker/daterangepicker.css",
                   "~/vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css",
                   "~/vendors/normalize-css/normalize.css",
                   "~/vendors/ion.rangeSlider/css/ion.rangeSlider.css",
                   "~/vendors/ion.rangeSlider/css/ion.rangeSlider.skinFlat.css",
                   "~/vendors/pnotify/dist/pnotify.css",
                   "~/vendors/pnotify/dist/pnotify.buttons.css",
                   "~/vendors/pnotify/dist/pnotify.nonblock.css",
                   "~/vendors/mjolnic-bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css",
                   "~/vendors/cropper/dist/cropper.min.css",
                   "~/css/bootstrap-year-calendar.min.css"
                   ));

            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js",
                            "~/Scripts/jquery-3.4.1.min.js",
                            "~/Scripts/modernizr-2.8.3.js",
                             "~/vendors/jquery/dist/jquery.min.js",
                            "~/Scripts/bootstrap-year-calendar.min.js",
                            "~/vendors/bootstrap/dist/js/bootstrap.bundle.min.js",
                            "~/vendors/fastclick/lib/fastclick.js",
                            "~/vendors/nprogress/nprogress.js",
                            "~/vendors/Chart.js/dist/Chart.min.js",
                            "~/vendors/gauge.js/dist/gauge.min.js",
                            "~/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js",
                            "~/vendors/iCheck/icheck.min.js",
                            "~/vendors/skycons/skycons.js",
                            "~/vendors/Flot/jquery.flot.js",
                            "~/vendors/Flot/jquery.flot.pie.js",
                            "~/vendors/Flot/jquery.flot.time.js",
                            "~/vendors/Flot/jquery.flot.stack.js",
                            "~/vendors/Flot/jquery.flot.resize.js",
                            "~/vendors/flot.orderbars/js/jquery.flot.orderBars.js",
                            "~/vendors/flot-spline/js/jquery.flot.spline.min.js",
                            "~/vendors/flot.curvedlines/curvedLines.js",
                            "~/vendors/DateJS/build/date.js",
                            "~/vendors/jqvmap/dist/jquery.vmap.js",
                            "~/vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                            "~/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js",
                            "~/vendors/moment/min/moment.min.js",
                            "~/vendors/moment/min/moment-with-locales.min.js",
                            "~/vendors/bootstrap-daterangepicker/daterangepicker.js",
                            "~/build/js/custom.js",
                            "~/vendors/cropper/dist/cropper.min.js",
                            "~/vendors/jquery-knob/dist/jquery.knob.min.js",
                            "~/vendors/datatables.net/js/jquery.dataTables.min.js",
                            "~/vendors/datatables.net-bs/js/dataTables.bootstrap.min.js",
                            "~/vendors/datatables.net-buttons/js/dataTables.buttons.min.js",
                            "~/vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
                            "~/vendors/datatables.net-buttons/js/buttons.flash.min.js",
                            "~/vendors/datatables.net-buttons/js/buttons.html5.min.js",
                            "~/vendors/datatables.net-buttons/js/buttons.print.min.js",
                            "~/vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
                            "~/vendors/datatables.net-keytable/js/dataTables.keyTable.min.js",
                            "~/vendors/datatables.net-responsive/js/dataTables.responsive.min.js",
                            "~/vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js",
                            "~/vendors/datatables.net-scroller/js/dataTables.scroller.min.js",
                            "~/vendors/ion.rangeSlider/js/ion.rangeSlider.min.js",
                            "~/vendors/jszip/dist/jszip.min.js",
                            "~/vendors/pdfmake/build/pdfmake.min.js",
                            "~/vendors/pdfmake/build/vfs_fonts.js",
                            "~/vendors/pnotify/dist/pnotify.js",
                            "~/vendors/pnotify/dist/pnotify.buttons.js",
                            "~/vendors/pnotify/dist/pnotify.nonblock.js",
                            "~/vendors/vendors/pnotify/dist/pnotify.nonblock.js",
                            "~/vendors/jquery.inputmask/dist/min/jquery.inputmask.bundle.min.js",
                            "~/vendors/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js",
                            "~/vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                            "~/vendors/bootstrap/dist/js/bootstrap.bundle.js"
                            ));

            // El orden es muy importante para el funcionamiento de estos archivos ya que tienen dependencias explícitas
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // Use la versión de desarrollo de Modernizr para desarrollar y aprender. Luego, cuando esté listo
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));
        }
    }
}