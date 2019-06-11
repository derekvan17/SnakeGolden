namespace Snake2
{
    public class Program
    {
        static void Main()
        {
            var game = new Game();

            game.Body();

            game.PrintHighScore();
            
            Main();
        }
    }
}
