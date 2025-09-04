using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif


[CreateAssetMenu(fileName = "LevelTextData", menuName = "LevelTextData", order = 1)]
public class LevelTextData : ScriptableObject
{
    public readonly static string CHINESE_SPACE = " ";

    public static string StripUselessCharacters(string text)
    {
        return text.Replace("\r", "").Replace(CHINESE_SPACE, "");
    }

    public TextSet[] TextSets;


    public bool GetText(string id, bool isVR, out string data)
    {
        foreach (var textSet in TextSets)
        {
            if (textSet.ID == id)
            {
                if (textSet.VRText == "")
                    data = StripUselessCharacters(textSet.PCText);
                else if (textSet.PCText == "")
                    data = StripUselessCharacters(textSet.VRText);
                else
                    data = isVR ? StripUselessCharacters(textSet.VRText) : StripUselessCharacters(textSet.PCText);

                return true;
            }
        }

        data = "";
        return false;
    }
    public bool GetTextAndAudioClip(string id, bool isVR, out string text, out AudioClip clip)
    {
        text = string.Empty;
        clip = null;

        foreach (var textSet in TextSets)
        {
            if (textSet.ID == id)
            {
                if (textSet.VRText == "")
                    text = textSet.PCText.Replace("\r", "");
                else if (textSet.PCText == "")
                    text = textSet.VRText.Replace("\r", "");
                else
                    text = isVR ? textSet.VRText.Replace("\r", "") : textSet.PCText.Replace("\r", "");

                if (textSet.VRClip)
                    clip = textSet.VRClip;
                else if (textSet.PCClip)
                    clip = textSet.PCClip;

                return true;
            }
        }

        return false;
    }

    public bool GetRawTextSet(string id, out TextSet _set)
    {
        foreach (var textSet in TextSets)
        {
            if (textSet.ID == id)
            {
                _set = textSet;
                return true;
            }
        }

        _set = new TextSet();
        return false;
    }

#if UNITY_EDITOR
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ \n";
    private const string Number = "0123456789";
    private const string Symbol = "!\"#$%^'()*+,-./:;<=>?@[]\\^_`{}|~";

    [MenuItem("Tools/掃描所有 LeveData 的文字")]
    public static void ScaneEveryLeveDataText()
    {
        string[] guids = AssetDatabase.FindAssets("t:LevelTextData");

        List<char> allChars = new List<char>();

        allChars.AddRange(Alphabet.ToCharArray());
        allChars.AddRange(Number.ToCharArray());
        allChars.AddRange(Symbol.ToCharArray());

        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            LevelTextData levelTextData = AssetDatabase.LoadAssetAtPath<LevelTextData>(path);

            if (!levelTextData)
                continue;

            for (int i = 0; i < levelTextData.TextSets.Length; i++)
            {
                var textSet = levelTextData.TextSets[i];

                foreach (var c in textSet.PCText)
                {
                    if (!allChars.Contains(c))
                        allChars.Add(c);
                }
                foreach (var c in textSet.VRText)
                {
                    if (!allChars.Contains(c))
                        allChars.Add(c);
                }
            }
        }
        Debug.Log(string.Join("", allChars.ToArray()));
    }


    [CustomEditor(typeof(LevelTextData))]
    public class LevelTextDataEditor : Editor
    {
        private SerializedProperty list; 
        string search = "";

        private void OnEnable()
        {
            list = serializedObject.FindProperty("TextSets");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space(10);
            search = EditorGUILayout.TextField(search);

            if (search != "")
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("Search Result: ");

                for (int i = 0; i < list.arraySize; i++)
                {
                    var textSet = list.GetArrayElementAtIndex(i);

                    var id = textSet.FindPropertyRelative("ID");
                    var pcText = textSet.FindPropertyRelative("PCText");
                    var vrText = textSet.FindPropertyRelative("VRText");

                    if (id.stringValue.Contains(search))
                    {
                        sb.Append(id.stringValue);
                        sb.Append(" ; ");
                    }
                    else if (pcText.stringValue.Contains(search))
                    {
                        sb.Append(id.stringValue);
                        sb.Append(" ; ");
                    }
                    else if (vrText.stringValue.Contains(search))
                    {
                        sb.Append(id.stringValue);
                        sb.Append(" ; ");
                    }
                }

                EditorGUILayout.LabelField(sb.ToString());
            }

            EditorGUILayout.Space(10);

            EditorGUILayout.PropertyField(list);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
