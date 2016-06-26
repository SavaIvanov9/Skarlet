﻿using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RPG_ConsoleGame.Models.Characters.Players;

namespace RPG_ConsoleGame.Core.Engines
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using Characters;
    using Interfaces;
    using Map;
    using UserInterface;

    public delegate void OnMenuClickHandler(string selectedValue);

    public class ViewEngine
    {
        private readonly IInputReader reader = new ConsoleInputReader();
        private readonly IRender render = new ConsoleRender();

        public bool InsideGame = false;

        public event OnMenuClickHandler OnMenuClick;

        private void OnClick(string value)
        {
            if (OnMenuClick != null)
            {
                OnMenuClick(value);
            }
        }

        //Singleton patern
        private static ViewEngine instance;

        public static ViewEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ViewEngine();
                }

                return instance;
            }
        }

        public void RenderMenu()
        {
            if (InsideGame == false)
            {
                render.Clear();

                StringBuilder screen = new StringBuilder();

                screen.AppendLine(
                    "Enter number to make your choise:" + Environment.NewLine + Environment.NewLine +
                    "1. Start New Single Player" + Environment.NewLine + Environment.NewLine +
                    "2. Multiplayer" + Environment.NewLine + Environment.NewLine +
                    "3. Survival Mode" + Environment.NewLine + Environment.NewLine +
                    "4. Load Game" + Environment.NewLine + Environment.NewLine +
                    "5. Credits");

                Console.ForegroundColor = ConsoleColor.Cyan;
                render.PrintScreen(screen);
                string choice = reader.ReadLine();
                render.WriteLine("");

                string[] validChoises = { "1", "2", "3", "4", "5" };


                while (!validChoises.Contains(choice))
                {
                    render.WriteLine("Invalid choice, please re-enter.");
                    choice = reader.ReadLine();
                }

                if (choice == "1" || choice == "2")
                {
                    InsideGame = true;
                }

                OnClick(choice);
            }
            else
            {
                render.Clear();

                StringBuilder screen = new StringBuilder();

                screen.AppendLine(
                    "Enter number to make your choise:" + Environment.NewLine + Environment.NewLine +
                    "1. New Game" + Environment.NewLine + Environment.NewLine +
                    "2. Continue Game" + Environment.NewLine + Environment.NewLine +
                    "3. Multiplayer" + Environment.NewLine + Environment.NewLine +
                    "4. Survival Mode" + Environment.NewLine + Environment.NewLine +
                    "5. Load Game" + Environment.NewLine + Environment.NewLine +
                    "6. Credits");

                Console.ForegroundColor = ConsoleColor.Cyan;
                render.PrintScreen(screen);
                string choice = reader.ReadLine();
                render.WriteLine("");

                string[] validChoises = { "1", "2", "3", "4", "5", "6" };


                while (!validChoises.Contains(choice))
                {
                    render.WriteLine("Invalid choice, please re-enter.");
                    choice = reader.ReadLine();
                }

                if (choice == "1")
                {
                    choice = "6";
                }
                else
                {
                    int number = int.Parse(choice) - 1;
                    choice = number.ToString();
                }

                OnClick(choice);
            }
        }

        public void RenderBattleStats(ICharacter char1, ICharacter char2, StringBuilder history)
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();
            screen.AppendLine();
            screen.AppendLine("You have entered in battle!!");
            screen.AppendLine();
            screen.AppendLine(new string('-', 60));
            screen.AppendLine();
            screen.AppendLine(char1.ToString());
            screen.AppendLine(char2.ToString());

            screen.AppendLine();
            screen.AppendLine("Choose number to cast ability:");

            for (int i = 0; i < char1.Abilities.Count; i++)
            {
                var ability = char1.Abilities[i];
                screen.AppendLine($"{i + 1} -> {ability}");
            }

            screen.AppendLine();
            screen.Append(history);
            render.PrintScreen(screen);
        }

        public void RenderMap(char[,] matrix)
        {
            StringBuilder map = new StringBuilder();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {

                    if (matrix[row, col] == '-')
                    {
                        map.Append("  ");
                    }
                    else
                    {
                        map.Append($"{matrix[row, col]} ");
                    }
                }

                map.AppendLine();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            render.PrintScreen(map);
        }

        public IPlayer GetPlayer()
        {
            var playerName = this.GetPlayerName();
            PlayerRace race = this.GetPlayerRace();
            IPlayer newPlayer = new Player(new Position(), 'P', playerName, race);
            Console.ForegroundColor = ConsoleColor.Green;

            return newPlayer;
        }

        private PlayerRace GetPlayerRace()
        {
            render.WriteLine("Choose a race:");
            render.WriteLine("1. Mage (damage: 50, health: 100, defense: 10)");
            render.WriteLine("2. Warrior (damage: 20, health: 300, defense: 20)");
            render.WriteLine("3. Archer (damage: 40, health: 150, defense: 15)");
            render.WriteLine("4. Rogue (damage: 30, health: 200, defense: 10)");
            render.WriteLine("5. Paladin (damage: 20, health: 180, defense: 20)");
            render.WriteLine("6. Warlock (damage: 10, health: 200, defense: 0");
            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };

            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice of race, please re-enter.");
                choice = reader.ReadLine();
            }

            PlayerRace race = (PlayerRace)int.Parse(choice);

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

        public void RenderCredits()
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();
            screen.AppendLine("Teleric Software Academy");
            screen.AppendLine();
            screen.AppendLine();
            screen.AppendLine("Team Scarlet");
            screen.AppendLine();
            screen.AppendLine("OOP Team Project - C# RPG Game");
            render.PrintScreen(screen);
            StartTimer(5);
        }

        public void RenderPlayerStats(IPlayer player)
        {
            render.WriteLine("");
            render.WriteLine(player.ToString());
        }

        public void RenderWarningScreen(ConsoleColor color, StringBuilder message1, int time, StringBuilder message2 = null)
        {
            render.Clear();

            Console.ForegroundColor = color;
            StringBuilder screen = new StringBuilder();

            screen.AppendLine(
                new string('*',
                (message1.Length > ((message2 != null) ? message2.Length : 0)) ?
                message1.Length : message2.Length));
            screen.AppendLine();
            screen.AppendLine();
            screen.Append(message1 + Environment.NewLine);
            if (message2 != null)
            {
                screen.Append(Environment.NewLine + message2 + Environment.NewLine);

            }
            screen.AppendLine();
            screen.AppendLine();
            screen.AppendLine(
               new string('*',
               (message1.Length > ((message2 != null) ? message2.Length : 0)) ?
               message1.Length : message2.Length));

            render.PrintScreen(screen);

            StartTimer(time);
        }

        //public void NoMoreInGame()
        //{
        //    this.InsideGame = false;
        //}

        public void StartTimer(int seconds)
        {
            for (int i = 0; i < seconds * 4; i++)
            {
                Thread.Sleep(250);

                if (reader.ReadKey() == "skip")
                {
                    break;
                }
            }
        }

        public string ChooseSaveSlot()
        {
            render.Clear();
            StringBuilder screen = new StringBuilder();

            screen.AppendLine("Choose slot to save the game:"
                              + Environment.NewLine + Environment.NewLine + "1"
                              + Environment.NewLine + Environment.NewLine + "2"
                              + Environment.NewLine + Environment.NewLine + "3"
                              + Environment.NewLine + Environment.NewLine + "4"
                              + Environment.NewLine + Environment.NewLine + "5");

            Console.ForegroundColor = ConsoleColor.Green;
            render.PrintScreen(screen);

            string choice = reader.ReadLine();

            string[] validChoises = { "1", "2", "3", "4", "5", "6" };
            while (!validChoises.Contains(choice))
            {
                render.WriteLine("Invalid choice, please re-enter.");
                choice = reader.ReadLine();
            }

            return choice;
        }

        public string ChooseSavedGameSlot()
        {
            render.Clear();

            StringBuilder screen = new StringBuilder();
            screen.AppendLine("Choose saved game slot:");

            for (int i = 1; i <= 5; i++)
            {
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{i}.xml", FileMode.Open);

                    BinaryFormatter formatter = new BinaryFormatter();

                    IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);

                    screen.AppendLine(Environment.NewLine + Environment.NewLine +
                                      $"{i}. Game saved on {obj.Date}");
                    fs.Close();
                }
                catch (Exception)
                {
                    screen.AppendLine(Environment.NewLine + Environment.NewLine + $"{i}. Free Slot");
                }
            }

            screen.AppendLine(Environment.NewLine + Environment.NewLine + "6. Return To Menu");
            Console.ForegroundColor = ConsoleColor.Cyan;
            render.PrintScreen(screen);

            string choice = reader.ReadLine();
            
            bool correctDecision = false;
            while (!correctDecision)
            {
                try
                {
                    FileStream fs = new FileStream($@"..\..\GameSavedData\Save-{choice}.xml", FileMode.Open);

                    //BinaryFormatter formatter = new BinaryFormatter();

                    //IGameDatabase obj = (IGameDatabase)formatter.Deserialize(fs);
                    fs.Close();
                    correctDecision = true;

                }
                catch (Exception)
                {
                    render.WriteLine("Invalid choice, please re-enter." + Environment.NewLine);
                    choice = reader.ReadLine();
                }

                if (choice == "6")
                {
                    choice = "exit";
                    correctDecision = true;
                }
            }
          
            return choice;
        }

        public void DisplayMessage(string message)
        {
            render.WriteLine(message);
        }
    }
}
