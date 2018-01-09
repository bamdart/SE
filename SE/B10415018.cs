using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE
{
    class B10415018 : TetrisView
    {
        //B10415037
        public B10415018(TetrisController con, TetrisModel m)
        {
            controller = con;
            model = m;
            this.Size = new Size(600, 700);
            this.Load += TetrisView_Load;

            InitializeComponent();
        }

        public override void drawComponent()
        {

            Color BackColor_ = new Color();
            BackColor_ = Color.FromArgb(30, 30, 30);//背景顏色
            this.BackColor = BackColor_;

            pen.Color = Color.GhostWhite;//筆刷顏色

            SolidBrush[] newBrush_ =//方塊顏色
            {
                new SolidBrush(Color.Black),//空白格子的顏色
                new SolidBrush(Color.GreenYellow),//田
                new SolidBrush(Color.DarkBlue),//倒L
                new SolidBrush(Color.DarkOrange),//L
                new SolidBrush(Color.Purple),//z
                new SolidBrush(Color.DeepPink),//倒z
                new SolidBrush(Color.Blue),//一
                new SolidBrush(Color.DarkTurquoise)//凸
            };

            Brush_ = newBrush_;//換遊戲畫面

            picBox.BackColor = BackColor_;
            picBox.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            picBox.Width = cubeWidth * gameWidth + 1;
            picBox.Location = new Point(250, 30);
            this.Controls.Add(picBox);

            startBtn.FlatStyle = FlatStyle.Flat;
            startBtn.BackColor = BackColor_;
            startBtn.ForeColor = Color.White;
            startBtn.Size = new Size(100, 50);
            startBtn.Text = "開始";
            startBtn.Location = new Point(20, 100);
            this.Controls.Add(startBtn);

            pauseBtn.FlatStyle = FlatStyle.Flat;
            pauseBtn.BackColor = BackColor_;
            pauseBtn.ForeColor = Color.White;
            pauseBtn.Size = new Size(100, 50);
            pauseBtn.Text = "暫停";
            pauseBtn.Location = new Point(20, 160);
            this.Controls.Add(pauseBtn);

            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.BackColor = BackColor_;
            exitBtn.ForeColor = Color.White;
            exitBtn.Size = new Size(100, 50);
            exitBtn.Text = "離開";
            exitBtn.Location = new Point(20, 220);
            this.Controls.Add(exitBtn);

            label.ForeColor = Color.White;

            label.Font = new Font("Consolas", 15);
            label.Location = new Point(20, 32);
            label.Text = "Score : 0";
            this.Controls.Add(label);

            //每個button對應的function
            startBtn.Click += StartBtn_Click;
            pauseBtn.Click += PauseBtn_Click;
            exitBtn.Click += ExitBtn_Click;
            timer.Tick += Timer_Tick;
        }
    }
}
