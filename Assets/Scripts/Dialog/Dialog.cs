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

# if UNITY_EDITOR
        private void Awake()
        {
            if(nodes.Count == 0)
            {
                nodes.Add(new DialogNode());
            }
        }

        public IEnumerable<DialogNode> GetAllNodes()
        {
            return nodes;
        }
#endif
    }
}