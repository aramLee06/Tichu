using UnityEngine;
using System.Collections.Generic;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNode
    {
        public string name = "New Node";
        #if UNITY_EDITOR
            public Color color = Color.white;
            public bool deletable = true;
            public Rect position = new Rect(500, 200, 140, 70);
        #endif
        public NodeType nodeType = NodeType.NONE;
        public ulong UUID;

        public int bpm = 130;

        [HideInInspector] public List<DMSNodeTransition> transitions = new List<DMSNodeTransition>();

        public enum NodeType
        {
            NONE,
            ENTRY,
            CLIP,
            LOOP,
            END,
            BLEND,
            COMMENT,
            PAUSE,
            OPERATION,
            PITCH_CHANGE,
            VOLUME_CHANGE,
        }
    }
}