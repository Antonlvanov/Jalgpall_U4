using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>(); // список игроков
        public string Name { get; private set; }
        public Game Game { get; set; } // игра в которой находится команда

        public Team(string name)
        {
            Name = name;
        }

        public void StartGame(int width, int height)
        {
            var positions = new List<Tuple<int, int>>()
            {
                Tuple.Create(5, height / 2),      // Вратарь
                Tuple.Create(10, height / 3),     // Защитник 1
                Tuple.Create(10, 2 * height / 3), // Защитник 2
                Tuple.Create(15, height / 4),     // Защитник 3
                Tuple.Create(20, height / 2),     // Полузащитник 1
                Tuple.Create(20, height / 3),     // Полузащитник 2
                Tuple.Create(25, height / 2),     // Полузащитник 3
                Tuple.Create(30, height / 4),     // Нападающий 1
                Tuple.Create(30, 3 * height / 4)  // Нападающий 2
            };

            for (int i = 0; i < Players.Count; i++)
            {
                int xPosition = this == Game.HomeTeam ? positions[i].Item1 : width - positions[i].Item1; // меняем положение игроков в зависимости от команды
                int yPosition = positions[i].Item2;

                Players[i].SetPosition(xPosition, yPosition);
                Players[i].Draw(); // Отрисовка игрока
            }
        }




        public void AddPlayer(Player player)
        {
            if (player.Team != null) return; // если игрок уже принадлежит команде, то нельзя добавлять
            Players.Add(player);
            player.Team = this;
        }

        //определения положения мяча для команды
        public (double, double) GetBallPosition()
        {
            return Game.GetBallPosition();
        }

        //установка направления мяча для команды
        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }

        //определение ближайшего игрока к мячу
        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }
            return closestPlayer;
        }

        // Метод перемещения игроков во время игры
        //public void Move()
        //{
        //    var closestPlayer = GetClosestPlayerToBall();

        //    foreach (var player in Players)
        //    {
        //        if (player == closestPlayer)
        //        {
        //            player.MoveTowardsBall();
        //        }
        //        player.Move();
        //        player.Draw();
        //    }
        //}
    }
}
