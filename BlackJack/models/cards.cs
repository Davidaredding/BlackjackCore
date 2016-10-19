namespace BlackJack.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    
    /******* Simple Card Summary**********
     A card is packed into a single byte.  
     This makes storage and manupliation easier while providing the 
     most flexibility between platforms (think 9600baud Serial)

     [0000][0000]
      Suit  Rank
     
      Suit = val & 127 
      Rank = val & 15 
      Value = where val&15 is less than 14 Value =  Math.Min(val&15,10)
              Where val&15 is 14 (an ace), Value =  11

      eg:
      King of Clubs -   0010 1101
      Ace of Hearts -   0100 1110
      Nine of Diamons - 0001 1001
      
    /*****************************/
    [Flags]
    public enum CardDefinition : Byte
    {
     Two = 2, Three = 3, Four = 4, Five = 5,
     Six = 6, Seven = 7, Eight = 8, Nine = 9,   
     Ten = 10, Jack = 11, Queen = 12, King = 13,
     Ace = 14,
     Diamond = 16, Club = 32, Heart = 64, Spade = 128 
    }

    public class Hand
    {
        public List<CardDefinition> Cards { get; private set; }
        public int Value { get; private set; }
        public Hand ()
        {
          Cards = new List<CardDefinition>();
        }

        public void AddCards(params CardDefinition[] cards)
        {
            foreach(var card in cards){
                Cards.Add(card);
            }
            Value = Hand.CalculateValue(Cards.Cast<Byte>().ToList());
        }

        //Gets max value less than /equal to 21 
        //unless busted
        public static int CalculateValue(List<Byte> cards)
        {
            var val = 0;
            cards.ForEach(card=>{val +=  (card & 15) != 14 ? Math.Min(card&15, 10): 11;});

            if(val>21)
            {
                cards.Where(card=> (card & 15) == 14).ToList().ForEach(card=>{
                    val -= 10;
                    if(val<=21) return;
                });
            }
            return val;
        }
        
    }

    public class Shoe
    {

        private Stack<CardDefinition> cards;
        private int size = 1;

        public Shoe (int shoeSize = 1, bool initialize = false)
        {
            size = shoeSize;
            if(initialize)
                Initialize(shoeSize);
        }

        public CardDefinition Draw(int count = 1)
        {
            return cards.Pop();
        }

        public void Initialize(int? shoeSize)
        {
            shoeSize = shoeSize??this.size;

            cards = new Stack<CardDefinition>();

            for(int singleShoe = 0; singleShoe<shoeSize; singleShoe++)
            {
                for(Byte suit = 1; suit <= 8; suit<<=1)
                {
                    for(Byte rank = 2; rank<= 14; rank++)
                    {
                        var card = (CardDefinition)(suit<<4|rank);
                        cards.Push(card);
                    }
                }
            }
            cards = cards.Shuffle();
        }

        public int CardsRemaining
        {
            get
            {
                return cards.Count();
            }
        }
    }

}