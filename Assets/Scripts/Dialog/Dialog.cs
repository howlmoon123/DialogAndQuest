using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialog
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/Dialog Item")]
    public class Dialog : ScriptableObject
    {
        [SerializeField] DialogNode[] nodes;

    }
}