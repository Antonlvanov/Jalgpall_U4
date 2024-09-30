using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Stadium
    {
        List<Figure> wallList;
        public Gates HomeGates { get; private set; }
        public Gates AwayGates { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Stadium(int width, int height)
        {
            Width = width;
            Height = height;

            wallList = new List<Figure>();

            Console.SetWindowSize(width + 1, height + 1);

            CreateWalls(); 
            CreateGates(); 
        }

        public void CreateWalls()
        {
            wallList.Add(new Line(1, 2, Width - 1, true, '_')); // Верхняя линия
            wallList.Add(new Line(1, Height, Width - 1, true, '‾')); // Нижняя линия
            wallList.Add(new Line(1, 3, Height - 2, false, '|')); // Левый столбец
            wallList.Add(new Line(Width - 1, 3, Height - 2, false, '|')); // Правый столбец
        }

        public void CreateGates()
        {
            var leftLine = (Line)wallList[2]; // координаты левых и правых границ поля
            var rightLine = (Line)wallList[3]; 

            int homeGateX = leftLine.GetPoints().Last().x + 1; // домашние ворота
            int awayGateX = rightLine.GetPoints().First().x - 3; // гостевые ворота
            int gateY = Height / 2 - 2; // Центр ворот по оси Y

            HomeGates = new Gates(homeGateX, gateY, 3, 6);
            AwayGates = new Gates(awayGateX, gateY, 3, 6);
        }

        public void Draw()
        {
            foreach (var wall in wallList)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                wall.Draw();
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            HomeGates.Draw(); // ворота домашней команды
            AwayGates.Draw(); // ворота гостевой команды
            
            Console.ResetColor();
        }

        // проверка на нахождение внутри стадиона
        public bool IsIn(double x, double y)
        {
            //Возвращает true, если координаты в пределах стадиона 
            return x >= 3 && x < Width && y >= 3 && y < Height;
        }
    }
}
