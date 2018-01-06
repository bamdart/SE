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
        public static string IDLE_STATE = "IDLE",
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
                             CONTINUE_STATE="CONTINUE";
        public TetrisController()
        {
            view = new TetrisView(this);
            model = new TetrisModel(view);
        }
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            TetrisController controller = new TetrisController();
            start();

        }
        public static void start()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(view);
        }
        public void userHasInput(string userInput)
        {
            if (userInput.Equals(IDLE_STATE))
            { }
            if (userInput.Equals(START_STATE))
            { }
            if (userInput.Equals(PLAY_STATE))
            { }
            if (userInput.Equals(DOWN_STATE))
            { model.GoDown(); }
            if (userInput.Equals(LEFT_STATE))
            { model.GoLeft(); }
            if (userInput.Equals(RIGHT_STATE))
            { model.GoRight(); }
            if (userInput.Equals(TOBUTTOM_STATE))
            { model.DownToBottom(); }
            if (userInput.Equals(ROTATE_STATE))
            { model.Rotate(); }
            if (userInput.Equals(CLEAR_STATE))
            { model.CheckClearRow(); }
            if (userInput.Equals(PAUSE_STATE))
            { model.pause(); }
            if (userInput.Equals(CONTINUE_STATE))
            { model.gameContinue(); }
            if (userInput.Equals(STOP_STATE))
            { model.exit(); }
        }
    }
}
