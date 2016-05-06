using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeEnd : DMSNode
    {
        public DMSNodeEnd()
        {
            #if UNITY_EDITOR
                color = Color.red;
                deletable = false;
                position.position += new Vector2(500, -100);
            #endif
            nodeType = NodeType.END;
            name = "End";
        }
    }
}