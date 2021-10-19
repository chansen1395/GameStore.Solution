namespace GameStore.Models
{
  public class CustomerGame
    {       
        public int CustomerGameId { get; set; }
        public int CustomerId { get; set; }
        public int GameId { get; set; }
        public bool Returned { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Game Game { get; set; }
    }
}