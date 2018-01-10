using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE
{
    class B10415002 : TetrisView
    {
        //B10415002
        public B10415002(TetrisController con, TetrisModel m)
        {
            controller = con;
            model = m;
            this.Size = new Size(600, 700);
            this.Load += TetrisView_Load;

            InitializeComponent();
        }

        public override void drawComponent()
        {

            this.BackColor = Color.FromArgb(0xD2, 0xE9, 0xFF);

            pen.Color = Color.White;//筆刷顏色

            SolidBrush[] newBrush_ =//方塊顏色
            {
                new SolidBrush(Color.LightGray),//空白格子的顏色
                new SolidBrush(Color.Aqua),//田
                new SolidBrush(Color.Bisque),//倒L
                new SolidBrush(Color.LightSalmon),//L
                new SolidBrush(Color.Tan),//z
                new SolidBrush(Color.SteelBlue),//倒z
                new SolidBrush(Color.Tomato),//一
                new SolidBrush(Color.Fuchsia)//凸
            };

            Brush_ = newBrush_;//換遊戲畫面

            picBox.BackColor = Color.FromArgb(0xD2, 0xE9, 0xFF);
            picBox.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            picBox.Width = cubeWidth * gameWidth + 1;
            picBox.Location = new Point(250, 10);
            picBox.BackColor = Color.FromArgb(0xFF, 0xEE, 0xDD);
            this.Controls.Add(picBox);




            startBtn.Size = new Size(90, 50);
            startBtn.Text = "開始";
            startBtn.Font = new Font("Arial", 14, FontStyle.Bold);
            startBtn.Location = new Point(40, 140);
            startBtn.BackColor = Color.White;
            this.Controls.Add(startBtn);



            pauseBtn.Size = new Size(90, 50);
            pauseBtn.Text = "暫停";
            pauseBtn.Font = new Font("Arial", 14, FontStyle.Bold);
            pauseBtn.Location = new Point(40, 240);
            pauseBtn.BackColor = Color.White;
            this.Controls.Add(pauseBtn);



            exitBtn.Size = new Size(90, 50);
            exitBtn.Text = "Exit";
            exitBtn.Font = new Font("Arial", 14, FontStyle.Bold);
            exitBtn.Location = new Point(40, 340);
            exitBtn.BackColor = Color.White;
            this.Controls.Add(exitBtn);

            label.ForeColor = Color.Green;
            label.Font = new Font("Arial", 20, FontStyle.Bold | FontStyle.Underline);
            label.Location = new Point(40, 40);
            label.Text = "Score : 0";
            label.Size = new Size(400, 100);
            this.Controls.Add(label);

            this.Text = "B10415002 View";

            //每個button對應的function
            startBtn.Click += StartBtn_Click;
            pauseBtn.Click += PauseBtn_Click;
            exitBtn.Click += ExitBtn_Click;
            timer.Tick += Timer_Tick;
        }
    }
}
