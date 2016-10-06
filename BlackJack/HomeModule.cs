namespace BlackJack
{
    using Nancy;

    public class Home_Module : NancyModule
    {
        public Home_Module() : base("/default")
        {
            Get("/", _ => "Hello World");
        }
    }
}
