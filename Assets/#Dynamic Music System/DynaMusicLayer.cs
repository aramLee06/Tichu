using UnityEngine;
using System.Collections.Generic;

namespace DynamicMusicSystem
{
    /// <summary>
    /// Layer class for DynaMusic.
    /// </summary>
    [System.Serializable]
    public class DynaMusicLayer
    {
        public string name = "New Layer";
        [System.NonSerialized] public List<DMSNode> nodeList; //A generalised list of nodes.
        [System.NonSerialized] private ulong nextUUID;
        public ulong startUUID;

        //From here on out it's separated lists for each kind of node, as a generalised (parent) class won't serialize its child properties.
        [SerializeField] private List<DMSNodeClip> clipNodes;
        [SerializeField] private DMSNodeEnd endNode;
        public DMSNodeEntry entryNode;
        [SerializeField] private DMSNodeLoop loopNode;
        [SerializeField] private List<DMSNodePause> pauseNodes;
        [SerializeField] private List<DMSNodePitchChange> pitchNodes;
        [SerializeField] private List<DMSNodeVolumeChange> volumeNodes;
        [SerializeField] private List<DMSNodeOperation> operationNodes;
        #if UNITY_EDITOR
            [SerializeField] private List<DMSNodeComment> commentNodes;
        #endif

        public DynaMusicLayer(ulong startUUID)
        {
            this.startUUID = startUUID;
            nextUUID = startUUID;

            LoadClass();
        }

        private bool ContainsUUID(ulong uuid)
        {
            foreach(DMSNode node in nodeList)
            {
                if (node.UUID == uuid)
                    return true;
            }
            return false;
        }

        public ulong GetUUID()
        {
            while (ContainsUUID(nextUUID))
                nextUUID++;
            ulong retval = nextUUID;
            nextUUID++;
            return retval;
        }

        /// <summary>
        /// Set up class for easy-usage
        /// </summary>
        public void LoadClass()
        {
            nextUUID = startUUID;

            nodeList = new List<DMSNode>();

            if (clipNodes != null)
            {
                foreach (DMSNode node in clipNodes)
                {
                    nodeList.Add(node);
                    nextUUID++;
                }
            }

            if (operationNodes != null)
            {
                foreach (DMSNode node in operationNodes)
                {
                    nodeList.Add(node);
                    nextUUID++;
                }
            }

            if (volumeNodes != null)
            {
                foreach (DMSNode node in volumeNodes)
                {
                    nodeList.Add(node);
                    nextUUID++;
                }
            }

            if (pitchNodes != null)
            {
                foreach (DMSNode node in pitchNodes)
                {
                    nodeList.Add(node);
                    nextUUID++;
                }
            }

            if (pauseNodes != null)
            {
                foreach (DMSNode node in pauseNodes)
                {
                    nodeList.Add(node);
                    nextUUID++;
                }
            }

            #if UNITY_EDITOR
                if (commentNodes != null)
                {
                    foreach (DMSNode node in commentNodes)
                    {
                        nodeList.Add(node);
                        nextUUID++;
                    }
                }
            #endif

            if (endNode != null)
                nodeList.Add(endNode);
            else
            {
                nodeList.Add(new DMSNodeEnd() { UUID = nextUUID });
                endNode = new DMSNodeEnd() { UUID = nextUUID };
            }
            nextUUID++;

            if (entryNode != null)
                nodeList.Add(entryNode);
            else
            {
                nodeList.Add(new DMSNodeEntry() { UUID = nextUUID });
                entryNode = new DMSNodeEntry() { UUID = nextUUID };
            }
            nextUUID++;

            if (loopNode != null)
                nodeList.Add(loopNode);
            else
            {
                nodeList.Add(new DMSNodeLoop() { UUID = nextUUID });
                loopNode = new DMSNodeLoop() { UUID = nextUUID };
            }
            nextUUID++;
        }

        /// <summary>
        /// Save class so it can be properly serialized.
        /// </summary>
        public void SaveClass()
        {
            if (clipNodes == null)
                clipNodes = new List<DMSNodeClip>();
            if (pauseNodes == null)
                pauseNodes = new List<DMSNodePause>();
            if (pitchNodes == null)
                pitchNodes = new List<DMSNodePitchChange>();
            if (volumeNodes == null)
                volumeNodes = new List<DMSNodeVolumeChange>();
            if (operationNodes == null)
                operationNodes = new List<DMSNodeOperation>();

            #if UNITY_EDITOR
                if (commentNodes == null)
                    commentNodes = new List<DMSNodeComment>();
                commentNodes.Clear();
            #endif

            clipNodes.Clear();
            pauseNodes.Clear();
            operationNodes.Clear();
            pitchNodes.Clear();
            volumeNodes.Clear();

            foreach(DMSNode node in nodeList)
            {
                if (node.GetType() == typeof(DMSNodeClip))
                    clipNodes.Add(node as DMSNodeClip);
                else if (node.GetType() == typeof(DMSNodePause))
                    pauseNodes.Add(node as DMSNodePause);
                else if (node.GetType() == typeof(DMSNodeOperation))
                    operationNodes.Add(node as DMSNodeOperation);
                else if (node.GetType() == typeof(DMSNodePitchChange))
                    pitchNodes.Add(node as DMSNodePitchChange);
                else if (node.GetType() == typeof(DMSNodeVolumeChange))
                    volumeNodes.Add(node as DMSNodeVolumeChange);
                else if (node.GetType() == typeof(DMSNodeEnd))
                    endNode = node as DMSNodeEnd;
                else if (node.GetType() == typeof(DMSNodeEntry))
                    entryNode = node as DMSNodeEntry;
                else if (node.GetType() == typeof(DMSNodeLoop))
                    loopNode = node as DMSNodeLoop;
                #if UNITY_EDITOR
                    else if (node.GetType() == typeof(DMSNodeComment))
                        commentNodes.Add(node as DMSNodeComment);
                #endif
            }
        }
    }
}