using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct TextSet
{
    public string ID;
    [TextArea(4, 10)]
    public string PCText;
    public AudioClip PCClip;
    [TextArea(4, 10)]
    public string VRText;
    public AudioClip VRClip;

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TextSet))]
    public class TextSetEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float pcHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("PCText"), GUIContent.none);
            float vrHeight = EditorGUI.GetPropertyHeight(property.FindPropertyRelative("VRText"), GUIContent.none);
            return 20 + Mathf.Max(pcHeight, vrHeight) + 40;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect idPosition = position;
            idPosition.height = 20;
            EditorGUI.PropertyField(idPosition, property.FindPropertyRelative("ID"));

            Rect pcLabelPosition = position;
            pcLabelPosition.y += 20;
            pcLabelPosition.width /= 2;
            pcLabelPosition.height = 20;

            Rect pcTextPosition = pcLabelPosition;

            Rect vrLabelPosition = position;
            vrLabelPosition.width /= 2;
            vrLabelPosition.height = 20;
            vrLabelPosition.y += 20;
            vrLabelPosition.x += vrLabelPosition.width;

            Rect vrTextPosition = vrLabelPosition;


            EditorGUI.LabelField(pcLabelPosition, "PC");
            var pcText = property.FindPropertyRelative("PCText");
            pcTextPosition.height = EditorGUI.GetPropertyHeight(pcText, GUIContent.none);
            EditorGUI.PropertyField(pcTextPosition, pcText, GUIContent.none);

            pcLabelPosition.y += pcTextPosition.height;
            EditorGUI.PropertyField(pcLabelPosition, property.FindPropertyRelative("PCClip"), GUIContent.none);

            EditorGUI.LabelField(vrLabelPosition, "VR");
            var vrText = property.FindPropertyRelative("VRText");
            vrTextPosition.height = EditorGUI.GetPropertyHeight(vrText, GUIContent.none);
            EditorGUI.PropertyField(vrTextPosition, vrText, GUIContent.none);

            vrLabelPosition.y += vrTextPosition.height;
            EditorGUI.PropertyField(vrLabelPosition, property.FindPropertyRelative("VRClip"), GUIContent.none);

            EditorGUI.EndProperty();
        }
    }
#endif
}
