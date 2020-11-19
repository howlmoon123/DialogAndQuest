using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Dialog.Editor
{
    public class DialogEditor : EditorWindow
    {
        [MenuItem("Window/Dialog Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogEditor), false, "Dialog Editor");
        }

        [OnOpenAssetAttribute(2)]
        public static bool OnOpenAsset(int instanceId, int line )
        {
            Dialog editor = EditorUtility.InstanceIDToObject(instanceId) as Dialog;
            if(editor != null)
            {
                ShowEditorWindow();
            }
            return true;
        }
    }
}