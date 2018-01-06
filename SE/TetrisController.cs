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
            model = new TetrisModel(view);
            view = new TetrisView(this,model);
            model.setView(view);
        }
        public TetrisView getView()
        {
            return view;
        }

        public void userHasInput(string userInput)
        {
            if (userInput.Equals(DOWN_STATE))
            {
                model.setState(DOWN_STATE);
                model.GoDown();
            }
            if (userInput.Equals(IDLE_STATE))
            {
                model.setState(IDLE_STATE);
                view.drawComponent();
            }
            if (userInput.Equals(START_STATE))
            {
                model.setState(START_STATE);
                model.initModel();
            }
            if (userInput.Equals(PLAY_STATE))
            {
                model.setState(PLAY_STATE);
            }

            if (userInput.Equals(CLEAR_STATE))
            {
                model.CheckClearRow();
                model.setState(CLEAR_STATE);
            }
            if (userInput.Equals(PAUSE_STATE))
            {
                model.pause();
                model.setState(PAUSE_STATE);
            }
            if (userInput.Equals(CONTINUE_STATE))
            {
                model.gameContinue();
                model.setState(CONTINUE_STATE);
            }
            if (userInput.Equals(STOP_STATE))
            {
                model.exit();
                model.setState(STOP_STATE);
            }
            if (userInput.Equals(EXIT_STATE))
            {
                model.setState(DOWN_STATE);
                model.GoDown();
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
                model.setState(RIGHT_STATE);
                model.GoRight();

            }
            if (keydcode == Keys.Down)
            {
                model.setState(TOBUTTOM_STATE);
                model.DownToBottom();

            }
            if (keydcode == Keys.Space)
            {
                model.setState(ROTATE_STATE);
                model.Rotate();

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
