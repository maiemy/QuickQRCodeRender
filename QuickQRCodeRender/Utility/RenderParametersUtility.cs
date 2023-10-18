using QRCoder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace QuickQRCodeRender.Utility
{
    public class RenderParametersUtility
    {
        RenderMatrix _matrix;
        RenderMatrixOptions _options;

        public int BorderPixel
        {
            get { return BorderNumModules * SingleModulePixel; }
        } // chiudo public int BorderPixel

        public int BorderNumModules
        {
            get { return _matrix.MatrixView.GetLength(0); }
        } // chiudo public int BorderNumModules

        public int SingleModulePixel
        {
            get { return Convert.ToInt16(Math.Round((decimal)_options.BoxSize / (decimal)BorderNumModules)); }
        } // chiudo public int SingleModulePixel

        public int LogoBorderWhiteModules
        {
            get
            {
                // trasformo la dimensione del logo da px a blocchi
                // ragiono sul lato lungo dell'immagine.
                //int logoWidth = Math.Max(_options.Logo.Width, _options.Logo.Height);

                // riproporziono i blocchi alla percentuale richiesta nelle options per il logo
                int whiteBlock = _options.BoxSize * _options.LogoSizePercent / 100;
                // calcolo i blocchi
                whiteBlock = whiteBlock / SingleModulePixel;

                return whiteBlock;
            }
        } // chiudo public int LogoBorderWhiteModules

        public int LogoPositionOffset
        {
            get
            {
                return Convert.ToInt32(Math.Round((BorderNumModules - LogoBorderWhiteModules) / 2d));
            }
        } // chiudo public int LogoPositionOffset

        public Point FinderPositionTopLeft
        {
            get
            {
                int QuietZoneOffset = _options.DrawQuietZones ? _options.QuietZoneNumModules : 0;

                return new Point(0 + QuietZoneOffset, 0 + QuietZoneOffset);
            }
        } // chiudo public Point FinderPositionTopLeft
        public Point FinderPositionTopRight
        {
            get
            {
                int QuietZoneOffset = _options.DrawQuietZones ? _options.QuietZoneNumModules : 0;

                // 7 è la grandezza standard del Finder
                return new Point(BorderNumModules - 7 - QuietZoneOffset, 0 + QuietZoneOffset);
            }
        } // chiudo public Point FinderPositionTopRight
        public Point FinderPositionBottom
        {
            get
            {
                int QuietZoneOffset = _options.DrawQuietZones ? _options.QuietZoneNumModules : 0;

                // 7 è la grandezza standard del Finder
                return new Point(0 + QuietZoneOffset, BorderNumModules - 7 - QuietZoneOffset);
            }
        } // chiudo public Point FinderPositionBottom

        public Bitmap LogoImage
        {
            get
            {
                return _options.Logo;
            }
        } // chiudo public Bitmap Logo

        public Bitmap FinderPatternImage
        {
            get
            {
                return _options.FinderPatternImage;
            }
        } // chiudo public Bitmap Logo

        public RenderParametersUtility(RenderMatrix matrix, RenderMatrixOptions opts)
        {
            _matrix = matrix;
            _options = opts;
        } // chiudo public RenderParametersUtility(RenderMatrix matrix, RenderMatrixOptions opts)

    } // chiudo public class RenderParametersUtility
} // chiudo namespace QuickQRCodeRender.Utility
