
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Spine;

public class Format
{
    private readonly string _location;
    private int _successCount;

    public Format(string location)
    {
        _location = location;
        _successCount = 0;
    }

    public async Task SpineDumpAsync()
    {
        InitialClean();
        await BulkDumpingAsync();
        PostClean();
    }

    private async Task BulkDumpingAsync()
    {
        var jsonFiles = Directory.GetFiles(_location, "*.json", SearchOption.AllDirectories);
        var totalFiles = jsonFiles.Length;

        foreach (var jsonFile in jsonFiles)
        {
            await ParseContentAsync(jsonFile);
        }

        Console.WriteLine($"Successfully dumped {_successCount}/{totalFiles} files.");
    }

    private async Task ParseContentAsync(string jsonFilePath)
    {
        try
        {
            Console.WriteLine($"Parsing {jsonFilePath}...");
            var content = await File.ReadAllTextAsync(jsonFilePath);
            var parsed = JsonNode.Parse(content);

            if (parsed is not JsonArray jsonArray) return;

            foreach (var item in jsonArray)
            {
                if (item?["Properties"] is not JsonObject properties) continue;

                var rawData = properties["rawData"]?.ToString();
                var atlasFileName = properties["atlasFileName"]?.ToString();
                var skelFileName = properties["skeletonDataFileName"]?.ToString();

                if (string.IsNullOrEmpty(rawData))
                {
                    Console.WriteLine($"Cannot find content for spine file, skipping...");
                    continue;
                }

                if (!string.IsNullOrEmpty(atlasFileName))
                {
                    var atlasPath = Path.Combine(Path.GetDirectoryName(jsonFilePath), Path.GetFileName(atlasFileName));
                    Console.WriteLine($"Writing atlas file {atlasPath}");
                    await WriteAtlasFileAsync(atlasPath, rawData);
                }

                if (!string.IsNullOrEmpty(skelFileName))
                {
                    var skelPath = Path.Combine(Path.GetDirectoryName(jsonFilePath), Path.GetFileName(skelFileName));
                    Console.WriteLine($"Writing skeleton file {skelPath}");
                    var skelBytes = DecodeSkel(RemoveBrackets(rawData));
                    await WriteSkelFileAsync(skelPath, skelBytes);
                }
            }
            
            _successCount++;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error parsing JSON file: {jsonFilePath}. Error: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading file: {jsonFilePath}. Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred while parsing {jsonFilePath}. Error: {ex.StackTrace}");
            throw;
        }
    }

    private static byte[] DecodeSkel(IEnumerable<int> values) => values.Select(v => (byte)v).ToArray();

    private static int[] RemoveBrackets(string rawSkelContent)
    {
        return rawSkelContent.Trim('[', ']').Split(',').Select(int.Parse).ToArray();
    }

    private static async Task WriteAtlasFileAsync(string path, string content) => await File.WriteAllTextAsync(path, content);

    private static async Task WriteSkelFileAsync(string path, byte[] content) => await File.WriteAllBytesAsync(path, content);

    #region Cleanup

    private void InitialClean()
    {
        foreach (var jsonTexture in Directory.GetFiles(_location, "*.json", SearchOption.AllDirectories)
                     .Where(f => f.Contains("Textures")))
        {
            File.Delete(jsonTexture);
        }
    }

    private void PostClean()
    {
        // Delete all JSON files
        foreach (var json in Directory.GetFiles(_location, "*.json", SearchOption.AllDirectories))
        {
            File.Delete(json);
        }

        // Move textures and delete texture folders
        foreach (var texture in Directory.GetFiles(_location, "*.png", SearchOption.AllDirectories)
                     .Where(x => x.Contains("Textures")))
        {
            var destFileName = texture.Replace(@"Textures", string.Empty);
            File.Move(texture, destFileName, true);
        }

        foreach (var folder in Directory.GetDirectories(_location, "*Textures*", SearchOption.AllDirectories))
        {
            Directory.Delete(folder, true);
        }
    }

    #endregion
}
