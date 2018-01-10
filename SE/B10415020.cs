using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE
{
    class B10415020 : TetrisView
    {

        //B10415020
        public B10415020(TetrisController con, TetrisModel m)
        {
            controller = con;
            model = m;
            this.Size = new Size(600, 660);
            this.Load += TetrisView_Load;
        }

        public override void updateView()
        {            
            List<List<int>> gameScreen = model.getScreen();
            List<List<Rectangle>> rect = model.getRect();
            graphics.Graphics.Clear(Color.White);//清除，底色

            graphics.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//反鋸齒

            //Image im = new Bitmap("");
            for (int i = 0; i < gameHeigh; i++)//畫圖
            {
                for (int j = 0; j < gameWidth; j++)
                {
                    graphics.Graphics.FillEllipse(Brush_[gameScreen[i][j]], rect[i][j]);//填滿顏色
                    graphics.Graphics.DrawRectangle(pen, rect[i][j]);//格線
                }
            }

            graphics.Render(picBox.CreateGraphics());
        }

        public override void drawComponent()
        {

            Color BackColor_ = new Color();
            BackColor_ = Color.BurlyWood;//背景顏色

            this.BackgroundImage= new Bitmap("wood.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = BackColor_;

            pen.Color = Color.White;//格線筆刷顏色
         
            SolidBrush[] newBrush_ =//方塊顏色
            {
                new SolidBrush(Color.WhiteSmoke),//空白格子的顏色
                new SolidBrush(Color.Coral),//田
                new SolidBrush(Color.Cyan),//倒L
                new SolidBrush(Color.Fuchsia),//L
                new SolidBrush(Color.Gold),//z
                new SolidBrush(Color.Indigo),//倒z
                new SolidBrush(Color.Khaki),//一
                new SolidBrush(Color.Maroon)//凸
            };

            Brush_ = newBrush_;//換遊戲畫面

            picBox.BackColor = BackColor_;
            picBox.Height = cubeWidth * gameHeigh + 1;//設定遊戲視窗大小
            picBox.Width = cubeWidth * gameWidth + 1;
            picBox.Location = new Point(250, 10);
            this.Controls.Add(picBox);

            startBtn.FlatStyle = FlatStyle.Flat;
            startBtn.BackColor = BackColor_;
            startBtn.ForeColor = Color.Black;
            startBtn.Size = new Size(75, 25);
            startBtn.Font= new Font("Consolas", 10, System.Drawing.FontStyle.Bold);
            startBtn.Text = "開始";
            startBtn.Location = new Point(20, 100);
            this.Controls.Add(startBtn);

            pauseBtn.FlatStyle = FlatStyle.Flat;
            pauseBtn.BackColor = BackColor_;
            pauseBtn.ForeColor = Color.Black;
            pauseBtn.Size = new Size(75, 25);
            pauseBtn.Font = new Font("Consolas", 10, System.Drawing.FontStyle.Bold);
            pauseBtn.Text = "暫停";
            pauseBtn.Location = new Point(20, 165);
            this.Controls.Add(pauseBtn);

            exitBtn.FlatStyle = FlatStyle.Flat;
            exitBtn.BackColor = BackColor_;
            exitBtn.ForeColor = Color.Black;
            exitBtn.Size = new Size(75, 25);
            exitBtn.Font = new Font("Consolas", 10, System.Drawing.FontStyle.Bold);
            exitBtn.Text = "離開";
            exitBtn.Location = new Point(20, 230);
            this.Controls.Add(exitBtn);

            label.ForeColor = Color.Black;

            label.Font = new Font("Consolas", 30, System.Drawing.FontStyle.Bold);
            label.Size= new System.Drawing.Size(240, 50);
            label.Location = new Point(0, 32);
            label.Text = "Score : 10";
            this.Controls.Add(label);

            //每個button對應的function
            startBtn.Click += StartBtn_Click;
            pauseBtn.Click += PauseBtn_Click;
            exitBtn.Click += ExitBtn_Click;
            timer.Tick += Timer_Tick;
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // B10415020
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "B10415020";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
