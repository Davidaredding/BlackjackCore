namespace BlackJack
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using Nancy.Owin;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());           
        }
    }

   
}
