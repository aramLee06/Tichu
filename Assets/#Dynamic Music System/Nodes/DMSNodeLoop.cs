using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeLoop : DMSNode
    {
        public DMSNodeLoop()
        {
            #if UNITY_EDITOR
                color = Color.yellow;
                position.position += new Vector2(500, 100);
                deletable = false;
            #endif
            nodeType = NodeType.LOOP;
            name = "Loop";
        }
    }
}