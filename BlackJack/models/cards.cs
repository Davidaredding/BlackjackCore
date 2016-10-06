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
      King of Clubs -   00101101
      Ace of Hearts -   01001110
      Nine of Diamons - 00011001
      
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
        public List<Byte> Cards { get; private set; }
        public int Value { get; private set; }
        public Hand ()
        {
          Cards = new List<Byte>();
        }

        public void AddCard(Byte card)
        {
            Cards.Add(card);
            Value = Hand.CalculateValue(Cards);
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

    public class Deck
    {

        public CardDefinition Draw(int count = 1)
        {
            return 0;
        }   
    }

}