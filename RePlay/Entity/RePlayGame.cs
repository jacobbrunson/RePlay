using Android;
using Android.App;
using System.Collections.Generic;

namespace RePlay.Entity
{
    // holds data pertaining to a specific game
    public class RePlayGame
    {
        // exposed attributes
        public readonly string Name;
        public readonly string AssetNamespace;
        public readonly string ImageAssetName;
        public readonly bool IsGameAvailable;
        public readonly string AssemblyQualifiedName;

        // basic constructor
        public RePlayGame(string name, string assetNamespace, string image_asset_name, bool available, string assemblyQualifiedName)
        {
            Name = name;
            AssetNamespace = assetNamespace;
            ImageAssetName = image_asset_name;
            IsGameAvailable = available;
            AssemblyQualifiedName = assemblyQualifiedName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is RePlayGame game)) return false;
            return Name == game.Name && AssetNamespace == game.AssetNamespace
                               && ImageAssetName == game.ImageAssetName && IsGameAvailable == game.IsGameAvailable
                               && AssemblyQualifiedName == game.AssemblyQualifiedName;
        }

        public override int GetHashCode()
        {
            var hashCode = 404668865;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AssetNamespace);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ImageAssetName);
            hashCode = hashCode * -1521134295 + IsGameAvailable.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(AssemblyQualifiedName);
            return hashCode;
        }

        public int GetAssetResource(Activity a)
        {
            int resource = a.Resources.GetIdentifier(ImageAssetName, "drawable", a.PackageName);
            return resource == 0 ? Resource.Drawable.trracer : resource;
        }
    }
}
