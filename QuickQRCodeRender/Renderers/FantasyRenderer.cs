using QRCoder.Interfaces;
using QRCoder.Models;
using QuickQRCodeRender.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickQRCodeRender.Renderers
{
    public class FantasyRenderer : IDrawQRCode
    {
        PathCleanerAndDraw pathCleaner = new PathCleanerAndDraw();

        // disegna il QRCode
        public Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options)
        {
            RenderParametersUtility utils = new RenderParametersUtility(matrix, options);
            

            // int blocchiPerLato = matrix.MatrixView.GetLength(0);
            int blocchiPerLato = utils.BorderNumModules;

            // calcolo la dimensione in pixel per ogni blocco della matrice
            // int pixelSize = Convert.ToInt16(Math.Round( (decimal)options.BoxSize / (decimal)blocchiPerLato) );
            int pixelSize = utils.SingleModulePixel;

            // creo una matrice di lavoro.
            int[,] matrixLavoro = matrix.MatrixView;

            // sbianco i riquadri dei filler se mi hanno passato una immagine.
            if (options.FinderPatternImage != null)
            {
                pathCleaner.ClearAllFinderArea(matrixLavoro, utils.FinderPositionTopLeft, utils.FinderPositionTopRight, utils.FinderPositionBottom);
            } // chiudo if(options.FinderPatternImage!=null)

            // se abbiamo il logo dobbiamo sbiancare la parte centrale del quadrato.
            if (options.Logo != null)
            {
                pathCleaner.ClearLogoArea(matrixLavoro, utils.LogoBorderWhiteModules, utils.LogoPositionOffset);
            } // chiudo if(options.Logo != null) {             

            // int size = blocchiPerLato * pixelSize;
            int size = utils.BorderPixel;
            Bitmap bitmapRisultato = new Bitmap(size, size);

            Bitmap darkModulePixel = MakeDotPixel(pixelSize, new SolidBrush(options.DarkColor), options.FantasyRenderImage);
            Bitmap lightModulePixel = MakeDotPixel(pixelSize, new SolidBrush(options.LightColor));

            using (Graphics graphics = Graphics.FromImage(bitmapRisultato))
            {
                using (SolidBrush lightBrush = new SolidBrush(options.LightColor))
                {
                    using (SolidBrush darkBrush = new SolidBrush(options.DarkColor))
                    {
                        // disegno il rettangolo del background
                        using (var brush = new SolidBrush(options.BackgroundColor))
                            graphics.FillRectangle(brush, new Rectangle(0, 0, size, size));

                        // qui cicliamo per disegnare il qrcode.
                        for (var x = 0; x < blocchiPerLato; x++)
                        {
                            for (var y = 0; y < blocchiPerLato; y++)
                            {
                                Bitmap bDraw = matrixLavoro[x, y] == 1 ? darkModulePixel : lightModulePixel;
                                graphics.DrawImage(bDraw, new Point(x * pixelSize, y * pixelSize));
                            } // chiudo for (var y = 0; y < blocchiPerLato; y++)
                        } // chiudo for (var x = 0; x < blocchiPerLato; x++)

                        graphics.Save();
                    } // chiudo using (var darkBrush = new SolidBrush(options.DarkColor))
                } // chiudo using (var lightBrush = new SolidBrush(options.LightColor))
            } // chiudo using (var graphics = Graphics.FromImage(bitmap))

            // qui inseriamo le immagini
            pathCleaner.DrawLogoAndFinder(bitmapRisultato, utils, options.RotateFinderImage);

            // se mi e' stato chiesto il bordo 
            //if (options.DrawQuietZones)
            //{
            //    // considero 4 blocchi come spazio di sicurezza                
            //    int offset = 4 * pixelSize;
            //    int qSize = size + (offset * 2);
            //    Bitmap bitmapZones = new Bitmap(qSize, qSize);

            //    using (Graphics graphics = Graphics.FromImage(bitmapZones))
            //    {
            //        // disegno il rettangolo del background
            //        using (var brush = new SolidBrush(options.BackgroundColor))
            //            graphics.FillRectangle(brush, new Rectangle(0, 0, qSize, qSize));

            //        // gli disegno dentro il qr creato sopra
            //        graphics.DrawImage(bitmapRisultato, new Point(offset, offset));
            //        graphics.Save();
            //    } // chiudo using (Graphics graphics = Graphics.FromImage(bitmapZones))                
            //    // ridimensiono alla richiesta delle opzioni.
            //    bitmapRisultato = pathCleaner.Resize(bitmapZones, options.BoxSize);
            //} // chiudo if(options.DrawQuietZones)

            return bitmapRisultato;
        } // chiudo public Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options)


        private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush, Bitmap customImage = null)
        {
            // draw a dot
            var bitmap = new Bitmap(pixelSize, pixelSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(brush, new Rectangle(0, 0, pixelSize, pixelSize));
                if(customImage != null)
                    graphics.DrawImage(pathCleaner.Resize(customImage, pixelSize), new Rectangle(0, 0, pixelSize - 2, pixelSize - 2));
                graphics.Save();
            } // chiudo using (var graphics = Graphics.FromImage(bitmap))

            return bitmap;

        } // chiudo private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush)
    } // chiudo public class FantasyRenderer : IDrawQRCode
} // chiudo namespace QuickQRCodeRender.Renderers
