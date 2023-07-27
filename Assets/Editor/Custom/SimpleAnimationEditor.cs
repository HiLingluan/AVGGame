using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(SimpleAnimation))]
public class SimpleAnimationEditor : Editor
{
    static class Styles
    {
        //public static GUIContent animation = new GUIContent("Animation", "The clip that will be played if Play() is called, or if \"Play Automatically\" is enabled");
        public static GUIContent animations = new GUIContent("Animations", "角色包含动作信息");
        //public static GUIContent playAutomatically = new GUIContent("Play Automatically", "If checked, the default clip will automatically be played");
        //public static GUIContent animatePhysics = new GUIContent("Animate Physics", "If checked, animations will be updated at the same frequency as Fixed Update");

        //public static GUIContent cullingMode = new GUIContent("Culling Mode", "Controls what is updated when the object has been culled");
    }

    SerializedProperty clip;
    SerializedProperty states;
    //SerializedProperty playAutomatically;
    //SerializedProperty animatePhysics;
    //SerializedProperty cullingMode;

    void OnEnable()
    {
        clip = serializedObject.FindProperty("m_Clip");
        states = serializedObject.FindProperty("m_States");
        //playAutomatically = serializedObject.FindProperty("m_PlayAutomatically");
        //animatePhysics = serializedObject.FindProperty("m_AnimatePhysics");
        //cullingMode = serializedObject.FindProperty("m_CullingMode");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        //EditorGUILayout.PropertyField(clip, Styles.animation);
        EditorGUILayout.PropertyField(states, Styles.animations, true);
        //EditorGUILayout.PropertyField(playAutomatically, Styles.playAutomatically);
        //EditorGUILayout.PropertyField(animatePhysics, Styles.animatePhysics);
        //EditorGUILayout.PropertyField(cullingMode, Styles.cullingMode);

        Rect mExcelPathRect = EditorGUILayout.GetControlRect(GUILayout.Width(500));
        EditorGUI.PrefixLabel(mExcelPathRect, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("批量替换拖动到此处←"));
        if ((Event.current.type == EventType.DragUpdated || Event.current.type == EventType.DragExited) && mExcelPathRect.Contains(Event.current.mousePosition))
        {
            DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
        }

        if (Event.current.type == EventType.DragPerform && mExcelPathRect.Contains(Event.current.mousePosition))
        {
            List<AnimationClip> aniClipList = new List<AnimationClip>();
            if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0)
            {
                foreach (var path in DragAndDrop.paths)
                {
                    var clip = UnityEditor.AssetDatabase.LoadAssetAtPath<AnimationClip>(path);
                    aniClipList.Add(clip);
                }
            }
            states.arraySize = aniClipList.Count;
            for (int i = 0; i < states.arraySize; i++)
            {
                states.GetArrayElementAtIndex(i).FindPropertyRelative("clip").objectReferenceValue = aniClipList[i];
                states.GetArrayElementAtIndex(i).FindPropertyRelative("duration").floatValue = 0.3f;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}

[CustomPropertyDrawer(typeof(SimpleAnimation.EditorState))]
class StateDrawer : PropertyDrawer
{
    class Styles
    {
        public static readonly GUIContent disabledTooltip = new GUIContent("", "The Default state cannot be edited, change the Animation clip to change the Default State");
    }

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        string index = label.text.Substring(label.text.Length-1,1);
        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Keyboard), new GUIContent("动画配置"+ index));

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        EditorGUILayout.BeginHorizontal();
        // Calculate rects
        Rect clipRect = new Rect(position.x, position.y, 50, position.height);
        Rect durationRect = new Rect(position.x + position.width/2 + 5, position.y, position.width/2 - 5, position.height);

        Rect clipTip = new Rect(position.x + position.width / 2 -50, position.y, position.width / 2 - 5, position.height);
        Rect durationTip = new Rect(position.x-50, position.y, position.width / 2 - 10, 2);

        //EditorGUI.BeginDisabledGroup(property.FindPropertyRelative("defaultState").boolValue);
        EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("clip"), new GUIContent());
        EditorGUI.PrefixLabel(clipTip, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("动画片段"));

        EditorGUI.PropertyField(clipRect, property.FindPropertyRelative("duration"), new GUIContent());
        EditorGUI.PrefixLabel(durationTip, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("融合时间"));

        //if (property.FindPropertyRelative("defaultState").boolValue)
        //{
        //    EditorGUI.LabelField(position, Styles.disabledTooltip);
        //}

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.EndHorizontal();
        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
