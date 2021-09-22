using GameFramework.DataTable;

namespace GameFramework
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, LoadType loadType)
        {
            return Utility.Text.Format("Art/Configs/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        }
        
        public static string GetDataTableAsset(string assetName, LoadType loadType)
        {
            return Utility.Text.Format("Art/DataTables/{0}.{1}", assetName, loadType == LoadType.Text ? "txt" : "bytes");
        }
        
        public static string GetUIFormAsset(string uiFormName)
        {
            return Utility.Text.Format("Art/UI/UIForms/{0}.prefab",uiFormName );
        }
        
        public static string GetEntityAsset(string entityAssetName)
        {
            if (entityAssetName.StartsWith("Entities"))
            {
                return Utility.Text.Format("Art/{0}.prefab",entityAssetName);
            }
        
            return Utility.Text.Format("Art/Entities/{0}.prefab",entityAssetName);
        }
        
        public static string GetUISprite(string spritePath)
        {
            return Utility.Text.Format("Art/UI/UISprites/{0}",spritePath);
        }

        public static string GetTimeLineAsset(string timeLinePath)
        {
            return Utility.Text.Format("Art/Battle/TimeLine/{0}.playable",timeLinePath);
        }
    }
}
