using QRCoder.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace QRCoder.Interfaces
{
    public interface IDrawQRCode
    {
        Bitmap DrawQRCode(RenderMatrix matrix, RenderMatrixOptions options);
    } // chiudo public interface IDrawQRCode
} // chiudo namespace QRCoder.Interfaces
