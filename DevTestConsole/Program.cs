using QRCoder;
using QRCoder.Interfaces;
using QRCoder.Models;
using QRCoder.Renderers;
using QuickQRCodeRender.MatrixConverters;
using QuickQRCodeRender.Renderers;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace DevTestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Bitmap ris = GenerateFunQRCode(3);

            //ris.Save(@"C:\Fabio\QRCode\qrForFun.png", ImageFormat.Png);

            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"https://net-informations.com/csharp/statements/img/break.png", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 4);

            Bitmap imgLogo = new Bitmap(@"C:\Fabio\ImgVarie\BreakLogo.jpg");
            //Bitmap imgFinder = new Bitmap(@"C:\Fabio\QRCode\GessiFinderMaestro.jpg");

            Bitmap imgFinder = new Bitmap(@"C:\Fabio\QRCode\GessiFinderMaestro.jpg");

            //Bitmap imgFinder = new Bitmap(@"C:\Fabio\QRCode\GessiFinder.jpg");

            //Bitmap imgLogo = new Bitmap(@"C:\Fabio\QRCode\QRCodeResources\LogoGessi.png");
            //Bitmap imgFantasy = new Bitmap(@"C:\Fabio\QRCode\s-l1600.jpg");
            //Bitmap imgFinder = new Bitmap(@"C:\Fabio\QRCode\Prove\MG.jpg");
            //Bitmap imgFantasy = new Bitmap(@"C:\Fabio\QRCode\Prove\5204morocco2.jpeg");

            //Bitmap bitmapRisultato = new Bitmap(imgFinder.Width, imgFinder.Height);

            //Console.WriteLine(DateTime.Now.ToString("hh-mm-ss_fff"));
            //for (int i = 0; i < imgFinder.Width; i++)
            //{
            //    for (int j = 0; j < imgFinder.Height; j++)
            //    {
            //        Color originalColor = imgFinder.GetPixel(i, j);
            //        if(originalColor.R <= 100 && originalColor.G <=100 && originalColor.B <= 100)
            //        {
            //            bitmapRisultato.SetPixel(i, j, Color.Blue);
            //        }
            //        else
            //        {
            //            bitmapRisultato.SetPixel(i,j, originalColor);
            //        }
            //    }
            //}
            //Console.WriteLine(DateTime.Now.ToString("hh-mm-ss_fff"));

            //Console.WriteLine(DateTime.Now.ToString("hh-mm-ss"));
            //for (int i = 0; i < 10000000; i++)
            //{
            //    bitmapRisultato.SetPixel(0, 0, Color.Blue);
            //}
            //Console.WriteLine(DateTime.Now.ToString("hh-mm-ss"));


            //bitmapRisultato.Save(@"C:\Fabio\QRCode\ProvaColori.png", ImageFormat.Png);

            RenderMatrixOptions opts = new RenderMatrixOptions();

            opts.BackgroundColor = Color.White;
            opts.DarkColor = Color.Black;
            opts.LightColor = Color.White;
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;
            opts.FinderPatternImage = imgFinder;
            opts.QuietZoneNumModules = 4;
            opts.RotateFinderImage = false;
            //opts.FantasyRenderImage = imgFantasy;

            IConvertToMatrixView converter = new VLineConverter();
            RenderMatrix renderMatrix = new RenderMatrix(qrData, converter);

            IDrawQRCode renderer = new VLineRender();
            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            bitmapRis.Save(@"C:\Fabio\QRCode\QRBreak.png", ImageFormat.Png);

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    bitmapRis.Save(ms, ImageFormat.Png);
            //    byte[] byteImage = ms.ToArray();
            //    string base64String = Convert.ToBase64String(byteImage); // Get Base64
            //}

            ////bitmapRis.MakeTransparent(Color.White);
            ////System.Drawing.Image img = new Bitmap(bitmapRis);

            //using (Graphics gr = Graphics.FromImage(bitmapRis)) // SourceImage is a Bitmap object
            //{
            //    var gray_matrix = new float[][] {
            //    new float[] { 0.299f, 0.299f, 0.299f, 0, 0 },
            //    new float[] { 0.587f, 0.587f, 0.587f, 0, 0 },
            //    new float[] { 0.114f, 0.114f, 0.114f, 0, 0 },
            //    new float[] { 0,      0,      0,      1, 0 },
            //    new float[] { 0,      0,      0,      0, 1 }
            //    };

            //    var ia = new System.Drawing.Imaging.ImageAttributes();
            //    ia.SetColorMatrix(new System.Drawing.Imaging.ColorMatrix(gray_matrix));
            //    ia.SetThreshold(0.8f); // Change this threshold as needed
            //    var rc = new Rectangle(0, 0, bitmapRis.Width, bitmapRis.Height);
            //    gr.DrawImage(bitmapRis, rc, 0, 0, bitmapRis.Width, bitmapRis.Height, GraphicsUnit.Pixel, ia);
            //}



            //Bitmap QRCodeNoBG = (Bitmap)bitmapRis.Clone();
            //QRCodeNoBG.MakeTransparent(Color.White);

            //Bitmap QRCodeNoForeC = (Bitmap)bitmapRis.Clone();
            //QRCodeNoForeC.MakeTransparent(Color.Black);

            //QRCodeNoBG.Save(@"C:\Fabio\QRCode\QRFabioNoBG.png", ImageFormat.Png);
            //QRCodeNoForeC.Save(@"C:\Fabio\QRCode\QRFabioNoForeC.png", ImageFormat.Png);

            //Bitmap currentMask = (Bitmap)bitmapRis.Clone();

            //Color bg = Color.White;
            //Color fc = Color.Black;

            //Bitmap ris = ChangeColor(bg, Color.Blue, (Bitmap)bitmapRis.Clone());

            //ris.Save(@"C:\Fabio\QRCode\QRFabioDefinitivo.png", ImageFormat.Png);

            //ris = ChangeColor(fc, Color.Yellow, ris);

            //ris.Save(@"C:\Fabio\QRCode\QRFabioDefinitivo.png", ImageFormat.Png);

            //ris = ChangeColor(Color.Blue, Color.Green, ris);

            //ris.Save(@"C:\Fabio\QRCode\QRFabioDefinitivo.png", ImageFormat.Png);

            //ris = ChangeColor(Color.Green, Color.Transparent, ris);
            //ris.Save(@"C:\Fabio\QRCode\QRFabioDefinitivo.png", ImageFormat.Png);

            //// Voglio cambiare il colore qr
            //using (Graphics g = Graphics.FromImage(ris))
            //{
            //    g.Clear(Color.Yellow);
            //    currentMask.MakeTransparent(Color.Black);

            //    g.DrawImage(currentMask, 0, 0);
            //    g.Save();
            //}
            //ris.Save(@"C:\Fabio\QRCode\QRFabioDefinitivo.png", ImageFormat.Png);

        }

        private static Bitmap GenerateFunQRCode(int iterations)
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qrData = qRCodeGenerator.CreateQrCode(@"https://m.media-amazon.com/images/I/91XB6Fu9oEL._AC_SX569_.jpg", QRCodeGenerator.ECCLevel.L, eciMode: QRCodeGenerator.EciMode.Utf8, requestedVersion: 4);

            Bitmap imgLogo = new Bitmap(@"C:\Fabio\QRCode\Maestro.png");
            Bitmap defaultFinder = new Bitmap(@"C:\Fabio\QRCode\QR.jpg");
            Bitmap imgFinder = iterations == 0 ? defaultFinder : GenerateFunQRCode(iterations - 1);

            RenderMatrixOptions opts = new RenderMatrixOptions();

            opts.BackgroundColor = Color.White;
            opts.DarkColor = Color.Black;
            opts.LightColor = Color.White;
            opts.DrawQuietZones = true;
            opts.Logo = imgLogo;
            opts.FinderPatternImage = imgFinder;
            opts.QuietZoneNumModules = 4;
            opts.RotateFinderImage = false;

            IDrawQRCode renderer;
            RenderMatrix renderMatrix;
            if (iterations % 2 == 0)
            {
                IConvertToMatrixView converter = new HLineConverter();
                renderMatrix = new RenderMatrix(qrData, converter);

                renderer = new HLineRender();
                
            }
            else
            {
                IConvertToMatrixView converter = new VLineConverter();
                renderMatrix = new RenderMatrix(qrData, converter);

                renderer = new VLineRender();
            }

            Bitmap bitmapRis = renderer.DrawQRCode(renderMatrix, opts);

            //bitmapRis.Save(@"C:\Fabio\QRCode\qrForFun.png", ImageFormat.Png);

            return bitmapRis;
        }

        private static Bitmap ChangeColor(Color coloreDaCambiare, Color nuovoColore, Bitmap currentMask)
        {
            Bitmap ris = new Bitmap(currentMask.Width, currentMask.Height);
            // Voglio cambiare lo sfondo
            using (Graphics g = Graphics.FromImage(ris))
            {
                g.Clear(nuovoColore);
                currentMask.MakeTransparent(coloreDaCambiare);

                g.DrawImage(currentMask, 0, 0);
                g.Save();
            }

            return ris;
        }
    }
}