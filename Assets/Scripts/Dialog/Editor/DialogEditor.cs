using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Dialog.Editor
{
    public class DialogEditor : EditorWindow
    {
        Dialog selectedDialog = null;
        GUIStyle nodeStyle;
       

        [MenuItem("Window/Dialog Editor")]
        public static void ShowEditorWindow()
        {
           GetWindow(typeof(DialogEditor), false, "Dialog Editor");
           
        }

        [OnOpenAsset(2)]
        public static bool OnOpenAsset(int instanceId, int line )
        {
            Dialog editor = EditorUtility.InstanceIDToObject(instanceId) as Dialog;
            if(editor != null)
            {
                ShowEditorWindow();
            }
            return true;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;
            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);
        }


        private void OnSelectionChanged()
        {
            Dialog newDialogue = Selection.activeObject as Dialog;
            if (newDialogue != null)
            {
                selectedDialog = newDialogue;
                
                Repaint();
            }
        }

        private void OnGUI()
        {
           if(selectedDialog == null)
            {
               EditorGUILayout.LabelField("No Dialog Selected");
            }else
            {
                EditorGUI.BeginChangeCheck();
                foreach (DialogNode node in selectedDialog.GetAllNodes())
                {
                    OnGUINode(node);
                }
            }
        }

        private void OnGUINode(DialogNode node)
        {
            GUILayout.BeginArea(node.position, nodeStyle);
            EditorGUILayout.LabelField("Node:", EditorStyles.whiteLabel);
            string newText = EditorGUILayout.TextField(node.text);
            string uniqueId = EditorGUILayout.TextField(node.uniqueId);
            if (EditorGUI.EndChangeCheck())
            {

                Undo.RecordObject(selectedDialog, "Update Dialog text");
                node.text = newText;
                node.uniqueId = uniqueId;
            }

            GUILayout.EndArea();
        }
    }
}