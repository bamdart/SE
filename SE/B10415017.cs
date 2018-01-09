using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE
{
    class B10415017 : TetrisView
    {
        //B10415037
        public B10415017(TetrisController con, TetrisModel m)
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
            BackColor_ = Color.LightGoldenrodYellow;//背景顏色
            this.BackColor = BackColor_;

            pen.Color = Color.Black;//筆刷顏色

            SolidBrush[] newBrush_ =//方塊顏色
            {
                new SolidBrush(BackColor_),//空白格子的顏色
                new SolidBrush(Color.LightPink),//田
                new SolidBrush(Color.LightBlue),//倒L
                new SolidBrush(Color.SlateBlue),//L
                new SolidBrush(Color.Violet),//z
                new SolidBrush(Color.LightGreen),//倒z
                new SolidBrush(Color.Orange),//一
                new SolidBrush(Color.Red)//凸
            };

            Brush_ = newBrush_;//換遊戲畫面

            picBox.BackColor = BackColor_;
            picBox.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            picBox.Width = cubeWidth * gameWidth + 1;
            picBox.Location = new Point((this.Width- picBox.Width)/2, 10);
            this.Controls.Add(picBox);

            startBtn.FlatStyle = FlatStyle.Flat;
            startBtn.BackColor = BackColor_;
            startBtn.ForeColor = Color.Black;
            startBtn.Size = new Size(75, 25);
            startBtn.Text = "開始";
            startBtn.Location = new Point((this.Width- startBtn .Width)/2- 100, this.Height- startBtn .Height- 50);
            this.Controls.Add(startBtn);

            pauseBtn.FlatStyle = FlatStyle.Flat;
            pauseBtn.BackColor = BackColor_;
            pauseBtn.ForeColor = Color.Black;
            pauseBtn.Size = new Size(75, 25);
            pauseBtn.Text = "暫停";
            pauseBtn.Location = new Point((this.Width- pauseBtn.Width)/2, this.Height - pauseBtn.Height- 50);
            this.Controls.Add(pauseBtn);

            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.BackColor = BackColor_;
            exitBtn.ForeColor = Color.Black;
            exitBtn.Size = new Size(75, 25);
            exitBtn.Text = "離開";
            exitBtn.Location = new Point((this.Width- exitBtn .Width)/2+ 100, this.Height - exitBtn.Height- 50);
            this.Controls.Add(exitBtn);

            label.ForeColor = Color.Black;
            label.Font = new Font("Consolas", 12);
            label.Location = new Point(this.Width-125, 32);
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
