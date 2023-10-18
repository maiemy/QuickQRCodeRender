using QRCoder.Interfaces;
using QRCoder.MatrixConverters;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCoder.Models
{
    public class RenderMatrix
    {
        private QRCodeData _qrData;
        IConvertToMatrixView _converterView = null;
        int[,] _matrixView = null;

        // restituisco il matrixview dalla conversione della matrice di partenza
        public int[,] MatrixView { 
            get
            {
                // singleton per calcolarlo una sola volta
                if (_matrixView == null)
                    _matrixView = _converterView.ToMatrixView(_qrData);
                return _matrixView;
            }
        }        

        // Una volta istanziata la classe non e' possibile modificare i dati passati nel costruttore.
        public RenderMatrix(QRCodeData qrData, IConvertToMatrixView converterMatrix = null)
        {
            // se non ho un converter uso quello di default della libreria
            _converterView = converterMatrix ?? new DefaultConverter();
            _qrData = qrData;
        } // chiudo public RenderMatrix(int[,] matrix)

    } // chiudo public class RenderMatrix

} // chiudo namespace QRCoder.Models
