//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace editor.cfg.error
{

    public enum EOperation
    {
        /// <summary>
        /// 登出
        /// </summary>
        LOGOUT = 0,
        /// <summary>
        /// 重启
        /// </summary>
        RESTART = 1,
    }

    public static partial class EOperation_Metadata
    {
        public static readonly Bright.Config.EditorEnumItemInfo LOGOUT = new Bright.Config.EditorEnumItemInfo("LOGOUT", "登出", 0, "登出");
        public static readonly Bright.Config.EditorEnumItemInfo RESTART = new Bright.Config.EditorEnumItemInfo("RESTART", "重启", 1, "重启");

        private static readonly System.Collections.Generic.List<Bright.Config.EditorEnumItemInfo> __items = new System.Collections.Generic.List<Bright.Config.EditorEnumItemInfo>
        {
            LOGOUT,
            RESTART,
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
