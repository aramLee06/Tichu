using UnityEngine;
using System.Collections;

namespace DynamicMusicSystem
{
    [System.Serializable]
    public class DMSNodeVolumeChange : DMSNode
    {
        public float targetVolume = 1;
        public float speed;

        public DMSNodeVolumeChange()
        {
            #if UNITY_EDITOR
                color = new Color(.4f,1f,.4f);
            #endif
            nodeType = NodeType.VOLUME_CHANGE;
            name = "New Volume Changer";
        }
    }
}