using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


[System.Serializable]
public struct TextSetReference
{
    public string ID;

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(TextSetReference))]
    public class TextSetReferenceEditor : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 60;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 20;
            var id = property.FindPropertyRelative("ID");
            EditorGUI.PropertyField(position, id, label);

            position.y += 20;
            position.height = 40;

            var levelManager = GameObject.FindObjectOfType<LevelManager>();

            if (levelManager)
            {
                if (levelManager.TryGetRawTextSet(id.stringValue, out TextSet textSet))
                {
                    EditorGUI.HelpBox(position, string.Format("PC: {0}\nVR: {1}", textSet.PCText, textSet.VRText), MessageType.Info);
                }
                else
                {
                    EditorGUI.HelpBox(position, string.Format("找不到"), MessageType.Warning);
                }
            }
        }
    }
#endif
}
