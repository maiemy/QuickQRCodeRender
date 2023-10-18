using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QRCoder.Models
{
    public class RenderMatrixOptions
    {
        public int BoxSize { get; set; } = 1024;

        // GetGraphic(int pixelsPerModule,
        public Color DarkColor { get; set; } = Color.Black;
        public Color LightColor { get; set; } = Color.White;
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Bitmap Logo { get; set; } = null;
        public int LogoSizePercent { get; set; } = 15;
        public bool DrawQuietZones { get; set; } = true;
        public int QuietZoneNumModules { get; set; } = 4;
        public Bitmap FinderPatternImage { get; set; } = null;

        public RenderMatrixOptions()
        {            
        } // chiudo public RenderMatrixOptions()
    } // chiudo public class RenderMatrixOptions

} // chiudo namespace QRCoder.Models
