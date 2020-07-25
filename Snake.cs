using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        public int X;
        public int Y;
        public int W;
        public int H;
        public int S;
        public int Direction;

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle(X, Y, W, H);
            }
        }

        public Snake (int x, int y, int w, int h, int s, int direction)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
            S = s;
            Direction = direction;
        }

        public void Draw (Graphics g)
        {
            g.FillRectangle(Brushes.LightYellow, X, Y, W, H);
        }
    }
}
