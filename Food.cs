using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Food
    {
        public int X;
        public int Y;
        public int W;
        public int H;

        public Food(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            W = w;
            H = h;
        }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle(X, Y, W, H);
            }
        }

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.IndianRed, X, Y, W, H);
        }
    }
}
