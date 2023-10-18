using QRCoder.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCoder.Interfaces
{
    public interface IConvertToMatrixView
    {
        int[,] ToMatrixView(QRCodeData qrData);
    } // chiudo interface IConvertToMatrixView
} // chiudo namespace QRCoder.Interfaces
