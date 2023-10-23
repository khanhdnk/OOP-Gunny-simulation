using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Weapon
    {
        private double enhanceRate;
        private string bulletType;
        private List<HitBox> _grounds;
        private List<Player> _players;
        private Player player;
        private Bullet _bullet;
        private int turns;
        private long now = DateTime.UtcNow.Ticks;
        private double force;
        private List<Bullet> _bullets;
        private List<Bullet> _listOfSignle;
        private double setAngle;
        private bool isShooting;
        private SoundEffect soundEffect;



        public Weapon(List<HitBox> grounds, List<Player> players,Player player)
        {
            soundEffect = SplashKit.LoadSoundEffect("shoot", "shoot.mp3");

            _grounds = new List<HitBox>();
            _players = new List<Player>();
            enhanceRate = 0; 
            bulletType = "bullet";
            //_grounds = new List<HitBox>();
            //_players = new List<Player>();
            _grounds = grounds; _players = players;
            turns = 0;
            this.player = player;
            _bullets = new List<Bullet>();  
            _listOfSignle = new List<Bullet>();
            isShooting = false;
        }

        public void SetEnhancedRate(double rate)
        {
            enhanceRate += rate;
        }

        public string SetBulletType
        {
            get
            {
                return bulletType;
            }
            set
            {
                bulletType = value;
            }
        }

        public int SetTurn
        {
            get { return turns; }
            set { turns = value; }
        }

        public double SetForce
        {
            get
            {
                return force;
            }
            set { force = value; }
        }

        public void Shoot()
        {
            now = DateTime.UtcNow.Ticks;
            isShooting = true;
            if (bulletType == "trinity")
            {
                for(int i = 0 ; i < 3 ; i++)
                {
                    if (player.Angle < 30)
                    {
                        setAngle = 0.1; 
                    }
                    else if ((player.Angle < 60) && (player.Angle > 30)){
                        setAngle = 0.15;
                    }
                    else
                    {
                        setAngle = 0.05;
                    }
                    _bullets.Add(new Bullet(player.X +  25, player.Top - 2 + i * -4, player.Angle + i * (player.Angle * setAngle), enhanceRate, force, _players, player.Side, _grounds, "bullet", player));
                    player.NumberOfShots++;


                }
                SplashKit.PlaySoundEffect(soundEffect);


            }
            else if (bulletType == "fly")
            {
                _listOfSignle.Add( _bullet = new Fly(player.X + 25, player.Top - 2, player.Angle, enhanceRate, force, _players, player.Side, _grounds, "bullet", player));
                SplashKit.PlaySoundEffect(soundEffect);


            }
            else if (bulletType == "freeze")
            {
                _listOfSignle.Add( _bullet = new Freeze(player.X + 25, player.Top - 2, player.Angle, enhanceRate, force, _players, player.Side, _grounds, "bullet", player));
                player.NumberOfShots++;
                SplashKit.PlaySoundEffect(soundEffect);


            }
            else
            {
                _listOfSignle.Add( _bullet = (new Bullet(player.X + 25, player.Top - 2, player.Angle, enhanceRate, force, _players, player.Side, _grounds, "bullet", player)));
                player.NumberOfShots++;
                SplashKit.PlaySoundEffect(soundEffect);


            }


        }

        

        public List<Player> Players
        {
            get
            {
                return _players;
            }
            set { _players = value; }
        }

        public void SetMulProperties()
        {
            enhanceRate = 0;
            bulletType = "bullet";
            isShooting = false;
            player.FinishTurn = true;
        }


        public void Draw()
        {//sole bullet shoot
            if ((_listOfSignle.Count > 0)&& isShooting && ((bulletType == "bullet") || (bulletType == "fly" || (bulletType == "freeze"))) )
            {
                if (turns > 0)
                {
                    long currentTick = DateTime.UtcNow.Ticks;
                    if (currentTick - now > 15000000)
                    {
                        Shoot();
                        now = currentTick;
                        turns--;
                    }
                }
                foreach(Bullet b in _listOfSignle)
                {
                    int stopedBullets = 0;

                    b.Draw();
                    if (b.Stop)
                    {
                        b.DrawExplode();

                    }
                    if (turns < 1)
                    {
                        if (b.Stop)
                        {
                            stopedBullets++;
                        }
                        if (stopedBullets == _listOfSignle.Count)
                        {
                            SetMulProperties();
                        }

                    }
                }
                if (turns < 1)
                {
                    if (_bullet.Stop)
                    {
                        SetMulProperties();
                    }

                }
            }


            if (_bullets.Count > 0 && bulletType == "trinity" && isShooting)
            {
                int stopedBullets = 0;
                if (turns > 0)
                {
                    long currentTick = DateTime.UtcNow.Ticks;
                    if (currentTick - now > 15000000)
                    {
                        Shoot();
                        now = currentTick;
                        turns--;
                    }
                }

                foreach(Bullet bullet in _bullets)
                {
                    bullet.Draw();
                    if (turns < 1)
                    {
                        if (bullet.Stop)
                        {
                            stopedBullets++;
                        }
                        if (stopedBullets == _bullets.Count)
                        {
                            SetMulProperties();
                        }

                    }

                }

                



                
            }
            foreach(Bullet bullet1 in _listOfSignle)
            {
                if(bullet1.Stop)
                {
                    bullet1.DrawExplode();
                }
            }
            foreach(Bullet bullet2 in _bullets)
            {
                if (bullet2.Stop)
                {
                    bullet2.DrawExplode();
                }
            }

        }




    }
}
