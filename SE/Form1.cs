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

        Point nowCube = new Point(7, 0);//現在方塊所在位置
        int[] nowShape = { 0, 0 };

        Pen pen = new Pen(Color.Black, 1);//格線

        int screenWidth = 15;
        int screenHeigh = 24;
        int cubeWidth = 20;

        SolidBrush[] Brush = {
        new SolidBrush(Color.Empty),
        new SolidBrush(Color.Red),
        new SolidBrush(Color.Green),
        new SolidBrush(Color.Blue),
        new SolidBrush(Color.Black),
        new SolidBrush(Color.Gray),
        new SolidBrush(Color.Purple)};

        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics graphics;

        List<List<int>> gameScreen = new List<List<int>>();
        //List<List<int>> gameScreenColor = new List<List<int>>();
        Random random = new Random();

        List<List<Rectangle>> rect = new List<List<Rectangle>>();//rect位置

        public int score = 0;

        public int GameSpeed = 100; 

        public void ClearNowShapeFromScreen()
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowCube.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowCube.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                gameScreen[tempY][tempX] = 0;
            }
        }

        public void AddShapeToScreen()
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowCube.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowCube.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                gameScreen[tempY][tempX] = nowShape[0] + 1;
            }
        }

        /*public void FocusCube()
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowCube.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowCube.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                gameScreen[tempY][tempX] = 2;
            }
        }*/

        public int CheckBound(Point cube, int[] shape)//0是正常 1是超出寬度 2是超出高度 3是碰到方塊
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = cube.X + cubeShape[shape[0]][shape[1]][i].X;
                int tempY = cube.Y + cubeShape[shape[0]][shape[1]][i].Y;
                if (tempX < 0 || tempX >= screenWidth)//超出寬度
                {
                    return 1;
                }
                if (tempY < -2 || tempY >= screenHeigh)//超出高度
                {
                    return 2;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                int tempX = cube.X + cubeShape[shape[0]][shape[1]][i].X;
                int tempY = cube.Y + cubeShape[shape[0]][shape[1]][i].Y;
                if (gameScreen[tempY][tempX] != 0  )//0是空的
                {
                    return 3;
                }
            }

            return 0;
        }

        public void Rotate()
        {
            int[] tempShape = new int[2];
            tempShape[0] = nowShape[0];
            tempShape[1] = nowShape[1];

            if (cubeShape[tempShape[0]].Count <= tempShape[1] + 1)//下一個形狀長怎樣
            {
                tempShape[1] = 0;
            }
            else
            {
                tempShape[1]++;
            }
            ClearNowShapeFromScreen();

            if (CheckBound(nowCube, tempShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            nowShape = tempShape;

            AddShapeToScreen();
        }

        public void GoLeft()
        {
            Point tempCube = new Point();
            tempCube = nowCube;
            tempCube.X = nowCube.X - 1;

            ClearNowShapeFromScreen();

            if (CheckBound(tempCube, nowShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            nowCube = tempCube;

            AddShapeToScreen();
        }

        public void GoRight()
        {
            Point tempCube = new Point();
            tempCube = nowCube;
            tempCube.X = nowCube.X + 1;

            ClearNowShapeFromScreen();

            if (CheckBound(tempCube, nowShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            nowCube = tempCube;

            AddShapeToScreen();
        }

        public void GoDown()
        {
            Point tempCube = new Point();
            tempCube = nowCube;
            ClearNowShapeFromScreen();

            while (CheckBound(tempCube, nowShape) == 0)
            {
                tempCube.Y = tempCube.Y + 1;
                ClearNowShapeFromScreen();
                if (CheckBound(tempCube, nowShape) == 2 || CheckBound(tempCube, nowShape) == 3)
                {
                    tempCube.Y = tempCube.Y - 1;
                    ClearNowShapeFromScreen();

                    nowCube = tempCube;

                    AddShapeToScreen();

                    CheckClearRow();

                    AddShapeToScreen();

                    break;
                }
            }
            AddShapeToScreen();


        }

        public void TimerDown()
        {
            Point tempCube = new Point();
            tempCube = nowCube;
            tempCube.Y = nowCube.Y + 1;
            ClearNowShapeFromScreen();

            if (CheckBound(tempCube, nowShape) == 2 || CheckBound(tempCube, nowShape) == 3)
            {
                tempCube.Y = tempCube.Y - 1;

                nowCube = tempCube;

                AddShapeToScreen();

                CheckClearRow();

                AddShapeToScreen();

                return;
            }

            ClearNowShapeFromScreen();

            nowCube = tempCube;

            AddShapeToScreen();
        }

        public bool CheckRowAllIsCube(int row)
        {
            for (int i = 0; i < screenWidth; i++)
                if (gameScreen[row][i] == 0 )
                    return false;
            return true;
        }

        public void CheckClearRow()
        {
            //FocusCube();

            for (int i = nowCube.Y; i < screenHeigh; i++)
            {
                if (CheckRowAllIsCube(i))
                {
                    score++;
                    label1.Text = score.ToString();
                    gameScreen.RemoveAt(i);
                    List<int> tempScreen = new List<int>();
                    for (int j = 0; j < rect[i].Count; j++)
                    {
                        tempScreen.Add(0);
                    }
                    gameScreen.Insert(0, tempScreen);
                }
            }

            CreateNewCube();
        }

        public void CreateNewCube()
        {
            nowCube.X = screenWidth / 2;
            nowCube.Y = 0;

            nowShape[0] = random.Next(0, 6);
            nowShape[1] = 0;
        }

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Height = cubeWidth * screenHeigh + 1;
            pictureBox1.Width = cubeWidth * screenWidth + 1;

            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            graphics = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);

            this.KeyPreview = true;
            KeyDown += new KeyEventHandler(From1_KeyDown);

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            InitCubeShape();//所有方塊的形狀

            CreateNewCube();//產生一個新方塊

            for (int i = 0; i < cubeWidth * screenHeigh; i += cubeWidth)//rect位置產生
            {
                List<Rectangle> tempRect = new List<Rectangle>();
                for (int j = 0; j < cubeWidth * screenWidth; j += cubeWidth)
                {
                    tempRect.Add(new Rectangle(j, i, cubeWidth, cubeWidth));
                }
                rect.Add(tempRect);
            }

            for (int i = 0; i < screenHeigh; i++)//
            {
                List<int> tempScreen = new List<int>();
                //List<int> tempScreen1 = new List<int>();
                for (int j = 0; j < screenWidth; j++)
                {
                    tempScreen.Add(0);
                    //tempScreen1.Add(0);
                }
                gameScreen.Add(tempScreen);
                //gameScreenColor.Add(tempScreen1);
            }

            timer1.Interval = GameSpeed;

            AddShapeToScreen();
            drawComponent(gameScreen);

        }

        public void drawComponent(List<List<int>> list)
        {
            graphics.Graphics.Clear(Color.White);

            for (int i = 0; i < screenHeigh; i++)
            {
                for (int j = 0; j < screenWidth; j++)
                {

                    graphics.Graphics.FillRectangle(Brush[list[i][j]], rect[i][j]);//填滿顏色
                    graphics.Graphics.DrawRectangle(pen, rect[i][j]);//格線
                }
            }

            graphics.Render(pictureBox1.CreateGraphics());
        }

        //讀按鍵功能
        private void From1_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (timer1.Enabled == true)
            {
                if (e.KeyCode == Keys.W)
                {
                    textBox1.Text += e.KeyCode;

                    Rotate();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.S)
                {
                    textBox1.Text += e.KeyCode;

                    GoDown();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.A)
                {
                    textBox1.Text += e.KeyCode;

                    GoLeft();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.D)
                {
                    textBox1.Text += e.KeyCode;

                    GoRight();
                    drawComponent(gameScreen);
                }
            }
        }

        //開始遊戲
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < screenHeigh; i++)//初始化畫面
            {
                for (int j = 0; j < screenWidth; j++)
                {
                    gameScreen[i][j] = 0;
                }
            }

            timer1.Enabled = true;//timer1 開始動作

            score = 0;
            label1.Text = score.ToString();

            CreateNewCube();

            AddShapeToScreen();

            drawComponent(gameScreen);
        }


        //離開遊戲按鈕
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //暫停遊戲
        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        //繼續遊戲
        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        //時鐘
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimerDown();
            drawComponent(gameScreen);
        }


        //cube形狀
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


    }
}
