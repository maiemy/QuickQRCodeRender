using QRCoder;
using QRCoder.Models;
using QRCoder.Renderers;
using System.Drawing;
using System.Drawing.Imaging;

namespace DevTestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"http://www.gessi.com", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 4);

            Bitmap imgLogo = new Bitmap(@"C:\Temp\Logo.jpg");

            RenderMatrixOptions opts = new RenderMatrixOptions();
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;

            RenderMatrix renderMatrix = new RenderMatrix(qrData);

            DefaultRenderer renderer = new DefaultRenderer();
            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            bitmapRis.Save(@"c:\temp\qrFabioEmy1.jpg", ImageFormat.Png);

        }
    }
}