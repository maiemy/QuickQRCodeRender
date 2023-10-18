using QRCoder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRCoder.MatrixConverters
{
    internal class DefaultConverter : IConvertToMatrixView
    {

        public int[,] ToMatrixView(QRCodeData qrData)
        {
            // la matrice ha la stessa dimensione della matrice di partenza sul qrData
            int[,] risultato = new int[qrData.ModuleMatrix.Count, qrData.ModuleMatrix.Count];

            int pos = 0;
            qrData.ModuleMatrix.ForEach(
                    riga =>
                    {
                        for (int i = 0; i < riga.Length; i++) {
                            risultato[pos, i] = Convert.ToInt16(riga[i]);
                        } // chiudo for(int i = 0; i< riga.Length; i++) { 
                        pos++;
                    } // chiudo riga =>                    
                ); // chiudo foreach

            // restituisco il risultato
            return risultato;
        } // chiudo public int[,] ToMatrixView(QRCodeData qrData)

    } // chiudo internal class DefaultConverter : IConvertToMatrixView
} // chiudo namespace QRCoder.MatrixConverters
