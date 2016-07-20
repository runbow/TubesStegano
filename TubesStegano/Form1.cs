using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TubesStegano
{
    public partial class SteganoForm : Form
    {
        // global variable
        private Bitmap bmp1 = null;
        private Bitmap bmp2 = null;
        private string dataText = string.Empty;
        private string fileImage = string.Empty;
        private string extractedText = string.Empty;
        private int sizeEmbedded, sizeFileTxt;
        private Steganography SteganoStandar = new Steganography();
        private SteganoNinePixelDiff SteganoNine = new SteganoNinePixelDiff();
        private SteganoFourPixelDiff SteganoFour = new SteganoFourPixelDiff();

        public SteganoForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ModeBox.SelectedIndex = 0;
        }

        private void openImgButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Bitmap Files| *.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                initialImage.Image = Image.FromFile(open_dialog.FileName);
                bmp1 = (Bitmap)initialImage.Image;

                if (ModeBox.SelectedIndex == 0)
                {
                    fileImage = open_dialog.FileName.ToString();
                    SteganoStandar.setfileName(fileImage);
                    SteganoStandar.setImage(bmp1);
                    sizeEmbedded = SteganoStandar.getMaxMsgSize();
                    maxBitsLabel.Visible = true;
                    countLabel.Text = sizeEmbedded.ToString() + " bits";
                    countLabel.Visible = true;
                }
                else if (ModeBox.SelectedIndex == 1)
                {
                    fileImage = open_dialog.FileName.ToString();

                }
                else if (ModeBox.SelectedIndex == 2)
                {
                    fileImage = open_dialog.FileName.ToString();
                    SteganoNine.setImage(bmp1);
                    sizeEmbedded = SteganoNine.getMaxMsgSize();
                    maxBitsLabel.Visible = true;
                    countLabel.Text = sizeEmbedded.ToString() + " bits";
                    countLabel.Visible = true;
                }
            }
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                fileNameBox.Text = open_dialog.FileName.ToString();
                FileInfo fi = new FileInfo(open_dialog.FileName);
                sizeFileTxt = (int)fi.Length;
                dataText = File.ReadAllText(open_dialog.FileName).ToString();
            }
        }

        private void fileNameBox_Click(object sender, EventArgs e)
        {
            browseButton_Click(sender, e);
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            bmp1 = (Bitmap)initialImage.Image;

            if (bmp1 == null)
            {
                MessageBox.Show("You have to load the image first");
                return;
            }

            if (dataText.Equals(""))
            {
                MessageBox.Show("The text you want to hide can't be empty", "Warning");
                return;
            }
            else
            {
                if (ModeBox.SelectedIndex == 0)
                {
                    sizeEmbedded = SteganoStandar.getMaxMsgSize();
                    if ((sizeFileTxt - 8) > sizeEmbedded)
                    {
                        MessageBox.Show("The file is too large to hide");
                        return;
                    }
                }
                else if (ModeBox.SelectedIndex == 1)
                {
                    SteganoFour.setCoverObject((Bitmap)initialImage.Image);
                    sizeEmbedded = SteganoFour.getMaxMsgSize();
                    if ((sizeFileTxt - 8) > sizeEmbedded)
                    {
                        MessageBox.Show("The file is too large to hide");
                        return;
                    }
                    SteganoFour.setMessage(dataText);
                }
                else if (ModeBox.SelectedIndex == 2)
                {
                    sizeEmbedded = SteganoNine.getMaxMsgSize();
                    if ((sizeFileTxt - 8) > sizeEmbedded)
                    {
                        MessageBox.Show("The file is too large to hide");
                        return;
                    }
                    SteganoNine.setMessage(dataText);
                }
            }

            if (keyBox.Text == "")
            {
                MessageBox.Show("Key must be provide");
                return;
            }
            else if (keyBox.Text.Length < 4)
            {
                MessageBox.Show("Password at least 4 character");
                return;
            } else
            {
                dataText = Vigenere.encrypt(dataText, keyBox.Text);
            }
            

            if (ModeBox.SelectedIndex == 0)
            {
                SteganoStandar.clear();
                SteganoStandar.setKey(keyBox.Text);
                SteganoStandar.setMessage(dataText);
                bmp2 = SteganoStandar.embedText();

                steganoImg.Image = bmp2;

                double tes;
                tes = calculatePSNR(bmp2);
                PSNRlabel.Text = "PSNR :";
                PSNRlabel.Text += " " + tes.ToString() + " dB";
            }
            // Mode four-pixel diff
            else if (ModeBox.SelectedIndex == 1)
            {
                bmp2 = SteganoFour.embedText();

                steganoImg.Image = bmp2;

                double tes;
                tes = calculatePSNR(bmp2);
                PSNRlabel.Text = "PSNR :";
                PSNRlabel.Text += " " + tes.ToString() + " dB";
            }
            // Mode nine-pixel diff
            else if (ModeBox.SelectedIndex == 2)
            {
                SteganoNine.setMessage(dataText);
                bmp2 = SteganoNine.embedText();

                steganoImg.Image = bmp2;

                double tes;
                tes = calculatePSNR(bmp2);
                PSNRlabel.Text = "PSNR :";
                PSNRlabel.Text += " " + tes.ToString() + " dB";
            }

        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            bmp2 = (Bitmap)steganoImg.Image;

            if (keyBox.Text == "")
            {
                MessageBox.Show("Key must be provide");
                return;
            }
            else if (keyBox.Text.Length < 4)
            {
                MessageBox.Show("Password at least 4 character");
                return;
            }

            if (bmp2 != null)
            {
                if (ModeBox.SelectedIndex == 0)
                {
                    SteganoStandar.clear();
                    SteganoStandar.setKey(keyBox.Text);
                    extractedText = SteganoStandar.extractText(bmp2);
                }
                // Mode four-pixel diff
                else if (ModeBox.SelectedIndex == 1)
                {

                }
                // Mode nine-pixel diff
                else if (ModeBox.SelectedIndex == 2)
                {
                    SteganoNine.setImage(bmp2);
                    extractedText = SteganoNine.extractText(bmp2);
                }

                extractedText = Vigenere.decrypt(extractedText, keyBox.Text);

                SaveFileDialog save_dialog = new SaveFileDialog();
                save_dialog.Filter = "Text Files|*.txt";

                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(save_dialog.FileName, extractedText);
                }
            }
        }

        private void saveSteganoImgButton_Click(object sender, EventArgs e)
        {
            bmp2 = (Bitmap)steganoImg.Image;

            if (bmp2 != null)
            {
                SaveFileDialog save_dialog = new SaveFileDialog();
                save_dialog.Filter = "Bitmap Image|*.bmp";

                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    bmp2.Save(save_dialog.FileName, ImageFormat.Bmp);
                }
            }
        }

        private void openSteganoImgButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Bitmap Files|*.bmp";

            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                steganoImg.Image = Image.FromFile(open_dialog.FileName);
                bmp1 = (Bitmap)steganoImg.Image;

                if (ModeBox.SelectedIndex == 0)
                {
                    fileImage = open_dialog.FileName.ToString();
                    SteganoStandar.setfileName(fileImage);
                    SteganoStandar.setImage(bmp1);
                    sizeEmbedded = SteganoStandar.getMaxMsgSize();
                    maxBitsLabel.Visible = true;
                    countLabel.Text = sizeEmbedded.ToString() + " bits";
                    countLabel.Visible = true;
                }
                else if (ModeBox.SelectedIndex == 1)
                {

                }
                else if (ModeBox.SelectedIndex == 2)
                {

                }
            }
        }

        private void ModeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bmp1 = (Bitmap)initialImage.Image;

            if (bmp1 == null)
            {
                return;
            }

            if (ModeBox.SelectedIndex == 0)
            {
                countLabel.Text = "";
                SteganoStandar.setImage((Bitmap)initialImage.Image);
                sizeEmbedded = SteganoStandar.getMaxMsgSize();
                countLabel.Text = sizeEmbedded.ToString() + " bits";
            }
            else if (ModeBox.SelectedIndex == 1)
            {
                countLabel.Text = "";
                SteganoFourPixelDiff sf = new SteganoFourPixelDiff();
                sf.setCoverObject(bmp1);
                sizeEmbedded = sf.getMaxMsgSize();
                countLabel.Text = sizeEmbedded.ToString() + " bits";
            }
            else if (ModeBox.SelectedIndex == 2)
            {
                countLabel.Text = "";
                SteganoNine.setImage((Bitmap)initialImage.Image);
                sizeEmbedded = SteganoNine.getMaxMsgSize();
                countLabel.Text = sizeEmbedded.ToString() + " bits";
            }
        }

        private double calculatePSNR(Bitmap b2)
        {
            double sum = 0;
            double rms;
            double error1, error2, error3, total;

            Bitmap b1 = new Bitmap(fileImage, true);

            for (int i = 0; i < b1.Height; i++)
            {
                for (int j = 0; j < b1.Width; j++)
                {
                    Color pixel1 = b1.GetPixel(j, i);
                    Color pixel2 = b2.GetPixel(j, i);
                    error1 = pixel1.R - pixel2.R;
                    error2 = pixel1.G - pixel2.G;
                    error3 = pixel1.B - pixel2.B;
                    total = Math.Pow(error1, 2) + Math.Pow(error2, 2) + Math.Pow(error3, 2);
                    sum += total;
                }
            }

            rms = (sum / (b1.Height * b1.Width)) / 3.0;

            double PSNR;
            PSNR = 20 * (Math.Log10(256 / (Math.Sqrt(rms))));

            return PSNR;
        }
        
    }
}
