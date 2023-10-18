using QRCoder.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace QuickQRCodeRender.Utility
{
    public class RenderParametersUtility
    {
        RenderMatrix _matrix;
        RenderMatrixOptions _options;

        public int TotalPixelSide
        {
            get { return ModuleSize * ModuleSize; }
        } // chiudo public int BlocchiPerLato


        public int ModulesForSide
        {
            get { return _matrix.MatrixView.GetLength(0); }
        } // chiudo public int BlocchiPerLato

        public int ModuleSize
        {
            get { return Convert.ToInt16(Math.Round((decimal)_options.BoxSize / (decimal)ModulesForSide)); }
        } // chiudo public int ModuleSize

        public int LogoWhiteModulesSide
        {
            get {
                // trasformo la dimensione del logo da px a blocchi
                // ragiono sul lato lungo dell'immagine.
                int logoWidth = Math.Max(_options.Logo.Width, _options.Logo.Height);

                // riproporziono i blocchi alla percentuale richiesta nelle options per il logo
                int whiteBlock = _options.BoxSize * _options.LogoSizePercent / 100;
                // calcolo i blocchi
                whiteBlock = whiteBlock / ModuleSize;

                return whiteBlock;
            }
        } // chiudo public int LogoWhiteModulesSide

        public int LogoModulesOffset
        {
            get
            {                
                return Convert.ToInt32(Math.Round((ModulesForSide - LogoWhiteModulesSide) / 2d));
            }
        } // chiudo public int BlocchiPerLato

        public RenderParametersUtility(RenderMatrix matrix, RenderMatrixOptions opts)
        {
            _matrix = matrix;
            _options = opts;
        } // chiudo public RenderParametersUtility(RenderMatrix matrix, RenderMatrixOptions opts)

    } // chiudo public class RenderParametersUtility
} // chiudo namespace QuickQRCodeRender.Utility
