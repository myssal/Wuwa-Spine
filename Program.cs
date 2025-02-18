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


        public static void ParseData(string root)
        {
            foreach (var file in Directory.GetFiles(root, "*.json*", SearchOption.AllDirectories).ToList())
            {
                //parse data from json file                
                using (StreamReader sr = new StreamReader(file))
                {
                    //pass empty file
                    if (new FileInfo(file).Length > 0 && !file.Contains("Textures"))
                    {
                        try
                        {
                            Console.WriteLine($"Parsing {file}...");
                            string jsonData = sr.ReadToEnd();
                            List<Root> spineData = JsonSerializer.Deserialize<List<Root>>(jsonData);
                            WriteAtlasToFile(spineData.ElementAt(0).Properties.rawData.ToString(), file.Replace("json", "atlas"));
                            Console.WriteLine($"Create {Path.GetFileName(file).Replace("json", "atlas")}.atlas");
                            DecodeBytes(getSkelIntValue(spineData), file.Replace("json", "skel"));
                            Console.WriteLine($"Create {Path.GetFileName(file).Replace("json", "skel")}.skel");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}: {ex.StackTrace}");
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
            string[] charToRemove = new string[] { "[", "]" };
            foreach (string s in charToRemove)
            {
                skelData = skelData.Replace(s, string.Empty);
            }
            return skelData.Split(',').Select(int.Parse).ToArray();
        }

        public static void CleanUpJson(string root)
        {
            Console.WriteLine($"Cleaning up {root}...");
            foreach (var jsonFile in Directory.GetFiles(root, "*.json", SearchOption.AllDirectories).ToList())
                File.Delete(jsonFile);
        }
    }
}