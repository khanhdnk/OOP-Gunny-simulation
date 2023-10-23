using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Gunny
{
    public class GameManager
    {
        private Player player1;
        private Player player2;
        private string turn;
        //private bool onShooting;
        private string gameState;
        private Button playerButton;
        private Button helpButton;
        private Button quitButton;
        private Button goBackButton;
        private int screenWidth;
        private int screenHeight;
        private List<Player> _gameblocks;
        private long now;
        private long now2;
        private long now3;
        private bool resetNow;
        private List<HitBox> grounds;
        private List<string> helpList;
        private Window _window;
        private bool resetNow2 = true;
        Bitmap bacground = SplashKit.LoadBitmap("background", "bg.jpg");
        private DrawingOptions Opts;
        private int screenBorder;
        private SoundEffect music;
        private bool playMusic;
        double opa = 1;
        private double roundsCount;
        private ScoreBoard scoreBoard;
        private double countTime;
        private long clock;
        private double p1Points;
        private double p2Points;
        private double p1Shots;
        private double p1SucShots;
        private double p2Shots;
        private double p2SucShots;
        private bool unlock;




        //Font font = SplashKit.LoadFont("Calibri", "Calibri-Regular.ttf");


        public GameManager(int _screenWidth, int _screenHeight, Window window) 
        {
            //set up the game as well as reset the game when play again
            ResetGame(_screenWidth, _screenHeight, window);
            screenBorder = 50;
            roundsCount = 0;
            p1Points = 0;
            p1Shots = 0;
            p1SucShots = 0;

            p2Points = 0;
            p2Shots = 0;
            p2SucShots = 0;

        }

        public void ResetGame(int _screenWidth, int _screenHeight, Window window)
        {
            playMusic = true;
            music = SplashKit.LoadSoundEffect("bgmusic", "bgmusic.mp3");
            _window = window;
            grounds = new List<HitBox>();
            for (int i = 0; i < 60; i++)
            {
                grounds.Add(new Ground(i * 60, 800, 60, 30, "block"));
                for(int j = 1; j < 9; j++)
                {
                    grounds.Add(new Ground(i * 60, 800 + j*30, 60, 30, "block"));

                }
            }

            for(int k = 0; k < 6; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int i = 0; i < 5 - j*2; i++)
                    {
                        grounds.Add(new Ground(i * 60 + 100 + (k * 700) + j * 60, 750 - j * 50, 60, 50, "dirt"));
                    }
                }

            }
            for (int i = 0; i < 20; i++)
            {
                grounds.Add(new Ground(i * 200 + 50, 200, 60, 30, "block"));
                grounds.Add(new Ground(i * 200 + 50 + 60, 200, 60, 30, "block"));
            }
            //for (int j = 0; j < 3; j++)
            //{
            //    for (int i = 0; i < 6 - j; i++)
            //    {
            //        grounds.Add(new Ground(i * 60 + 100, 770 - j * 30, 60, 30));
            //    }
            //}
            //int j = 4;
            //while()
            player1 = new Player("dnk", "the darkness inside your heart", 500, 730, grounds, _window);
            player2 = new Player("dnkst", "the darkness inside your heart", 1000, 730, grounds, _window);
            turn = "p1";
            //onShooting = true;
            gameState = "menu";
            playerButton = new Button(_screenWidth / 2 - 100, _screenHeight / 2 - 110 - 150, 200, 100, "game", "Play", 743, 209);
            helpButton = new Button(_screenWidth / 2 - 100, _screenHeight / 2 + 110 - 150, 200, 100, "help", "Help?", 743, 428);
            quitButton = new Button(_screenWidth / 2 - 100, _screenHeight / 2 + 110, 200, 100, "quit", "History", 726, 577);
            goBackButton = new Button(5, 5, 100, 70, "menu", "Back", 27, 20);
            screenWidth = _screenWidth;
            screenHeight = _screenHeight;
            _gameblocks = new List<Player>
            {
                player1,
                player2
            };
            player1.GameBlocks(_gameblocks);
            player2.GameBlocks(_gameblocks);
            now = DateTime.UtcNow.Ticks;
            now2 = DateTime.UtcNow.Ticks;
            now3 = DateTime.UtcNow.Ticks;
            clock = DateTime.UtcNow.Ticks; 
            resetNow = true;
            helpList = new List<string>();
            //GetData(_gameblocks);
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
            Opts = SplashKit.OptionToScreen(Opts);
            opa = 1;
            scoreBoard = new ScoreBoard(268,0, player1, player2, _window, this);
            UpdateCameraPosition(SplashKit.ToWorld(SplashKit.MousePosition()), "reset");
            countTime = 0;
            unlock = true;





        }

        public void GetGameState()
        {
            
            if (playerButton.GetState() == "game")
            {
                gameState = playerButton.GetState();
            }
            else if (helpButton.GetState() == "help")
            {
                gameState = helpButton.GetState();
            }
            else if (quitButton.GetState() == "quit")
            {
                gameState = quitButton.GetState();
            }
            else if (goBackButton.GetState() == "menu")
            {
                gameState = goBackButton.GetState();
            }

        }

        public void GetData()
        {
            string filePath = "data.txt";
            List<string> lines = new List<string>();
            try
            {
                // Open the file with StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read the first line
                    while(!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        lines.Add(line);
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");

            }
            for(int a = 0; a < lines.Count; a++) {
                SplashKit.DrawText(lines[a], Color.Black, "fontName", 14, 200, 100 + a * 25);
            }
        }

        public double RoundsCount
        {
            get
            {
                return roundsCount;
            }
            set
            {
                roundsCount = value;
            }
        }

        public double CountTime
        {
            get
            {
                return countTime;
            }
            set { countTime = value; }
        }

        public double P1Points
        {
            get { return p1Points; }
            set { p1Points = value; }
        }
        public double P2Points
        {
            get { return p2Points; }
            set { p2Points = value; }
        }

        public void Update()
        {
            if (gameState == "game")
            {
                long currentTime = DateTime.UtcNow.Ticks; 
                if (currentTime - clock > 10000000)
                {
                    countTime++;
                    clock = currentTime;
                }
                if (turn == "p1")
                {
                    if (player1.TurnDelay > 0)
                    {
                        player1.TurnDelay--;
                        player1.Shooted = true;
                    }
                    else
                    {
                        if(resetNow2 == true)
                        {
                            player1.FinishTurn = false;
                            player1.ResetCoolDown();
                            player1.CheckInvisible();
                            unlock = true;
                            resetNow2= false;

                        }
                        player1.PlayerMove(turn);
                        player1.IsTurn = true;
                    }

                    if (player1.Shooted && player1.FinishTurn)
                    {
                        player2.Shooted = false;
                        player1.IsTurn = false;
                        turn = "p2";
                        resetNow2 = true;
                    }

                }
                else
                {
                    if (player2.TurnDelay > 0)
                    {
                        player2.TurnDelay--;

                        player2.Shooted = true;
                    }
                    else
                    {
                        if (resetNow2)
                        {
                            player2.FinishTurn = false;
                            player2.CheckInvisible();
                            player2.ResetCoolDown();
                            unlock = true;
                            resetNow2 = false;


                        }
                        player2.PlayerMove(turn);
                        player2.IsTurn = true;
                    }
                    if (player2.Shooted && player2.FinishTurn)
                    {

                        player1.Shooted = false;
                        player2.IsTurn= false;
                        turn = "p1";
                        resetNow2 = true;
                    }

                }


                if ((player1.HP <= 0) || player2.HP <= 0)
                {
                    SplashKit.DrawText("You're a loser haha!", Color.Black, "fontName", 20, 500, 500);
                    
                    long currentTick = DateTime.UtcNow.Ticks;
                    if (resetNow == true)
                    {
                        now = DateTime.UtcNow.Ticks;
                        resetNow = false;
                        UpdateFigures();
                    }
                    if (currentTick - now > 50000000)
                    {
                        roundsCount++;
                        if(roundsCount < 3 && p1Points < 2 && p2Points < 2)
                        {
                            ResetGame(screenWidth, screenHeight, _window);
                            gameState = "game";



                        }
                        else
                        {
                            SplashKit.StopSoundEffect(music);
                            SaveData();

                            ResetGame(screenWidth, screenHeight, _window);
                            gameState = "menu";
                            ResetFigures();


                        }
                        currentTick = now;

                    }


                    //await Task.Delay(5000);

                    //gameState = "menu";
                    //currentTick = now;



                    //System.Environment.Exit(0);
                }




            }

            if (gameState == "menu")
            {
                GetGameState();

            }
        }

        public void ResetFigures()
        {
            roundsCount = 0;
            p1Points = 0;
            p1SucShots = 0;
            p1Shots = 0;

            p2Points = 0;
            p2SucShots = 0;
            p2Shots = 0;

        }

        public void UpdateFigures()
        {

            double exchangeNum;
            //exchangeNum = player1.NumberOfSuccessShots;
            //player1.NumberOfSuccessShots = player2.NumberOfSuccessShots;
            //player2.NumberOfSuccessShots = exchangeNum;

            p1Shots += player1.NumberOfShots;
            p1SucShots += player1.NumberOfSuccessShots;
            p2Shots += player2.NumberOfShots;
            p2SucShots += player2.NumberOfSuccessShots;
            //roundsCount++;
            if (player1.HP <= 0)
            {
                p2Points++;
            }
            else if (player2.HP <= 0)
            {
                p1Points++;

            }
        }

        public void SaveData()
        {
            try
            {
                // Open the file for writing using StreamWriter
                using (StreamWriter writer = new StreamWriter("data.txt", true))
                {
                    // Write data to the file
                    writer.WriteLine($"Player 1 | Number of shooted bullets: {p1Shots} | Number of successfull bullets {p1SucShots} | Point(s): {p1Points}");
                    writer.WriteLine($"Player 2 | Number of shooted bullets: {p2Shots} | Number of successfull bullets {p2SucShots} | Point(s): {p2Points}");
                    writer.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");


                }

                Console.WriteLine("Data has been written to the file.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void UpdateCameraPosition(Point2D mousePos, string reset)
        {

            long currentTicku = DateTime.UtcNow.Ticks;
            if (currentTicku - now2 > 140000)
            {

            }
            // Test edge of screen boundaries to adjust the camera
            double leftEdge = Camera.X + screenBorder;
            double rightEdge = leftEdge + SplashKit.ScreenWidth() - 2 * screenBorder;
            double topEdge = Camera.Y + screenBorder;
            double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * screenBorder;

            // Test if the player is outside the area and move the camera
            // the player will appear to stay still and everything else
            // will appear to move :)

            // Test top/bottom of screen
            if (mousePos.Y < topEdge)
            {
                SplashKit.MoveCameraBy(0, mousePos.Y - topEdge);
            }
            else if (mousePos.Y > bottomEdge)
            {
                SplashKit.MoveCameraBy(0, mousePos.Y - bottomEdge);
            }

            // Test left/right of screen
            if (mousePos.X < leftEdge)
            {
                SplashKit.MoveCameraBy(mousePos.X - leftEdge, 0);
            }
            else if (mousePos.X > rightEdge)
            {
                SplashKit.MoveCameraBy(mousePos.X - rightEdge, 0);
            }
            double[] cameraPos = { leftEdge, rightEdge, topEdge, bottomEdge };
            if (reset == "reset")
            {
                Camera.X = 0;
                Camera.Y = 0;
            }
            if (SplashKit.KeyDown(KeyCode.RightKey))
            {
                Camera.X += 5;
                Camera.X += 5;
            }
            if (SplashKit.KeyDown(KeyCode.LeftKey))
            {
                Camera.X -= 5;
                Camera.X -= 5;
            }
            if (Camera.X > 2000)
            {
                Camera.X = 2000;
            }
            if (Camera.X < 0)
            {
                Camera.X = 0;
            }
            if (Camera.Y < 0)
            {
                Camera.Y = 0;
            }
            if (Camera.Y > 200)
            {
                Camera.Y = 200;
            }

            now2 = currentTicku;
        }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


        public void SetCameraCenter(string reset, Player playerCam)
        {
            long currentTicku = DateTime.UtcNow.Ticks;
            if (currentTicku - now2 > 140000)
            {

            }
                // Test edge of screen boundaries to adjust the camera
                double leftEdge = Camera.X + 718;
                double rightEdge = leftEdge + SplashKit.ScreenWidth() - 2 * 718;
                double topEdge = Camera.Y + 40;
                double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * 40;

                // Test if the player is outside the area and move the camera
                // the player will appear to stay still and everything else
                // will appear to move :)
            if (unlock)
            {

                if (playerCam.Bottom > bottomEdge)
                {
                    SplashKit.MoveCameraBy(0, 1);

                }
                if (playerCam.Bottom <= bottomEdge)
                {
                    //unlock = false;

                }

                // Test left/right of screen
                if (playerCam.X < leftEdge)
                {
                    SplashKit.MoveCameraBy(-8, 0);
                    if (playerCam.X >= leftEdge - 5)
                    {

                        unlock = false;
                    }
                }
                else if (playerCam.Right > rightEdge)
                {
                    SplashKit.MoveCameraBy(8, 0);
                    if (playerCam.Right <= rightEdge + 5)
                    {
                        unlock = false;

                    }

                }

                if (reset == "reset")
                {
                    Camera.X = 0;
                    Camera.Y = 0;
                }
                if (SplashKit.KeyDown(KeyCode.RightKey))
                {
                    Camera.X+= 5;
                    Camera.X+=5;
                }
                if (SplashKit.KeyDown(KeyCode.LeftKey))
                {
                    Camera.X-= 5;
                    Camera.X-= 5;
                }
                if (Camera.X > 2000)
                {
                    Camera.X = 2000;
                }
                if (Camera.X < 0)
                {
                    unlock = false;
                }
                if (Camera.X > 1999)
                {
                    unlock = false;
                }
                if (Camera.Y < 0)
                {
                    Camera.Y = 0;
                }
                if (Camera.Y > 200)
                {
                    Camera.Y = 200;
                }

            }
                // Test top/bottom of screen
            now2 = currentTicku;
        }

        public void UpdateCameraPositionForHistory(Point2D mousePos, string reset)
        {
            int SCREENBORDER = 100;
            long currentTicku = DateTime.UtcNow.Ticks;
            if (currentTicku - now2 > 140000)
            {

            }
            // Test edge of screen boundaries to adjust the camera
            double leftEdge = Camera.X + SCREENBORDER;
            double rightEdge = leftEdge + SplashKit.ScreenWidth() - 2 * SCREENBORDER;
            double topEdge = Camera.Y + SCREENBORDER;
            double bottomEdge = topEdge + SplashKit.ScreenHeight() - 2 * SCREENBORDER;

            // Test if the player is outside the area and move the camera
            // the player will appear to stay still and everything else
            // will appear to move :)

            // Test top/bottom of screen
            //if (mousePos.Y < topEdge)
            //{
            //    SplashKit.MoveCameraBy(0, mousePos.Y - topEdge);
            //}
            //else if (mousePos.Y > bottomEdge)
            //{
            //    SplashKit.MoveCameraBy(0, mousePos.Y - bottomEdge);
            //}

            // Test left/right of screen
            //if (mousePos.X < leftEdge)
            //{
            //    SplashKit.MoveCameraBy(mousePos.X - leftEdge, 0);
            //}
            //else if (mousePos.X > rightEdge)
            //{
            //    SplashKit.MoveCameraBy(mousePos.X - rightEdge, 0);
            //}
            double[] cameraPos = { leftEdge, rightEdge, topEdge, bottomEdge };
            if (reset == "reset")
            {
                Camera.X = 0;
                Camera.Y = 0;
            }
            if (Camera.X > 2000)
            {
                Camera.X = 2000;
            }
            if (Camera.X < 0)
            {
                Camera.X = 0;
            }
            if (Camera.Y < 0)
            {
                Camera.Y = 0;
            }

            if (SplashKit.KeyDown(KeyCode.UpKey))
            {
                Camera.Y-= 6;
                Camera.Y-= 6;
            }
            //if (SplashKit.MouseWheelScroll(MouseButton.))
            if (SplashKit.KeyDown(KeyCode.DownKey))
            {
                Camera.Y+= 6;
                Camera.Y+= 6;
            }

            now2 = currentTicku;
        }

        public string GameState
        {
            get
            {
                return gameState;
            }
        }
        
        public Player Player1 { get { return player1; } }
        public Player Player2 { get { return player2;} }

        public void Draw()
        {
            if (GameState == "game")
            {
                if (turn == "p1")
                {

                    SetCameraCenter("hello", player1);
                }
                else
                {
                    SetCameraCenter("hello", player2);

                }
                UpdateCameraPosition(SplashKit.ToWorld(SplashKit.MousePosition()), "play");


            }
            if (GameState == "menu")
            {
                long currentTicku = DateTime.UtcNow.Ticks;
                if (currentTicku - now3 > 10)
                {

                    UpdateCameraPosition(SplashKit.ToWorld(SplashKit.MousePosition()), "reset");
                    now3 = currentTicku;
                }

            }
            if (gameState == "game")
            {
                if (playMusic && !SplashKit.SoundEffectPlaying(music))
                {

                    SplashKit.PlaySoundEffect(music);
                    playMusic = false;

                }

                SplashKit.DrawBitmap(bacground, 0, 0, Opts);

                foreach (Ground ground in grounds)
                {
                    ground.Draw();
                }
                player1.Draw();
                player2.Draw();
                player1.Weapon.Draw();
                player2.Weapon.Draw();
                scoreBoard.Draw();
                if (player1.HP <= 0)
                {
                    SplashKit.DrawText("Player 2 win!", Color.Yellow, "fontRoboto", 15, 700, 70, Opts);
                }
                else if (player2.HP <= 0)
                {
                    SplashKit.DrawText("Player 1 win!", Color.Yellow, "fontRoboto", 15, 700, 70, Opts);


                }

                if (turn == "p1")
                {
                    //SplashKit.DrawText("Player 1's turn!", Color.Orange, "fontName", 12, screenWidth/2 - 200, 100, Opts);
                }
                else
                {
                    //SplashKit.DrawText("Player 2's turn!", Color.Orange, "fontName", 12, screenWidth / 2 - 200, 100, Opts);
                }
                //lineBar.Draw();
                long currentTicku = DateTime.UtcNow.Ticks;
                if (currentTicku - now3 > 100000)
                {
                    if (opa > 0)
                    {
                        opa-= 0.01;

                    }
                    now3 = currentTicku;
                }
                if (opa > 0)
                {
                    SplashKit.FillRectangle(SplashKit.RGBAColor(0, 0, 0, opa), -100, -200, 7000, 2000);

                }

            }
            if (gameState == "menu")
            {
                SplashKit.StopSoundEffect(music);

                SplashKit.DrawBitmap(bacground, 0, 0, Opts);
                SplashKit.DrawText("Gunny", Color.Black, "fontGame", 70, 620, 20);

                playerButton.Draw();
                helpButton.Draw();
                quitButton.Draw();
            }
            if (gameState == "help")
            {
                goBackButton.Draw();
                GetGameState();
                SplashKit.DrawText("Use Space for controling force", Color.Black, "fontGame", 20, 560, 300);
                SplashKit.DrawText("Use arrow up/down to control angle", Color.Black, "fontGame", 20, 560, 330);
                SplashKit.DrawText("Use 1-7 to enhance bullet damge", Color.Black, "fontGame", 20, 560, 360);
                SplashKit.DrawText("Use f to teleport", Color.Black, "fontGame", 20, 560, 390);
                SplashKit.DrawText("Use G to freeze", Color.Black, "fontGame", 20, 560, 420);
                SplashKit.DrawText("Use H to invisible", Color.Black, "fontGame", 20, 560, 450);
                SplashKit.DrawText("Use arrow left/right to control camera", Color.Black, "fontGame", 20, 560, 480);
                SplashKit.DrawText("Use cursor to control camera", Color.Black, "fontGame", 20, 560, 510);



            }
            if (gameState == "quit")
            {
                SplashKit.StopSoundEffect(music);

                UpdateCameraPositionForHistory(SplashKit.ToWorld(SplashKit.MousePosition()), "play");

                DrawingOptions options = new DrawingOptions
                {
                    Dest = _window,
                    LineWidth =  10 // Set the line width to 5 pixels
                };
                goBackButton.Draw();
                GetGameState();


                SplashKit.FillRectangle(Color.Black, 150, 0, 1350, 1000, options);
                SplashKit.FillRectangle(Color.Green, 170, 30, 1300, 930, options);
                SplashKit.DrawText("Use the up/down button", Color.Black, "fontName", 9, 2, 700, options);
                SplashKit.DrawText("to view more history", Color.Black, "fontName", 9, 5, 712, options);
                //SplashKit.DrawText("to view more history", Color.Black, "fontName", 10, 10, 724);
                GetData();

            }
        }
        
    }
}
