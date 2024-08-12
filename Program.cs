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
            string root = "F:\\FullSetC\\Tool\\Datamine\\UnrealEngine\\Fmodel\\Output\\Exports\\Client\\Content\\Aki\\UI\\UIResources\\Common\\Spine\\Anke";
            ParseData(root);
            Console.ReadLine();

        }


        public static void ParseData(string root)
        {

            List<string> filePath = Directory.GetFiles(root, "*.*", SearchOption.AllDirectories).ToList();
            foreach (var file in filePath)
            {
                //parse data from json file                
                using (StreamReader sr = new StreamReader(file))
                {
                    //pass empty file
                    if (new FileInfo(file).Length > 0 && !file.Contains("Textures"))
                    {
                        try
                        {
                            Console.WriteLine($"Parse {file}");
                            string jsonData = sr.ReadToEnd();
                            List<Root> spineData = JsonSerializer.Deserialize<List<Root>>(jsonData);
                            WriteAtlasToFile(spineData.ElementAt(0).Properties.rawData.ToString(), file.Replace("json", "atlas"));
                            DecodeBytes(getSkelIntValue(spineData), file.Replace("json", "skel"));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }

            }
        }

        public static void DecodeBytes(int[] skel, string subFolder)
        {
            int[] inputInt = skel.ToArray();
            byte[] buffer = inputInt.Select(x => (byte)x).ToArray();
            File.WriteAllBytes(subFolder, buffer);
        }

        public static void WriteAtlasToFile(string data, string subFolder)
        {
            File.WriteAllText(subFolder, data);
        }

        public static int[] getSkelIntValue(List<Root> spineData)
        {
            string skelData = spineData.ElementAt(1).Properties.rawData.ToString();
            skelData.Replace("[", string.Empty);
            skelData.Replace("]", string.Empty);
            return skelData.Split(';').Select(int.Parse).ToArray();
        }
    }
}