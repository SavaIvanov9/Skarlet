﻿namespace RPG_ConsoleGame.Core
{
    using System;
    using System.Collections.Generic;
    using Interfaces;

    [Serializable()]
    public class GameDatabase : IGameDatabase
    {
        private IList<char[,]> maps = new List<char[,]>();
        private IList<IPlayer> players = new List<IPlayer>();
        private IList<ICreature> creatures = new List<ICreature>();
        private IList<IBoss> bosses = new List<IBoss>();
        private IList<IShop> shops = new List<IShop>();

        public IList<char[,]> Maps
        {
            get { return this.maps; }
            set { this.maps = value; }
        } 

        public IList<IPlayer> Players
        {
            get { return this.players; } 
            set { this.players = value; }
        }

        public IList<ICreature> Creatures
        {
            get { return this.creatures; }
            set { this.creatures = value; }
        }

        public IList<IShop> Shops
        {
            get { return this.shops; }
            set { this.shops = value; }
        }

        public IList<IBoss> Bosses
        {
            get { return this.bosses; }
            set { this.bosses = value; }
        }

        public void AddMap(char[,] map)
        {
            Maps.Add(map);
        }

        public void AddCreature(ICreature creature)
        {
            Creatures.Add(creature);
        }

        public void AddPlayer(IPlayer player)
        {
            Players.Add(player);
        }

        public void AddShop(IShop shop)
        {
            Shops.Add(shop);
        }
        
        public void AddBoss(IBoss boss)
        {
            Bosses.Add(boss);
        }

        public void ClearData()
        {
            this.Maps.Clear();
            this.Players.Clear();
            this.Creatures.Clear();
            this.Bosses. Clear();
        }
    }
}
