using System;

// Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
public class AtlasPage
{
    public string ObjectName { get; set; }
    public string ObjectPath { get; set; }
}

public class BulkData
{
    public string BulkDataFlags { get; set; }
    public int ElementCount { get; set; }
    public int SizeOnDisk { get; set; }
    public string OffsetInFile { get; set; }
}

public class ImportedSize
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Mip
{
    public BulkData BulkData { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    public int SizeZ { get; set; }
}

public class Properties
{
    public List<AtlasPage> atlasPages { get; set; }
    public object rawData { get; set; }
    public string atlasFileName { get; set; }
    public string skeletonDataFileName { get; set; }
    public string AddressX { get; set; }
    public string AddressY { get; set; }
    public ImportedSize ImportedSize { get; set; }
    public string LightingGuid { get; set; }
    public string CompressionSettings { get; set; }
    public string LODGroup { get; set; }
}

public class Root
{
    public string Type { get; set; }
    public string Name { get; set; }
    public string Class { get; set; }
    public Properties Properties { get; set; }
    public int? SizeX { get; set; }
    public int? SizeY { get; set; }
    public int? PackedData { get; set; }
    public string PixelFormat { get; set; }
    public int? FirstMipToSerialize { get; set; }
    public List<Mip> Mips { get; set; }
}


