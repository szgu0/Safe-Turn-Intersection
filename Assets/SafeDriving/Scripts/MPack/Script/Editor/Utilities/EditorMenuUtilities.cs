using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EditorMenuUtilities : MonoBehaviour
{
    [MenuItem("Tools/Rename", false, 0)]
    public static void OpenRenameWindow()
    {
        RenameWindow window = EditorWindow.GetWindow<RenameWindow>("Rename", true);
        window.SetSelection();
    }

    public class RenameWindow : EditorWindow
    {
        private GameObject[] _selection;
        private string prefix;

        public void SetSelection()
        {
            _selection = Selection.gameObjects;
            // GameObject[] objs = Selection.gameObjects;
            System.Array.Sort(_selection, new UnityTransformSort());
        }

        void OnGUI()
        {
            prefix = EditorGUILayout.TextField(prefix);

            if (GUILayout.Button("Rename"))
            {
                for (int i = 0; i < _selection.Length; i++)
                {
                    _selection[i].name = $"{prefix} ({i + 1})";
                }
                Close();
            }
        }
    }

    public class UnityTransformSort: System.Collections.Generic.IComparer<GameObject>
    {
        public int Compare(GameObject lhs, GameObject rhs)
        {
            if (lhs == rhs) return 0;
            if (lhs == null) return -1;
            if (rhs == null) return 1;
            return (lhs.transform.GetSiblingIndex() > rhs.transform.GetSiblingIndex()) ? 1 : -1;
        }
    }
}
