using QRCoder;
using QRCoder.Interfaces;
using QRCoder.Models;
using QRCoder.Renderers;
using QuickQRCodeRender.MatrixConverters;
using QuickQRCodeRender.Renderers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;

namespace DevTestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"https://www.gessi.com", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 3);

            Bitmap imgLogo = new Bitmap(@"C:\temp\Logo.jpg");
            Bitmap imgFinder = new Bitmap(@"C:\temp\boxfinder.jpg");
            Bitmap imgFantasy = new Bitmap(@"C:\temp\GessiFinder2.jpg");


            RenderMatrixOptions opts = new RenderMatrixOptions();

            opts.BackgroundColor = Color.White;
            opts.LightColor = Color.White;
            opts.DarkColor = Color.Black;
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;
            opts.FinderPatternImage = imgFinder;
            opts.FantasyRenderImage = imgFantasy;
            

            IConvertToMatrixView converter = new VLineConverter();
            RenderMatrix renderMatrix = new RenderMatrix(qrData);

            IDrawQRCode renderer = new FantasyRenderer();
            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            bitmapRis.Save(@"c:\temp\qrFabioEmy1.jpg", ImageFormat.Png);

        }
    }
}