using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE
{
    public class TetrisController
    {
        private TetrisModel model;
        private TetrisView view;
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

        public TetrisController()
        {
            //   view = new TetrisView(this,model);
            view = new B10415020(this, model);
            model = new TetrisModel(view);
            model.setView(view);
        }

        public TetrisView getView()
        {
            return view;
        }

        public void userHasInput(string userInput)
        {
           // Console.Write(userInput + "\n");
            if (userInput.Equals(DOWN_STATE))
            {
                model.GoDown();

                if (view.timer.Enabled ==true)
                    model.setState(DOWN_STATE);
                else if(view.timer.Enabled == false)
                    userHasInput(STOP_STATE);
                /**/
            }
            if (userInput.Equals(IDLE_STATE))
            {
                model.setState(IDLE_STATE);
            }
            if (userInput.Equals(START_STATE))
            {
                model.initModel();
                model.setState(START_STATE);
            }
            if (userInput.Equals(PLAY_STATE))
            {
                model.gameContinue();
                model.setState(PLAY_STATE);
            }

            if (userInput.Equals(CLEAR_STATE))
            {
                model.CheckClearRow();
                model.setState(CLEAR_STATE);
            }
            if (userInput.Equals(PAUSE_STATE))
            {
                if (model.getState() == START_STATE|| model.getState() == DOWN_STATE)
                {
                    model.pause();
                    model.setState(PAUSE_STATE);
                }
            }
            if (userInput.Equals(CONTINUE_STATE))
            {
                model.gameContinue();
                model.setState(CONTINUE_STATE);
            }
            if (userInput.Equals(STOP_STATE))
            {
                model.setState(STOP_STATE);
            }
            if (userInput.Equals(EXIT_STATE))
            {
                model.exit();
                model.setState(EXIT_STATE);
            }
        }
        public void userHasInput(Keys keydcode)
        {
            if (keydcode == Keys.Left)
            {
                model.GoLeft();
                model.setState(LEFT_STATE);
            }
            if (keydcode == Keys.Right)
            {
                model.GoRight();
                model.setState(RIGHT_STATE);
            }
            if (keydcode == Keys.Down)
            {
                model.GoDown();
                model.setState(DOWN_STATE);
            }

            if (keydcode == Keys.Up)
            {
                model.Rotate();
                model.setState(ROTATE_STATE);
            }
            if (keydcode == Keys.Space)
            {
                model.DownToBottom();
                model.setState(TOBUTTOM_STATE);
            }
        }
    }
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            TetrisController controller = new TetrisController();
            Application.Run(controller.getView());

        }
    }
}
