using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gunny
{
    public abstract class GameBlock
    {
        private double _x;
        private double _y;
        //private double _width;
        //private double _height;

        public GameBlock(double x, double y)
        {
            _x = x;
            _y = y;


        }

        public double X
        {
            get { return _x; }
            set { _x = value; }
        }

        public double Y
        {
            get { return _y; }
            set { _y = value; }
        }

        //public abstract void Draw();

        //public double Width
        //{
        //    get { return _width; }
        //    set { _width = value; }
        //}

        //public double Height
        //{
        //    get { return _height; }
        //    set { _height = value; }
        //}

        




    }
}
