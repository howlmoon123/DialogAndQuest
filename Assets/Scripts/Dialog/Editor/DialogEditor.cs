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
                EditorGUILayout.LabelField(selectedDialog.name);
            }
        }
    }
}