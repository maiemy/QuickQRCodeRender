using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QuickQRCodeRender.Utility
{
    public class PathCleaner
    {
        public void ClearLogoArea(int[,] matrix, RenderParametersUtility utils)
        {

            //// riproporziono i blocchi alla percentuale richiesta nelle options per il logo
            //int whiteBlock = options.BoxSize * options.LogoSizePercent / 100;
            //// calcolo i blocchi
            //whiteBlock = whiteBlock / pixelSize;

            int whiteBlock = utils.LogoBorderWhiteModules;

            // adesso sbianco nella matrice
            // calcolo la posizione X del quadrato "bianco" per il logo.
            //int x = Convert.ToInt32(Math.Round( (blocchiPerLato - whiteBlock) / 2d ));
            int x = utils.LogoPositionOffset;

            CleanBox(matrix, x, x, whiteBlock);
            
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
                    matrix[i + x, j + x] = 0;
                } // chiudo for (int j = 0; j < whiteBlock; j++)
            } // chiudo for( int i = 0; i < whiteBlock; i++ )
        }
    }
}
