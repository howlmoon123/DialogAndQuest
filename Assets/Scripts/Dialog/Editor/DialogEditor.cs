using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Dialog.Editor
{
    public class DialogEditor : EditorWindow
    {
        private Dialog selectedDialog = null;
        private GUIStyle nodeStyle;
        Vector2 draggingOffset;
        private DialogNode draggingNode = null;

        [MenuItem("Window/Dialog Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogEditor), false, "Dialog Editor");
        }

        [OnOpenAsset(2)]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            Dialog editor = EditorUtility.InstanceIDToObject(instanceId) as Dialog;
            if (editor != null)
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
            if (selectedDialog == null)
            {
                EditorGUILayout.LabelField("No Dialog Selected");
            }
            else
            {
                ProcessEvents();
                foreach (DialogNode node in selectedDialog.GetAllNodes())
                {
                    OnGUINode(node);
                }
            }
        }

        private void ProcessEvents()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition);
                if(draggingNode != null)
                {
                    draggingOffset = draggingNode.rect.position - Event.current.mousePosition;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialog, "Move Dialog node");
                draggingNode.rect.position = Event.current.mousePosition + draggingOffset;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
        }

       

        private void OnGUINode(DialogNode node)
        {
            GUILayout.BeginArea(node.rect, nodeStyle);
            EditorGUI.BeginChangeCheck();
           

            EditorGUILayout.LabelField("Node:", EditorStyles.whiteLabel);

            string newText = EditorGUILayout.TextField(node.text);
            string uniqueId = EditorGUILayout.TextField(node.uniqueId);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(selectedDialog, "Update Dialog text");
                node.text = newText;
                node.uniqueId = uniqueId;
            }

            foreach (DialogNode childNode in selectedDialog.GetAllChildren(node))
            {
                EditorGUILayout.LabelField(childNode.text);
            }

            GUILayout.EndArea();
        }

        private DialogNode GetNodeAtPoint(Vector2 point)
        {
            DialogNode foundNode = null;
            foreach (DialogNode node in selectedDialog.GetAllNodes())
            {
                if (node.rect.Contains(point))
                {
                    foundNode = node;
                }
            }
            return foundNode;
        }
    }
}