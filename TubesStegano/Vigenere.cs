using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesStegano
{
    class Vigenere
    {
        //enkripsi
        public static String encrypt(String plain, String kunci)
        {
            String chip = "";//ciphertext
            for (int i = 0, j = 0; i < plain.Length; i++)
            {
                char c = plain[i];
                chip = chip + (char)((c + kunci[j]) % 256);
                j = ++j % kunci.Length;
            }
            return chip;
        }

        //dekripsi
        public static String decrypt(String chip, String kunci)
        {
            String plain = "";//plaintext
            for (int i = 0, j = 0; i < chip.Length; i++)
            {
                char c = chip[i];
                plain = plain + (char)((c - kunci[j]) % 256);
                j = ++j % kunci.Length;
            }
            return plain;
        }
    }
}
