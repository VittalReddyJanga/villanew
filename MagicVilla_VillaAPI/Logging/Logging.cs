namespace MagicVilla_VillaAPI.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("error - " + message);
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                if(type == "warning")
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("error - " + message);
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(message);
            }
        }
    }
}
