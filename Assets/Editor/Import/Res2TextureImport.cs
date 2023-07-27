using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 图片导入格式设置
/// </summary>
public class TextureImportChange : AssetPostprocessor
{
    void OnPostprocessTexture(Texture2D texture)
    {
        //CheckTextureSize(texture);

        //if (assetPath.Contains("JMain/BuildAsset/Image"))
        //{
        //    TextureImporter importer = (TextureImporter)assetImporter;
        //    ImportImage(importer);
        //}

        if (assetPath.Contains("Build/Atlas"))
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            ImportAtlas(importer);
        }
    }

    void CheckTextureSize(Texture2D texture)
    {
        if(texture.width % 4 != 0 || texture.height % 4 != 0)
        {
            TextureImporter importer = (TextureImporter)assetImporter;
            Debug.LogError("图片不符合规范,请检查!"+ importer.assetPath);
            AssetDatabase.DeleteAsset(importer.assetPath);
        }
    }


    void ImportImage(TextureImporter importer)
    {
        importer.textureType = TextureImporterType.Sprite;
        importer.alphaIsTransparency = true;
        importer.isReadable = false;
        importer.mipmapEnabled = false;
        importer.textureShape = TextureImporterShape.Texture2D;
        //importer.spriteImportMode = SpriteImportMode.Single;

        TextureImporterSettings importerSet = new TextureImporterSettings
        {
            spriteGenerateFallbackPhysicsShape = false,
            spriteMeshType = SpriteMeshType.FullRect,
            spritePixelsPerUnit = 100
        };
        importerSet.ApplyTextureType(TextureImporterType.Sprite);
        importer.ReadTextureSettings(importerSet);
        importer.SetTextureSettings(importerSet);

        TextureImporterPlatformSettings androidTs = importer.GetPlatformTextureSettings("android");
        androidTs.maxTextureSize = 2048;
        androidTs.overridden = true;
        androidTs.format = TextureImporterFormat.ETC2_RGBA8;
        androidTs.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
        androidTs.compressionQuality = (int)TextureCompressionQuality.Best;
        androidTs.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
        importer.SetPlatformTextureSettings(androidTs);

        TextureImporterPlatformSettings iosTs = importer.GetPlatformTextureSettings("iPhone");
        iosTs.overridden = true;
        iosTs.maxTextureSize = 2048;
        iosTs.format = TextureImporterFormat.ASTC_4x4;//ASTC_RGBA_ 系统被禁止  错误避免是通过使用 ASTC_RGB_ 系统实现的。阿尔法判断似乎是自动的
        iosTs.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
        iosTs.compressionQuality = (int)TextureCompressionQuality.Best;
        importer.SetPlatformTextureSettings(iosTs);
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }

    void ImportAtlas(TextureImporter importer)
    {
        importer.textureType = TextureImporterType.Sprite;
        importer.alphaIsTransparency = true;
        importer.isReadable = false;
        importer.mipmapEnabled = false;
        importer.textureShape = TextureImporterShape.Texture2D;
        //importer.spriteImportMode = SpriteImportMode.Single;

        TextureImporterSettings importerSet = new TextureImporterSettings
        {
            spriteGenerateFallbackPhysicsShape = false,
            spriteMeshType = SpriteMeshType.FullRect,
            spritePixelsPerUnit = 100
        };
        importerSet.ApplyTextureType(TextureImporterType.Sprite);
        importer.ReadTextureSettings(importerSet);
        importer.SetTextureSettings(importerSet);

        TextureImporterPlatformSettings androidTs = importer.GetPlatformTextureSettings("android");
        androidTs.maxTextureSize = 2048;
        androidTs.overridden = true;
        androidTs.format = TextureImporterFormat.ETC2_RGBA8;
        androidTs.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
        androidTs.compressionQuality = (int)TextureCompressionQuality.Best;
        androidTs.androidETC2FallbackOverride = AndroidETC2FallbackOverride.UseBuildSettings;
        importer.SetPlatformTextureSettings(androidTs);

        TextureImporterPlatformSettings iosTs = importer.GetPlatformTextureSettings("iPhone");
        iosTs.overridden = true;
        iosTs.maxTextureSize = 2048;
        iosTs.format = TextureImporterFormat.ASTC_4x4;//ASTC_RGBA_ 系统被禁止  错误避免是通过使用 ASTC_RGB_ 系统实现的。阿尔法判断似乎是自动的
        iosTs.resizeAlgorithm = TextureResizeAlgorithm.Bilinear;
        iosTs.compressionQuality = (int)TextureCompressionQuality.Best;
        importer.SetPlatformTextureSettings(iosTs);
        EditorUtility.SetDirty(importer);
        importer.SaveAndReimport();
    }
}
