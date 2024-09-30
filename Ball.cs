using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Ball : Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }

        public double velocityX, velocityY;

        private Game _game;

        public Ball(double x, double y, Game game)
        {
            _game = game;
            X = x;
            Y = y;
        }

        public void Draw()
        {
            // проверка на наложение 
            foreach (var player in _game.HomeTeam.Players.Concat(_game.AwayTeam.Players))
            {
                if ((int)X == (int)player.X && (int)Y == (int)player.Y)
                {
                    // изменение позиции мяча, чтобы избежать наложения
                    X += 1; 
                }
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition((int)X, (int)Y);
            Console.Write('@');
        }

        public void Clear()
        {
            Console.SetCursorPosition((int)X, (int)Y);
            Console.Write(' ');
        }

        public void SetSpeed(double vx, double vy)
        {
            velocityX = vx;
            velocityY = vy;
        }

        public void ResetPosition()
        {
            X = _game.Stadium.Width / 2;
            Y = _game.Stadium.Height / 2;
        }

        public void Move()
        {
            Clear();

            double newX = X + velocityX;
            double newY = Y + velocityY;

            // Проверяем, не выходит ли мяч за границы поля
            if (newX < 2 || newX >= _game.Stadium.Width - 1)
            {
                velocityX = -velocityX; // изменить направление скорости по Х
                newX = X + velocityX;   
            }

            if (newY < 3 || newY >= _game.Stadium.Height - 1)
            {
                velocityY = -velocityY; // изменить направление скорости по Y
                newY = Y + velocityY;  
            }

            X = newX;
            Y = newY;

            CheckGoal();

            Draw();
        }

        private void CheckGoal()
        {
            if (_game.IsInHomeGoal(X, Y))
            {
                _game.IncrementScore(false);
                _game.Ball.ResetPosition(); 
            }
            else if (_game.IsInAwayGoal(X, Y))
            {
                _game.IncrementScore(true);
                _game.Ball.ResetPosition();
            }
        }
    }
}
