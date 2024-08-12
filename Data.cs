using System;

namespace Spine
{
	public class SpineData()
	{
		public SpineAtlasAsset spineAtlasAsset {  get; set; }
		public SpineSkeletonDataAsset spineSkeletonDataAsset { get; set; }
		public Texture2D texture2D { get; set; }
	}

	// .atlas format
	public class SpineAtlasAsset()
	{
		public string atlasType { get; set; }
		public string atlasName { get; set; }
		public string atlasClass { get; set; }
		public AtlasProperties atlasProperties { get; set; }
	}	
	
	public class AtlasProperties()
	{
		public List<AtlasPages> atlasPages {  get; set; }
		public string rawData {  get; set; }
		public string atlasFileName { get; set; }
	}
	
	public class AtlasPages()
	{
        public string objectName { get; set; }
		public string objectPath { get; set; }	
	}

    // .json format
	public class SpineSkeletonDataAsset()
	{
        public string skeletonType { get; set; }
		public string skeletonName { get; set; }
		public string skeletonClass { get; set; }
		public SkeletonProperties skeletonProperties { get; set; }
	}

	public class SkeletonProperties()
	{
		public List<int> rawData {  get; set; }
		public string skeletonDataFileName { get; set; }
	}
	
	// .png format
	public class Texture2D()
	{
		public string textureType {  get; set; }
		public string textureName { get; set; }
		public string textureClass { get; set; }
		public TextureProperties textureProperties { get; set; }
		public int sizeX { get; set; }
		public int sizeY { get; set; }
		public int packedData { get; set; }
		public string pixelFormat {  get; set; }
        public int firstMipToSerialize { get; set; }
        public List<Mips> mips {  get; set; }
	}

	public class TextureProperties()
	{
		public string addressX { get; set; }
		public string addressY { get; set; }
		public ImportedSize importedSize { get; set; }
		public string lightningGuid { get; set; }
		public string compressionSettings {  get; set; }
		public string LODGroup { get; set; }
	}
	 
	public class ImportedSize()
	{
		public string x { get; set; }
		public string y { get; set; }
	}
	
	public class Mips()
	{
		public BulkData bulkData { get; set; }
		public int sizeX { get; set; }
		public int sizeY { get; set; }
		public int sizeZ { get; set; }
	}
	
	public class BulkData()
	{
		public string bulkDataFlags {  get; set; }
		public int elementCount { get; set; }
        public int sizeOnDisk { get; set; }
		public string offsetInFile {  get; set; }
	}
}
	

