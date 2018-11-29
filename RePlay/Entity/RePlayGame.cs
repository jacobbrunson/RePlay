namespace RePlay.Entity
{
    public class RePlayGame
    {
        public readonly string Name;
        public readonly string AssetNamespace;
        public readonly string ImageAssetName;
        public readonly bool IsGameAvailable;
        public readonly string AssemblyQualifiedName;

        public RePlayGame(string name, string assetNamespace, string image_asset_name, bool available, string assemblyQualifiedName)
        {
            Name = name;
            AssetNamespace = assetNamespace;
            ImageAssetName = image_asset_name;
            IsGameAvailable = available;
            AssemblyQualifiedName = assemblyQualifiedName;
        }
    }
}
