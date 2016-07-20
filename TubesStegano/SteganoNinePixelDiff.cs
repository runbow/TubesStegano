using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesStegano
{
    class SteganoNinePixelDiff
    {
        private Bitmap bmp;
        private string message;

        public enum State
        {
            Hiding,
            Filling_With_Zeros
        };

        public void setImage(Bitmap b1)
        {
            bmp = b1;
        }

        public void setMessage(string teks)
        {
            message = teks;
        }

        public Bitmap embedText()
        {
            State state = State.Hiding;
            int charIndex = 0;
            int charValue = 0;
            int buffer;
            int zeros = 0;
            int[] R = new int[9];
            int[] G = new int[9];
            int[] B = new int[9];

            double dR, dG, dB;
            int grupR, grupG, grupB, nLSBR, nLSBG, nLSBB;

            charValue = message[charIndex++];

            for (int i = 0; i < bmp.Height; i += 3)
            {
                for (int j = 0; j < bmp.Width; j += 3)
                {
                    if ((i % 3 == 0) && (j % 3 == 0) && ((i + 2) < bmp.Height) && ((j + 2) < bmp.Width))
                    {
                        dR = getPVDofR(j, i);
                        grupR = group((int)dR);
                        nLSBR = nSubstitutionLSB(grupR);
                        if (nLSBR == 3) { nLSBR = 2; grupR = 0; }
                        if (nLSBR == 5) { nLSBR = 4; grupR = 2; }

                        dG = getPVDofG(j, i);
                        grupG = group((int)dG);
                        nLSBG = nSubstitutionLSB(grupG);
                        if (nLSBG == 3) { nLSBG = 2; grupG = 0; }
                        if (nLSBG == 5) { nLSBG = 4; grupG = 2; } 

                        dB = getPVDofB(j, i);
                        grupB = group((int)dB);
                        nLSBB = nSubstitutionLSB(grupB);
                        if (nLSBB == 3) { nLSBB = 2; grupB = 0; }
                        if (nLSBB == 5) { nLSBB = 4; grupB = 2; } 

                        int counter = 0;

                        for (int m = i; m < i + 3; m++)
                        {
                            for (int n = j; n < j + 3; n++)
                            {
                                Color pixel = bmp.GetPixel(n, m);

                                // bersihkan RGB sebanyak nLSB dan simpan di array
                                R[counter] = pixel.R - (pixel.R % (int)Math.Pow(2, nLSBR));
                                G[counter] = pixel.G - (pixel.G % (int)Math.Pow(2, nLSBG));
                                B[counter] = pixel.B - (pixel.B % (int)Math.Pow(2, nLSBB));

                                counter++;
                            }
                        }

                        switch (grupR)
                        {
                            case 0:
                                {
                                    R[8] += 0;
                                } break;
                            case 1:
                                {
                                    R[8] += 1;
                                } break;
                            case 2:
                                {
                                    R[8] += 2;
                                } break;
                            case 3:
                                {
                                    R[8] += 3;
                                } break;
                        }

                        switch (grupG)
                        {
                            case 0:
                                {
                                    G[8] += 0;
                                } break;
                            case 1:
                                {
                                    G[8] += 1;
                                } break;
                            case 2:
                                {
                                    G[8] += 2;
                                } break;
                            case 3:
                                {
                                    G[8] += 3;
                                } break;
                        }

                        switch (grupB)
                        {
                            case 0:
                                {
                                    B[8] += 0;
                                } break;
                            case 1:
                                {
                                    B[8] += 1;
                                } break;
                            case 2:
                                {
                                    B[8] += 2;
                                } break;
                            case 3:
                                {
                                    B[8] += 3;
                                } break;
                        }

                       
                        // proses komponen R terlebih dahulu
                        for (int m = 0; m < 8; m++)
                        {
                            if (state == State.Filling_With_Zeros)
                            {
                                if (zeros < 8)
                                {
                                    R[m] += 0;
                                    zeros++;
                                }
                            }
                            else
                            {
                                switch (nLSBR)
                                {
                                    case 2:
                                        {
                                            R[m] += (charValue % 4);
                                            charValue /= 4;
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            if (m == 2)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                R[m] += (cbuff << 2) + buff;
                                            }
                                            else if (m == 5)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                R[m] += (cbuff << 1) + buff;
                                            }
                                            else
                                            {
                                                R[m] += (charValue % 8);
                                                charValue /= 8;
                                            }
                                        } break;
                                    case 4:
                                        {
                                            R[m] += (charValue % 16);
                                            charValue /= 16;
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            if (m == 1)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                R[m] += (cbuff << 3) + buff;
                                            }
                                            else if (m == 3)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 16;
                                                charValue /= 16;
                                                R[m] += (cbuff << 1) + buff;
                                            }
                                            else if (m == 4)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                R[m] += (cbuff << 4) + buff;
                                            }
                                            else if (m == 6)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 8;
                                                charValue /= 8;
                                                R[m] += (cbuff << 2) + buff;
                                            }
                                            else
                                            {
                                                R[m] += (charValue % 32);
                                                charValue /= 32;
                                            }
                                        } break;
                                }
                            }
                            
                        }
                                                                                                
                        // proses komponen G terlebih dahulu
                        for (int m = 0; m < 8; m++)
                        {
                            if (state == State.Filling_With_Zeros)
                            {
                                if (zeros < 8)
                                {
                                    G[m] += 0;
                                    zeros++;
                                }
                            }
                            else
                            {
                                switch (nLSBG)
                                {
                                    case 2:
                                        {
                                            G[m] += (charValue % 4);
                                            charValue /= 4;
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {
                                                        //
                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            if (m == 2)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                G[m] += (cbuff << 2) + buff;
                                            }
                                            else if (m == 5)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                G[m] += (cbuff << 1) + buff;
                                            }
                                            else
                                            {
                                                G[m] += (charValue % 8);
                                                charValue /= 8;
                                            }
                                        } break;
                                    case 4:
                                        {
                                            G[m] += (charValue % 16);
                                            charValue /= 16;
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            if (m == 1)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                G[m] += (cbuff << 3) + buff;
                                            }
                                            else if (m == 3)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 16;
                                                charValue /= 16;
                                                G[m] += (cbuff << 1) + buff;
                                            }
                                            else if (m == 4)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                G[m] += (cbuff << 4) + buff;
                                            }
                                            else if (m == 6)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 8;
                                                charValue /= 8;
                                                G[m] += (cbuff << 2) + buff;
                                            }
                                            else
                                            {
                                                G[m] += (charValue % 32);
                                                charValue /= 32;
                                            }
                                        } break;
                                }
                            }
                            
                        }

                        
                        for (int m = 0; m < 8; m++)
                        {
                            if (state == State.Filling_With_Zeros)
                            {
                                if (zeros < 8)
                                {
                                    B[m] += 0;
                                    zeros++;
                                }
                            }
                            else
                            {
                                switch (nLSBB)
                                {
                                    case 2:
                                        {
                                            B[m] += (charValue % 4);
                                            charValue /= 4;
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            if (m == 2)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                B[m] += (cbuff << 2) + buff;
                                            }
                                            else if (m == 5)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                B[m] += (cbuff << 1) + buff;
                                            }
                                            else
                                            {
                                                B[m] += (charValue % 8);
                                                charValue /= 8;
                                            }
                                        } break;
                                    case 4:
                                        {
                                            B[m] += (charValue % 16);
                                            charValue /= 16;
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            if (m == 1)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 4;
                                                charValue /= 4;
                                                B[m] += (cbuff << 3) + buff;
                                            }
                                            else if (m == 3)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 16;
                                                charValue /= 16;
                                                B[m] += (cbuff << 1) + buff;
                                            }
                                            else if (m == 4)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 2;
                                                charValue /= 2;
                                                B[m] += (cbuff << 4) + buff;
                                            }
                                            else if (m == 6)
                                            {
                                                int buff = charValue;
                                                if (charIndex < message.Length)
                                                {
                                                    charValue = message[charIndex++];
                                                }
                                                else
                                                {
                                                    if (state == State.Filling_With_Zeros)
                                                    {

                                                    }
                                                    else
                                                    {
                                                        state = State.Filling_With_Zeros;
                                                    }
                                                }
                                                int cbuff = charValue % 8;
                                                charValue /= 8;
                                                B[m] += (cbuff << 2) + buff;
                                            }
                                            else
                                            {
                                                B[m] += (charValue % 32);
                                                charValue /= 32;
                                            }
                                        } break;
                                }
                            }
                            
                        }

                        // adjusment


                        // setPixel
                        counter = 0;
                        for (int m = i; m < i+3; m++)
                        {
                            for (int n = j; n < j+3; n++)
                            {
                                bmp.SetPixel(n, m, Color.FromArgb(R[counter], G[counter], B[counter]));
                                counter++;
                            }
                        }
                        Console.WriteLine();

                        if (zeros == 8)
                        {
                            break;
                        }

                    }
                }
            }

            return bmp;
        }

        public string extractText(Bitmap cover)
        {
            int charValue = 0;
            string extractedText = string.Empty;
            int buffer;
            Boolean finish = false;

            int[] R = new int[9];
            int[] G = new int[9];
            int[] B = new int[9];

            int grupR, grupG, grupB, nLSBR, nLSBG, nLSBB;

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    if ((i % 3 == 0) && (j % 3 == 0) && ((i + 2) < bmp.Height) && ((j + 2) < bmp.Width))
                    {
                        int counter = 0;

                        if (finish)
                        {
                            break;
                        }

                        for (int m = i; m < i + 3; m++)
                        {
                            for (int n = j; n < j + 3; n++)
                            {
                                Color pixel = bmp.GetPixel(n, m);

                                R[counter] = pixel.R;
                                G[counter] = pixel.G;
                                B[counter] = pixel.B;

                                counter++;
                            }
                        }

                        grupR = R[8] % 4;
                        grupG = G[8] % 4;
                        grupB = B[8] % 4;

                        nLSBR = nSubstitutionLSB(grupR);
                        nLSBG = nSubstitutionLSB(grupG);
                        nLSBB = nSubstitutionLSB(grupB);

                        // proses komponen R terlebih dahulu
                        for (int m = 0; m < 8; m++)
                        {
                            if (finish)
                            {

                            }
                            else
                            {
                                switch (nLSBR)
                                {
                                    case 2:
                                        {
                                            buffer = R[m] % 4;
                                            if (m % 4 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 1)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 2)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 3)
                                            {
                                                buffer = buffer << 6;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            buffer = R[m] % 8;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                int buf = R[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                int buf = R[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 6)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 5;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 4:
                                        {
                                            buffer = R[m] % 16;
                                            if (m % 2 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 2 == 1)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            buffer = R[m] % 32;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                int buf = R[m] % 8;
                                                buf = buf << 5;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                int buf = R[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                int buf = R[m] % 16;
                                                buf = buf << 4;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 6)
                                            {
                                                int buf = R[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                }
                            }
                            
                        }

                        // proses komponen G terlebih dahulu
                        for (int m = 0; m < 8; m++)
                        {
                            if (finish)
                            {

                            }
                            else
                            {
                                switch (nLSBG)
                                {
                                    case 2:
                                        {
                                            buffer = G[m] % 4;
                                            if (m % 4 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 1)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 2)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 3)
                                            {
                                                buffer = buffer << 6;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            buffer = G[m] % 8;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                int buf = G[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                int buf = G[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 6)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 5;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 4:
                                        {
                                            buffer = G[m] % 16;
                                            if (m % 2 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 2 == 1)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            buffer = G[m] % 32;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                int buf = G[m] % 8;
                                                buf = buf << 5;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                int buf = G[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                int buf = G[m] % 16;
                                                buf = buf << 4;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 6)
                                            {
                                                int buf = G[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                }
                            }
                            
                        }

                        // proses komponen B terlebih dahulu
                        for (int m = 0; m < 8; m++)
                        {
                            if (finish)
                            {

                            }
                            else
                            {
                                switch (nLSBB)
                                {
                                    case 2:
                                        {
                                            buffer = B[m] % 4;
                                            if (m % 4 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 1)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 2)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m % 4 == 3)
                                            {
                                                buffer = buffer << 6;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 4 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 3:
                                        {
                                            buffer = B[m] % 8;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                int buf = B[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                int buf = B[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }                                                
                                            }
                                            else if (m == 6)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 5;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 4:
                                        {
                                            buffer = B[m] % 16;
                                            if (m % 2 == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m % 2 == 1)
                                            {
                                                buffer = buffer << 4;
                                                charValue += buffer;
                                            }
                                            if ((m + 1) % 2 == 0)
                                            {
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                    case 5:
                                        {
                                            buffer = B[m] % 32;
                                            if (m == 0)
                                            {
                                                charValue += buffer;
                                            }
                                            else if (m == 1)
                                            {
                                                int buf = B[m] % 8;
                                                buf = buf << 5;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 3;
                                                charValue += buffer;
                                            }
                                            else if (m == 2)
                                            {
                                                buffer = buffer << 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 3)
                                            {
                                                int buf = B[m] % 2;
                                                buf = buf << 7;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 4)
                                            {
                                                int buf = B[m] % 16;
                                                buf = buf << 4;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 4;
                                                charValue += buffer;
                                            }
                                            else if (m == 5)
                                            {
                                                buffer = buffer << 1;
                                                charValue += buffer;
                                            }
                                            else if (m == 6)
                                            {
                                                int buf = B[m] % 4;
                                                buf = buf << 6;
                                                charValue += buf;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                                buffer = buffer >> 2;
                                                charValue += buffer;
                                            }
                                            else if (m == 7)
                                            {
                                                buffer = buffer << 3;
                                                charValue += buffer;
                                                if (charValue == 0)
                                                {
                                                    finish = true;
                                                }
                                                else
                                                {
                                                    char c = (char)charValue;
                                                    extractedText += c.ToString();
                                                    charValue = 0;
                                                }
                                            }
                                        } break;
                                }
                            }
                            
                        }
                    }
                }
            }

            return extractedText;
        }

        public int getMaxMsgSize()
        {
            int sum = 0;
            double d;
            int nLSB;
            for (int i = 0; i < bmp.Height; i += 3)
            {
                for (int j = 0; j < bmp.Width; j += 3)
                {
                    if ((i % 3 == 0) && (j % 3 == 0) && ((i+2)<bmp.Height) && ((j+2)<bmp.Width))
                    {
                        d = getPVDofR(j, i);
                        nLSB = nSubstitutionLSB(group((int)d));
                        sum += (9 * nLSB) - 2;

                        d = getPVDofG(j, i);
                        nLSB = nSubstitutionLSB(group((int)d));
                        sum += (9 * nLSB) - 2;

                        d = getPVDofB(j, i);
                        nLSB = nSubstitutionLSB(group((int)d));
                        sum += (9 * nLSB) - 2;
                    }
                }
            }
            return sum;
        }

        private double getPVDofR(int koorX, int koorY){
            double result;
            double sum = 0;
            double min = getMinBlockofR(koorX, koorY);
            for (int i = koorY; i < (koorY + 3); i++)
            {
                for (int j = koorX; j < (koorX + 3); j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    sum += Math.Abs((double)pixel.R - min);
                }
            }
            result = sum / 8;
            return result;
        }

        private double getMinBlockofR(int koorX, int koorY)
        {
            Color pixel = bmp.GetPixel(koorX, koorY);
            double Min, tmp;
            Min = pixel.R;
            for (int i = koorY; i < (koorY + 3); i++)
            {
                for (int j = koorX; j < (koorX + 3); j++)
                {
                    pixel = bmp.GetPixel(j, i);
                    tmp = pixel.R;
                    if (tmp < Min)
                    {
                        Min = tmp;
                    }
                }
            }
            return Min;
        }

        private double getPVDofG(int koorX, int koorY)
        {
            double result;
            double sum = 0;
            double min = getMinBlockofG(koorX, koorY);
            for (int i = koorY; i < koorY + 3; i++)
            {
                for (int j = koorX; j < koorX + 3; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    sum += Math.Abs((double)pixel.G - min);
                }
            }
            result = sum / 8;
            return result;
        }

        private double getMinBlockofG(int koorX, int koorY)
        {
            Color pixel = bmp.GetPixel(koorX, koorY);
            double Min, tmp;
            Min = pixel.G;
            for (int i = koorY; i < (koorY + 3); i++)
            {
                for (int j = koorX; j < (koorX + 3); j++)
                {
                    pixel = bmp.GetPixel(j, i);
                    tmp = pixel.G;
                    if (tmp < Min)
                    {
                        Min = tmp;
                    }
                }
            }
            return Min;
        }

        private double getPVDofB(int koorX, int koorY)
        {
            double result;
            double sum = 0;
            double min = getMinBlockofB(koorX, koorY);
            for (int i = koorY; i < koorY + 3; i++)
            {
                for (int j = koorX; j < koorX + 3; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    sum += Math.Abs((double)pixel.B - min);
                }
            }
            result = sum / 8;
            return result;
        }

        private double getMinBlockofB(int koorX, int koorY)
        {
            Color pixel = bmp.GetPixel(koorX, koorY);
            double Min, tmp;
            Min = pixel.B;
            for (int i = koorY; i < (koorY + 3); i++)
            {
                for (int j = koorX; j < (koorX + 3); j++)
                {
                    pixel = bmp.GetPixel(j, i);
                    tmp = pixel.B;
                    if (tmp < Min)
                    {
                        Min = tmp;
                    }
                }
            }
            return Min;
        }
        
        private int nSubstitutionLSB(int group)
        {
            int bit;
            if (group == 0)
                bit = 2;
            else if (group == 1)
                bit = 3;
            else if (group == 2)
                bit = 4;
            else
                bit = 5;
            return bit;
        }

        private int group(int d)
        {
            int kelompok;
            if (d <= 7)
                kelompok = 0;   //lower
            else if (d >= 8 && d <= 15)
                kelompok = 1;   //lower-middle
            else if (d >= 16 && d <= 31)
                kelompok = 2;   //higher-middle
            else
                kelompok = 3;   //higher
            return kelompok;
        }

    }
}
