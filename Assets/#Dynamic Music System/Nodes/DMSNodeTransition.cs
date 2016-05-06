using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeTransition
    {
        public DMSCondition condition = new DMSCondition();
        public bool useCondition = false;

        [System.NonSerialized]public DMSNode _targetNode;
        /*private DMSNode targetNode
        {
            get
            {
                return _targetNode;
            }

            set
            {
                _targetNode = value;
                targetUUID = value.UUID;
            }
        }*/
        public ulong targetUUID;

        public void GetTargetNode(DynaMusicLayer myLayer)
        {
            foreach(DMSNode node in myLayer.nodeList)
            {
                if(node.UUID == targetUUID)
                {
                    _targetNode = node;
                    targetUUID = node.UUID;
                }
            }
        }
    }

    [System.Serializable]
    public struct DMSCondition
    {
        public ConditionType conditionType;
        public string variableName;
        public float value;

        public enum ConditionType
        {
            EQUAL,
            GREATER,
            LOWER,
            NOT_EQUAL,
            EQUAL_GREATER,
            EQUAL_LOWER,
        }
    }
}