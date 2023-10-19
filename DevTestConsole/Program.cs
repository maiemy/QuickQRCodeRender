using QRCoder;
using QRCoder.Interfaces;
using QRCoder.Models;
using QRCoder.Renderers;
using QuickQRCodeRender.MatrixConverters;
using QuickQRCodeRender.Renderers;
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
            QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"https://www.gessi.com", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 3);

            Bitmap imgLogo = new Bitmap(@"C:\Temp\Logo.jpg");
            Bitmap imgFinder = new Bitmap(@"C:\Temp\GessiFinder.jpg");

            RenderMatrixOptions opts = new RenderMatrixOptions();
            opts.BackgroundColor = Color.White;
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;
            opts.FinderPatternImage = imgFinder;

            VLineConverter vlineConverter = new VLineConverter();
            RenderMatrix renderMatrix = new RenderMatrix(qrData, vlineConverter);

            IDrawQRCode renderer = new VLineRender();
            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            bitmapRis.Save(@"c:\temp\qrFabioEmy1.jpg", ImageFormat.Png);

        }
    }
}