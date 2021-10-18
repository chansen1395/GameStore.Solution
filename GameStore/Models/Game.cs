using System.Collections.Generic;

namespace GameStore.Models
{
  public class Game
    {
        public Game()
        {
            this.JoinEntities = new HashSet<CustomerGame>();
        }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public int Inventory { get; set; }
        public int RentPrice { get; set; }
        public virtual ICollection<CustomerGame> JoinEntities { get; set; }
    }
}