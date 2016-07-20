# Steganografi

Tubes 1 Kriptografi

## Deadline 27 Februari 2015 pukul 17.00

## Spesifikasi

1. Program menerima masukan berupa citra digital, nama file pesan, dan kunci steganografi.

2. Pesan harus dienkripsi dengan Vigenere Cipher sebelum disisipkan ke dalam citra. 

3. Pengguna memasukkan sebuah kata kunci yang berfungsi dua: sebagai kunci enkripsi pada Vigenere Cipher dan sebagai kunci (seed) pembangkitan bilangan acak.

4. Jangan menyisipkan kunci di dalam file citra.

5. Program menolak menyisipkan pesan jika ukuran file pesan melebihi kapasitas maksimal yang dapat disisipkan.

6. Program dapat menyimpan stego-image (citra yang sudah disisipi pesan).

7. Program dapat mengekstraksi pesan utuh seperti sediakala dan menyimpannya sebagai file dengan nama lain (save as).

8. Agar format file hasil ekstraksi diketahui, maka properti file seperti ekstensi (.exe, .doc, .pdf, dll), sebaiknya juga disimpan.

9. Program dapat menampilkan (view) citra asli dan citra stegano dalam dua jendela berbeda.

10. Program dapat menampilkan ukuran kualitas citra hasil steganografi dengan PSNR (Peak Signal-to-Noise Ratio).

11.	Citra uji yang digunakan adalah beberapa citra uji standard (Lenna, peppers, cameraman, boat, dll) dan citra natural lainnya. 

12. Alirkan bit-bit data sebanyak kapasitas maksimal, sembunyikan dengan metode LSB yang diuji, lalu hitung PSNR. Rangkumlah hasil pengujian anda.

selengkapnya [lihat disini](http://informatika.stei.itb.ac.id/~rinaldi.munir/Kriptografi/2014-2015/Tubes1-Kripto-2015.doc)

## Deliverable

1. source code

2. laporan (*pdf & hardcopy)

  a. Teori singkat (steganografi, metode modifikasi LSB standard, metode modifikasi LSB dari Liao dkk,  Metode modifikasi LSB dari Swain.

  b. Implementasi program, termasuk : rancangan program. 

  c. Pengujian program dan analisis hasil. Uji program dengan bermacam-macam citra bitmap dan bermacam-macam file pesan. 

  d. Pengujian kinerja seperti yang dijelaskan pada butir 12 pada spesifikasi program.

  e. Kesimpulan dari hasil implementasi.

  f. Tampilkan foto anda bertiga di cover laporan sebagai pengganti logo gajah.


## Paper rujukan
1. Liao, X., Wena, Q., Zhang, J. (2011): [A steganographic method for digital images with four-pixel differencing and modified LSB substitution](http://informatika.stei.itb.ac.id/~rinaldi.munir/Kriptografi/2014-2015/Paper-1.pdf) , Journal of Visual Communication and Image Representation. 2011; 22:1–8.

2. Swain, S. (2014): [Digital Image Steganography using Nine-Pixel Differencing and Modified LSB Substitution](http://informatika.stei.itb.ac.id/~rinaldi.munir/Kriptografi/2014-2015/Paper-2.pdf) , Indian Journal of Science and Technology, Vol 7(9), 1444–1450, September 2014 
