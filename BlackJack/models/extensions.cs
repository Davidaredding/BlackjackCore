namespace BlackJack
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using BlackJack.Models;

    public static class ExtensionMethods
    {
        public static Stack<CardDefinition> Shuffle(this Stack<CardDefinition> cards)
        {
            var c = cards.ToArray();
            cards.Clear();
            var rnd = new Random();
         
            for(int i = c.Length-1; i>1; i--)
            {
                var position = rnd.Next(i);
                var card_2 = c[position];

                c[position] = c[i];
                c[i] = card_2;
            }
            return new Stack<CardDefinition>(c);
        }
    }
}