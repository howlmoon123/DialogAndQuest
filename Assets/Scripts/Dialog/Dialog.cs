using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Dialog Item")]
    public class Dialog : ScriptableObject
    {
        [SerializeField]
        List<DialogNode> nodes = new List<DialogNode>();

        Dictionary<string, DialogNode> nodeLookup = new Dictionary<string, DialogNode>();

# if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count == 0)
            {
                nodes.Add(new DialogNode());
            }
        }

        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogNode node in GetAllNodes())
            {
               
                    nodeLookup[node.uniqueId] = node;
               
            }
            
        }


        public DialogNode GetRootNode()
        {
            return nodes[0];
        }

        
#endif
        public IEnumerable<DialogNode> GetAllNodes()
        {
            return nodes;
        }
        public IEnumerable<DialogNode> GetAllChildren(DialogNode parentNode)
        {
           
            foreach (string childId in parentNode.children)
            {
                if (nodeLookup.ContainsKey(childId))
                {
                   yield return(nodeLookup[childId]);
                }
            }
            
        }
    }
}