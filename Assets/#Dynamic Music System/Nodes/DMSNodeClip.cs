using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeClip : DMSNode
    {
        public AudioClip clip;
        public float length = 1;

        public DMSNodeClip()
        {
            #if UNITY_EDITOR
                color = new Color(.9f,.9f,1f);
            #endif
            nodeType = NodeType.CLIP;
            name = "New Node";
        }
    }
}