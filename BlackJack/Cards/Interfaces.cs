using Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.Interfaces
{
    public interface IShoe
    {
        int CardsRemaining { get; }
        CardDefinition Draw(int count = 1);
        void Initialize(int? shoeSize);
    }

    public interface IHand
    {
        List<CardDefinition> Cards { get; }
        int Value { get; }
        void AddCards(params CardDefinition[] cards);
    }
}
