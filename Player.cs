using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Player:GameBlock
    {
        //private Inventory _inventory;
        private int _playerArea = 50;
        private string name;
        private double force;
        private bool deforce;
        private Bullet _bullet;
        //private Player _secplayer;
        private List<Player> _gameblocks;
        private double angle;
        private bool side;
        private double oldHp;
        private double hp;
        private double mana;
        private int j;
        private bool IsInvalid;
        private long now = DateTime.UtcNow.Ticks;
        private long now1 = DateTime.UtcNow.Ticks;
        private bool resetNow;
        private List<HitBox> grounds;
        private double gravity;
        private bool shooted;
        private Window _window;
        private DrawingOptions drawingOptions;
        Rectangle rectangle = new Rectangle();
        private bool canGoA;
        private bool canGoD;
        private ForceBar forceBar;
        private string type;
        private string enhancedBullet;
        private Weapon weapon;
        private List<Item> items;
        private double turnDelay;
        private double turnAbility;
        //private double turn
        private bool invisibleState;
        private DrawingOptions Opts;
        private double numberOfShoots;
        private double numberofSuccessShots;
        private double roundsPlayed;
        private double roundsWin;
        private List<double> listOfRecord;
        private bool isTurn;
        private Bitmap cannon;
        private Bitmap playerImage;
        private double totalUsed;
        private bool finishTurn;
        private DrawingOptions Opts2;
        private Bitmap iceImage;
        private AnimationScript freezeScript;
        private Animation freezeAnimation;
        private DrawingOptions option;
        private double updateAni;
        private SoundEffect iceCracking;
        private bool isFreeze;
        private Bitmap arrowDown;
 


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        private Item threeTurn;
        private Item trinity;
        private Item thirteen;
        private Item fourteen;
        private Item fithteen;
        private Item sixteen;
        private Item seventeen;
        private Item fly;
        private Item invisible;
        private Item freeze;


        public Player(string name, string desc, double x, double y, List<HitBox> grounds, Window window) : base(x, y)
        {
            //_inventory = new Inventory();
            _window = window;
            this.name = name;
            deforce = false;
            _gameblocks = new List<Player>();
            angle = 45;
            oldHp = 100;
            hp = 100;
            mana = 100;
            resetNow = true;
            if (X < 900)
            {
                side = true;
            }
            else
            {
                side = false;
            }
            this.grounds = grounds;
            shooted = false;
            canGoA = true;
            isFreeze = false;
            canGoD = true;
            forceBar = new ForceBar(10, 820, 1800, 40, force, _window);
            weapon = new Weapon(grounds, _gameblocks, this);
            turnDelay = 0;
            turnAbility = 0;
            invisibleState = false;
            isTurn = false;
            arrowDown = SplashKit.LoadBitmap("arrowDown", "arrowPointDown.png");
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
            numberofSuccessShots = 0;
            numberOfShoots = 0;
            roundsPlayed = 0;
            roundsWin = 0;
            listOfRecord = new List<double> { numberOfShoots, numberofSuccessShots, roundsPlayed , roundsWin};
            cannon = SplashKit.LoadBitmap("cannon", "handcannonicon.png");
            Opts = SplashKit.OptionToWorld(Opts);
            drawingOptions = SplashKit.OptionToWorld(drawingOptions);
            playerImage = SplashKit.LoadBitmap("player", "redplayer.png");
            iceImage = SplashKit.LoadBitmap("ice", "freeze.png");
            iceImage.SetCellDetails(100, 100, 4,1, 4);
            freezeScript = SplashKit.LoadAnimationScript("freezescript", "freeze.txt");
            freezeAnimation = SplashKit.CreateAnimation(freezeScript, "ice");
            option = SplashKit.OptionWithAnimation(freezeAnimation);
            iceCracking = SplashKit.LoadSoundEffect("iceCracking", "icecracking.mp3");
            updateAni = 24;



            totalUsed = 0;
            finishTurn = false;
            Opts2 = new DrawingOptions()
            {
                Dest = _window,
                AnchorOffsetX = 0,
                AnchorOffsetY = 0,
                ScaleX = 1,
                ScaleY = 1,
                Angle = 0,
                FlipY = false
            };
            Opts2 = SplashKit.OptionToScreen(Opts2);



            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~``
            threeTurn = new Item(1480, 200, 0, 40,0, this, weapon, "three turn", "bullet", _window, "reload.png", "1");
            trinity = new Item(1480, 240, 0, 40,0, this, weapon, "three bullets", "trinity", _window, "trinity.png", "2");
            thirteen = new Item(1480, 280, 0.3, 20,0, this, weapon, "30%", "bullet", _window, "30.png", "3");
            fourteen = new Item(1480, 320, 0.4, 30,0, this, weapon, "40%", "bullet", _window, "40.png", "4");
            fithteen = new Item(1480, 360, 0.5, 40,0, this, weapon, "50%", "bullet", _window, "50.png", "5");
            sixteen = new Item(1480, 400, 0.6, 50,0, this, weapon, "60%", "bullet", _window, "60.png", "6");
            seventeen = new Item(1480, 440, 0.7, 50,0, this, weapon, "70%", "bullet", _window, "70.png", "7");
            fly = new Item(1480, 480, 0, 85, 3, this, weapon, "teleport", "fly"    , _window, "teleport.png", "F");
            invisible = new Item(1480, 520, 0, 85,4, this, weapon, "invisible", "bullet", _window, "invisible.png", "H");
            freeze = new Item(1480, 560, 0, 85,4, this, weapon, "freeze", "freeze", _window, "freezing.png", "G");
            items = new List<Item>
            {
                threeTurn,
                trinity,
                thirteen,
                fourteen,
                fithteen,
                sixteen,
                seventeen,
                fly,
                invisible,
                freeze
            };




        }

        public Rectangle Rectangle { get { return rectangle; } }

        public bool IsTurn
        {
            get
            {
                return isTurn;
            }
            set { isTurn = value; }
        }

        public bool IsFreeze
        {
            get
            {
                return isFreeze;
            }
            set
            {
                isFreeze = value;
            }
        }

        public double Mana
        {
            get
            {
                return mana;
            }
            set
            {
                mana = value;
            }
        }

        public double OldHP
        {
            get
            {
                return oldHp;
            }
            set
            {
                oldHp = value;
            }
        }

        public bool FinishTurn
        {
            get
            {
                return finishTurn;
            }
            set { finishTurn = value; }
        }

        public int PlayerArea
        {
            get
            {
                return _playerArea;
            }
        }

        public void ResetCoolDown()
        {
            foreach(Item item in items)
            {
                item.ResetAbility();
            }
            Mana = 100;
        }

        public double Top
        {
            get { return Y; }
        }

        public double Bottom
        {
            get { return (Y + _playerArea); }

        }

        public double Left
        {
            get { return X; }
        }
        public double Right
        {
            get { return X + _playerArea; }
        }

        public bool Side
        {
            get { return side; }
            set { side = value; }
        }

        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
            }
        }
        //public Inventory Inventory { get { return _inventory; } }
        public double HP
        {
            get
            {
                return hp;
            }
        }

        public double Force
        {
            get { return force; }
            set { force = value; }
        }

        public double TotalUsed
        {
            get
            {
                return totalUsed;
            }
            set
            {
                totalUsed = value;
            }
        }

        public bool Invisible
        {
            get
            {
                return invisibleState;
            }
            set
            {
                invisibleState = value;
            }
        }

        public string EnhancedBullet
        {
            get
            {
                return enhancedBullet;
            }
            set { enhancedBullet = value; }
        }

        public double NumberOfSuccessShots
        {
            get
            {
                return numberofSuccessShots;
            }
            set
            {
                numberofSuccessShots = value;
            }
        }

        public double NumberOfShots
        {
            get
            {
                return numberOfShoots;
            }
            set
            {
                numberOfShoots = value;
            }
        }

        public double RoundsWin
        {
            get
            {
                return roundsWin;
            }
            set
            {
                roundsWin = value;
            }
        }

        public double RoundsPlayed
        {
            get
            {
                return roundsPlayed;
            }
            set
            {
                roundsPlayed = value;
            }
        }

        public List<double> ListOfRecord
        {
            get
            {
                return listOfRecord;
            }
            set 
            { 
            
                listOfRecord = value;
            }
        }

        public void UpdateListOfRecord(List<double> datatext)
        {
            for(int i = 0;  i < datatext.Count; i++)
            {
                ListOfRecord[i] = datatext[i];
            }
        
        }

        public void GameBlocks(List<Player> listOfGB)
        {
            _gameblocks = listOfGB;
            weapon.Players = _gameblocks;
        }

        public void GetHit(double damage, Player shootedPlayer)
        {

            hp -= damage;
            if (hp < 0)
            {
                hp = 0;
            }
            foreach(Player iplayer in _gameblocks)
            {
                if (iplayer != this)
                {
                    iplayer.NumberOfSuccessShots++;

                }

            }
            resetNow = true;


        }

        public bool CheckFall(List<HitBox> grounds)
        {
            foreach (HitBox game in grounds)
            {
                //if ((Bottom >= game.Top) && (Left < game.Right) && (Right >= game.Left))
                //{
                //    return true;
                //}

                if (SplashKit.RectanglesIntersect(rectangle, game.Rectangle))
                {
                    if (Bottom < game.Rectangle.Y)
                    {
                        //Y = game.Rectangle.Y - 52;


                    }
                    canGoA = true;
                    canGoD = true;
                    return true;
                }



            }
            canGoA = false;
            canGoD = false;
            return false;


        }

        public void CheckCollide(List<HitBox> grounds)
        {
            foreach (HitBox block in grounds)
            {

                if ((((Bottom > block.Top + 10) && (Bottom <= block.Bottom + 7)) || ((Top > block.Top) && (Top < block.Bottom))) && (Left > block.Left) && (Left < block.Right))
                {
                    X = block.Right + 1;
                }

                else if ((((Bottom > block.Top + 10) && (Bottom <= block.Bottom + 7)) || ((Top > block.Top) && (Top < block.Bottom))) && (Right > block.Left) && (Right < block.Right) )   //&& (Right > block.Left) && (Right < block.Right)
                {
                    X = block.Left - PlayerArea -1 ;
                }




            }
        }

        public bool CheckForFly(List<HitBox> grounds)
        {
            foreach (HitBox block in grounds)
            {
                if ((Bottom > block.Top + 10) && (Left > block.Left) && (Left < block.Right))
                {
                    X = block.Right + 1;
                    return true;



                }
                else if ((Bottom > block.Top + 10) && (Right > block.Left) && (Right < block.Right))
                {
                    X = block.Left - PlayerArea - 1;
                    return true;
                }




            }
            return false;
        }


        public Bullet Bullet
        {
            get
            {
                if (_bullet != null)
                {
                    return (_bullet);
                }
                return null;
            }
        }

       

        public void PlayerMove(string turn)
        {
            if (!shooted) { 
            
                if (SplashKit.KeyDown(KeyCode.AKey) && canGoA == true && X > 1)
                {
                    if (Mana > 0)
                    {
                        X = X - 2;
                        Mana-= 0.2;

                    }
                    side = false;

                }

                if (SplashKit.KeyDown(KeyCode.DKey) && canGoD == true)
                {
                    if (Mana > 0)
                    {
                        X = X + 2;

                        Mana-= 0.2;

                    }

                    side = true;
                }

                if (SplashKit.KeyDown(KeyCode.WKey))
                {
                    //Y = Y - 2;

                }

                if (SplashKit.KeyDown(KeyCode.SKey))
                {
                    //Y = Y + 2;

                }
            
                if (SplashKit.KeyDown(KeyCode.UpKey))
                {
                    if (angle != 81)
                    {
                        long currentTick = DateTime.UtcNow.Ticks;
                        if (currentTick - now > 100000)
                        {
                            angle += 1;
                            now = currentTick;
                        }

                    }
                }

                if (SplashKit.KeyDown(KeyCode.DownKey))
                {
                    if (angle != 14)
                    {
                        long currentTick = DateTime.UtcNow.Ticks;
                        if (currentTick - now > 100000)
                        {
                            angle -= 1;
                            now = currentTick;
                        }


                    }
                }

                if (SplashKit.KeyDown(KeyCode.SpaceKey) && (turnDelay < 1))
                {

                    if (deforce == false)
                    {
                        long currentTickss = DateTime.UtcNow.Ticks;
                        if (currentTickss - now1 > 100000)
                        {
                            force += 0.3;
                            now1 = currentTickss;
                        }
                        if (force > 100)
                        {
                            deforce = true;
                        }
                    }
                    else
                    {
                        force -= 0.5;
                        if (force < 1)
                        {
                            deforce = false;
                        }

                    }

                }

                if (SplashKit.KeyTyped(KeyCode.Num1Key))
                {
                    threeTurn.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num2Key))
                {
                    trinity.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num3Key))
                {
                    thirteen.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num4Key))
                {
                    fourteen.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num5Key))
                {
                    fithteen.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num6Key))
                {
                    sixteen.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.Num7Key))
                {
                    seventeen.UseItem();
                
                }

                if (SplashKit.KeyTyped(KeyCode.FKey)) { 
                    fly.UseItem();
                }

                if (SplashKit.KeyTyped(KeyCode.GKey))
                {
                    freeze.UseItem();
                }

                if (SplashKit.KeyTyped(KeyCode.HKey))
                {
                    invisible.UseItem();
                }




                if (SplashKit.KeyReleased(KeyCode.SpaceKey))
                {
                    //if (resetNow)
                    //{
                    //    now = DateTime.UtcNow.Ticks;
                    //}

                    if (turnDelay < 1)
                    {
                        long currentTick = DateTime.UtcNow.Ticks;
                        if (currentTick - now > 10000000)
                        {
                        }
                        weapon.SetForce = force;
                        weapon.Shoot();
                        now = currentTick;
                        shooted = true;
                        force = 0;

                    }

                }
            }


        }

        public bool Shooted
        {
            get
            {
                return shooted;
            }
            set { shooted = value; }
        }

        public double UpdateAni
        {
            get { return updateAni; }
            set { updateAni = value; }
        }

        public void DrawState()
        {
            if (IsTurn)
            {
                isFreeze = false;
            }
            if (turnDelay > 0 || isFreeze)
            {
                if (turnDelay == 2)
                {
                    SplashKit.RestartAnimation(freezeAnimation);
                    SplashKit.PlaySoundEffect(iceCracking);
                }
                if (hp > 0)
                {
                    SplashKit.DrawBitmap(iceImage, X - 20, Y - 20, option);

                }
                if (updateAni > 0)
                {
                    long currentTick = DateTime.UtcNow.Ticks;
                    if (currentTick - now > 400000)
                    {
                        SplashKit.UpdateAnimation(freezeAnimation);
                        now = currentTick;
                        updateAni--;
                    }

                }
                

            }
        }

        //public bool IsMouseInRectangle(double xpos, double ypos, int width, int height)
        //{
        //    double mouseX = SplashKit.MousePosition().X;
        //    double mouseY = SplashKit.MousePosition().Y;

        //    if ((mouseX > xpos) && (mouseY > ypos) && (xpos + width > mouseX) && (mouseY < ypos + height))
        //    {
        //        return true;

        //    }
        //    return false;
        //}



        public void DelayTurn()
        {
            if (turnDelay > 0)
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 10000000)
                {
                    weapon.SetForce = force;
                    weapon.Shoot();
                    now = currentTick;
                    shooted = true;
                }
                force = 0;
            }
        }

        public double TurnDelay
        {
            get
            {
                return turnDelay;
            }
            set
            {
                turnDelay = value;
            }
        }

        public double TurnAbility
        {
            get { return turnAbility; }
            set { turnAbility = value; }
        }

        public Weapon Weapon { get { return weapon; } } 

        public void CheckInvisible()
        {
           
            if (TurnAbility > 0)
            {
                turnAbility--;
            }
            if (TurnAbility == 0) {
                invisibleState = false;            
            }
        }

        public void Update()
        {
            CheckCollide(grounds);
            if (CheckFall(grounds))
            {
                gravity = 0;
                Y += gravity;
            }
            else
            {
                gravity += 0.1;
                Y += gravity;

            }
            rectangle.X = X; rectangle.Y = Y;
            rectangle.Width = PlayerArea;
            rectangle.Height = PlayerArea;
            forceBar.Force = force;
            Opts.Angle = -((float)Angle - 45);
            if (Y > 1700)
            {
                hp = 0;
            }
            if (hp < 1)
            {
                _gameblocks.Remove(this);
            }

        }





        public void Draw()
        {
            Update();
            if (resetNow)
            {
                now1 = DateTime.UtcNow.Ticks;
                resetNow = false;
            }
            long currentTick = DateTime.UtcNow.Ticks;
            //if (currentTick - now > 10000000)
            //{

            if (hp < oldHp)
            {
                if (currentTick - now > 100000) //100000
                {
                    if (currentTick - now1 > 3000000)
                    {
                        oldHp--;

                    }
                    currentTick = now;
                }

            }
            if (!invisibleState && hp > 0)
            {
                SplashKit.FillRectangle(Color.Red, X -25, Top -18, oldHp, 10);

            }
            //}
            if (!invisibleState && hp > 0)
            {
                //draw the player's HP border
                SplashKit.DrawRectangle(Color.Black, X-2-25, Top - 20, 100 + 4, 10 + 4);
                //draw the HP
                SplashKit.FillRectangle(Color.Green, X - 25, Top - 18, HP, 10);
                //draw player
                SplashKit.FillRectangle(Color.Red, X, Y, PlayerArea, PlayerArea);
                //draw the player's side

                if (side)
                {
                    Opts.FlipY = false;
                    drawingOptions.FlipY = false;
                    Opts.Angle = -((float)Angle - 45);
                    SplashKit.DrawBitmap(playerImage, X - 20, Y - 15, drawingOptions);
                    SplashKit.DrawBitmap(cannon, X, Y, Opts);
                    

                }
                else
                {
                    Opts.FlipY = true;
                    drawingOptions.FlipY = true;

                    Opts.Angle = ((float)Angle - 45);
                    SplashKit.DrawBitmap(playerImage, X - 10, Y - 15, drawingOptions);

                    SplashKit.DrawBitmap(cannon, X, Y, Opts);



                }
                if (IsTurn)
                {
                    SplashKit.DrawBitmap(arrowDown, X + 12, Y - 60);
                }
            }
            forceBar.Draw();
            if (IsTurn)
            {
                SplashKit.FillRectangle(SplashKit.RGBAColor(211,211,211, 0.5), 1460, 180, 74, 440, Opts2);
                foreach(Item item in items)
                {
                    item.Draw();
                }

            }
            DrawState();

            //weapon.Draw();


        }
    }
}
