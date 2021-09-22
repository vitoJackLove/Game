using GameFramework.DataTable;

namespace GameFramework
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, LoadType loadType)
        {
            return Utility.Text.Format("Assets/ABResources/DataTables/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        }
        
        public static string GetDataTableAsset(string assetName, LoadType loadType)
        {
            return Utility.Text.Format("Assets/ABResources/DataTables/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        }
    }
}
