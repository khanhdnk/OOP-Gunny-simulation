using Microsoft.VisualBasic.FileIO;
using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunny
{
    public class Fly: Bullet
    {
        private Player player;
        private Bitmap fly;
        private AnimationScript script;
        private Animation animation;

        private DrawingOptions drawingOptions;
        private long now = DateTime.UtcNow.Ticks;
        private Bitmap teleportBullet;



        public Fly(double x, double y, double angle, double enhanced, double force, List<Player> gameblocks, bool side, List<HitBox> grounds, string type, Player player) : base(x, y, angle, enhanced, force, gameblocks, side, grounds, type, player)
        {
            this.player = player;
            UpdateAni = 40;
            fly = SplashKit.LoadBitmap("flyani", "flyani.png");
            fly.SetCellDetails(200, 200, 5, 4, 20);
            script = SplashKit.LoadAnimationScript("flyscript", "flyscript.txt");
            animation = SplashKit.CreateAnimation(script, "flyani");
            drawingOptions = SplashKit.OptionWithAnimation(animation);
            drawingOptions = SplashKit.OptionToWorld(drawingOptions);
            teleportBullet = SplashKit.LoadBitmap("teleportbullet", "teleportbullet.png");
        }

        public override void Update()
        {
            Algorithm();

            foreach (HitBox groundChecks in Grounds)
            {
                if (CheckCollide(groundChecks))
                {
                    player.Y = groundChecks.Top - 60;
                    player.X = X - 25;
                    if (player.CheckForFly(Grounds))
                    {
                        player.Y = Y - 60;
                    }
                    Stop = true;
                }
            }





        }
        public override void DrawExplode()
        {
            if (UpdateAni > 0)
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 155000)
                {
                    if (Side)
                    {
                        if (!player.Invisible)
                        {
                            SplashKit.DrawBitmap(fly, player.X - 110 + 25, player.Y - 70, drawingOptions);

                        }

                    }
                    else
                    {
                        if (!player.Invisible)
                        {
                            SplashKit.DrawBitmap(fly, player.X - 110 +25, player.Y - 70, drawingOptions);

                        }

                    }
                    SplashKit.UpdateAnimation(animation);
                    now = currentTick;
                    UpdateAni--;

                }

            }
        }


        public override void Draw()
        {
            if (Stop == false)
            {
                Update();
                //Console.WriteLine(_force);
                if (!player.Invisible)
                {
                    SplashKit.FillCircle(Color.LightSkyBlue, X, Y, Radius);

                    SplashKit.DrawBitmap("teleportbullet", X - teleportBullet.Width/2, Y- teleportBullet.Height/2);

                }

            }
        }
    }
}
