using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Item: GameBlock
    {
        private double _enhanceRate;
        private bool used;
        private double _width;
        private double _height;
        private Rectangle rectangle;
        private Color color;
        private double manaCost;
        private Weapon weapon;
        private string text;
        private string bulletType;
        private double shootingTurn;
        private Player targetedPlayer;
        private DrawingOptions Opts;
        private Window _window;
        private Bitmap bitmap;
        private string fileName;
        private SoundEffect effect;
        private string button;
        private double cooldown;
        private double turnCounter;
        public Item(double x ,double y, double enhanceRate, double manaCost, double cooldown, Player player, Weapon weapon, string text, string bulletType, Window window, string fileName, string button):base(x, y)
        {
            _window = window;
            this.button = button;
            _enhanceRate = enhanceRate;
            this.fileName = fileName;
            used = false;
            _width = 130;
            _height = 40;
            turnCounter = 0;
            rectangle = new Rectangle();
            color = Color.Yellow;
            rectangle.Width = _width;
            rectangle.Height = _height;
            rectangle.X = X; rectangle.Y = Y;
            this.manaCost = manaCost;
            this.weapon = weapon;
            this.text = text;
            this.bulletType = bulletType;
            this.weapon = weapon;
            targetedPlayer = player;
            this.cooldown = cooldown;
            SplashKit.LoadFont("fontName", "Space Mono/SpaceMono-Regular.ttf");
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
            bitmap = SplashKit.LoadBitmap(fileName, this.fileName);
            effect = SplashKit.LoadSoundEffect("effect", "abilitysound.mp3");

            //switch (text)
            //{
            //    case "three turn":
            //        bitmap = SplashKit.LoadBitmap("", fileName);
            //        break;
            //    case "trinity":
            //        bitmap = SplashKit.LoadBitmap("", "trinity");
            //        break;
            //    case "30%":
            //        bitmap = SplashKit.LoadBitmap("", "30");
            //        break;
            //    case "40%":
            //        bitmap = SplashKit.LoadBitmap("", "40%");
            //        break;
            //    case "50":
            //        bitmap = SplashKit.LoadBitmap("", "50");
            //        break;
            //    case "60":
            //        bitmap

            //}


        }

        public bool Used
        {
            get { return used; }
            set { used = value; }
        }

        public void Update()
        {
            rectangle.Width = _width;   
            rectangle.Height = _height;
            rectangle.X = X; rectangle.Y = Y;
            CheckUsed();
        }

        public void WeaponConfig()
        {
            if (!Used)
            {
                SplashKit.PlaySoundEffect(effect);
                targetedPlayer.TotalUsed++;
                if (weapon.SetBulletType != "trinity")
                {

                    weapon.SetBulletType = bulletType;

                }
                if (text == "three turn")
                {
                    weapon.SetTurn = 2;
                }
                if (text == "invisible")
                {
                    targetedPlayer.Invisible = true;
                    targetedPlayer.TurnAbility = 2;
                }
                targetedPlayer.Mana -= manaCost;
                weapon.SetEnhancedRate(_enhanceRate);

            }
            else
            {
            }
        }

        public void ResetAbility()
        {
            Used = false;
            if (turnCounter > 0)
            {
                turnCounter--;
            }
        }

        public void UseItem()
        {
            if (!Used && targetedPlayer.Mana >= manaCost && turnCounter == 0)
            {

                if (SplashKit.PointInRectangle(SplashKit.MousePosition(), rectangle))
                {

                }
                WeaponConfig();
                Used = true;
                turnCounter = cooldown;
            }

            
        }

        public void CheckUsed()
        {
            if (Used)
            {
                color = Color.Gray;
            }
            else if (turnCounter > 0)
            {
                color = Color.Red; 
            }
            else if (targetedPlayer.Mana < manaCost)
            {
                color = Color.Gray;
            }
            else if (Used == false)
            {
                color = Color.Yellow;
            }

        }

        


        public void Draw()
        {
            Update();
            //SplashKit.FillRectangle(color, X, Y, _width, _height, Opts);
            //SplashKit.DrawRectangle(Color.Black, X, Y, _width, _height, Opts);
            //SplashKit.DrawText(text, Color.Black, "fontName",  X * 1.5, Y * 1.5);
            //SplashKit.DrawText(text, Color.Black, "Arial", 23, X * 1.5, Y* 1.5);
            SplashKit.FillRectangle(color, X - 2, Y - 2, 30+4, 30 +4, Opts); 
            SplashKit.DrawRectangle(Color.Black, X - 2, Y - 2, 30+4, 30 +4, Opts);
            SplashKit.DrawText($"[{button}]", Color.Blue, "fontName", 10, X + 34, Y + 5, Opts);
            SplashKit.DrawBitmap(bitmap, X, Y, Opts);
            //SplashKit.DrawText(text, Color.Black, "fontName", 14, X, Y, Opts);


            //SplashKit.DrawText(_enhanceRate.)
        }

    }
}
