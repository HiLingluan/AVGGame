//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace editor.cfg.test
{

    public enum AudioType
    {
        UNKNOWN = 0,
        ACC = 1,
        AIFF = 2,
    }

    public static partial class AudioType_Metadata
    {
        public static readonly Bright.Config.EditorEnumItemInfo UNKNOWN = new Bright.Config.EditorEnumItemInfo("UNKNOWN", "", 0, "");
        public static readonly Bright.Config.EditorEnumItemInfo ACC = new Bright.Config.EditorEnumItemInfo("ACC", "", 1, "");
        public static readonly Bright.Config.EditorEnumItemInfo AIFF = new Bright.Config.EditorEnumItemInfo("AIFF", "", 2, "");

        private static readonly System.Collections.Generic.List<Bright.Config.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Bright.Config.EditorEnumItemInfo>
        {
            UNKNOWN,
            ACC,
            AIFF,
        };

        public static System.Collections.Generic.List<Bright.Config.EditorEnumItemInfo> GetItems() => __items;

        public static Bright.Config.EditorEnumItemInfo GetByName(string name)
        {
            return __items.Find(c => c.Name == name);
        }

        public static Bright.Config.EditorEnumItemInfo GetByNameOrAlias(string name)
        {
            return __items.Find(c => c.Name == name || c.Alias == name);
        }

        public static Bright.Config.EditorEnumItemInfo GetByValue(int value)
        {
            return __items.Find(c => c.Value == value);
        }
    }
}
