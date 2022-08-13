using GameFramework;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;

namespace JMatrix.Editor
{
    public sealed class JMatrixBuildUpdateEventHandler : IBuildEventHandler
    {
        string versionFilePath;
        public bool ContinueOnFailure
        {
            get
            {
                return false;
            }
        }

        public void OnPreprocessAllPlatforms(string productName, string companyName, string gameIdentifier, string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName, bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName, string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions,
            string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
            versionFilePath = outputFullPath;

            string streamingAssetsPath = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "StreamingAssets"));
            if (!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
                return;
            }

            string[] fileNames = Directory.GetFiles(streamingAssetsPath, "*", SearchOption.AllDirectories);
            foreach (string fileName in fileNames)
            {
                if (fileName.Contains(".gitkeep"))
                {
                    continue;
                }

                File.Delete(fileName);
            }

            Utility.Path.RemoveEmptyDirectory(streamingAssetsPath);


        }

        public void OnPostprocessAllPlatforms(string productName, string companyName, string gameIdentifier, string gameFrameworkVersion, string unityVersion, string applicableGameVersion, int internalResourceVersion,
            Platform platforms, AssetBundleCompressionType assetBundleCompression, string compressionHelperTypeName, bool additionalCompressionSelected, bool forceRebuildAssetBundleSelected, string buildEventHandlerTypeName, string outputDirectory, BuildAssetBundleOptions buildAssetBundleOptions,
            string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, string buildReportPath)
        {
#if UNITY_ANDROID
            if (platforms != Platform.Android)
            {
                return;
            }
#endif

#if UNITY_IOS
            if (platforms != Platform.IOS)
            {
                return;
            }    
#endif
            string streamingAssetsPath = Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "StreamingAssets"));

            if (!Directory.Exists(streamingAssetsPath))
            {
                Directory.CreateDirectory(streamingAssetsPath);
            }
            string outputPackedPlatPath = Path.Combine(outputPackedPath + platforms.ToString());
            Utility.Path.CopyFolder(outputPackedPlatPath, streamingAssetsPath);
        }

        public void OnPreprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath)
        {
        }

        public void OnBuildAssetBundlesComplete(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, AssetBundleManifest assetBundleManifest)
        {

        }

        public void OnOutputUpdatableVersionListData(Platform platform, string versionListPath, int versionListLength, int versionListHashCode, int versionListCompressedLength, int versionListCompressedHashCode)
        {
            ResourceVersionInfo resourceVersionInfo = new ResourceVersionInfo(versionListLength, versionListHashCode, versionListCompressedLength, versionListCompressedHashCode);
            string versionFilePlatformPath = versionFilePath + platform.ToString();
            if (!Directory.Exists(versionFilePlatformPath))
            {
                Directory.CreateDirectory(versionFilePlatformPath);
            }
            string fullPath = versionFilePlatformPath + "/Version.txt";
            File.WriteAllText(fullPath,LitJson.JsonMapper.ToJson(resourceVersionInfo));
        }

        public void OnPostprocessPlatform(Platform platform, string workingPath, bool outputPackageSelected, string outputPackagePath, bool outputFullSelected, string outputFullPath, bool outputPackedSelected, string outputPackedPath, bool isSuccess)
        {
           
        }
    }
}
