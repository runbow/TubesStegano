using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesStegano
{
    class SteganoFourPixelDiff
    {
        public int treshold = 8;
        public int kl = 1; //Number of bit subsitution when 'smooth'
        public int kh = 4; //Number of bit subsitution when 'rough'

        private Bitmap cover;
        private Random numGen;
        private string message;

        public double CalculateD(int y1, int y2, int y3, int y4)
        {
            int ymin = Math.Min(y4, Math.Min(y3, Math.Min(y1, y2)));
            double d;
            int sum = y1 + y2 + y3 + y4 - 4 * ymin;
            d = sum / 3;
            return d;
        }

        public void setCoverObject(Bitmap bmp)
        {
            cover = bmp;
        }

        public void setKey(String key)
        {
            int hash = 7;
            for (int i = 0; i < key.Length; i++)
            {
                hash = hash * 31 + key[i] % 10000;
            }
            numGen = new Random(hash);
        }

        public void setMessage(string text)
        {
            message = text;
        }

        public int getMaxMsgSize()
        {
            if (cover == null) { return 0; }
            else
            {
                int sum = 0;
                for (int i = 0; i < cover.Height - 2; i = i + 2)
                { //getting 4 block at a time
                    for (int j = 0; j < cover.Width - 2; j = j + 2)
                    {
                        Color pixel1 = cover.GetPixel(j, i);
                        Color pixel2 = cover.GetPixel(j, i + 1);
                        Color pixel3 = cover.GetPixel(j + 1, i);
                        Color pixel4 = cover.GetPixel(j + 1, i + 1);

                        int R1, R2, R3, R4;
                        R1 = pixel1.R;
                        R2 = pixel2.R;
                        R3 = pixel3.R;
                        R4 = pixel4.R;

                        double d = CalculateD(R1, R2, R3, R4);
                        if (d <= treshold)
                        {
                            int vmax = Math.Max(R1, R2);
                            vmax = Math.Max(R3, vmax);
                            vmax = Math.Max(R4, vmax);

                            int vmin = Math.Min(R1, R2);
                            vmin = Math.Min(R3, vmin);
                            vmin = Math.Min(R4, vmin);
                            if ((vmax - vmin) > (2 * treshold + 2))
                            {// error block {

                            }
                            else
                            {
                                sum += 4 * kl;
                            }
                        }
                        else
                        {
                            sum += 4 * kh;
                        }

                        int G1, G2, G3, G4;
                        G1 = pixel1.G;
                        G2 = pixel2.G;
                        G3 = pixel3.G;
                        G4 = pixel4.G;

                        d = CalculateD(G1, G2, G3, G4);
                        if (d <= treshold)
                        {
                            int vmax = Math.Max(G1, G2);
                            vmax = Math.Max(G3, vmax);
                            vmax = Math.Max(G4, vmax);

                            int vmin = Math.Min(G1, G2);
                            vmin = Math.Min(G3, vmin);
                            vmin = Math.Min(G4, vmin);
                            if ((vmax - vmin) > (2 * treshold + 2))
                            {// error block {

                            }
                            else
                            {
                                sum += 4 * kl;
                            }
                        }
                        else
                        {
                            sum += 4 * kh;
                        }

                        int B1, B2, B3, B4;
                        B1 = pixel1.B;
                        B2 = pixel2.B;
                        B3 = pixel3.B;
                        B4 = pixel4.B;

                        d = CalculateD(B1, B2, B3, B4);
                        if (d <= treshold)
                        {
                            int vmax = Math.Max(B1, B2);
                            vmax = Math.Max(B3, vmax);
                            vmax = Math.Max(B4, vmax);

                            int vmin = Math.Min(B1, B2);
                            vmin = Math.Min(B3, vmin);
                            vmin = Math.Min(B4, vmin);
                            if ((vmax - vmin) > (2 * treshold + 2))
                            {// error block {

                            }
                            else
                            {
                                sum += 4 * kl;
                            }
                        }
                        else
                        {
                            sum += 4 * kh;
                        }

                    }
                }

                return sum;
            }
        }

        public enum State
        {
            Hiding,
            Filling_With_Zeros
        };

        public Bitmap embedText()
        {
            State state = State.Hiding;
            int charIndex = 0;
            int charValue = 0;
            int zeros = 0;
            int pixelElementIndex = 0;

            int k;

            for (int i = 0; i < cover.Height -2; i += 2)
            {
                for (int j = 0; j < cover.Width -2; j += 2)
                {
                    if ((i % 2 == 0) && (j % 2 == 0) && ((i + 1) < cover.Width) && ((j + 1) < cover.Height))
                    {
                        Color pixel1 = cover.GetPixel(j, i);
                        Color pixel2 = cover.GetPixel(j, i + 1);
                        Color pixel3 = cover.GetPixel(j + 1, i);
                        Color pixel4 = cover.GetPixel(j + 1, i + 1);

                        int R1, R2, R3, R4;
                        R1 = pixel1.R;
                        R2 = pixel2.R;
                        R3 = pixel3.R;
                        R4 = pixel4.R;

                        double d = CalculateD(R1, R2, R3, R4);
                        if (d <= treshold)
                        {
                           k = kl;
                        }
                        else
                        {
                            k = kh;
                        }
                         int vmax = Math.Max(R1, R2);
                         vmax = Math.Max(R3, vmax);
                         vmax = Math.Max(R4, vmax);

                         int vmin = Math.Min(R1, R2);
                         vmin = Math.Min(R3, vmin);
                         vmin = Math.Min(R4, vmin);
                         if ((vmax - vmin) > (2 * treshold + 2))
                         {// error block {

                         }
                         else
                         {
                             for (int m = i; m < i + 2; m++)
                                {
                                    for (int n = j; n < j + 2; n++)
                                    {
                                        Color pixel = cover.GetPixel(m, n);
                                        int R;
                                        R = pixel.R - pixel.R % (int)Math.Pow(2, k);

                                        if (pixelElementIndex % 8 == 0)
                                        {
                                            if (charIndex >= message.Length)
                                            {
                                                state = State.Filling_With_Zeros;
                                            }
                                            else
                                            {
                                                charValue = message[charIndex++];
                                            }
                                        }

                                       

                                        if (state == State.Hiding)
                                        {
                                            R += charValue % (int)Math.Pow(2, k);

                                            charValue /= (int)Math.Pow(2, k);
                                        }

                                        cover.SetPixel(m, n, Color.FromArgb(R, pixel.G, pixel.B));
                                        pixelElementIndex += k;
                                        if (state == State.Filling_With_Zeros)
                                        {
                                            zeros++;
                                        }
                                        
                                    }
                                }
                         }
                    }
                }
            }
            return cover;
        }

        public String extractText(Bitmap cover)
        {
            string extractedText = String.Empty;
            int k;
            for (int i = 0; i < cover.Width; i += 2)
            {
                for (int j = 0; j < cover.Height; j += 2)
                {
                    if ((i % 2 == 0) && (j % 2 == 0) && ((i + 1) < cover.Width) && ((j + 1) < cover.Height))
                    {
                        Color pixel1 = cover.GetPixel(j, i);
                        Color pixel2 = cover.GetPixel(j, i + 1);
                        Color pixel3 = cover.GetPixel(j + 1, i);
                        Color pixel4 = cover.GetPixel(j + 1, i + 1);

                        int R1, R2, R3, R4;
                        R1 = pixel1.R;
                        R2 = pixel2.R;
                        R3 = pixel3.R;
                        R4 = pixel4.R;

                        double d = CalculateD(R1, R2, R3, R4);
                        if (d <= treshold)
                        {
                            k = kl;
                        }
                        else
                        {
                            k = kh;
                        }
                        int vmax = Math.Max(R1, R2);
                        vmax = Math.Max(R3, vmax);
                        vmax = Math.Max(R4, vmax);

                        int vmin = Math.Min(R1, R2);
                        vmin = Math.Min(R3, vmin);
                        vmin = Math.Min(R4, vmin);
                        if ((vmax - vmin) > (2 * treshold + 2))
                        {// error block {

                        }
                        else
                        {
                            //ekstrak
                        }
                    }
                }
            }
                return extractedText;
        }
    }
}
