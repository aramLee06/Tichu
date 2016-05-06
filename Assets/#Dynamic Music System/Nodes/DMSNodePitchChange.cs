using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodePitchChange : DMSNode
    {
        public float targetPitch = 1;
        public float speed;

        public DMSNodePitchChange()
        {
            #if UNITY_EDITOR
                color = new Color(.4f,.4f,1f);
            #endif
            nodeType = NodeType.PITCH_CHANGE;
            name = "New Pitch Changer";
        }
    }
}