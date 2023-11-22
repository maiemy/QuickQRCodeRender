using QRCoder;
using QRCoder.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuickQRCodeRender.MatrixConverters
{
    public class HLineConverter : IConvertToMatrixView
    {
        public enum Dots
        {
            White = 0,
            Fill = 1,
            Sx = 2,
            Dx = 3,
            Dotted = 4
        } // chiudo public enum Dots

        public int[,] ToMatrixView(QRCodeData qrData)
        {
            // la matrice ha la stessa dimensione della matrice di partenza sul qrData 
            int[,] risultato = new int[qrData.ModuleMatrix.Count, qrData.ModuleMatrix.Count];

            for (int x = 0; x < qrData.ModuleMatrix.Count; x++)
            {
                for (int y = 0; y < qrData.ModuleMatrix.Count; y++)
                {
                    int sinistra = x - 1 >= 0 ? Convert.ToInt16(qrData.ModuleMatrix[x - 1][y]) : 0;
                    int destra = x + 1 < qrData.ModuleMatrix.Count ? Convert.ToInt16(qrData.ModuleMatrix[x+1][y]) : 0;
                    int valorePosizione = Convert.ToInt16(qrData.ModuleMatrix[x][y]);

                    Dots valore = Dots.White;
                    // se il valore di posizione e' uno allora devo decidere come disegnarmi altrimenti rimango bianco
                    if (valorePosizione > 0)
                    {
                        valore = sinistra > 0 && destra > 0 ? Dots.Fill : valore;
                        valore = sinistra <= 0 && destra <= 0 ? Dots.Dotted : valore;
                        valore = sinistra > 0 && destra <= 0 ? Dots.Dx : valore;
                        valore = sinistra <= 0 && destra > 0 ? Dots.Sx : valore;
                    } // chiudo if (posizione > 0)

                    risultato[x, y] = (int)valore;
                } // for (int y = 0; y < qrData.ModuleMatrix.Count; y++)
            } // for (int x =0; x < qrData.ModuleMatrix.Count; x++)

            // restituisco il risultato
            return risultato;
        } // chiudo public int[,] ToMatrixView(QRCodeData qrData)
    } // chiudo public class HLineConverter : IConvertToMatrixView
} // chiudo namespace QuickQRCodeRender.MatrixConverters
