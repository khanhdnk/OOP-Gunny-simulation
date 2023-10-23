using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Freeze: Bullet
    {
        private Player player;
        private long now = DateTime.UtcNow.Ticks;
        private Bitmap iceExplode;
        private AnimationScript script;
        private Animation animation;
        private DrawingOptions drawingOptions;


        public Freeze(double x, double y, double angle, double enhanced, double force, List<Player> gameblocks, bool side, List<HitBox> grounds, string type, Player player) : base(x, y, angle, enhanced, force, gameblocks, side, grounds, type, player)
        { 
            this.player = player;
            UpdateAni = 90;
            iceExplode = SplashKit.LoadBitmap("iceExploded", "iceExplode.png");
            iceExplode.SetCellDetails(150, 150, 5, 5, 25);
            script = SplashKit.LoadAnimationScript("iceScript", "iceExplodeScript.txt");
            animation = SplashKit.CreateAnimation(script, "iceExplode");
            drawingOptions = SplashKit.OptionWithAnimation(animation);
            drawingOptions = SplashKit.OptionToWorld(drawingOptions);
        }

        public override void Update()
        {
            Algorithm();

            foreach (HitBox groundChecks in Grounds)
            {
                if (CheckCollide(groundChecks))
                {
                    //player.TurnDelay = 1;
                    Stop = true;
                }
            }

            foreach(Player playerNum in PlayersLists)
            {
                if (CheckCollide(playerNum))
                {

                    playerNum.UpdateAni = 25;
                    playerNum.TurnDelay = 2;
                    playerNum.IsFreeze = true;
                    Stop = true;
                }
            }



        }

        public override void DrawExplode()
        {
            if (UpdateAni > 0)
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 100000)
                {
                    if (Side)
                    {

                        SplashKit.DrawBitmap(iceExplode, X - 110 + 25, Y - 70, drawingOptions);
                    }
                    else
                    {
                        SplashKit.DrawBitmap(iceExplode, X - 110 + 25, Y - 70, drawingOptions);

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
                SplashKit.FillCircle(Color.Black, X, Y, Radius);
            }
        }
    }
}
