using System.Text.Json;

namespace Spine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Format fm = new Format(args[0]);
            fm.SpineDump();
            
        }
    }
}