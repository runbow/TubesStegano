using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TubesStegano
{
    class Point
    {
        private int x, y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public void setX(int a)
        {
            x = a;
        }

        public void setY(int b)
        {
            y = b;
        }

        public void setPoint(int a, int b)
        {
            x = a;
            y = b;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }
}
