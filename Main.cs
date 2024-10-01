using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall_U4
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Создание стадиона
            Stadium stadium = new Stadium(70, 32);
            stadium.Draw();

            // Создание команд
            Team homeTeam = new Team("Home Team");
            Team awayTeam = new Team("Away Team");

            // Добавление игроков в домашнюю команду
            for (int i = 1; i < 10; i++)
            {
                homeTeam.AddPlayer(new Player($"Игрок {i} домашней команды", ConsoleColor.Blue, (char)(i + '0')));
                awayTeam.AddPlayer(new Player($"Игрок {i} гостевой команды", ConsoleColor.Red, (char)(i + '0')));
            }

            // Создание игры
            Game game = new Game(homeTeam, awayTeam, stadium);
            game.Start();

            // Главный игровой цикл
            while (true)
            {
                stadium.Draw();
                game.Move();
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
