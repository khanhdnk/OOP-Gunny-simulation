using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class ScoreBoard: GameBlock
    {
        private Bitmap sbImage;
        private DrawingOptions Opts;
        private DrawingOptions drawingOptions;
        private Bitmap playerImage;
        private Player player1;
        private Player player2;
        private Window _window;
        private GameManager _gameManager;
        public ScoreBoard(double x, double y, Player player1, Player player2, Window window, GameManager gameManager): base(x, y) {
            _gameManager = gameManager;
            _window = window;
            sbImage = SplashKit.LoadBitmap("sbimage", "scoreboard.png");
            Opts = new DrawingOptions()
            {
                Dest = _window,
                AnchorOffsetX = 0,
                AnchorOffsetY = 0,
                ScaleX = 1,
                ScaleY = 1,
                Angle = 0,
                FlipY = false


            };


            drawingOptions = new DrawingOptions()
            {
                Dest = _window,
                AnchorOffsetX = 0,
                AnchorOffsetY = 0,
                ScaleX = 1,
                ScaleY = 1,
                Angle = 0,
                FlipY = false


            };
            drawingOptions = SplashKit.OptionToScreen();

            Opts = SplashKit.OptionToScreen();
            this.player1 = player1;
            this.player2 = player2;
            playerImage = SplashKit.LoadBitmap("playerImage", "redplayerresize.png");
        }

        public string FormatTime(double countTime)
        {
            // Calculate minutes and seconds
            int minutes = (int)(countTime / 60);
            int seconds = (int)(countTime % 60);

            // Format the time as "00:00"
            string formattedTime = $"{minutes:D2}:{seconds:D2}";

            return formattedTime;
        }

        public void Draw()
        {
            SplashKit.DrawBitmap(sbImage, X,Y, drawingOptions);

            Opts.FlipY = false;
            SplashKit.DrawBitmap(playerImage, 275, 10, Opts);
            SplashKit.FillRectangle(Color.Black, 330 - 3, 15 - 3, 100 * 1.8 + 6, 15 + 6, Opts);
            SplashKit.FillRectangle(Color.Red, 330, 15, player1.OldHP * 1.8, 15, Opts);

            SplashKit.FillRectangle(Color.Black, 330 - 3, 37 - 3, 100 * 1.8 + 6, 15 + 6, Opts);
            SplashKit.FillRectangle(Color.Blue, 330, 37, player1.Mana * 1.8, 15, Opts);


            Opts.FlipY = true;
            SplashKit.DrawBitmap(playerImage, 1220, 10, Opts);
            SplashKit.FillRectangle(Color.Black, 1035 + 100 * 1.8 + 3, 15 - 3, -100 * 1.8 -6, 15 + 6, Opts);
            SplashKit.FillRectangle(Color.Red, 1035 + 100 * 1.8, 15, -player2.OldHP * 1.8, 15, Opts);

            SplashKit.FillRectangle(Color.Black, 1035 + 100 * 1.8 + 3, 37 - 3, -100 * 1.8 - 6, 15 + 6, Opts);
            SplashKit.FillRectangle(Color.Blue, 1035 + 100 * 1.8 , 37, -player2.Mana * 1.8, 15, Opts);

            SplashKit.DrawText($"Round", Color.Yellow, "fontGame", 17, 745, 10, drawingOptions);
            SplashKit.DrawText($"{_gameManager.RoundsCount + 1}/3", Color.Orange, "fontGame", 17, 765, 30, drawingOptions);

            //border for points
            SplashKit.FillRectangle(Color.Black, 280, 60, 20, 20, drawingOptions);
            SplashKit.FillRectangle(Color.Black, 320, 60, 20, 20, drawingOptions);
            //points
            if (_gameManager.P1Points > 0)
            {
                SplashKit.FillRectangle(Color.Red, 280+ 2, 60 +2 , 20 - 4, 20 - 4, drawingOptions);

            }
            if (_gameManager.P1Points >= 2)
            {

                SplashKit.FillRectangle(Color.Red, 320 + 2, 60 + 2, 20- 4, 20 - 4, drawingOptions);
            }
            //if (_gameManager.P1Points)

            SplashKit.FillRectangle(Color.Black, 1168, 60, 20, 20, drawingOptions);
            SplashKit.FillRectangle(Color.Black, 1168 + 40, 60, 20, 20, drawingOptions);
            if (_gameManager.P2Points >0)
            {
                SplashKit.FillRectangle(Color.Red, 1168 + 2, 60 + 2, 20 - 4, 20 - 4, drawingOptions);

            }
            if (_gameManager.P2Points >= 2)
            {
                SplashKit.FillRectangle(Color.Red, 1168 + 40 + 2, 60 + 2, 20 - 4, 20 - 4, drawingOptions);

            }

            SplashKit.DrawText(FormatTime(_gameManager.CountTime).ToString(), Color.White, "fontGame", 15, 880, 70, drawingOptions);









        }
    }
}
