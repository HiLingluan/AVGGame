using Game.UI;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Editor
{
    [CustomPropertyDrawer(typeof(Variable))]
    public class VariableDrawer : PropertyDrawer
    {
        private const float HORIZONTAL_GAP = 5;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var name = property.FindPropertyRelative("name");
            var objectValue = property.FindPropertyRelative("objectValue");
            var dataValue = property.FindPropertyRelative("dataValue");
            var variableType = property.FindPropertyRelative("variableType");

            float y = position.y;
            float x = position.x;
            float height = GetPropertyHeight(property, label);
            float width = position.width - HORIZONTAL_GAP * 2;

            Rect nameRect = new Rect(x, y, Mathf.Min(200, width * 0.4f), height);
            Rect typeRect = new Rect(nameRect.xMax + HORIZONTAL_GAP, y, Mathf.Min(120, width * 0.2f), height);
            Rect valueRect = new Rect(typeRect.xMax + HORIZONTAL_GAP, y, position.xMax - typeRect.xMax - HORIZONTAL_GAP, height);

            EditorGUI.PropertyField(nameRect, name, GUIContent.none);

            VariableType variableTypeValue = (VariableType)variableType.enumValueIndex;

            if (variableTypeValue == VariableType.Component)
            {
                int index = 0;
                List<System.Type> types = new List<System.Type>();
                var component = (Component)objectValue.objectReferenceValue;
                if (component != null)
                {
                    GameObject go = component.gameObject;
                    foreach (var c in go.GetComponents<Component>())
                    {
                        if (c == null)
                            continue;

                        if (!types.Contains(c.GetType()))
                            types.Add(c.GetType());
                    }

                    for (int i = 0; i < types.Count; i++)
                    {
                        if (component.GetType().Equals(types[i]))
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (types.Count <= 0)
                    types.Add(typeof(Transform));

                List<GUIContent> contents = new List<GUIContent>();
                foreach (var t in types)
                {
                    contents.Add(new GUIContent(t.Name, t.FullName));
                }

                EditorGUI.BeginChangeCheck();
                var newIndex = EditorGUI.Popup(typeRect, GUIContent.none, index, contents.ToArray(), EditorStyles.popup);
                if (EditorGUI.EndChangeCheck())
                {
                    if (component != null)
                        objectValue.objectReferenceValue = component.gameObject.GetComponent(types[newIndex]);
                    else
                        objectValue.objectReferenceValue = null;
                }
            }
            else
            {
                EditorGUI.LabelField(typeRect, variableTypeValue.ToString());
            }

            switch (variableTypeValue)
            {
                case VariableType.Component:
                    EditorGUI.BeginChangeCheck();
                    objectValue.objectReferenceValue = EditorGUI.ObjectField(valueRect, GUIContent.none, objectValue.objectReferenceValue, typeof(UnityEngine.Component), true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (string.IsNullOrEmpty(name.stringValue) && objectValue.objectReferenceValue != null)
                            name.stringValue = NormalizeName(objectValue.objectReferenceValue.name);
                    }
                    break;
                case VariableType.GameObject:
                    EditorGUI.BeginChangeCheck();
                    objectValue.objectReferenceValue = EditorGUI.ObjectField(valueRect, GUIContent.none, objectValue.objectReferenceValue, typeof(UnityEngine.GameObject), true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (string.IsNullOrEmpty(name.stringValue) && objectValue.objectReferenceValue != null)
                            name.stringValue = NormalizeName(objectValue.objectReferenceValue.name);
                    }
                    break;
                case VariableType.Object:
                    EditorGUI.BeginChangeCheck();
                    objectValue.objectReferenceValue = EditorGUI.ObjectField(valueRect, GUIContent.none, objectValue.objectReferenceValue, typeof(UnityEngine.Object), true);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (string.IsNullOrEmpty(name.stringValue) && objectValue.objectReferenceValue != null)
                            name.stringValue = NormalizeName(objectValue.objectReferenceValue.name);
                    }
                    break;
                default:
                    break;
            }
            EditorGUI.EndProperty();
        }


        protected virtual string NormalizeName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "";

            name = name.Replace(" ", "");
            return char.ToLower(name[0]) + name.Substring(1);
        }
    }
}