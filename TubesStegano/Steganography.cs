using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace TubesStegano
{
    class Steganography
    {
        public enum State
        {
            Hiding,
            Filling_With_Zeros
        };

        private string fileName;
        private string message;
        private string key;
        private Bitmap bmp;
        private List<Point> koorSeed = new List<Point>();
        private int counter = 0;

        public void setfileName(string name)
        {
            fileName = name;
        }

        public void setMessage(string text)
        {
            message = text;
        }

        public void setImage(Bitmap b)
        {
            bmp = b;
        }

        public void setKey(string s)
        {
            key = s;
        }

        public Bitmap embedText()
        {
            // pertama kita akan melakukan penyisipan, state nya hiding
            State state = State.Hiding;

            int charIndex = 0;
            int charValue = 0;
            long pixelElementIndex = 0;
            int zeros = 0;
            int R = 0, G = 0, B = 0;
            Point koordinat = new Point();
            koordinat.setPoint(0,0);

            Bitmap cover = bmp;

            if (isGrayScale())
            {
                // Pemrosesan grayscale image

                while (counter < getMaxMsgSize())
                {
                    koordinat = getSeed(koordinat);

                    Color pixel = cover.GetPixel(koordinat.getX(), koordinat.getY());

                    // kosongkan bit terakhir
                    R = pixel.R - pixel.R % 2;

                    // cek apakah seluruh 8 bit sudah diproses atau belum
                    if (pixelElementIndex % 8 == 0)
                    {
                        if (state == State.Filling_With_Zeros && zeros == 8)
                        {
                            if ((pixelElementIndex - 1) % 3 < 2)
                            {
                                cover.SetPixel(koordinat.getX(), koordinat.getY(), Color.FromArgb(R));
                            }

                            counter = 0;
                            return cover;
                        }

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
                        R += charValue % 2;

                        charValue /= 2;
                    }

                    cover.SetPixel(koordinat.getX(), koordinat.getY(), Color.FromArgb(R,R,R));

                    pixelElementIndex++;

                    if (state == State.Filling_With_Zeros)
                    {
                        zeros++;
                    }
                    counter++;
                }
            }
            else
            {
                // pemrosesan RGB image

                while (counter < getMaxMsgSize())
                {
                    koordinat = getSeed(koordinat);

                    Color pixel = cover.GetPixel(koordinat.getX(), koordinat.getY());

                    // kosongkan bit terakhir
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;

                    for (int n = 0; n < 3; n++)
                    {
                        // cek apakah seluruh 8 bit sudah diproses atau belum
                        if (pixelElementIndex % 8 == 0)
                        {
                            if (state == State.Filling_With_Zeros && zeros == 8)
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    cover.SetPixel(koordinat.getX(), koordinat.getY(), Color.FromArgb(R, G, B));
                                }

                                counter = 0;
                                return cover;
                            }

                            if (charIndex >= message.Length)
                            {
                                state = State.Filling_With_Zeros;
                            }
                            else
                            {
                                charValue = message[charIndex++];
                            }
                        }

                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                {
                                    if (state == State.Hiding)
                                    {
                                        R += charValue % 2;
                                        charValue /= 2;
                                    }
                                } break;
                            case 1:
                                {
                                    if (state == State.Hiding)
                                    {
                                        G += charValue % 2;

                                        charValue /= 2;
                                    }
                                } break;
                            case 2:
                                {
                                    if (state == State.Hiding)
                                    {
                                        B += charValue % 2;

                                        charValue /= 2;
                                    }

                                    cover.SetPixel(koordinat.getX(), koordinat.getY(), Color.FromArgb(R, G, B));
                                } break;
                        }

                        pixelElementIndex++;

                        if (state == State.Filling_With_Zeros)
                        {
                            zeros++;
                        }
                    }
                    counter++;
                }
            }

            return cover;
        }

        public String extractText(Bitmap cover)
        {
            int colorUnitIndex = 0;
            int charValue = 0;
            Point koordinat = new Point();
            koordinat.setPoint(0, 0);
            string extractedText = String.Empty;

            if (isGrayScale())
            {
                // pemrosesan grayscale

                while (true)
                {
                    koordinat = getSeed(koordinat);

                    Color pixel = cover.GetPixel(koordinat.getX(), koordinat.getY());

                    charValue = charValue * 2 + pixel.R % 2;

                    colorUnitIndex++;

                    if (colorUnitIndex % 8 == 0)
                    {
                        charValue = reverseBits(charValue);

                        if (charValue == 0)
                        {
                            counter = 0;
                            return extractedText;
                        }

                        char c = (char)charValue;

                        extractedText += c.ToString();
                    }

                    counter++;
                }
            }
            else
            {
                // pemrosesan RGB
                while (true)
                {
                    koordinat = getSeed(koordinat);

                    Color pixel = cover.GetPixel(koordinat.getX(), koordinat.getY());

                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2;
                                } break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                } break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                } break;
                        }

                        colorUnitIndex++;

                        if (colorUnitIndex % 8 == 0)
                        {
                            charValue = reverseBits(charValue);

                            if (charValue == 0)
                            {
                                counter = 0;
                                return extractedText;
                            }

                            char c = (char)charValue;

                            extractedText += c.ToString();
                        }
                    }
                    counter++;
                }
            }

            return extractedText;
        }

        private int reverseBits(int n)
        {
            int result = 0;

            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;

                n /= 2;
            }

            return result;
        }

        // get random seed
        private Point getSeed(Point koor)
        {
            Point A = new Point();
            bool cek = false;
            int x, y, a ,b;
            x = key[0] + key[2];
            y = key[1] + key[3];
            a = koor.getX() + x;
            b = koor.getY() + y;
            do
            {
                a += 1;
                b += 1;
                if (a >= bmp.Width)
                {
                    a = a - bmp.Width;
                }
                if (b >= bmp.Height)
                {
                    b = b - bmp.Height;
                }
                A.setPoint(a, b);
                if (!sudahAdaPoint(A))
                {
                    cek = true;
                    koorSeed.Add(A);
                }
            } while (!cek);
            return A;
        }

        public void clear()
        {
            koorSeed.Clear();
            counter = 0;
        }

        // cek apakah point sudah pernah dimunculkan atau belum
        private Boolean sudahAdaPoint(Point X)
        {
            bool cek = false;
            for (int i = 0; i < counter; i++)
            {
                if ((X.getX() == koorSeed[i].getX()) && (X.getY() == koorSeed[i].getY()))
                {
                    cek = true;
                }
            }
            return cek;
        }

        // mendapatkan maksimum ukuran pesan pada gambar bitmaps
        public int getMaxMsgSize()
        {
            int size;
            if (isGrayScale())
            {
                // maksimum pesan di grayscale
                size = bmp.Height * bmp.Width;
            }
            else
            {
                // maksimum pesan di RGB
                size = bmp.Height * bmp.Width * 3;
            }

            return size;
        }

        // cek apakah bitmap merupakan grayscale atau RGB
        // mengembalikan true jika grayscale
        // mengembalikan false jika RGB
        public Boolean isGrayScale()
        {
            Boolean cek = true;

            // Buka file gambar
            FileStream inStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            byte[] buffer = new byte[2];
            inStream.Seek(28, 0);
            inStream.Read(buffer, 0, 2);
            Int16 nBit = BitConverter.ToInt16(buffer, 0);

            if (nBit == 8) { /*true grayscale, do nothing*/ }
            else cek = false;

            return cek;
        }

    }
}
