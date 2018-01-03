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
        List<List<List<Point>>> cubeShape = new List<List<List<Point>>>();//各方塊的初始形狀

        Point nowCube = new Point(4, 4);//現在方塊所在位置
        int[] nowShape = { 6, 0 };

        Pen pen = new Pen(Color.Black, 1);//格線

        const int screenWidth = 28;
        const int screenHeigh = 30;
        const int cubeWidth = 15;

        SolidBrush[] Brush = {
        new SolidBrush(Color.Red),
        new SolidBrush(Color.Green),
        new SolidBrush(Color.Blue),
        new SolidBrush(Color.Black),
        new SolidBrush(Color.Gray),
        new SolidBrush(Color.Purple)};

        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics graphics;

        List<List<int>> gameScreen = new List<List<int>>();//1R 2G 3B 4黑 5灰

        Random random = new Random();//測試用

        List<List<Rectangle>> rect = new List<List<Rectangle>>();//rect位置

        public void Rotate()
        {
            int[] tempShape = nowShape;

            if(cubeShape[tempShape[0]].Count <= tempShape[1] +1 )//下一個形狀長怎樣
            {
                tempShape[1] = 0;
            }
            else
            {
                tempShape[1]++;
            }

            for (int i = 0; i < 4; i++) {
                int tempX = nowCube.X + cubeShape[tempShape[0]][tempShape[1]][i].X;
                int tempY = nowCube.Y + cubeShape[tempShape[0]][tempShape[1]][i].Y;
                if (tempX < 0|| tempX > screenWidth)//超出寬度
                {
                    return;
                }
                if(tempY < 0 || tempY > screenHeigh)//超出高度
                {
                    return;
                }
                if (gameScreen[tempX][tempY] == 2)//0是空的 1是現在 2是固定的
                {
                    return;
                }
            }
            nowShape = tempShape;
        }

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

        private void InitCubeShape()
        {
            for (int i = 0; i < 7; i++)
            {
                cubeShape.Add(new List<List<Point>>());
            }

            List<Point> tempShape = new List<Point>();
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 1));
            cubeShape[0].Add(tempShape);//方塊

            tempShape = new List<Point>();//L左
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(2, 1));
            cubeShape[1].Add(tempShape);
            tempShape = new List<Point>();//L左
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(0, 2));
            cubeShape[1].Add(tempShape);
            tempShape = new List<Point>();//L左
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(2, 1));
            cubeShape[1].Add(tempShape);
            tempShape = new List<Point>();//L左
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(1, 2));
            tempShape.Add(new Point(0, 2));
            cubeShape[1].Add(tempShape);

            tempShape = new List<Point>();//L右
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(2, 1));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(0, 1));
            cubeShape[2].Add(tempShape);
            tempShape = new List<Point>();//L右
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(0, 2));
            tempShape.Add(new Point(1, 2));
            cubeShape[2].Add(tempShape);
            tempShape = new List<Point>();//L右
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(0, 1));
            cubeShape[2].Add(tempShape);
            tempShape = new List<Point>();//L右
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(1, 2));
            cubeShape[2].Add(tempShape);

            tempShape = new List<Point>();//z左
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(1, 2));
            cubeShape[3].Add(tempShape);
            tempShape = new List<Point>();//z左
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 1));
            cubeShape[3].Add(tempShape);

            tempShape = new List<Point>();//z右
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(0, 2));
            cubeShape[4].Add(tempShape);
            tempShape = new List<Point>();//z右
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(2, 1));
            cubeShape[4].Add(tempShape);

            tempShape = new List<Point>();//直線
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(0, 2));
            tempShape.Add(new Point(0, 3));
            cubeShape[5].Add(tempShape);
            tempShape = new List<Point>();//直線
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(3, 0));
            cubeShape[5].Add(tempShape);

            tempShape = new List<Point>();//凸
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(2, 1));
            cubeShape[6].Add(tempShape);
            tempShape = new List<Point>();//凸
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(0, 2));
            tempShape.Add(new Point(1, 1));
            cubeShape[6].Add(tempShape);
            tempShape = new List<Point>();//凸
            tempShape.Add(new Point(0, 0));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(2, 0));
            tempShape.Add(new Point(1, 1));
            cubeShape[6].Add(tempShape);
            tempShape = new List<Point>();//凸
            tempShape.Add(new Point(0, 1));
            tempShape.Add(new Point(1, 0));
            tempShape.Add(new Point(1, 1));
            tempShape.Add(new Point(1, 2));
            cubeShape[6].Add(tempShape);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCubeShape();

            for (int i = 0; i < cubeWidth * screenWidth; i += cubeWidth)//rect位置產生
            {
                List<Rectangle> tempRect = new List<Rectangle>();
                for (int j = 0; j < cubeWidth * screenHeigh; j += cubeWidth)
                {
                    tempRect.Add(new Rectangle(i, j, cubeWidth, cubeWidth));
                }
                rect.Add(tempRect);
            }
            for (int i = 0; i < rect.Count; i++)//隨機產生顏色
            {
                List<int> tempScreen = new List<int>();
                for (int j = 0; j < rect[i].Count; j++)
                {
                    tempScreen.Add(random.Next(0, 4));
                }
                gameScreen.Add(tempScreen);
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
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowCube.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowCube.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                graphics.Graphics.FillRectangle(Brush[5], rect[tempX][tempY]);//測試轉動用
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

                Rotate();
                drawComponent(gameScreen);
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
