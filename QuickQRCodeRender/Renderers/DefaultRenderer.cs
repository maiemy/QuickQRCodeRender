using QRCoder.Interfaces;
using QRCoder.Models;
using QuickQRCodeRender.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace QRCoder.Renderers
{

    public class DefaultRenderer : IDrawQRCode
    {
        // disegna il QRCode
        public Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options)
        {
            Bitmap risultato = null;

            RenderParametersUtility utils = new RenderParametersUtility(matrix, options);
            PathCleaner pathCleaner = new PathCleaner();


            // int blocchiPerLato = matrix.MatrixView.GetLength(0);
            int blocchiPerLato = utils.BorderNumModules;

            // calcolo la dimensione in pixel per ogni blocco della matrice
            // int pixelSize = Convert.ToInt16(Math.Round( (decimal)options.BoxSize / (decimal)blocchiPerLato) );
            int pixelSize = utils.SingleModulePixel;

            // creo una matrice di lavoro.
            int[,] matrixLavoro = matrix.MatrixView;

            // sbianco i riquadri dei filler
            pathCleaner.ClearAllFinderArea(matrixLavoro, utils.FinderPositionTopLeft, utils.FinderPositionTopRight, utils.FinderPositionBottom);

            // se abbiamo il logo dobbiamo sbiancare la parte centrale del quadrato.
            if(options.Logo != null) {
                
                pathCleaner.ClearLogoArea(matrixLavoro, utils);

            } // chiudo if(options.Logo != null) { 

            
            
            // int size = blocchiPerLato * pixelSize;
            int size = utils.BorderPixel;
            Bitmap bitmap = new Bitmap(size, size);

            Bitmap darkModulePixel = MakeDotPixel(pixelSize, new SolidBrush( options.DarkColor) );
            Bitmap lightModulePixel = MakeDotPixel(pixelSize, new SolidBrush( options.LightColor) );

            using (Graphics graphics = Graphics.FromImage(bitmap))
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
                                Bitmap bDraw = matrixLavoro[x,y] == 1 ? darkModulePixel : lightModulePixel;
                                graphics.DrawImage(bDraw, new Point(x * pixelSize, y * pixelSize));
                            } // chiudo for (var y = 0; y < blocchiPerLato; y++)
                        } // chiudo for (var x = 0; x < blocchiPerLato; x++)

                        graphics.Save();
                    } // chiudo using (var darkBrush = new SolidBrush(options.DarkColor))
                } // chiudo using (var lightBrush = new SolidBrush(options.LightColor))
            } // chiudo using (var graphics = Graphics.FromImage(bitmap))

            // qui inseriamo il logo


            // se mi e' stato chiesto il bordo 
            if(options.DrawQuietZones)
            {
                // considero 4 blocchi come spazio di sicurezza                
                int offset = 4 * pixelSize;
                int qSize = size + (offset * 2);
                Bitmap bitmapZones = new Bitmap(qSize, qSize);

                using (Graphics graphics = Graphics.FromImage(bitmapZones))
                {
                    // disegno il rettangolo del background
                    using (var brush = new SolidBrush(options.BackgroundColor))
                        graphics.FillRectangle(brush, new Rectangle(0, 0, qSize, qSize));

                    // gli disegno dentro il qr creato sopra
                    graphics.DrawImage(bitmap, new Point(offset, offset));
                    graphics.Save();
                } // chiudo using (Graphics graphics = Graphics.FromImage(bitmapZones))                
                // ridimensiono alla richiesta delle opzioni.
                bitmap = Resize(bitmapZones, options.BoxSize);                
            } // chiudo if(options.DrawQuietZones)

            risultato = bitmap;

            return risultato;
        } // chiudo public Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options)


        private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush)
        {
            // draw a dot
            var bitmap = new Bitmap(pixelSize, pixelSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(brush, new Rectangle(0, 0, pixelSize, pixelSize));
                graphics.Save();
            } // chiudo using (var graphics = Graphics.FromImage(bitmap))

            return bitmap;
        } // chiudo private Bitmap MakeDotPixel(int pixelSize, SolidBrush brush)

        private Bitmap Resize(Bitmap image, int newSize)
        {
            if (image == null) return null;

            float scale = Math.Min((float)newSize / image.Width, (float)newSize / image.Height);
            var scaledWidth = (int)(image.Width * scale);
            var scaledHeight = (int)(image.Height * scale);
            var offsetX = (newSize - scaledWidth) / 2;
            var offsetY = (newSize - scaledHeight) / 2;

            var scaledImage = new Bitmap(image, new Size(scaledWidth, scaledHeight));

            var bm = new Bitmap(newSize, newSize);

            using (Graphics graphics = Graphics.FromImage(bm))
            {
                using (var brush = new SolidBrush(Color.Transparent))
                {
                    graphics.FillRectangle(brush, new Rectangle(0, 0, newSize, newSize));

                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    graphics.DrawImage(scaledImage, new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight));
                    graphics.Save();
                }
            }
            return bm;
        } // chiudo private Bitmap Resize(Bitmap image, int newSize)

    } // chiudo public class DefaultRenderer

} // chiudo namespace QRCoder.Renderers
