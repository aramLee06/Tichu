using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeEntry : DMSNode
    {
        public DMSNodeEntry()
        {
            #if UNITY_EDITOR
                color = Color.green;
                deletable = false;
            #endif
            nodeType = NodeType.ENTRY;
            name = "Entry";
        }
    }
}