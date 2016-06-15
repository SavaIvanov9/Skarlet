﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Characters;
using RPG_ConsoleGame.Core;
using RPG_ConsoleGame.Core.Factories;
using RPG_ConsoleGame.Interfaces;
using RPG_ConsoleGame.Map;
using RPG_ConsoleGame.UserInterface;

namespace RPG_ConsoleGame.Engine
{
    public class GameEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();
        private readonly IPlayerFactory playerFactory = new PlayerFactory();
        private readonly IBotFactory botFactory = new BotFactory();
        private readonly IGameDatabase database = new GameDatabase();

        public bool IsRunning { get; private set; }

        static Map.Map mapMatrix = new Map.Map();

        char[,] map = mapMatrix.ReadMap("map1.txt");

        static Position plPos = new Position();

        //private IInputReader reader;
        //private IRender render;

        //public GameEngine(IInputReader reader, IRender render)
        //{
        //    this.reader = reader;
        //    this.render = render;
        //    //this.characters = new List<GameObject>();
        //    //this.items = new List<GameObject>();
        //}

        private static GameEngine instance;

        //Singleton patern
        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEngine();
                }

                return instance;
            }
        }

        public void Run()
        {
            var playerName = this.GetPlayerName();
            PlayerClass race = this.GetPlayerRace();
            Player newPlayer = new Player(plPos, 'P', playerName, race);

            database.Players.Add(newPlayer);

            this.IsRunning = true;
            while (this.IsRunning)
            {
                if (Console.KeyAvailable)
                {
                    Console.Clear();

                    newPlayer.Move(map);

                    PrintMap(map, plPos.X, plPos.Y);
                }
            }
        }

        //string command = this.reader.ReadLine();

        //try
        //{
        //    this.ExecuteCommand(command);
        //}
        //catch (ObjectOutOfBoundsException ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}
        //catch (NotEnoughBeerException ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}
        //catch (Exception ex)
        //{
        //    this.renderer.WriteLine(ex.Message);
        //}

        //if (this.characters.Count == 0)
        //{
        //    this.IsRunning = false;
        //    this.renderer.WriteLine("Valar morgulis!");
        //}

        static void CheckForBattle(int x, int y)
        {

        }

        //static void PrintMap(char[,] matrix, int currentRow, int currentCol)
        //{
        //    for (int row = 0; row < matrix.GetLength(0); row++)
        //    {
        //        for (int col = 0; col < matrix.GetLength(1); col++)
        //        {
        //            if (matrix[row, col] == '.')
        //            {
        //                Console.Write(". ");
        //            }
        //            else if ((row >= currentRow - 5) && (row <= currentRow + 5) &&
        //                (col >= currentCol - 5) && (col <= currentCol + 5))
        //            {

        //                if (matrix[row, col] == '-')
        //                {
        //                    Console.Write("  ");
        //                }
        //                else
        //                {
        //                    Console.Write("{0} ", matrix[row, col]);
        //                }
        //            }
        //            else
        //            {
        //                Console.Write("  ");
        //            }
        //        }

        //        Console.WriteLine();
        //    }
        static void PrintMap(char[,] matrix, int currentRow, int currentCol)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == '-')
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.Write("{0} ", matrix[row, col]);
                    }
                }

                Console.WriteLine();
            }
        }

        private PlayerClass GetPlayerRace()
        {
            render.WriteLine("Choose a race:");
            render.WriteLine("1. Mage (damage: 50, health: 100)");
            render.WriteLine("2. Warrior (damage: 20, health: 300)");
            render.WriteLine("3. Archer (damage: 40, health: 150)");
            render.WriteLine("4. Rogue (damage: 30, health: 200)");

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4" };

            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice of race, please re-enter.");
                choice = reader.ReadLine();
            }

            PlayerClass race = (PlayerClass)int.Parse(choice);

            return race;
        }
        private string GetPlayerName()
        {
            render.WriteLine("Please enter your name:");

            string playerName = reader.ReadLine();
            while (string.IsNullOrWhiteSpace(playerName))
            {
                render.WriteLine("Player name cannot be empty. Please re-enter.");
                playerName = reader.ReadLine();
            }

            return playerName;
        }
    }
}
