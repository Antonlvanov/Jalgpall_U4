using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Point
    {
        public int x;
        public int y;
        public char sym;
        public ConsoleColor color;

        Random random = new Random();
        ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));

        public Point()
        {
        }
        public Point(int _x, int _y, char _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }

        public Point(int _x, int _y, char _sym, ConsoleColor _color)
        {
            x = _x;
            y = _y;
            sym = _sym;
            color = _color;
        }

        public Point(Point p)
        {
            x = p.x;
            y = p.y;
            sym = p.sym;
        }

        public bool IsHit(Point p)
        {
            return p.x == this.x && p.y == this.y;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        public void DrawWithColor()
        {
            if (color == ConsoleColor.Black)
            {
                color = colors[random.Next(colors.Length)];
            }
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(sym);
            Console.ResetColor();
        }

        internal void Clear()
        {
            sym = ' ';
            Draw();
        }

        public override string ToString()
        {
            return x + ", " + y + ", " + sym;
        }
    }
}
