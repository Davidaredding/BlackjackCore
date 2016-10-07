namespace BlackJack.Models
{
    using System;
    using System.Collections.Generic;

    public enum PlayerState{
        Waiting = 0,
        ActionOn = 1,
        Surrender = 2,
        Bust = 3,
        Stand = 4
    }

    public class Player : IComparable<Player>{
        public string Id { get; }
        public Hand Hand { get; set; }
        public double Cash { get; set; }
        public PlayerState State { get; set; }
        public double Bet { get; private set; }

        public Player (string id, double cash = 500.00)
        {
          Id = id;
          Cash = cash;
          Hand = new Hand();
        }

        public void PlaceBet(double amnt)
        {
            if(amnt<=0)
                return;
            Cash -= amnt;
            Bet  += amnt;
        }

        public void PayBet()
        {
            if(Bet>0)
            {
                Cash+= Bet*2;
                Bet = 0;
            }
        }

        public void TakeBet()
        {
            Bet = 0;
        }

        public void Surrender()
        {
            Cash+= Bet/2;
            Bet = 0;
        }

        public void Push()
        {
            Cash += Bet;
            Bet = 0;
        }

        public int CompareTo(Player dealer)
        {
            if(dealer.State > this.State)
                return -1;
            else if(dealer.State < this.State)
                return 1;
            else 
                return this.Hand.Value - dealer.Hand.Value;
        }   
    }
}