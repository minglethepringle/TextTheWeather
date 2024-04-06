namespace TextTheWeather.Core
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new TextTheWeather().FunctionHandler();
        }
    }
}