using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Line : Figure
    {
        public Line(int startX, int startY, int length, bool isHorizontal, char symbol)
        {
            pList = new List<Point>();

            if (isHorizontal)
            {
                for (int x = startX; x < startX + length; x++)
                {
                    Point p = new Point(x, startY, symbol);
                    pList.Add(p);
                }
            }
            else
            {
                for (int y = startY; y < startY + length; y++)
                {
                    Point p = new Point(startX, y, symbol);
                    pList.Add(p);
                }
            }
        }
    }
}
