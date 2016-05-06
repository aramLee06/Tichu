using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodePause : DMSNode
    {
        public float duration;

        public DMSNodePause()
        {
            #if UNITY_EDITOR
                color = new Color(.9f,1f,.9f);
            #endif
            nodeType = NodeType.PAUSE;
            name = "New Pause";
        }
    }
}