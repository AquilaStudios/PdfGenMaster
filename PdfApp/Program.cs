using PdfApp;

class Program
{
    static void Main()
    {
        ConsoleAppBuilder appBuilder = new ConsoleAppBuilder();
        if (appBuilder.BuildDocument())
        {
            Console.WriteLine("Success!");
        }
    }
}
