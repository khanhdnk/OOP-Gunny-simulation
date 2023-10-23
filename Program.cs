using System;
using System.Runtime.InteropServices;
using SplashKitSDK;
using static System.Net.Mime.MediaTypeNames;

namespace Gunny
{
    public class Program
    {
        public const int SCREEN_BORDER = 200;
        public const int LeftScreenBorder = 0;
        //public const long NOW = DateTime.UtcNow.Ticks;
        public static void Main()
        {
            //string filePath = "data.txt";

            //// Data to be written to the file
            //string dataToWrite = "Hello, this is some data that will be written to the file.";

            //// Write the data to the file
            //using (StreamWriter writer = new StreamWriter(filePath))
            //{
            //    writer.Write(dataToWrite);
            //}


            //// Read the data from the file
            //using (StreamReader reader = new StreamReader(filePath))
            //{
            //    string dataRead = reader.ReadToEnd();
            //    Console.WriteLine("Data read from the file:");
            //    Console.WriteLine(dataRead);
            //}
            int screenWidth = 1536;
            int screenHeight = 864;
            bool togleF = true;
            //Player _player = new Player("dnk", "the darkness inside your heart", 20, 20);
            //Player player2 = new Player("dnk", "the darkness inside your heart", 1000, 200);
            SplashKit.LoadFont("fontGame", "BruceForeverRegular-X3jd2.ttf");
            SplashKit.LoadFont("fontRoboto", "Roboto-Regular.ttf");

            Bitmap images = SplashKit.LoadBitmap("ninja" ,"ninja.png");
            Bitmap lugach = SplashKit.LoadBitmap("lugach" ,"lugach.png");
            Color backgroundColor = Color.Wheat;

            Window window = new Window("Gunny", screenWidth, screenHeight);
            if (togleF)
            {
                //window.ToggleFullscreen();

            }
            GameManager gameManager = new GameManager(screenWidth, screenHeight, window);
            Player player1 = gameManager.Player1;
            Player player2 = gameManager.Player2;

            DrawingOptions Opts = new DrawingOptions()
            {
                Dest = window,
                AnchorOffsetX = 0,
                AnchorOffsetY = 0,
                ScaleX = 1, ScaleY = 1,
                Angle = 0
               
                
            };
            Bitmap explode = SplashKit.LoadBitmap("explode", "explode.png");
            explode.SetCellDetails(180, 180, 5, 2, 7);
            AnimationScript animationScript = SplashKit.LoadAnimationScript("script", "script.txt");
            Animation animationTest = SplashKit.CreateAnimation(animationScript, "explode");
            DrawingOptions option;
            option = SplashKit.OptionWithAnimation(animationTest);
            SplashKit.LoadSoundEffect("explodesound", "explodesound.mp3");


            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(backgroundColor);

                Opts.Angle += 4;
                player1  = gameManager.Player1;

                //SplashKit.FillRectangle(Color.Blue, 0, 0, 400, 400);
                //SplashKit.FillCircle(Color.Red, XPos, YPos, 20);
                //SplashKit.DrawBitmap(images, _player.X, _player.Y);
                //SplashKit.DrawBitmap(images, player2.X, player2.Y);
                gameManager.Update();
                gameManager.Draw();

                //SplashKit.DrawBitmap(explode, 20, 20, option);
                //if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                //{
                //    SplashKit.UpdateAnimation(animationTest);
                //}
                //Console.WriteLine("System ticks: " + systemTicks);

                //SplashKit.DrawBitmap(lugach, XPos + lugach.Width/2 - cameraPos[0], YPos + lugach.Height/2, Opts);

                SplashKit.RefreshScreen(60);
            } while (!window.CloseRequested);
        }
    }
}

