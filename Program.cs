using System.Text.Json;

namespace Spine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                Format fm = new Format(args[0]);
                fm.SpineDump();
            }
            else
            {
                Console.WriteLine("Usage: WuwaSpine.exe <path/to/spine/folder>");
            }
            
        }
    }
}