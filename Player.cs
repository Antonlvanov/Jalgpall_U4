using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Player
    {
        public string Name { get; }
        public double X { get; private set; }
        public double Y { get; private set; }
        private double _vx, _vy;
        public ConsoleColor Color { get; set; }
        public char Symbol { get; set; }
        public Team? Team { get; set; } = null;

        private const double MaxSpeed = 5;
        private const double MaxKickSpeed = 25;
        private const double BallKickDistance = 7;

        private Random _random = new Random();

        public Player(string name)
        {
            Name = name;
        }

        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public Player(string name, ConsoleColor color, char sym)
        {
            Name = name;
            Color = color;
            Symbol = sym;
        }

        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void Draw()
        {
            if ((int)X == (int)Team.Game.Ball.X && (int)Y == (int)Team.Game.Ball.Y)
            {
                X += 1; // перемещение игрока на одну позицию вправо для избежания наложения
            }
            Console.SetCursorPosition((int)X, (int)Y);
            Console.ForegroundColor = Color;
            Console.Write(Symbol);
        }

        public void Clear() // очистить игрока
        {
            Console.SetCursorPosition((int)X, (int)Y);
            Console.Write(' ');
        }

        public double GetDistanceToBall() // расстояние до мяча
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void MoveTowardsBall()
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }
        public void Move()
        {
            Clear();

            // Проверяем, можем ли мы ударить по мячу
            if (GetDistanceToBall() < BallKickDistance)
            {
                MoveTowardsBall();
                KickBall();
            }
            else
            {
                MoveTowardsBall();
            }

            UpdatePosition(); // Обновляем координаты

            Draw(); // рисуем игрока
        }

        private void UpdatePosition()
        {
            var newX = X + _vx;
            var newY = Y + _vy;

            if (Team.Game.Stadium.IsIn(newX, newY))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0; 
            }
        }

        private void KickBall()
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(),
                MaxKickSpeed * (_random.NextDouble() - 0.5)
            );
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(15, 0);
            Console.WriteLine($"Игрок {Name} ударил мяч!");
        }

        public void Wander()
        {
            // Случайное изменение направления (движение в случайную сторону)
            var random = new Random();
            _vx = (random.NextDouble() - 0.5) * MaxSpeed; // случайное направление по x
            _vy = (random.NextDouble() - 0.5) * MaxSpeed; // случайное направление по y
        }
    }
}
