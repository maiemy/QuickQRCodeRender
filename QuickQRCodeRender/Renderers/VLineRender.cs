using QRCoder.Interfaces;
using QRCoder.Models;
using QuickQRCodeRender.MatrixConverters;
using QuickQRCodeRender.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickQRCodeRender.Renderers
{
    public class VLineRender : IDrawQRCode
    {
        // disegna il QRCode
        public Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options)
        {
            RenderParametersUtility utils = new RenderParametersUtility(matrix, options);
            PathCleanerAndDraw pathCleaner = new PathCleanerAndDraw();

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

            Bitmap lightModulePixel = MakeDotPixel(pixelSize, new SolidBrush(options.LightColor), VLineConverter.Dots.White);
            Bitmap darkModulePixel = MakeDotPixel(pixelSize, new SolidBrush(options.DarkColor), VLineConverter.Dots.Fill);            
            Bitmap darkDottedPixel = MakeDotPixel(pixelSize, new SolidBrush(options.DarkColor), VLineConverter.Dots.Dotted);
            Bitmap darkTopPixel = MakeDotPixel(pixelSize, new SolidBrush(options.DarkColor), VLineConverter.Dots.Top);
            Bitmap darkBottomPixel = MakeDotPixel(pixelSize, new SolidBrush(options.DarkColor), VLineConverter.Dots.Bottom);

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
                                Bitmap bDraw = null;
                                switch (matrixLavoro[x, y])
                                {
                                    case (int)VLineConverter.Dots.White:
                                        bDraw = lightModulePixel;
                                        break;
                                    case (int)VLineConverter.Dots.Top:                                        
                                        bDraw = darkTopPixel;
                                        break;
                                    case (int)VLineConverter.Dots.Bottom:
                                        bDraw = darkBottomPixel;
                                        break;
                                    case (int)VLineConverter.Dots.Fill:
                                        bDraw = darkModulePixel;
                                        break;
                                    case (int)VLineConverter.Dots.Dotted:
                                        bDraw = darkDottedPixel;
                                        break;
                                } // chiudo switch(matrixLavoro[x, y])
                                
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


        private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush, VLineConverter.Dots dot)
        {
            // draw a dot
            var bitmap = new Bitmap(pixelSize, pixelSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                switch (dot)
                {
                    case VLineConverter.Dots.White:
                    case VLineConverter.Dots.Fill:
                        graphics.FillRectangle(brush, new Rectangle(2, 0, pixelSize-2, pixelSize));
                        graphics.Save();
                        break;                    
                        
                    case VLineConverter.Dots.Top:
                        graphics.FillEllipse(brush, new Rectangle(2, 0, pixelSize-2, pixelSize));
                        graphics.FillRectangle(brush, new Rectangle(2, (int)Math.Round(pixelSize/2f)+1, pixelSize - 2, (int)Math.Round(pixelSize /2f)+1));
                        graphics.Save();
                        break;                        
                    case VLineConverter.Dots.Bottom:
                        graphics.FillEllipse(brush, new Rectangle(2, 0, pixelSize - 2, pixelSize));
                        graphics.FillRectangle(brush, new Rectangle(2, 0, pixelSize - 2, (int)Math.Round(pixelSize / 2f)+1));
                        graphics.Save();
                        break;                        
                    case VLineConverter.Dots.Dotted:
                        graphics.FillEllipse(brush, new Rectangle(2, 0, pixelSize - 2, pixelSize));
                        graphics.Save();
                        break;                        
                } // chiudo switch

            } // chiudo using (var graphics = Graphics.FromImage(bitmap))

            return bitmap;
        } // chiudo private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush)
    } // chiudo public class VLineRender
} // chiudo namespace QuickQRCodeRender.Renderers
