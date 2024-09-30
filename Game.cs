using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Game
    {
        public Team HomeTeam { get; }
        public Team AwayTeam { get; }
        public Stadium Stadium { get; }
        public Ball Ball { get; private set; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }
        public Gates HomeGates { get; set; }
        public Gates AwayGates { get; set; }

        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
            HomeScore = 0;
            AwayScore = 0;

            HomeGates = stadium.HomeGates;
            AwayGates = stadium.AwayGates;
        }

        public bool IsInHomeGoal(double x, double y)
        {
            return HomeGates.IsInGoals(x, y);
        }

        public bool IsInAwayGoal(double x, double y)
        {
            return AwayGates.IsInGoals(x, y);
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            Ball.Draw();
            HomeTeam.StartGame(Stadium.Width, Stadium.Height);
            AwayTeam.StartGame(Stadium.Width, Stadium.Height);
        }

        public void IncrementScore(bool isHomeTeam)
        {
            if (isHomeTeam)
            {
                HomeScore++;
                Console.SetCursorPosition(14, 1);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Домашняя команда забила! Текущий счет: {HomeScore} - {AwayScore}");
                Ball.Draw();
                Thread.Sleep(5000);
                Ball.Clear();
            }
            else
            {
                AwayScore++;
                Console.SetCursorPosition(14, 1);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Гостевая команда забила! Текущий счет: {HomeScore} - {AwayScore}");
                Ball.Draw();
                Thread.Sleep(5000);
                Ball.Clear();
            }
        }

        public (double, double) GetBallPosition()
        {
            return (Ball.X, Ball.Y);
        }

        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam)
            {
                Ball.SetSpeed(vx, vy);
            }
            else
            {
                Ball.SetSpeed(-vx,vy);
            }
        }

        public void Move()
        {
            var closestHomePlayer = HomeTeam.GetClosestPlayerToBall();
            var closestAwayPlayer = AwayTeam.GetClosestPlayerToBall();

            Player closestPlayer;
            if (closestHomePlayer.GetDistanceToBall() <= closestAwayPlayer.GetDistanceToBall())
            {
                closestPlayer = closestHomePlayer;
            }
            else
            {
                closestPlayer = closestAwayPlayer;
            }

            closestPlayer.Move();

            //var newclosestHomePlayer = HomeTeam.GetClosestPlayerToBall();
            //var newclosestAwayPlayer = AwayTeam.GetClosestPlayerToBall();

            //Console.SetCursorPosition(50, 0);
            //Console.ForegroundColor = ConsoleColor.Blue;
            //Console.WriteLine("Игрок N" + newclosestHomePlayer.Symbol + " - " + (int)newclosestHomePlayer.GetDistanceToBall() + " M");
            //Console.SetCursorPosition(50, 1);
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("Игрок N" + newclosestAwayPlayer.Symbol + " - " + (int)newclosestAwayPlayer.GetDistanceToBall() + " M");

            // Двигаем мяч
            Ball.Move();

            foreach (var player in HomeTeam.Players.Concat(AwayTeam.Players))
            {
                player.Wander();
                player.Draw();
            }

            Ball.Draw();
            
        }
    }
}
