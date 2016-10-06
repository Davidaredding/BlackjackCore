namespace BlackJack.Modules
{
    using System;
    using Nancy;
    using BlackJack.Models;

    public class TestModule : NancyModule
    {
        public TestModule() : base("/test")
        {
            Get("/",  parameters => {
                return "Tests";
            });

            Get("/HandValue", parameters=>{
                //two new cards
                var card1 = (Byte)(CardDefinition.King | CardDefinition.Diamond);
                var card2 = (Byte)(CardDefinition.Ace | CardDefinition.Spade);
                var Hand = new Hand();
                Hand.AddCard(card1);
                Hand.AddCard(card2);
                return Hand;
            });
        }
    }
}