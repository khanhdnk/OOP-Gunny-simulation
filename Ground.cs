using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunny
{
    public class Ground: GameBlock, HitBox
    {
        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private bool destroyed;
        private bool mainDestroyed;
        private bool secondDestroyed;
        private bool thirdDestroyed;
        private List<HitBox> grounds;
        private Rectangle rectangle = new Rectangle();
        private Bitmap dirt;
        private string type;



        public Ground(double x, double y, int width, int height, string type): base(x, y)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            rectangle.X = _x;
            rectangle.Y = _y;
            rectangle.Width = _width;
            rectangle.Height = _height;
            this.type = type;
            if(type == "block")
            {
                dirt = SplashKit.LoadBitmap("block", "groundimage.jpg");

            }
            else if (type == "dirt")
            {
                dirt = SplashKit.LoadBitmap("dirt", "wall.jpg");

            }

        }


        public Rectangle Rectangle
        {
            get { return rectangle; }
        }

        public bool Destroyed
        {
            get { return destroyed; }
            set { destroyed = value; }
        }

        public bool MainDestroyed
        {
            get
            {
                return mainDestroyed;
            }
            set { mainDestroyed = value; }
        }

        public bool SecondDestroyed
        {
            get
            {
                return secondDestroyed;
            }
            set { secondDestroyed = value; }
        }

        public bool ThirdDestroyed
        {
            get
            {
                return thirdDestroyed;
            }
            set { thirdDestroyed = value; }
        }

        public void Update()
        {

        }

        //public bool CheckCollide(List<HitBox> grounds)
        //{
        //    foreach (Ground game in grounds)
        //    {
        //        if (((X == game.Right) && (Y == game.Top)) || ((Right == game.Left) && (Y == game.Top)))
        //        {
        //            game.GetHit();
        //            return true;
        //        }

        //    }
        //    return false;
        //}

        //public bool CheckDestroyed()
        //{
            
        //}

        public double Top
        {
            get { return _y; }
        }

        public double Bottom
        {
            get { return (_y + _height); }

        }

        public double Left
        {
            get { return _x; }
        }
        public double Right
        {
            get { return _x + _width; }
        }

        public void Draw()
        {

            SplashKit.FillRectangle(Color.Orange, X, Y, _width, _height);
            SplashKit.DrawBitmap(dirt, X, Y);

        }
    }
}
