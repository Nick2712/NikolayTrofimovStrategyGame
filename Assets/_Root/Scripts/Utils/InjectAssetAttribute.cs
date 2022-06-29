using System;


namespace NikolayTrofimov_StrategyGame.Utils
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class InjectAssetAttribute : Attribute
    {
        public readonly string AssetName;
        public InjectAssetAttribute(string assetName = null)
        {
            AssetName = assetName;
        }
    }
}