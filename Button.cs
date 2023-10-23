using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;

namespace Gunny
{
    public class Button: GameBlock
    {
        private bool onclick;
        private int width;
        private int height;
        private string gameState;
        private string text;
        private long now = DateTime.UtcNow.Ticks;
        private int j;
        double xText; double ytext;



        public Button(double x , double y, int _width, int _height, string _gameState, string text, double xText, double ytext) : base(x, y)
        {
            onclick = false;
            width = _width;
            height = _height;
            gameState = _gameState;
            this.text = text;
            this.xText = xText;
            this.ytext = ytext;
        }
        public bool IsMouseInRectangle(double xpos, double ypos, int width, int height)
        {
            Point2D mousePos = SplashKit.MousePosition();
            mousePos = SplashKit.ToWorld(mousePos);
            double mouseX = mousePos.X;
            double mouseY = mousePos.Y;


            if ((mouseX > xpos) && (mouseY > ypos) && (xpos + width > mouseX) && (mouseY < ypos + height))
            {
                return true;

            }
            return false;
        }

        public bool OnClickButton()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton) && (IsMouseInRectangle(X, Y, Width, Height)))
            {
                return true;
            }
            return false;

        }

        public string GetState()
        {
            if (OnClickButton() == true)
            {
                return gameState;
            }
            return "no state";
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public void Draw()
        {
            GetState();
            if (IsMouseInRectangle(X, Y, width, height))
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 500000) //100000
                {
                    if (j < 6)
                    {
                        j++;
                    }
                    //SplashKit.FillRectangle(Color.Black, X - 3, Y - 3, width + 6 + j, height + 6 + j);
                    if (OnClickButton())
                    {
                        SplashKit.FillRectangle(Color.White, X - 3, Y - 3, width + 6 + j, height + 6 + j);

                    }
                    else
                    {
                        SplashKit.FillRectangle(Color.Black, X - 3, Y - 3, width + 6 + j, height + 6 + j);

                    }
                    currentTick = now;
                }


                SplashKit.FillRectangle(Color.Green, X, Y, width, height);
            }
            else
            {
                long currentTick = DateTime.UtcNow.Ticks;
                if (currentTick - now > 500000) //100000
                {
                    if (j > 1)
                    {
                        j--;
                    }
                    if (OnClickButton())
                    {
                        SplashKit.FillRectangle(Color.White, X - 3, Y - 3, width + 6 + j, height + 6 + j);

                    }
                    else
                    {
                        SplashKit.FillRectangle(Color.Black, X - 3, Y - 3, width + 6 + j, height + 6 + j);

                    }

                    currentTick = now;
                }
                SplashKit.FillRectangle(Color.Green, X, Y, width, height);

            }
            SplashKit.DrawText(text, Color.Black, "fontName", 20, xText, ytext);






        }
    }
}
