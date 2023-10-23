using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Bullet : GameBlock, HitBox
    {
        private double _angle;
        private Color _color;
        private bool _stop;
        private int _radius;
        private double _enhanced;
        private double _wind;
        private double _force;
        private List<HitBox> _gameblocks;
        private List<Player> playersList;
        private double gravity;
        private double friction;
        private bool _side;
        private List<HitBox> grounds;
        private Rectangle rectangle;
        private string _type;
        private Bitmap bitmap;
        private long now = DateTime.UtcNow.Ticks;
        double goc = 10;
        private double increaseByAngle;
        double timeCount;
        private Bitmap explode;
        AnimationScript animationScript;
        Animation animationTest;
        DrawingOptions option;
        private int updateAni;
        private Player shootPlayer;


        public Bullet(double x, double y, double angle, double enhanced, double force, List<Player> gameblocks, bool side, List<HitBox> grounds, string type, Player thePlayer) : base(x, y)
        {
            _angle = angle;
            _color = Color.Blue;
            _stop = false;
            _radius = 15;
            _force = force;
           
            _enhanced = enhanced;
            _wind = 0.7;
            _gameblocks = new List<HitBox>();
            gravity = 0;
            friction = 0;
            _side = side;
            playersList = new List<Player>();
            playersList = gameblocks;
            this.grounds = grounds;
            rectangle = new Rectangle();
            _type = type;
            bitmap = SplashKit.LoadBitmap("bullet", "bullet.png");
            if(_angle > 55)
            {

                increaseByAngle = 17;
            }
            explode = SplashKit.LoadBitmap("explode", "explode.png");
            explode.SetCellDetails(190, 180, 5, 2, 7);
            animationScript = SplashKit.LoadAnimationScript("script", "script.txt");
            animationTest = SplashKit.CreateAnimation(animationScript, "explode");
            option = SplashKit.OptionWithAnimation(animationTest);
            updateAni = 60;
            shootPlayer = thePlayer;

        }
        public bool CheckCollide(Player playerCheck)
        {
            if ((X >= playerCheck.Left) && (X <= playerCheck.Right) && (Y >= playerCheck.Top) && (Y <= playerCheck.Bottom))
            {
                return true;
            }

            return false;
        }

        public Rectangle Rectangle { get { return rectangle; } }

        public bool CheckCollide(HitBox groundCheck)
        {
            if ((X >= groundCheck.Left) && (X <= groundCheck.Right) && (Y >= groundCheck.Top) && (Y <= groundCheck.Bottom))
            {
                return true;
            }

            return false;
        }

        public List<Player> PlayersList()
        {
            foreach (Player game in _gameblocks)
            {
                if (game.GetType() == typeof(Player))
                {
                    playersList.Add(game);
                }


            }
            return playersList;
        }

        public List<HitBox> Gameblocks
        {
            get { return _gameblocks; }
        }


        public void Algorithm()
        {
            gravity += 0.3;
            //if (friction < 20)
            //{
            friction += 0.001;

            //}

            double angleInRadians = _angle * (Math.PI / 180);
            double YDropVelo = -_force / 3 * Math.Sin(angleInRadians);
            double velocityX = _force / 2 * Math.Cos(angleInRadians) ;
            double velocityY = -_force / 2 * Math.Sin(angleInRadians) + gravity;
            velocityX += _wind;
            friction -= 0.01;
            if (_side)
            {
                X += velocityX;
                Y += velocityY;
            }
            else
            {
                X -= velocityX;
                Y += velocityY;
            }

            if ((X < 0) || (X > 10000) || (Y < -6000) || (Y > 1080))
            {
                _stop = true;
            }
        }

        public virtual void Update()
        {
            Algorithm();
            if(_type == "bullet")
            {
                RemovePlayer();
                RemoveGround();

            }


        }

        public void RemovePlayer()
        {
            foreach(Player playerCheck in playersList)
            {

                if (CheckCollide(playerCheck))
                {
                    _stop = true;
                    playerCheck.GetHit(10 + (_enhanced * 10),shootPlayer );
                }
            }


        }

        public void RemoveGround()
        {
            //foreach (HitBox groundChecks in grounds)
            //{
            //    if (CheckCollide(groundChecks))
            //    {
            //        grounds.Remove(groundChecks);
            //        _stop = true;

            //    }


            //}

            List<HitBox> groundsToRemove = new List<HitBox>();

            foreach (HitBox groundChecks in grounds)
            {
                if (CheckCollide(groundChecks))
                {
                    groundsToRemove.Add(groundChecks);
                    _stop = true;
                }
            }

            foreach (HitBox groundToRemove in groundsToRemove)
            {
                grounds.Remove(groundToRemove);

            }


        }

        public int UpdateAni
        {
            get
            {
                return updateAni;
            }
            set { updateAni = value; }
        }

        public bool Side
        {
            get
            {
                return _side;
            }
            set
            {
                _side = value;
            }
        }

        public double Top
        {
            get
            {
                return Y;
            }
        }

        public double Bottom
        {
            get
            {
                return Y + _radius;
            }


        }

        public double Left
        {
            get
            {
                return X;
            }

        }
        public double Right
        {
            get
            {
                return X + _radius;
            }

        }
        public bool Stop
        {
            get
            {
                return _stop;
            }
            set { _stop = value; }
        }

        public List<HitBox> Grounds
        {
            get
            {
                return grounds;
            }
            set
            {
                grounds = value;
            }
        }

        public int Radius
        {
            get { return _radius; }

        }

        public List<Player> PlayersLists
        {
            get
            {
                return playersList;
            }
            set
            {
                playersList = value;
            }
        }

        public virtual void DrawExplode()
        {
            if (updateAni == 60)
            {
                SplashKit.PlaySoundEffect("explodesound");
            }
            if (updateAni > 0)
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 155000)
                {
                    if (Side)
                    {

                        SplashKit.DrawBitmap(explode, X - 100, Y - 100, option);
                    }
                    else
                    {
                        SplashKit.DrawBitmap(explode, X - 90, Y - 100, option);

                    }
                    SplashKit.UpdateAnimation(animationTest);
                    now = currentTick;  
                    updateAni--;

                }


            }
        }

        public virtual void  Draw()
        {
            if (_stop == false)
            {
                Update();
                
                    //Console.WriteLine(_force);
                long currentTick = DateTime.UtcNow.Ticks;
                if (_force < 25)
                {
                    timeCount = 200000;

                }
                else
                {
                    timeCount = 400000;

                }


                if (goc > -40)
                {
                    if (currentTick - now > timeCount)
                    {
                        goc--;
                        now = currentTick;
                        if (increaseByAngle > 0)
                        {
                            increaseByAngle--;

                        }
                    }

                    
                }
                if (_side)
                {
                    SplashKit.FillCircle(SplashKit.RGBAColor(0, 0, 255, 0.5), X - 15 - goc/5 + increaseByAngle/2 , Y + 2 + goc, _radius);
                    SplashKit.FillCircle(SplashKit.RGBAColor(255, 0, 0, 0.4), X - 30 - goc / 2.5 + increaseByAngle , Y + 3 + goc * 1.6, _radius);
                    SplashKit.FillCircle(SplashKit.RGBAColor(0, 255, 0, 0.3), X - 45 - goc / 1.7 + increaseByAngle * 1.5, Y+ 7 + goc * 2.4, _radius);

                }
                else
                {
                    SplashKit.FillCircle(SplashKit.RGBAColor(0, 0, 255, 0.5), X + 15 + goc / 5 - increaseByAngle / 2, Y + 2 + goc, _radius);
                    SplashKit.FillCircle(SplashKit.RGBAColor(255, 0, 0, 0.4), X + 30 + goc / 2.5 - increaseByAngle, Y + 3 + goc * 1.6, _radius);
                    SplashKit.FillCircle(SplashKit.RGBAColor(0, 255, 0, 0.3), X + 45 + goc / 1.7 - increaseByAngle * 1.5, Y + 7 + goc * 2.4, _radius);
                    //~~~~~~
                    //SplashKit.FillCircle(SplashKit.RGBAColor(0, 0, 255, 0.6), X + 15, Y + 2 + goc, _radius);
                    //SplashKit.FillCircle(SplashKit.RGBAColor(255, 0, 0, 0.4), X + 30, Y + 3 + goc * 1.2, _radius);
                    //SplashKit.FillCircle(SplashKit.RGBAColor(0, 255, 0, 0.35), X + 35, Y + 7 + goc * 2, _radius);
                }
                SplashKit.FillCircle(_color, X, Y, _radius);
                SplashKit.DrawBitmap(bitmap, X - bitmap.Width/2, Y - bitmap.Height/2);
            }

        }
    }
}
