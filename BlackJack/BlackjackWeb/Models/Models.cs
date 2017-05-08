using Cards;
using Cards.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackjackWeb.Models
{
    public enum PlayerState
    {
        Waiting = 0,
        ActionOn = 1,
        Surrender = 2,
        Bust = 3,
        Stand = 4
    }
    public class Player
    {
        public Guid PlayerID { get; set; }
        public IHand Hand { get; set; }
        public double Bankroll { get; set; }
        public PlayerState PlayerState { get; set; }
        public double CurrentBet { get; set; }
    }
    
    
    



}
