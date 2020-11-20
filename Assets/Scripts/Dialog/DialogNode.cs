using UnityEngine;


namespace Dialog
{
    [System.Serializable]
    public class DialogNode
    {
        public string uniqueId;
        public string text;
        public string[] children;
        public Rect rect = new Rect(0, 0, 200, 250);

    }
}

