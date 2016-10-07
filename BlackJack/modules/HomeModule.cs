namespace BlackJack.Modules
{
    using System;
    using Nancy;
    using BlackJack.Models;

    public class MainModule : NancyModule
    {
        public static Table table;
        string playerId = string.Empty;

        public MainModule ()
        {
            Before += (ctx)=>{
                table = table??new Table();
                playerId = this.Request.UserHostAddress.ToString();
                if(table.GetPlayer(playerId)==null)
                    table.AddPlayer(playerId);
                return null;
            };



            Get("/", args => {
                    return View["index"];
                });

            Post("/bet/{amount}", args=>{
                table.Bet(playerId, (double)args.amount);
                return table.GetPlayer(playerId);
            });

            Get("/Player", _=>{
                return table.GetPlayer(playerId);
            });

            Get("/Deal", _=>{
                table.StartHand();
                return table.CurrentPlayer;
            });

            Post("/Hit", _=>{
                table.Hit(playerId);
                return table.GetPlayer(playerId);
            });

            Post("/Stand", _=>{
                table.Stand(playerId);
                return table.GetPlayer(playerId);
            });
        }
    }
}