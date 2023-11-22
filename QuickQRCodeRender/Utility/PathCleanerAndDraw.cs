using QRCoder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace QuickQRCodeRender.Utility
{
    internal class PathCleanerAndDraw
    {
        public void ClearLogoArea(int[,] matrix, int nrWhiteBlockSide, int logoOffset)
        {
            // adesso sbianco nella matrice
            CleanBox(matrix, logoOffset, logoOffset, nrWhiteBlockSide);
            
        } // chiudo public void ClearLogoArea(int[,] matrix, RenderParametersUtility utils)

        public void ClearAllFinderArea(int[,] matrix, Point TopLeftFinder, Point TopRightFinder, Point BottomFinder)
        {
            CleanBox(matrix, TopLeftFinder.X, TopLeftFinder.Y, 7);
            CleanBox(matrix, TopRightFinder.X, TopRightFinder.Y, 7);
            CleanBox(matrix, BottomFinder.X, BottomFinder.Y, 7);

        } // chiudo public void ClearAllFinderArea(bool drawQuietZones, int quietZoneNumModules = 4)

        private void CleanBox(int[,] matrix, int x, int y, int whiteBlock)
        {
            for (int i = 0; i < whiteBlock; i++)
            {
                for (int j = 0; j < whiteBlock; j++)
                {
                    matrix[i + x, j + y] = 0;
                } // chiudo for (int j = 0; j < whiteBlock; j++)
            } // chiudo for( int i = 0; i < whiteBlock; i++ )
        } // chiudo private void CleanBox(int[,] matrix, int x, int y, int whiteBlock)

        public Bitmap DrawLogoAndFinder(Bitmap sourceImage, RenderParametersUtility utils, bool rotateFinder = true) {

            Bitmap risultato = sourceImage;

            // disegno il logo solo se e' presente.
            if (utils.LogoImage != null)
            {
                // trovo il posizionamento del logo dall'util. 
                // ho deciso di tenere sempre un 1% di margine nello spazio del logo per staccare dal moduli del qrcode
                // int posxLogo = utils.LogoModulesOffset * utils.ModuleSize;
                int posxLogo = utils.LogoPositionOffset * utils.SingleModulePixel;
                // adesso calcolo quando lasciare da questa posizione per avere un margine in px dell'1%
                int offsetBoxLogo = Convert.ToInt32(Math.Round((utils.LogoBorderWhiteModules * utils.SingleModulePixel) * 0.01f / 2));
                // adesso faccio il resize del logo 
                Bitmap logoRimensione = Resize(utils.LogoImage, Convert.ToInt32(Math.Round((utils.LogoBorderWhiteModules * utils.SingleModulePixel) * 0.99f)));
                // gli disegno dentro il qr creato sopra
                using (Graphics graphics = Graphics.FromImage(risultato))
                {
                    graphics.DrawImage(logoRimensione, new Point(posxLogo + offsetBoxLogo, posxLogo + offsetBoxLogo));
                    graphics.Save();
                } // chiudo using (Graphics graphics = Graphics.FromImage(bitmapZones))
            } // chiudo logo != null

            // disegno i finder se li ho
            if(utils.FinderPatternImage != null) {
                // facciamo il resize dell'immagine
                Bitmap finder = Resize(utils.FinderPatternImage, 7 * utils.SingleModulePixel);
                using (Graphics graphics = Graphics.FromImage(risultato))
                {
                    graphics.DrawImage(finder, new Point(utils.FinderPositionTopLeft.X * utils.SingleModulePixel, utils.FinderPositionTopLeft.Y * utils.SingleModulePixel));

                    if(rotateFinder)
                        finder.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    graphics.DrawImage(finder, new Point(utils.FinderPositionTopRight.X * utils.SingleModulePixel, utils.FinderPositionTopRight.Y * utils.SingleModulePixel));

                    if(rotateFinder)
                        finder.RotateFlip(RotateFlipType.RotateNoneFlipXY);

                    graphics.DrawImage(finder, new Point(utils.FinderPositionBottom.X * utils.SingleModulePixel, utils.FinderPositionBottom.Y * utils.SingleModulePixel));

                    graphics.Save();
                } // chiudo using (Graphics graphics = Graphics.FromImage(bitmapZones))                                

            } // chiudo if(utils.FinderPatternImage != null) { 

            return risultato;
        } // chiudo public Bitmap DrawImage(Bitmap destBitmap, Bitmap sourceBitmap, int x, int y) { 

        public Bitmap Resize(Bitmap image, int newSize)
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
    }
}
