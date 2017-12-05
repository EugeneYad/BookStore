using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }

        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }

    public class PlayerEx
    {
        public PlayerEx()
        {
            
        }

        public PlayerEx(Player player)
        {
            Id = player.Id;
            Name = player.Name;
            Age = player.Age;
            Position = player.Position;
            Key = player.Id.ToString();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public string Key { get; set; }
    }
}