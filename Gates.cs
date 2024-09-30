using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Gates : Point
    {
        private List<Point> pList;

        public Gates(int startX, int startY, int gateWidth, int gateHeight)
        {
            pList = new List<Point>();
            CreateGates(startX, startY, gateWidth, gateHeight);
        }

        private void CreateGates(int startX, int startY, int gateWidth, int gateHeight)
        {
            // перекладины
            for (int i = 0; i < gateWidth; i++)
            {
                pList.Add(new Point(startX + i, startY, 'G')); // верхняя
                pList.Add(new Point(startX + i, startY + gateHeight - 1, 'G')); // нижняя
            }

            // боковые стойки ворот
            for (int i = 1; i < gateHeight - 1; i++)
            {
                pList.Add(new Point(startX, startY + i, 'G')); // левая стойка
                pList.Add(new Point(startX + gateWidth - 1, startY + i, 'G')); // правая стойка
            }
        }

        public List<Point> GetPoints()
        {
            return pList;
        }

        public bool IsInGoals(double x, double y)
        {
            foreach (var point in pList)
            {
                if ((int)x == point.x && (int)y == point.y)
                {
                    return true;
                }
            }
            return false;
        }

        public void Draw()
        {
            foreach (var point in pList)
            {
                point.Draw();
            }
        }
    }
}
