namespace Spine;

public partial class Format
{
    public int[] RemoveBrackets(string rawSkelContent)
    {
        string[] charToRemove = { "[", "]" };
        foreach (string s in charToRemove)
        {
            rawSkelContent = rawSkelContent.Replace(s, string.Empty);
        }
        return rawSkelContent.Split(',').Select(int.Parse).ToArray();
    }
    
    public void WriteAtlasFile(string path, string atlasName, string content) => File.WriteAllText(Path.Combine(path, atlasName), content);

    public void WriteSkelFile(string path, string skelName, byte[] content) => File.WriteAllBytes(Path.Combine(path, skelName), content);
    
    public string FileNameOnly(string path) => Path.GetFileName(path);
    
    public string PathOnly(string path) => Path.GetDirectoryName(path);
}