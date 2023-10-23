using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
namespace Gunny
{
    public class ForceBar: GameBlock
    {
        private double _width;
        private double _height;
        private double _force;
        private Rectangle rectangle;
        private double linePoint;
        private DrawingOptions Opts;
        private Window _window;
        public ForceBar(double x, double y, double width, double height, double force, Window window) : base(x, y)
        {
            _width = width;
            _height = height;
            _force = force;
            rectangle = new Rectangle();
            rectangle.X = X;
            rectangle.Y = Y;
            rectangle.Width = _width;
            rectangle.Height = _height;
            linePoint = X;
            _window = window;
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


        }

        public void ModifyLineBar()
        {
            if (SplashKit.PointInRectangle(SplashKit.MousePosition(), rectangle) && SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                linePoint = SplashKit.MouseX();
            }
               
        }

        public void Update()
        {
            
        }

        public double Force
        {
            get { return _force; }
            set { _force = value; }
        }

        public void Draw()
        {
            ModifyLineBar();
            //SplashKit.DrawLine(Color.Red, linePoint, Y, linePoint, Y + _height);
            SplashKit.FillRectangle(SplashKit.RGBAColor(173, 216, 230, 0.4), (X - 2), Y - 2, _width + 4, _height + 4, Opts);

            SplashKit.FillRectangle(Color.Red, linePoint, Y, 2, _height, Opts);
            SplashKit.DrawRectangle(Color.Black, (X - 2), Y - 2, _width + 4, _height + 4, Opts);
            SplashKit.FillRectangle(Color.Green, X, Y, _force * 18, _height, Opts);
            for (int i = 0; i < 100; i++)
            {
                //draw the level of the force
                if (i % 10 == 0)
                {
                    SplashKit.DrawText(i.ToString(), Color.Black, "fontName", 12, X + i * 18 - 5, Y + 5, Opts);

                }
                //SplashKit.DrawLine(Color.Purple, X + i * 18, Y, X + i * 18, Y + _height, Opts);
                SplashKit.FillRectangle(Color.Purple, X + i * 18, Y, 1, _height, Opts);
            }

        }
    }
}
