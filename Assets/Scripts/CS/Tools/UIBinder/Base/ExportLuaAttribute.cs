using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;


namespace Game.Binder
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public sealed class ExportLuaAttribute : PropertyAttribute
    {
        public string comment = null;
        public ExportLuaAttribute(string comment)
        {
            this.comment = comment;
        }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ExportLuaAttribute))]
    public class TitleAttributeDrawer : DecoratorDrawer
    {
        private GUIStyle style = new GUIStyle();

        public override void OnGUI(Rect position)
        {
            ExportLuaAttribute ta = attribute as ExportLuaAttribute;
            GUI.Label(position, ta.comment);
        }
    }
#endif
}
