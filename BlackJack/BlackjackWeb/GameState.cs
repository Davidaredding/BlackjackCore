using BlackjackWeb.Models;
using Cards.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackWeb
{
    public class GameState
    {
        private ConcurrentBag<Player> Players { get; set; }
        private Player Dealer { get; set; }
        public Guid TableID { get; private set; }
        public IShoe Shoe { get; private set; }


        public GameState(IShoe shoe)
        {
            Players = new ConcurrentBag<Player>();
            Dealer = new Player { PlayerID = Guid.NewGuid(), Bankroll = double.MaxValue };
            Shoe = shoe;
            TableID = Guid.NewGuid();
        }

        public Player GetPlayer(Guid ID)
        {
            return Players.FirstOrDefault(p => p.PlayerID == ID);
        }

        public void Bet(Guid playerId, double amount) { }
        public void Hit(Guid playerId) { }
        private void Stand(Guid playerId) { }
        public void DealerPlay() { }
        private void ResolveHand() { }


    }
}
