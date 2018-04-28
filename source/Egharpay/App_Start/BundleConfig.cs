using System.Web;
using System.Web.Optimization;

namespace Egharpay
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.ResetAll();

            bundles.Add(new ScriptBundle("~/Scripts/bower").Include(
                "~/bower_components/jquery/dist/jquery.min.js",
                "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
                "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
                "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/bower_components/moment/min/moment.min.js",
                "~/bower_components/angular/angular.min.js",
                "~/bower_components/angular-animate/angular-animate.min.js",
                "~/bower_components/angular-sanitize/angular-sanitize.min.js",
                "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
                "~/bower_components/angular-responsive-tables/release/angular-responsive-tables.min.js",
                "~/bower_components/angular-ui-select/dist/select.min.js",
                "~/bower_components/angular-ui-uploader/dist/uploader.min.js",
                "~/bower_components/angular-img-cropper/dist/angular-img-cropper.min.js",
                "~/bower_components/angular-ui-mask/dist/mask.min.js",
                //"~/bower_components/bootstrap-daterangepicker/daterangepicker.js",
                "~/bower_components/bootbox/bootbox.js",
                "~/bower_components/ngBootbox/ngBootbox.js",
                "~/bower_components/ion.rangeSlider/js/ion.rangeSlider.min.js",
                "~/bower_components/ngInfiniteScroll/build/ng-infinite-scroll.min.js",
                "~/bower_components/angular-spinner/dist/angular-spinner.min.js",
                "~/bower_components/ngGeolocation/ngGeolocation.js"
                //"~/bower_components/ez-plus/src/jquery.ez-plus.js"
                //"~/bower_components/angular-ez-plus/js/angular-ezplus.js"
                ));

            bundles.Add(new ScriptBundle("~/Scripts/Application").Include(
                "~/Scripts/Angular/Moment.js",
                "~/Scripts/Angular/app.js",
                "~/Scripts/Angular/Prototypes/*.js",
                "~/Scripts/Angular/Controllers/*.js",
                //"~/Scripts/moris/*.js",
                "~/Scripts/Angular/Services/*.js",
                "~/Scripts/Angular/Filters/*.js",
                "~/Scripts/Angular/Directives/*.js",
                "~/Scripts/App/*.js"

                ));

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/Styles").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/css/Site.css",
                      "~/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/bower_components/ion.rangeSlider/css/ion.rangeSlider.css",
                      "~/bower_components/ion.rangeSlider/css/ion.rangeSlider.skinFlat.css",
                      "~/bower_components/angular-ui-select/dist/select.min.css",
                      "~/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/bower_components/bootstrap-daterangepicker/daterangepicker.css"
                      //"~/bower_components/wip-image-zoom/dist/wip-image-zoom.min.css"
                      ));


            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/Styles").Include(
                      "~/fonts/font-awesome/css/font-awesome.min.css", new CssRewriteUrlTransform()));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.1.1.min.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/Scripts/plugins/pace/pace.min.js"
                      /*"~/Scripts/app/inspinia.js"*/));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));



            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/datatables.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/datatables.min.js"));


            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // ionRange styles
            //bundles.Add(new StyleBundle("~/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
            //          "~/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
            //          "~/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            //bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
            //          "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));


            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/plugins/cropper/cropper.min.css"));

            // Typehead
            bundles.Add(new ScriptBundle("~/plugins/typehead").Include(
                      "~/Scripts/plugins/typehead/bootstrap3-typeahead.min.js"));


            // Clockpicker styles
            bundles.Add(new StyleBundle("~/plugins/clockpickerStyles").Include(
                      "~/Content/plugins/clockpicker/clockpicker.css"));

            // Clockpicker
            bundles.Add(new ScriptBundle("~/plugins/clockpicker").Include(
                      "~/Scripts/plugins/clockpicker/clockpicker.js"));



        }
    }
}
