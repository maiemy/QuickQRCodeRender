using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QRCoder.Models
{
    public class RenderMatrixOptions
    {
        private int _quietZoneModules;

        public int BoxSize { get; set; } = 1024;

        // GetGraphic(int pixelsPerModule,
        public Color DarkColor { get; set; } = Color.Black;
        public Color LightColor { get; set; } = Color.Transparent;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Bitmap Logo { get; set; } = null;
        public int LogoSizePercent { get; set; } = 15;
        public bool DrawQuietZones { get; set; } = true;
        
        /// <summary>
        /// Range Accepted: 0 -> 4
        /// </summary>
        public int QuietZoneNumModules
        {
            get
            {
                return _quietZoneModules;
            }
            set
            {
                if (value == null)
                    _quietZoneModules = 4;
                _quietZoneModules = value > 4 ? 4 : value;
            }
        }
        public Bitmap FinderPatternImage { get; set; } = null;

        /// <summary>
        /// Da utilizzare con il fantasy renderer per impostare un'immagine da inserire in ogni blocco del QR Code
        /// </summary>
        public Bitmap FantasyRenderImage { get; set; } = null;
        public bool RotateFinderImage { get; set; } = true;

        public RenderMatrixOptions()
        {            
        } // chiudo public RenderMatrixOptions()
    } // chiudo public class RenderMatrixOptions

} // chiudo namespace QRCoder.Models
