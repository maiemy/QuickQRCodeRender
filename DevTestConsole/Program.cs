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

            Bitmap imgLogo = new Bitmap(@"C:\Fabio\QRCode\Logo.jpg");
            Bitmap imgFinder = new Bitmap(@"C:\Fabio\QRCode\GessiFinder.jpg");
            Bitmap imgFantasy = new Bitmap(@"C:\Fabio\QRCode\s-l1600.jpg");


            RenderMatrixOptions opts = new RenderMatrixOptions();

            opts.BackgroundColor = Color.White;
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;
            opts.FinderPatternImage = imgFinder;

            //IConvertToMatrixView converter = new VLineConverter();
            RenderMatrix renderMatrix = new RenderMatrix(qrData);

            IDrawQRCode renderer = new FantasyRenderer();
            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            bitmapRis.Save(@"c:\temp\qrFabioEmy1.jpg", ImageFormat.Png);

        }
    }
}