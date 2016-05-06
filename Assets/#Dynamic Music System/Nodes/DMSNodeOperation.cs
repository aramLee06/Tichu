using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeOperation : DMSNode
    {
        public string targetParameter;
        public Operation operation;
        public float value;

        public DMSNodeOperation()
        {
            #if UNITY_EDITOR
                color = new Color(.6f,1f,1f);
                position.height = 50;
            #endif
            nodeType = NodeType.OPERATION;
            name = "New Operation";
        }

        public enum Operation
        {
            SET,
            ADD,
            SUBTRACT,
            MULTIPLY,
            DIVIDE,
        }
    }
}