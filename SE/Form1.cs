using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SE
{
    public partial class Form1 : Form
    {
        Pen pen = new Pen(Color.Black, 1);//格線

        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics graphics;

        List<List<int>> gameScreen = new List<List<int>>();//1R 2G 3B 4黑 5灰

        const int screenWidth = 24;
        const int screenHeigh = 30;
        const int cubeWidth = 15;

        SolidBrush[] Brush = {
        new SolidBrush(Color.Red),
        new SolidBrush(Color.Green),
        new SolidBrush(Color.Blue),
        new SolidBrush(Color.Black),
        new SolidBrush(Color.Gray), };

        Random random = new Random();//測試用

        List<List<Rectangle>> rect = new List<List<Rectangle>>();//rect位置

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Height = cubeWidth * screenHeigh;
            pictureBox1.Width = cubeWidth * screenWidth;


            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            graphics = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);

            this.KeyPreview = true;
            KeyDown += new KeyEventHandler(From1_KeyDown);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            for (int i = 0; i < cubeWidth * screenWidth; i += cubeWidth)//rect位置產生
            {
                List<Rectangle> temp = new List<Rectangle>();
                for (int j = 0; j < cubeWidth * screenHeigh; j += cubeWidth)
                {
                    temp.Add(new Rectangle(i, j, cubeWidth, cubeWidth));
                }
                rect.Add(temp);
            }
            for (int i = 0; i < rect.Count; i++)//隨機產生顏色
            {
                List<int> temp = new List<int>();
                for (int j = 0; j < rect[i].Count; j++)
                {
                    temp.Add(random.Next(0, 4));
                }
                gameScreen.Add(temp);
            }
            
        }

        public void drawComponent(List<List<int>> list)
        {            
            graphics.Graphics.Clear(Color.White);

            for (int i = 0; i < rect.Count; i++)
            {
                for (int j = 0; j < rect[i].Count; j++)
                {
                    graphics.Graphics.FillRectangle(Brush[list[i][j]], rect[i][j]);//填滿顏色
                    graphics.Graphics.DrawRectangle(pen, rect[i][j]);//格線
                }
            }
            graphics.Render(pictureBox1.CreateGraphics());
        }

        private void button1_Click(object sender, EventArgs e)//測試用
        {
            for (int i = 0; i < rect.Count; i++)//隨機產生顏色
            {
                for (int j = 0; j < rect[i].Count; j++)
                {
                    gameScreen[i][j] = random.Next(0, 4);
                }
            }
            drawComponent(gameScreen);
        }

        private void From1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.W)
            {
                textBox1.Text += e.KeyCode;
            }
            if (e.KeyCode == Keys.S)
            {
                textBox1.Text += e.KeyCode;
            }
            if (e.KeyCode == Keys.A)
            {
                textBox1.Text += e.KeyCode;
            }
            if (e.KeyCode == Keys.D)
            {
                textBox1.Text += e.KeyCode;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
