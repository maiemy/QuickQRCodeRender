using QRCoder;
using QRCoder.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace QuickQRCodeRender.MatrixConverters
{
    public class VLineConverter : IConvertToMatrixView
    {
        public enum Dots
        {
            White = 0,
            Fill = 1,
            Top = 2,
            Bottom = 3,
            Dotted = 4
        } // chiudo public enum Dots

        public int[,] ToMatrixView(QRCodeData qrData)
        {
            // la matrice ha la stessa dimensione della matrice di partenza sul qrData
            int[,] risultato = new int[qrData.ModuleMatrix.Count, qrData.ModuleMatrix.Count];
            
            for (int x =0; x < qrData.ModuleMatrix.Count; x++)
            {
                for (int y = 0; y < qrData.ModuleMatrix.Count; y++)
                {
                    int sopra = y-1 >=0 ? Convert.ToInt16(qrData.ModuleMatrix[x][y-1]) : 0;
                    int sotto = y+1 < qrData.ModuleMatrix.Count ? Convert.ToInt16(qrData.ModuleMatrix[x][y+1]) : 0;
                    int valorePosizione = Convert.ToInt16(qrData.ModuleMatrix[x][y]);

                    Dots valore = Dots.White;
                    // se il valore di posizione e' uno allora devo decidere come disegnarmi altrimenti rimango bianco
                    if (valorePosizione > 0)
                    {
                        valore = sopra > 0 && sotto > 0 ? Dots.Fill : valore;
                        valore = sopra <= 0 && sotto <= 0 ? Dots.Dotted : valore;
                        valore = sopra > 0 && sotto <= 0 ? Dots.Bottom : valore;
                        valore = sopra <= 0 && sotto > 0 ? Dots.Top : valore;
                    } // chiudo if (posizione > 0)

                    risultato[x, y] = (int) valore;
                } // for (int y = 0; y < qrData.ModuleMatrix.Count; y++)
            } // for (int x =0; x < qrData.ModuleMatrix.Count; x++)

            // restituisco il risultato
            return risultato;
        } // chiudo public int[,] ToMatrixView(QRCodeData qrData)
    } // chiudo public class VLineConverter
} // chiudo namespace QuickQRCodeRender.MatrixConverters
