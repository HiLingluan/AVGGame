using GameFramework;

namespace Game
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Configs/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDataTableAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/DataTables/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDictionaryAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Localization/{0}/Dictionaries/{1}.{2}", GameEntry.Localization.Language, assetName, fromBytes ? "bytes" : "xml");
        }

        public static string GetFontAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Fonts/{0}.asset", assetName);
        }

        public static string GetSceneAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Scenes/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Sounds/{0}.mp3", assetName);
        }

        public static string GetEntityAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Entities/{0}.prefab", assetName);
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/UI/{0}.prefab", assetName);
        }

        public static string GetUISoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Sounds/{0}.mp3", assetName);
        }

        public static string GetPrefabAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Prefab/{0}.prefab", assetName);
        }

        public static string GetAtlasAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Atlas/{0}.png", assetName);
        }

        public static string GetAtlasABAsset(string assetName)
        {
            return Utility.Text.Format("GameMain/Res/Build/Atlas/{0}.dat", assetName);
        }

        public static string GetAtlasABName(string assetName)
        {
            return Utility.Text.Format("GameMain/Res/Build/Atlas/{0}", assetName).ToLower();
        }

        public static string GetTextureAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Textures/{0}", assetName);
        }

        public static string GetEffectAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/Effect/{0}.prefab", assetName);
        }

        public static string GetSpritesCollectionAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Res/Build/AtlasCollection/{0}.asset", assetName);
        }
    }
}
