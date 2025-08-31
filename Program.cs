namespace Spine;

internal static class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length == 1)
        {
            var format = new Format(args[0]);
            await format.SpineDumpAsync();
        }
        else
        {
            Console.WriteLine("Usage: WuwaSpine.exe <path/to/spine/folder>");
        }
    }
}