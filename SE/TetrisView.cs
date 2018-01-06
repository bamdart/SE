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
        public Label label = new Label();
        private Button startBtn = new Button();
        private Button pauseBtn = new Button();
        private Button exitBtn = new Button();
        private PictureBox picBox = new PictureBox();
        BufferedGraphicsContext bufferedGraphicsContext;//buffer
        BufferedGraphics graphics;//畫圖用
        public string IDLE_STATE = "IDLE",
              START_STATE = "START",
              PLAY_STATE = "PLAY",
              DOWN_STATE = "DOWN",
              LEFT_STATE = "LEFT",
              RIGHT_STATE = "RIGHT",
              TOBUTTOM_STATE = "TOBUTTOM",
              ROTATE_STATE = "ROTATE",
              CLEAR_STATE = "CLEAR",
              PAUSE_STATE = "PAUSE",
              STOP_STATE = "STOP",
              CONTINUE_STATE = "CONTINUE",
              EXIT_STATE = "EXIT";
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

        Random random = new Random(Guid.NewGuid().GetHashCode());//用來隨機產生方塊種類


        public TetrisView(TetrisController con,TetrisModel m)
        {
            controller = con;
            model = m;
            this.Size = new Size(600, 600);
            this.Load += TetrisView_Load;
            InitializeComponent();

        }

        private void TetrisView_Load(object sender, EventArgs e)
        {
            controller.userHasInput(IDLE_STATE);
        }

        public virtual void drawComponent()
        {
            picBox.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            picBox.Width = cubeWidth * gameWidth + 1;
            picBox.Location = new Point(10, 10);
            this.Controls.Add(picBox);
            startBtn.Size = new Size(75, 25);
            startBtn.Text = "開始";
            startBtn.Location = new Point(440, 60);
            this.Controls.Add(startBtn);
            pauseBtn.Size = new Size(75, 25);
            pauseBtn.Text = "暫停";
            pauseBtn.Location = new Point(440, 125);
            this.Controls.Add(pauseBtn);
            exitBtn.Size = new Size(75, 25);
            exitBtn.Text = "離開";
            exitBtn.Location = new Point(440, 190);
            this.Controls.Add(exitBtn);
            label.Location = new Point(440, 12);
            label.Text = "Score:0";
            this.Controls.Add(label);
            //
            startBtn.Click += StartBtn_Click;
            pauseBtn.Click += PauseBtn_Click;
            exitBtn.Click += ExitBtn_Click;
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            controller.userHasInput(model.DOWN_STATE);
        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            controller.userHasInput(model.EXIT_STATE);
        }

        private void PauseBtn_Click(object sender, EventArgs e)
        {
            if (model.getState() != model.PAUSE_STATE)
            {
                pauseBtn.Text = "繼續";
                controller.userHasInput(model.PAUSE_STATE);
            }
            else
            {
                pauseBtn.Text = "暫停";
                controller.userHasInput(model.PLAY_STATE);
            }
        }

        private void StartBtn_Click(object sender, EventArgs e)
        {
            controller.userHasInput(model.START_STATE);
        }

        public TetrisView()
        {
            //InitializeComponent();
            //畫圖用
            bufferedGraphicsContext = BufferedGraphicsManager.Current;
            graphics = bufferedGraphicsContext.Allocate(picBox.CreateGraphics(), picBox.DisplayRectangle);

        }

        //Override ProcessCmdKey
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (timer.Enabled == true)
            {
                if (keyData == Keys.Up)
                {
                    controller.userHasInput(keyData);
                    return true;
                }
                if (keyData == Keys.Down)
                {
                    controller.userHasInput(keyData);
                    return true;
                }
                if (keyData == Keys.Left)
                {
                    controller.userHasInput(keyData);
                    return true;
                }
                if (keyData == Keys.Right)
                {
                    controller.userHasInput(keyData);
                    return true;
                }
                if (keyData == Keys.Space)
                {
                    controller.userHasInput(keyData);
                    return true;
                }
            }
            // Call the base class
            return base.ProcessCmdKey(ref msg, keyData);
        }
        ////畫圖
        //public void updateView(List<List<int>> list)
        //{
        //    graphics.Graphics.Clear(Color.White);//清除，底色

        //    for (int i = 0; i < gameHeigh; i++)//畫圖
        //    {
        //        for (int j = 0; j < gameWidth; j++)
        //        {
        //            graphics.Graphics.FillRectangle(Brush[list[i][j]], rect[i][j]);//填滿顏色
        //            graphics.Graphics.DrawRectangle(pen, rect[i][j]);//格線
        //        }
        //    }

        //    graphics.Render(pictureBox1.CreateGraphics());
        //}

        public void updateView()
        {
            List<List<int>> gameScreen = model.getScreen();
            List<List<Rectangle>> rect = model.getRect();
            graphics.Graphics.Clear(Color.White);//清除，底色

            for (int i = 0; i < gameHeigh; i++)//畫圖
            {
                for (int j = 0; j < gameWidth; j++)
                {
                    graphics.Graphics.FillRectangle(Brush[gameScreen[i][j]], rect[i][j]);//填滿顏色
                    graphics.Graphics.DrawRectangle(pen, rect[i][j]);//格線
                }
            }

            graphics.Render(picBox.CreateGraphics());
        }

        //讀按鍵功能
        private void From1_KeyDown(object sender, KeyEventArgs e)
        {
            //Move to ProcessCmdKey(...)

            //Console.WriteLine(e.KeyCode);
            //if (timer1.Enabled == true)
            //{
            //    if (e.KeyCode == Keys.Up)
            //    {
            //        Rotate();
            //        drawComponent(gameScreen);
            //    }
            //    if (e.KeyCode == Keys.Down)
            //    {
            //        GoDown();
            //        drawComponent(gameScreen);
            //    }
            //    if (e.KeyCode == Keys.Left)
            //    {
            //        GoLeft();
            //        drawComponent(gameScreen);
            //    }
            //    if (e.KeyCode == Keys.Right)
            //    {
            //        GoRight();
            //        drawComponent(gameScreen);
            //    }
            //    if (e.KeyCode == Keys.Space)
            //    {
            //        DownToBottom();
            //        drawComponent(gameScreen);
            //    }
            //}
        }


        public void stateChanged(string state)
        {
            if (state.Equals(model.IDLE_STATE))
            {
                drawComponent();
            }
            if (state.Equals(model.LEFT_STATE) ||
                state.Equals(model.RIGHT_STATE) ||
                    state.Equals(model.DOWN_STATE) ||
                state.Equals(model.TOBUTTOM_STATE) ||
                state.Equals(model.ROTATE_STATE))
            {
                updateView();
            }

            if (state.Equals(model.START_STATE))
            {
                timer.Enabled = true;
                updateView();
            }
            if (state.Equals(model.PLAY_STATE))
            {

            }

            if (state.Equals(model.CLEAR_STATE))
            {

            }
            if (state.Equals(model.PAUSE_STATE))
            {

            }
            if (state.Equals(model.CONTINUE_STATE))
            {

            }
            if (state.Equals(model.STOP_STATE))
            {

            }

        }
    }
}
