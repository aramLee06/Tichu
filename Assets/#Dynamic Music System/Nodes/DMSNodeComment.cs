using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeComment : DMSNode
    {
        public DMSNodeComment()
        {
            #if UNITY_EDITOR
                color = new Color(0,0,.5f,.5f);
                position.height /= 2;
                position.width *= 2;
            #endif
            nodeType = NodeType.COMMENT;
            name = "New Comment";
        }
    }
}