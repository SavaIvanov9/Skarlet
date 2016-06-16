﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_ConsoleGame.Interfaces;
using RPG_ConsoleGame.Items;
using RPG_ConsoleGame.Map;

namespace RPG_ConsoleGame.Characters
{
    public class Bot: Character, IBot
    {
        private readonly List<Item> inventory;

        public int currentRow = 1;
        public int currentCol = 1;

        public Bot(Position position, char objectSymbol, string name, PlayerRace race)
            : base(position, objectSymbol, name, 0, 0, 0)
        {
            this.Race = race;
            this.inventory = new List<Item>();
            this.SetPlayerStats();
            //this.CurrentCol = currentCol;
            //this.CurrentRow = currentRow;

        }

        //public int CurrentCol { get; set; }
        //public int CurrentRow { get; set; }

        public PlayerRace Race { get; private set; }

        public IEnumerable<Item> Inventory
        {
            get
            {
                return this.inventory;
            }
        }
        
        public void AddItemToInventory(Item item)
        {
            this.inventory.Add(item);
        }

        //public void Heal()
        //{
        //    var beer = this.inventory.FirstOrDefault() as Beer;

        //    if (beer == null)
        //    {
        //        throw new NotEnoughBeerException("Not enough beer!!!");
        //    }

        //    this.Health += beer.HealthRestore;
        //    this.inventory.Remove(beer);
        //}

        public override string ToString()
        {
            return string.Format(
                "Player {0} ({1}): Damage ({2}), Health ({3}), Number of beers: {4}",
                this.Name,
                this.Race,
                this.Damage,
                this.Health,
                this.Inventory.Count());
        }

        private void SetPlayerStats()
        {
            switch (this.Race)
            {
                case PlayerRace.Mage:
                    this.Damage = 50;
                    this.Health = 100;
                    break;
                case PlayerRace.Warrior:
                    this.Damage = 20;
                    this.Health = 300;
                    break;
                case PlayerRace.Archer:
                    this.Damage = 40;
                    this.Health = 150;
                    break;
                case PlayerRace.Rogue:
                    this.Damage = 30;
                    this.Health = 200;
                    break;
                default:
                    throw new ArgumentException("Unknown player race.");
            }
        }
    }
}