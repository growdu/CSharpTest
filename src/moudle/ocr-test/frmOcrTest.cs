using asprise_ocr_api;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;

namespace ocr_test
{
    public partial class frmOcrTest : Form
    {
        public frmOcrTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //PictureBox控件显示图片
                pictureBox1.Load(openFileDialog.FileName);
                //获取用户选择文件的后缀名 
                string extension = Path.GetExtension(openFileDialog.FileName);
                //声明允许的后缀名 
                string[] str = new string[] { ".jpg", ".png" };
                if (!str.Contains(extension))
                {
                    MessageBox.Show("仅能上传jpg,png格式的图片！");
                }
                else
                {
                    AspriseOCR.SetUp();
                    AspriseOCR ocr = new AspriseOCR();
                    ocr.StartEngine("chi_sim", AspriseOCR.SPEED_FASTEST);

                    string s = ocr.Recognize(openFileDialog.FileName, -1, -1, -1, -1, -1,
                      AspriseOCR.RECOGNIZE_TYPE_ALL, AspriseOCR.OUTPUT_FORMAT_PLAINTEXT);
                    //Console.WriteLine("OCR Result: " + s);
                    richTextBox1.Text = s;
                    // process more images here ...

                    ocr.StopEngine();
                    //识别图片文字
                    //var img = new Bitmap(openFileDialog.FileName);

                    //var ocr = new TesseractEngine(@"H:\Tesseract-OCR\tessdata\tesseract-ocr-3.02.chi_sim\tesseract-ocr\tessdata", "chi_sim", EngineMode.TesseractAndCube);
                    //var page = ocr.Process(img);
                    //richTextBox1.Text = page.GetText();
                }
            }
        }
    }
}
