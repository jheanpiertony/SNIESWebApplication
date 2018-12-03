namespace SNIESWebApplication.Helpers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class ImagenHelper : Controller
    {
        public ActionResult MostrarImagen(string _documento, byte[] _imagen, string _url, string _urlAlter, string _imagenFormato= "Image/png")
        {

            if ( string.IsNullOrEmpty(_documento))
            {
                var Path = System.Web.HttpContext.Current.Server.MapPath(_url);
                System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
                ImageConverter imageConverter = new ImageConverter();
                byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
                return File(imageByte, _imagenFormato);
            }
            if (_imagen== null)
            {
                var Path = System.Web.HttpContext.Current.Server.MapPath(_urlAlter);
                System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
                ImageConverter imageConverter = new ImageConverter();
                byte[] imageByte = (byte[])imageConverter.ConvertTo(image, typeof(byte[]));
                return File(imageByte, _imagenFormato);
            }
            return File(_imagen, _imagenFormato);
        }

        //public ActionResult Capture()
        //{
        //    ElimarFotoCapture();
        //    if (Request.InputStream.Length > 0)
        //    {
        //        using (StreamReader reader = new StreamReader(Request.InputStream))
        //        {
        //            string hexString = Server.UrlEncode(reader.ReadToEnd());
        //            string imageName = "imagenCapt";
        //            string imagePath = string.Format("~/Content/Images/FotoWebCam/{0}.png", imageName);
        //            System.IO.File.WriteAllBytes(Server.MapPath(imagePath), ConvertHexToBytes(hexString));
        //            Session("CapturedImage") = VirtualPathUtility.ToAbsolute(imagePath);
        //        }
        //    }
        //    else
        //        using (StreamReader reader = new StreamReader(Request.InputStream))
        //        {
        //            string hexString = "~/Content/Images/Unicoc-horizontal.png";
        //            string imageName = "imagenCapt";
        //            string imagePath = "~/Content/Images/Unicoc-horizontal.png";
        //            Session("CapturedImage") = VirtualPathUtility.ToAbsolute(imagePath);
        //        }
        //    return View();
        //}

        //private static byte[] ConvertHexToBytes(string hex)
        //{
        //    byte[] bytes =  byte[hex.Length / (double)2 - 1 + 1];
        //    for (int i = 0; i <= hex.Length - 1; i += 2)
        //        bytes[i / (double)2] = Convert.ToByte(hex.Substring(i, 2), 16);
        //    return bytes;
        //}
        //private void ElimarFotoCapture()
        //{
        //    var path = Server.MapPath("~/Content/Images/FotoWebCam/imagenCapt.png");
        //    if ((System.IO.File.Exists(path)))
        //        System.IO.File.Delete(path);
        //}

        //private bool ActulizarFotoCapture()
        //{
        //    var path = Server.MapPath("~/Content/Images/FotoWebCam/imagenCapt.png");
        //    if ((System.IO.File.Exists(path)))
        //        return true;
        //    else
        //        return false;
        //}


        //$(document).ready(function () {
        //    $("#Gender").attr('class', 'form-control');
        //    $("#DEFAULTDEPTID").attr('class', 'form-control');
        //    $("#Political").attr('class', 'form-control');
        //    $("#TITLE").attr('class', 'form-control');
        //    $("#contry").attr('class', 'form-control');
        //    $("#CITY").attr('Class', 'form-control');

        //    jQuery("#webcam").webcam({
        //    width: 320,
        //        height: 240,
        //        mode: "save",
        //        swffile: '/Scripts/jscam.swf',
        //        debug: function(type, status) {
        //        },
        //        onSave: function(data, ab) {
        //            $.ajax({
        //            type: "POST",
        //                url: '/USERINFO/GetCapture',
        //                data: '',
        //                contentType: "application/json; charset=utf-8",
        //                dataType: "text",
        //                success: function(r) {
        //                    $("#imgCapture").css("visibility", "visible");
        //                    $("#imgCapture").attr("src", r);
        //                },
        //                failure: function(response) {
        //                    alert(response.d);
        //                }
        //            });
        //        },
        //        onCapture: function() {
        //            webcam.save('/USERINFO/Capture');
        //        }
        //    });
        //});
        //function Capture()
        //{
        //    webcam.capture();
        //}


    }
}