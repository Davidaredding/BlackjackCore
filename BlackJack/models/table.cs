namespace BlackJack.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Table
    {
        public string Id { get; private set;}
        public Shoe Shoe { get; set; }
        
        public Dictionary<string,Player> Players { get; private set; }
        public Stack<String> PlayersInHand { get; private set; }
        public Player CurrentPlayer{get;private set;}
        public Player Dealer{get; private set;}
        public bool InHand{get; private set;}

        public Table ()
        {
            Id = Guid.NewGuid().ToString();
            Shoe = new Shoe(6,true);
            Players = new Dictionary<string,Player>();
            Dealer = new Player("Dealer", double.MaxValue);
            
        }
        
        public void AddPlayer(string id)
        {
            if(Players.ContainsKey(id))
            {
                Console.WriteLine($"Player {id} already Seated");
                return;
            }

            Players.Add(id, new Player(id));
        }

        public void RemovePlayer(string id)
        {
            if(Players.ContainsKey(id))
                Players.Remove(id);
        }

        public Player GetPlayer(string id)
        {
            Player player = Players.ContainsKey(id) ? Players[id] : null;
            return player;
        }

        public void StartHand()
        {
                PlayersInHand = new Stack<String>();
                foreach(var Key in Players.Keys)
                {
                    if(Players[Key].Bet <= 0)
                        continue;
                    var Player = Players[Key];

                    Player.Hand.AddCards(Shoe.Draw(), Shoe.Draw());
                    PlayersInHand.Push(Key);
                }

                while(PlayersInHand.Count > 0)
                {
                    CurrentPlayer = SetCurrentPlayer();
                    if(CurrentPlayer!=null)
                    {
                        InHand = true;
                        Console.WriteLine($"Current Player: {CurrentPlayer.Id}");
                        Dealer.State = PlayerState.Waiting;
                        Dealer.Hand = new Hand();
                        Dealer.Hand.AddCards(Shoe.Draw(), Shoe.Draw());
                        break;        
                    }
                }

                if(CurrentPlayer==null)
                {
                   Console.WriteLine("No current Players.");
                }

        }


        public void Bet(string playerId, double amount)
        {
            if(Players.ContainsKey(playerId))
            {
                Console.WriteLine($"Player {playerId} is betting {amount}");
                var player = Players[playerId]; 
                player.PlaceBet(amount);
                player.Hand = new Hand();
                player.State = PlayerState.Waiting;
                if(Dealer.State != PlayerState.Waiting)
                {
                    Dealer.Hand = new Hand();
                    Dealer.State = PlayerState.Waiting;   
                }

            }
        }

        public void Hit(string playerId)
        {
            
            if(CurrentPlayer == null || CurrentPlayer.Id != playerId || !InHand)
                return;
            var newCard = Shoe.Draw();
            CurrentPlayer.Hand.AddCards(newCard);
            Console.WriteLine($"Player {CurrentPlayer.Id} is Hitting and gets {newCard}");
            if(CurrentPlayer.Hand.Value > 21)
            {
                CurrentPlayer.State = PlayerState.Bust;
                SetCurrentPlayer();
            }
        }

        public void Stand(string playerId)
        {
            if(CurrentPlayer == null || CurrentPlayer.Id != playerId || !InHand)
                return;
            Console.WriteLine($"Player Stands on {CurrentPlayer.Hand.Value}");
            CurrentPlayer.State = PlayerState.Stand;
            SetCurrentPlayer();
        }

        private void DealerPlay()
        {
            Console.WriteLine($"Dealer Plays with {Dealer.Hand.Value}...");
            //Dealer stands on a soft 17 or better;
            while(Dealer.Hand.Value<=17)
            {
                var newCard = Shoe.Draw();
                Console.WriteLine($"Dealer Hits and gets {newCard}");
                Dealer.Hand.AddCards(newCard);
               
            }
            if(Dealer.Hand.Value>21)
            {
                Console.WriteLine($"Dealer Busts with {Dealer.Hand.Value}");
                Dealer.State = PlayerState.Bust;
            }   
            else
            {
                Console.WriteLine($"Dealer Stands with {Dealer.Hand.Value}");
                Dealer.State = PlayerState.Stand;
            }

            ResolveHand();
        }

        private void Surrender()
        {
            
        }

        private void ResolveHand()
        {
            
            Players.Values.Where(p=>((int)p.State)>2).ToList().ForEach(p=>{
                var scheudule = p.CompareTo(Dealer);
                if(scheudule>0)
                {
                    Console.WriteLine($"Player Wins ${p.Bet}!");
                    p.PayBet();
                }
                else if(scheudule<0)
                {
                    Console.WriteLine($"House Wins ${p.Bet}!");
                    p.TakeBet();
                }
                else
                {
                    Console.WriteLine($"Push ${p.Bet} returned to player.");
                    p.Push();
                }
            });
            InHand = false;
        }

        private Player SetCurrentPlayer()
        {
            if(PlayersInHand.Count > 0)
            {
                var id = PlayersInHand.Pop();
                Players[id].State = PlayerState.ActionOn;
                return Players[id];
            }
            else
            {
                DealerPlay();
                return Dealer;
            }
            
        }

    }
}