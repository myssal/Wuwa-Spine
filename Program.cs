using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Spine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            if (args.Length < 1 || args[0] == "--help" || args[0] == "-h")
            {
                Console.WriteLine("Wuwa_Spine.exe {spine folder} or directly drag folder into .exe");
                return;
            }
            string root = args[0];*/
            string root = "F:\\FullSetC\\Tool\\Datamine\\UnrealEngine\\Fmodel\\Output\\Exports\\Client\\Content\\Aki\\UI\\UIResources\\Common\\Spine";
            List<Root> spineList = ParseJsonData(root);
            Console.ReadLine();
               
        }

       
        public static List<Root> ParseJsonData(string root)
        {
            //List<string> fileList = Directory.EnumerateFiles(root);
            foreach (var file in Directory.EnumerateFiles(root)) Console.WriteLine(file);
            List<Root> spineData = new List<Root>();


            foreach (var file in Directory.EnumerateFiles(root))
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    //pass empty file
                    if (new FileInfo(file).Length > 0 && !file.Contains("Textures"))
                    {
                        try
                        {
                            Console.WriteLine($"Parse {file}");
                            string jsonData = sr.ReadToEnd();
                            spineData = JsonSerializer.Deserialize<List<Root>>(file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            return spineData;
        }

        public static void DecodeBytes(List<int> skel)
        {
            int[] inputInt = skel.ToArray();
            byte[] buffer = inputInt.Select(x => (byte)x).ToArray();
            File.WriteAllBytes("output.dat", buffer);
        }
    }
}