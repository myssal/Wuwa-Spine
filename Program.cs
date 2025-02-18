using System.Text.Json;

namespace Spine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string location = @"F:\FullSetC\Temp\Spine - Copy";
            Format fm = new Format(location);
            //fm.DumpSpine();
            fm.PostCleanJson();
            
        }
    }
}