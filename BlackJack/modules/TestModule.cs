namespace BlackJack.Modules
{
    using System;
    using Nancy;
    using BlackJack.Models;

    public class TestModule : NancyModule
    {
        public static Table simpleTable;

        public TestModule() : base("/test")
        {
           Before += (ctx)=> {
               Console.WriteLine($"Request from IP: {this.Request.UserHostAddress}");
               
               return null;
            };

            

            Get("/HandValue", parameters=>{
                //two new cards
                var card1 = CardDefinition.King | CardDefinition.Diamond;
                var card2 = CardDefinition.Ace | CardDefinition.Spade;
                var card3 = CardDefinition.Nine | CardDefinition.Club;
                var Hand = new Hand();
                Hand.AddCards(card1,card2,card3);
                return Hand;
            });

            Get("/Shoe", parameters=>{
                var shoe = new Shoe(initialize : true);
                return shoe.Draw();
            });

            Get("/Table", p=>{
                TestModule.simpleTable = TestModule.simpleTable??new Table();
                return TestModule.simpleTable;
            });

            Get("/Hand", p=>{
                TestModule.simpleTable = TestModule.simpleTable??new Table();
                //return $"Card : {simpleTable.Shoe.Draw()}, count: {simpleTable.Shoe.CardsRemaining}";
                return new {
                    card = simpleTable.Shoe.Draw(), 
                    cardCount = simpleTable.Shoe.CardsRemaining
                    };
            });

            Get("/User/", p=>{
                return "";
            });

            Get("/TableStart", p=>{
                var playerId = this.Request.UserHostAddress.ToString();
                TestModule.simpleTable = TestModule.simpleTable??new Table();
                TestModule.simpleTable.AddPlayer(playerId);
                TestModule.simpleTable.GetPlayer(playerId).PlaceBet(10);
                TestModule.simpleTable.StartHand();
                return TestModule.simpleTable.CurrentPlayer;
            });

            
        }
    }
}