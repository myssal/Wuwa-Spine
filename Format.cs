using Newtonsoft.Json.Linq;

namespace Spine;

public partial class Format
{
    string location { get; set; }
    private int dumpCount { get; set; }
    public Format(string location)
    {
            this.location = location;
            dumpCount = 0;
    }

    public void SpineDump()
    {
        InitialCleanJson();
        BulkDumping();
        PostClean();
    }
    
    public void BulkDumping()
    {
        InitialCleanJson();
        foreach (var json in Directory.GetFiles(location, "*.json", SearchOption.AllDirectories))
                 ParseContent(json);
    }

    public void ParseContent(string path)
    {
        try
        {
            Console.WriteLine($"Parsing {path}...");
            var content = File.ReadAllText(path);
            JArray parsed = JArray.Parse(content);
            int count = 1;
            foreach (JObject item in parsed)
            {
                JObject properties = (JObject)item["Properties"];
                string rawData = properties["rawData"]?.ToString();
                string expectedAtlasName = properties["atlasFileName"]?.ToString();
                string expectedSkelName = properties["skeletonDataFileName"]?.ToString();
                if (rawData == null)
                    Console.WriteLine($"Cannot find content for spine file, skipping index {count}...");
                else
                {
                    if (expectedAtlasName != null) 
                    {
                        // parse atlas
                        Console.WriteLine($"Writing atlas file {Path.Combine(PathOnly(path), FileNameOnly(expectedAtlasName))}");
                        WriteAtlasFile(PathOnly(path), FileNameOnly(expectedAtlasName), rawData);
                        dumpCount++;
                    }
                    
                    if (expectedSkelName != null)
                    {
                        // parse skel 
                        Console.WriteLine($"Writing skeleton file {Path.Combine(PathOnly(path), FileNameOnly(expectedSkelName))}");
                        WriteSkelFile(PathOnly(path), FileNameOnly(expectedSkelName), DecodeSkel(RemoveBrackets(rawData)));
                        dumpCount++;
                    }
                }
                count++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw;
        }
    }
    
    public byte[] DecodeSkel(int[] values) => values.Select(v => (byte)v).ToArray();
    

    #region CleanUpSection
    
    public void InitialCleanJson()
    {
        foreach (var jsonTexture in Directory.GetFiles(location, "*.json", SearchOption.AllDirectories).Where(f => f
                     .Contains("Textures")))
            File.Delete(jsonTexture);
    }
    
    public void PostClean()
    {
        PostCleanJson();
        MoveTexture();
        PostCleanTextureFolder();
    }

    public void PostCleanTextureFolder()
    {
        foreach (var folder in Directory.GetDirectories(location, "*Textures*", SearchOption.AllDirectories))
            Directory.Delete(folder, true);
    }
    public void PostCleanJson()
    {
        foreach (var json in Directory.GetFiles(location, "*.json", SearchOption.AllDirectories))
            File.Delete(json);
    }
    
    public void MoveTexture()
    {
        foreach (var texture in Directory.GetFiles(location, "*.png", SearchOption.AllDirectories).Where(x => x.Contains("Textures")))
        {
            File.Move(texture, texture.Replace(@"Textures\", String.Empty), true);
        }
    }

    #endregion
}