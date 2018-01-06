using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SE
{
    public partial class TetrisView : Form
    {
        TetrisModel model;
        TetrisController controller;
        public System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public Label lebal=new Label();
        List<List<List<Point>>> cubeShape = new List<List<List<Point>>>();//各方塊的初始形狀

        Point nowPoint = new Point(7, 0);//現在方塊所在位置
        int[] nowShape = { 0, 0 };//現在的圖形 , 現在圖形是第幾旋轉形狀

        Pen pen = new Pen(Color.Black, 1);//格線顏色 , 粗度

        int gameWidth = 10; //寬有幾格
        int gameHeigh = 20; //高有幾格
        int cubeWidth = 30; //格子寬度

        SolidBrush[] Brush = {
        new SolidBrush(Color.LightGray),//空白格子的顏色
        new SolidBrush(Color.Red),//田
        new SolidBrush(Color.Orange),//倒L
        new SolidBrush(Color.DarkBlue),//L
        new SolidBrush(Color.DarkRed),//z
        new SolidBrush(Color.DarkGreen),//倒z
        new SolidBrush(Color.Blue),//一
        new SolidBrush(Color.Purple)//凸
        };

        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics graphics;//畫圖用

        List<List<int>> gameScreen = new List<List<int>>();//畫面現在有的方塊 0~7 0是空白 其他是各種方塊
        Random random = new Random();//用來隨機產生方塊種類

        List<List<Rectangle>> rect = new List<List<Rectangle>>();//rect位置，畫圖用

        public int score = 0;//分數

        public int gameSpeed = 500;//遊戲速度 
        public TetrisView(TetrisController con)
        {
            controller = con;
        }
        public void ClearNowShapeFromScreen()//把現有方塊從畫面上先消除，避免影響判斷碰撞
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowPoint.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowPoint.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                gameScreen[tempY][tempX] = 0;
            }
        }

        public void AddShapeToScreen()//將現有方塊加入畫面中
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = nowPoint.X + cubeShape[nowShape[0]][nowShape[1]][i].X;
                int tempY = nowPoint.Y + cubeShape[nowShape[0]][nowShape[1]][i].Y;
                gameScreen[tempY][tempX] = nowShape[0] + 1;//從1~7 0被用來當空白，所以+1
            }
        }

        //0是正常 1是超出寬度 2是超出高度 3是碰到方塊
        public int CheckBound(Point cube, int[] shape)
        {
            for (int i = 0; i < 4; i++)
            {
                int tempX = cube.X + cubeShape[shape[0]][shape[1]][i].X;
                int tempY = cube.Y + cubeShape[shape[0]][shape[1]][i].Y;
                if (tempX < 0 || tempX >= gameWidth)//超出寬度
                {
                    return 1;
                }
                if (tempY < 0 || tempY >= gameHeigh)//超出高度
                {
                    return 2;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                int tempX = cube.X + cubeShape[shape[0]][shape[1]][i].X;
                int tempY = cube.Y + cubeShape[shape[0]][shape[1]][i].Y;
                if (gameScreen[tempY][tempX] != 0)
                {
                    return 3;
                }
            }

            return 0;
        }

        public void Rotate()//轉動
        {
            int[] tempShape = new int[2];
            tempShape[0] = nowShape[0];
            tempShape[1] = nowShape[1];

            //下一個形狀長怎樣
            if (cubeShape[tempShape[0]].Count <= tempShape[1] + 1)
            {
                tempShape[1] = 0;
            }
            else
            {
                tempShape[1]++;
            }

            ClearNowShapeFromScreen();

            //如果有碰撞，直接畫原本的回去畫面
            if (CheckBound(nowPoint, tempShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            //新的位置
            nowShape = tempShape;
            AddShapeToScreen();
        }

        //往左
        public void GoLeft()
        {
            Point tempCube = new Point();
            tempCube = nowPoint;
            tempCube.X = nowPoint.X - 1;

            ClearNowShapeFromScreen();

            //如果有碰撞，直接畫原本的回去畫面
            if (CheckBound(tempCube, nowShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            nowPoint = tempCube;
            AddShapeToScreen();
        }

        public void GoRight()
        {
            Point tempCube = new Point();
            tempCube = nowPoint;
            tempCube.X = nowPoint.X + 1;

            ClearNowShapeFromScreen();

            //如果有碰撞，直接畫原本的回去畫面
            if (CheckBound(tempCube, nowShape) != 0)
            {
                AddShapeToScreen();
                return;
            }

            nowPoint = tempCube;
            AddShapeToScreen();
        }

        public void GoDown()
        {
            Point tempPoint = new Point();
            tempPoint = nowPoint;
            tempPoint.Y = tempPoint.Y + 1;

            ClearNowShapeFromScreen();

            //死亡判斷
            if ((CheckBound(tempPoint, nowShape) == 2 || CheckBound(tempPoint, nowShape) == 3) && nowPoint.Y == 0)
            {
                timer1.Enabled = false;//遊戲停止
                AddShapeToScreen();
                return;
            }

            //下降碰撞判斷，有碰到
            if (CheckBound(tempPoint, nowShape) == 2 || CheckBound(tempPoint, nowShape) == 3)
            {
                tempPoint.Y = tempPoint.Y - 1;//往回

                nowPoint = tempPoint;
                AddShapeToScreen();

                CheckClearRow();
                AddShapeToScreen();

                return;
            }

            nowPoint = tempPoint;
            AddShapeToScreen();
        }

        //下到底部
        public void DownToBottom()
        {
            Point tempPoint = new Point();
            tempPoint = nowPoint;
            ClearNowShapeFromScreen();

            if ((CheckBound(tempPoint, nowShape) == 2 || CheckBound(tempPoint, nowShape) == 3) && nowPoint.Y == 0)
            {
                timer1.Enabled = false;//遊戲停止
                AddShapeToScreen();
                return;
            }

            while (CheckBound(tempPoint, nowShape) == 0)
            {
                tempPoint.Y = tempPoint.Y + 1;
                if (CheckBound(tempPoint, nowShape) == 2 || CheckBound(tempPoint, nowShape) == 3)
                {
                    tempPoint.Y = tempPoint.Y - 1;

                    nowPoint = tempPoint;
                    AddShapeToScreen();

                    CheckClearRow();
                    AddShapeToScreen();

                    break;
                }
            }
        }

        public bool CheckRowAllIsCube(int row)//確定整排都有東西
        {
            for (int i = 0; i < gameWidth; i++)
                if (gameScreen[row][i] == 0)
                    return false;
            return true;
        }

        //確定有沒有消除
        public void CheckClearRow()
        {
            for (int i = nowPoint.Y; i < gameHeigh; i++)
            {
                if (CheckRowAllIsCube(i))//如果整排被填滿
                {
                    score++;//加分
                    label1.Text = score.ToString();//分數刷新

                    //消除特效
                    for (int j = 0; j < gameWidth; j++)
                    {
                        gameScreen[i][j] = 0;
                        drawComponent(gameScreen);
                        Thread.Sleep(50);
                    }

                    gameScreen.RemoveAt(i);//刪除被選取的那行

                    List<int> tempScreen = new List<int>();
                    for (int j = 0; j < rect[i].Count; j++)
                    {
                        tempScreen.Add(0);
                    }
                    gameScreen.Insert(0, tempScreen);//插回最上層
                }
            }

            CreateNewCube();//新方塊
        }

        public void CreateNewCube()
        {
            nowPoint.X = gameWidth / 2;
            nowPoint.Y = 0;

            nowShape[0] = random.Next(0, 7);
            nowShape[1] = 0;
        }

        public TetrisView()
        {
            InitializeComponent();
            pictureBox1.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            pictureBox1.Width = cubeWidth * gameWidth + 1;

            //畫圖用
            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            graphics = bufferedGraphicsContext.Allocate(pictureBox1.CreateGraphics(), pictureBox1.DisplayRectangle);

            //按鍵事件
            KeyDown += new KeyEventHandler(From1_KeyDown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitCubeShape();//所有方塊的形狀先產生好

            CreateNewCube();//產生一個新方塊

            for (int i = 0; i < cubeWidth * gameHeigh; i += cubeWidth) //rect位置初始化
            {
                List<Rectangle> tempRect = new List<Rectangle>();
                for (int j = 0; j < cubeWidth * gameWidth; j += cubeWidth)
                {
                    tempRect.Add(new Rectangle(j, i, cubeWidth, cubeWidth));
                }
                rect.Add(tempRect);
            }

            for (int i = 0; i < gameHeigh; i++)//畫面數據初始化
            {
                List<int> tempScreen = new List<int>();
                for (int j = 0; j < gameWidth; j++)
                {
                    tempScreen.Add(0);
                }
                gameScreen.Add(tempScreen);
            }

            timer1.Interval = gameSpeed;//設定遊戲速度

            AddShapeToScreen();
            drawComponent(gameScreen);
        }
        //畫圖
        public void drawComponent(List<List<int>> list)
        {
            graphics.Graphics.Clear(Color.White);//清除，底色

            for (int i = 0; i < gameHeigh; i++)//畫圖
            {
                for (int j = 0; j < gameWidth; j++)
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
                if (e.KeyCode == Keys.Up)
                {
                    Rotate();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.Down)
                {
                    GoDown();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.Left)
                {
                    GoLeft();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.Right)
                {
                    GoRight();
                    drawComponent(gameScreen);
                }
                if (e.KeyCode == Keys.Space)
                {
                    DownToBottom();
                    drawComponent(gameScreen);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GoDown();//隨時間下降

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

        //開始遊戲
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gameHeigh; i++)//初始化畫面
            {
                for (int j = 0; j < gameWidth; j++)
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

        //繼續遊戲
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            stateChanged()
        }

        //暫停遊戲
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        //離開遊戲
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void stateChanged(string state)
        {

        }
        public void updateView()
        {

        }

    }
}
