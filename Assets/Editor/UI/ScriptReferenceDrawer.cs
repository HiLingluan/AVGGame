
using Loxodon.Framework.XLua;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Editor
{
    [CustomPropertyDrawer(typeof(Game.UI.ScriptReference))]
    public class ScriptReferenceDrawer : PropertyDrawer
    {
        private const float HORIZONTAL_GAP = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            var objectProperty = property.FindPropertyRelative("cachedAsset");
            var typeProperty = property.FindPropertyRelative("type");
            var filenameProperty = property.FindPropertyRelative("filename");

            float y = position.y;
            float x = position.x;
            float height = GetPropertyHeight(property, label);
            float width = position.width - HORIZONTAL_GAP * 2;

            Rect nameRect = new Rect(x, y, 60, height);
            Rect typeRect = new Rect(nameRect.xMax + HORIZONTAL_GAP, y, 80, height);
            Rect valueRect = new Rect(typeRect.xMax + HORIZONTAL_GAP, y, 100, height);
            Rect fileNameRect = new Rect(valueRect.xMax + HORIZONTAL_GAP, y, position.xMax - valueRect.xMax - HORIZONTAL_GAP, height);

            EditorGUI.LabelField(nameRect, property.displayName);
            EditorGUI.LabelField(fileNameRect, filenameProperty.stringValue);
            
            Object asset = objectProperty.objectReferenceValue;


            EditorGUI.BeginChangeCheck();
            if (EditorGUI.EndChangeCheck())
            {
                if (ValidateSetting(asset))
                {
                    UpdateProperty(filenameProperty, asset);
                }
            }

            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 0.1f;

            EditorGUI.BeginChangeCheck();
            Object newAsset = null;
            if (asset != null)
            {
                var name = asset.name;
                asset.name = filenameProperty.stringValue;
                newAsset = EditorGUI.ObjectField(valueRect, GUIContent.none, asset, typeof(Object), false);
                asset.name = name;
            }
            else
            {
                newAsset = EditorGUI.ObjectField(valueRect, GUIContent.none, asset, typeof(Object), false);
            }

            if (EditorGUI.EndChangeCheck())
            {
                if (ValidateAsset(newAsset) && ValidateSetting(newAsset))
                {
                    objectProperty.objectReferenceValue = null;
                    UpdateProperty(filenameProperty,newAsset);
                }
            }

            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.EndProperty();
        }

        protected virtual bool ValidateAsset(Object asset)
        {
            if (asset == null)
                return true;

            if (!(asset is TextAsset || asset is DefaultAsset))
            {
                Debug.LogWarningFormat("Invalid asset for ScriptReference");
                return false;
            }

            string path = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(path))
                return false;

            if (asset is DefaultAsset && Directory.Exists(path))
            {
                Debug.LogWarningFormat("Invalid asset for ScriptReference path = '{0}'.", path);
                return false;
            }

            if (path.EndsWith(".cs"))
            {
                Debug.LogWarningFormat("Invalid asset for ScriptReference path = '{0}'.", path);
                return false;
            }
            return true;
        }

        protected virtual bool ValidateSetting(Object asset)
        {
            //if (asset == null || type == ScriptReferenceType.TextAsset)
                return true;

            string path = AssetDatabase.GetAssetPath(asset);
            LuaSettings luaSettings = LuaSettings.GetOrCreateSettings();
            foreach (string root in luaSettings.SrcRoots)
            {
                if (path.StartsWith(root))
                    return true;
            }

            if (path.IndexOf("Resources") >= 0)
                return true;

            if (EditorUtility.DisplayDialog("Notice", string.Format("The file \"{0}\" is not in the source code folder of lua. Do you want to add a source code folder?", asset.name), "Yes", "Cancel"))
            {
                SettingsService.OpenProjectSettings("Project/LuaSettingsProvider");
                return false;
            }
            else
            {
                return true;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label);
        }

        public virtual void UpdateProperty(SerializedProperty filenameProperty, Object asset)
        {
            if (asset != null)
                filenameProperty.stringValue = GetFilename(asset);
            else
                filenameProperty.stringValue = null;
        }

        protected virtual string GetFilename(Object asset)
        {
            if (asset == null)
                return null;

            string path = AssetDatabase.GetAssetPath(asset);
            if (string.IsNullOrEmpty(path))
                return null;

            int start = path.LastIndexOf("/");
            int dotIndex = path.IndexOf(".", start);
            if (dotIndex > -1)
                path = path.Substring(0, dotIndex);

            LuaSettings luaSettings = LuaSettings.GetOrCreateSettings();
            foreach (string root in luaSettings.SrcRoots)
            {
                if (path.StartsWith(root))
                {
                    path = path.Replace(root + "/", "").Replace("/", ".");
                    return path;
                }
            }

            int index = path.IndexOf("Resources");
            if (index >= 0)
                path = path.Substring(index + 10);

            path = path.Replace("/", ".");
            return path;
        }
    }
}