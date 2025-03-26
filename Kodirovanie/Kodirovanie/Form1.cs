using System.Drawing;

using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot;


namespace Kodirovanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int[][] RGBFromImage1;
        private double[][] PFromImage;

        private void button1_Click(object sender, EventArgs e)
        {
            RGBFromImage1 = ImageToRGBArray(pictureBox1.Image);

            var modelR = new PlotModel { Title = "R Color" };
            var modelG = new PlotModel { Title = "G Color" };
            var modelB = new PlotModel { Title = "B Color" };

            AreaSeries seriesR = new AreaSeries();
            AreaSeries seriesG = new AreaSeries();
            AreaSeries seriesB = new AreaSeries();

            DiagBilder(ref seriesR, RGBFromImage1[0]);
            DiagBilder(ref seriesG, RGBFromImage1[1]);
            DiagBilder(ref seriesB, RGBFromImage1[2]);

            seriesR.Fill = OxyColors.Red;
            seriesR.Color = OxyColors.Red;
            seriesB.Fill = OxyColors.Blue;
            seriesB.Color = OxyColors.Blue;

            modelR.Series.Add(seriesR);
            modelG.Series.Add(seriesG);
            modelB.Series.Add(seriesB);

            this.plotView1.Model = modelR;
            this.plotView2.Model = modelG;
            this.plotView3.Model = modelB;

            button3.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                pictureBox1.ImageLocation = filePath;
                button1.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RGBArrayToPArray();

            textBox1.Text = $"{HSolution(PFromImage[0]):f8}";
            textBox2.Text = $"{HSolution(PFromImage[1]):f8}";
            textBox3.Text = $"{HSolution(PFromImage[2]):f8}";

            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
        }

        private int[][] ImageToRGBArray(Image image)
        {
            //Требуется System.Drawing

            int[] R = new int[256];
            int[] G = new int[256];
            int[] B = new int[256];
            Bitmap bitmap = new Bitmap(image);
            for (int w = 0; w < bitmap.Width; w++)
            {
                for (int h = 0; h < bitmap.Height; h++)
                {
                    Color pixelColor = bitmap.GetPixel(w, h);
                    R[pixelColor.R]++;
                    G[pixelColor.G]++;
                    B[pixelColor.B]++;
                }
            }
            return [R, G, B];
        }

        private void RGBArrayToPArray()
        {
            double V = pictureBox1.Image.Height * pictureBox1.Image.Width;

            double[][] result = {
            new double[256],
            new double[256],
            new double[256],
            };

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    result[i][j] = RGBFromImage1[i][j] / V;
                }
            }

            PFromImage = result;
        }

        private double HSolution(double[] colorChannel)
        {
            double result = 0;

            for (int i = 1; i < 256; i++)
            {
                if (colorChannel[i] != 0) result += colorChannel[i] * Math.Log(colorChannel[i], 2);
            }
            result *= -1;

            return result;
        }

        private void DiagBilder(ref AreaSeries series, int[] arr)
        {
            for (int i = 1; i < 256; i++)
            {
                series.Points.Add(new DataPoint(i, arr[i]));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var f2 = new Form2(PFromImage[0]);
            f2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var f2 = new Form2(PFromImage[1]);
            f2.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var f2 = new Form2(PFromImage[2]);
            f2.Show();
        }
    }
}