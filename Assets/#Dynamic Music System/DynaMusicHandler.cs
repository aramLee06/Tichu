using UnityEngine;
using System.Collections;
using DynamicMusicSystem;

/// <summary>
/// Layer end-node reached event.
/// </summary>
/// <param name="args"></param>
public delegate void DynaMusicLayerEnd(DynaMusicLayerEventArgs args);
/// <summary>
/// Layer loop-node reached event.
/// </summary>
/// <param name="args"></param>
public delegate void DynaMusicLayerLoop(DynaMusicLayerEventArgs args);

public class DynaMusicLayerEventArgs : System.EventArgs
{
    public int layerId;
    public DynaMusicHandler handler;
    public MusicController controller;

    public DynaMusicLayerEventArgs(int layerId, DynaMusicHandler handler, MusicController controller)
    {
        this.layerId = layerId;
        this.handler = handler;
        this.controller = controller;
    }
}

/// <summary>
/// Component class that handles the playing of the dynamic music tree.
/// </summary>
[AddComponentMenu("Dynamic Music System/DynaMusic Handler")]
public class DynaMusicHandler : MonoBehaviour
{
    public MusicController musicController; //The controller with the containing data.
    public AudioSource[] sources; //Array of all audiosources. Two will be made for each layer, for the sake of proper transitions.
    public bool[] activeSource; //Current primary source.
    public DMSNode[] lastNodes; //Last node for each layer.
    private DMSNode[] firstNodes; //First node ever played, used as a fall-back for broken start-node transitions. 
    public float[] pauseEndTime; //Time moment a pause node ends.
    public float[] layerPitch, layerVolume, pitchSpeed, volumeSpeed; //Various pitch/volume operation variables
    public float masterVolume = 1, masterPitch = 1; //Global volume and pitch.

    public event DynaMusicLayerEnd layerEndEvent = delegate { };
    public event DynaMusicLayerLoop layerLoopEvent = delegate { };

    private void Start() //Needs cleanup. Basically currently used to set up everything and activate the first node in each layer.
    {
        if(musicController == null)
        {
            Debug.LogWarning("No controller was attached to the DynaMusic Handler. Consider adding one.",this);
            Destroy(this);
        }

        musicController.LoadClass();
        sources = new AudioSource[musicController.layers.Count*2];
        lastNodes = new DMSNode[sources.Length/2];
        firstNodes = new DMSNode[sources.Length/2];
        activeSource = new bool[sources.Length];
        pauseEndTime = new float[sources.Length / 2];
        layerPitch = new float[sources.Length / 2];
        layerVolume = new float[sources.Length / 2];
        pitchSpeed = new float[sources.Length / 2];
        volumeSpeed = new float[sources.Length / 2];

        for(int i = 0; i < sources.Length-1; i += 2)
        {
            layerPitch[i / 2] = 1;
            layerVolume[i / 2] = 1;
            pitchSpeed[i / 2] = 1;
            volumeSpeed[i / 2] = 1;

            musicController.layers[i/2].LoadClass();
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].loop = true;
            sources[i + 1] = gameObject.AddComponent<AudioSource>();
            sources[i + 1].loop = true;
            activeSource[i] = i % 2 == 0;
            ulong? firstNodeIdNullable = null;
            int transitionId = 0;
            while(true)
            {
                if (CanTransite(musicController.layers[i/2].entryNode.transitions[transitionId]))
                {
                    firstNodeIdNullable = musicController.layers[i/2].entryNode.transitions[transitionId].targetUUID;
                    break;
                }
                transitionId++;
                if (transitionId >= musicController.layers[i/2].entryNode.transitions.Count)
                    break;
            }

            if(firstNodeIdNullable != null)
            {
                ulong firstNodeId = (ulong)firstNodeIdNullable;

                DMSNode targetNode = null;
                foreach(DMSNode node in musicController.layers[i / 2].nodeList)
                {
                    if(node.UUID == firstNodeId)
                    {
                        targetNode = node;
                        break;
                    }
                }

                if(targetNode != null)
                {
                    switch(targetNode.nodeType)
                    {
                        case DMSNode.NodeType.CLIP:
                            DMSNodeClip nodeClip = (DMSNodeClip)targetNode;
                            if (nodeClip.clip != null)
                            {
                                sources[i].clip = nodeClip.clip;
                            }
                            lastNodes[i / 2] = targetNode;
                            break;
                        case DMSNode.NodeType.PAUSE:
                            pauseEndTime[i / 2] = Time.time + ((DMSNodePause)targetNode).duration;
                            lastNodes[i / 2] = targetNode;
                            break;
                        case DMSNode.NodeType.OPERATION:
                            Operation(i, i+1, (DMSNodeOperation)targetNode, i / 2, sources[i], sources[i+1]);
                            break;
                        case DMSNode.NodeType.PITCH_CHANGE:
                            PitchChange(i, i+1, (DMSNodePitchChange)targetNode, i / 2, sources[i], sources[i+1]);
                            break;
                        case DMSNode.NodeType.VOLUME_CHANGE:
                            VolumeChange(i, i+1, (DMSNodeVolumeChange)targetNode, i / 2, sources[i], sources[i+1]);
                            break;
                    }
                    firstNodes[i / 2] = targetNode; 
                }
            }
            
            sources[i].Play();
        }

        StartCoroutine(CoroutineUpdate());
    }

	public void Stop ()
	{
		foreach (AudioSource audio in sources)
		{
			audio.Stop ();
		}
	}

    private IEnumerator CoroutineUpdate()
    {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < sources.Length; i++)
        {
            if (activeSource[i])
            {
                int otherSource = i % 2 == 0 ? i + 1 : i - 1;

                switch (lastNodes[i / 2].nodeType)
                {
                    case DMSNode.NodeType.CLIP:
                        PlayNextClip(i, otherSource, sources[i], sources[otherSource], lastNodes[i / 2] as DMSNodeClip, i / 2);
                        break;
                    case DMSNode.NodeType.PAUSE:
                        Pause(i, otherSource, (DMSNodePause)lastNodes[i / 2], i / 2, sources[i], sources[otherSource]);
                        break;
                    case DMSNode.NodeType.OPERATION:
                        Operation(i, otherSource, (DMSNodeOperation)lastNodes[i / 2], i / 2, sources[i], sources[otherSource]);
                        break;
                    case DMSNode.NodeType.PITCH_CHANGE:
                        PitchChange(i, otherSource, (DMSNodePitchChange)lastNodes[i / 2], i / 2, sources[i], sources[otherSource]);
                        break;
                    case DMSNode.NodeType.VOLUME_CHANGE:
                        VolumeChange(i, otherSource, (DMSNodeVolumeChange)lastNodes[i / 2], i / 2, sources[i], sources[otherSource]);
                        break;
                }
            }
        }
        StartCoroutine(CoroutineUpdate());
    }

    private void FixedUpdate()
    {
        for(int i = 1; i < sources.Length; i += 2)
        {
            sources[i - 1].pitch = Mathf.MoveTowards(sources[i - 1].pitch, layerPitch[i / 2] * masterPitch, pitchSpeed[i / 2]);
            sources[i].pitch = Mathf.MoveTowards(sources[i].pitch, layerPitch[i / 2] * masterPitch, pitchSpeed[i / 2]);

            sources[i - 1].volume = Mathf.MoveTowards(sources[i - 1].volume, layerVolume[i / 2] * masterVolume, volumeSpeed[i / 2]);
            sources[i].volume = Mathf.MoveTowards(sources[i].volume, layerVolume[i / 2] * masterVolume, volumeSpeed[i / 2]);
        }
    }

    //From here on out, it's all node-execution functions.

    private void PitchChange(int currentId, int otherId, DMSNodePitchChange pitchNode, int layerId, AudioSource current, AudioSource next)
    {
        layerPitch[layerId] = pitchNode.targetPitch;
        pitchSpeed[layerId] = pitchNode.speed;

        foreach (DMSNodeTransition transition in pitchNode.transitions)
        {
            if (CanTransite(transition))
            {
                transition.GetTargetNode(musicController.layers[layerId]);
                DMSNode node = transition._targetNode;

                Play(node, current, next, currentId, otherId);
                break;
            }
        }
    }

    private void VolumeChange(int currentId, int otherId, DMSNodeVolumeChange volumeNode, int layerId, AudioSource current, AudioSource next)
    {
        layerVolume[layerId] = volumeNode.targetVolume;
        volumeSpeed[layerId] = volumeNode.speed;

        foreach (DMSNodeTransition transition in volumeNode.transitions)
        {
            if (CanTransite(transition))
            {
                transition.GetTargetNode(musicController.layers[layerId]);
                DMSNode node = transition._targetNode;

                Play(node, current, next, currentId, otherId);
                break;
            }
        }
    }

    private void Operation(int currentId, int otherId, DMSNodeOperation operationNode, int layerId, AudioSource current, AudioSource next)
    {
        float value = musicController.parameters[operationNode.targetParameter];

        switch(operationNode.operation)
        {
            case DMSNodeOperation.Operation.SET:
                value = operationNode.value;
                break;
            case DMSNodeOperation.Operation.ADD:
                value += operationNode.value;
                break;
            case DMSNodeOperation.Operation.SUBTRACT:
                value -= operationNode.value;
                break;
            case DMSNodeOperation.Operation.MULTIPLY:
                value *= operationNode.value;
                break;
            case DMSNodeOperation.Operation.DIVIDE:
                value /= operationNode.value;
                break;
        }

        musicController.parameters[operationNode.targetParameter] = value;

        foreach (DMSNodeTransition transition in operationNode.transitions)
        {
            if (CanTransite(transition))
            {
                transition.GetTargetNode(musicController.layers[layerId]);
                DMSNode node = transition._targetNode;

                Play(node, current, next, currentId, otherId);
                break;
            }
        }
    }

    private void Pause(int currentId, int otherId, DMSNodePause pauseNode, int layerId, AudioSource current, AudioSource next)
    {
        if(Time.time > pauseEndTime[currentId/2])
        {
            foreach (DMSNodeTransition transition in pauseNode.transitions)
            {
                if (CanTransite(transition))
                {
                    transition.GetTargetNode(musicController.layers[layerId]);
                    DMSNode node = transition._targetNode;

                    Play(node, current, next, currentId, otherId);
                    break;
                }
            }
        }
    }

    private void PlayNextClip(int currentId, int otherId, AudioSource current, AudioSource next, DMSNodeClip lastNode, int layerId)
    {
        bool passedLength = (current.time >= lastNode.length);
        if (current.pitch < 0)
            passedLength = (current.time < lastNode.length);

        if(activeSource[currentId] && passedLength)
        {
            #if UNITY_EDITOR
            if (next.isPlaying)
                Debug.LogWarning("<b>Warning:</b> Already trying to play next clip while the next source is still playing a sound!\nClip <i>\"" + next.clip + "\"</i> will most likely be cut off. Consider adding a longer length to its prior clip nodes.");
            #endif

            bool playing = false;
            foreach(DMSNodeTransition transition in lastNode.transitions)
            {
                if(CanTransite(transition))
                {
                    transition.GetTargetNode(musicController.layers[layerId]);
                    DMSNode node = transition._targetNode;

                    Play(node, current, next, currentId, otherId);
                    playing = true;
                    break;
                }
            }

            if(!playing)
            {
                Play(lastNode,current,next,currentId,otherId);
            }
        }
    }

    private void Play(DMSNode node, AudioSource current, AudioSource next, int currentId, int otherId)
    {
        switch (node.nodeType)
        {
            case DMSNode.NodeType.CLIP:
                activeSource[currentId] = false;
                current.loop = false;
                activeSource[otherId] = true;
                next.loop = true;
                DMSNodeClip nodeClip = (DMSNodeClip)node;
                next.clip = nodeClip.clip;
                next.Play();
                break;
            case DMSNode.NodeType.LOOP:
                node = firstNodes[currentId / 2];
                layerLoopEvent(new DynaMusicLayerEventArgs(currentId / 2, this, musicController));

                foreach(DMSNodeTransition transition in musicController.layers[currentId / 2].entryNode.transitions)
                {
                    transition.GetTargetNode(musicController.layers[currentId / 2]);
                    if (CanTransite(transition))
                    {
                        node = transition._targetNode;
                    }
                }

                Play(node, current, next, currentId, otherId);
                break;
            case DMSNode.NodeType.END:
                current.loop = false;
                next.loop = false;
                layerEndEvent(new DynaMusicLayerEventArgs(currentId / 2, this, this.musicController));
                break;
            case DMSNode.NodeType.PAUSE:
                current.loop = false;
                next.loop = false;
                pauseEndTime[currentId / 2] = ((DMSNodePause)node).duration + Time.time;
                break;
        }
        lastNodes[currentId / 2] = node;
    }

    private bool CanTransite(DMSNodeTransition transition)
    {
        if (transition.useCondition)
        {
            DMSCondition condition = transition.condition;

            float targetValue = condition.value;
            float currentValue = musicController.parameters[condition.variableName];

            switch(condition.conditionType)
            {
                case DMSCondition.ConditionType.EQUAL:
                    return currentValue == targetValue;
                case DMSCondition.ConditionType.EQUAL_GREATER:
                    return currentValue >= targetValue;
                case DMSCondition.ConditionType.EQUAL_LOWER:
                    return currentValue <= targetValue;
                case DMSCondition.ConditionType.GREATER:
                    return currentValue > targetValue;
                case DMSCondition.ConditionType.LOWER:
                    return currentValue < targetValue;
                case DMSCondition.ConditionType.NOT_EQUAL:
                    return currentValue != targetValue;
            }
            return false;
        }
        else
            return true;
    }


    /// <summary>
    /// Safely set a parameter variable.
    /// </summary>
    /// <param name="variableName">Name of the parameter</param>
    /// <param name="value">Parameter's new value</param>
    public void SetVariable(string variableName, float value)
    {
        musicController.SetValue(variableName, value);
    }

    /// <summary>
    /// Safely get a parameter's value.
    /// </summary>
    /// <param name="variableName">The parameter's name.</param>
    /// <returns>The parameter's current value. Returns NaN if parameter doesn't exist.</returns>
    public float GetVariable(string variableName)
    {
        if (musicController.parameters.ContainsKey(variableName))
            return musicController.parameters[variableName];
        else
            return float.NaN;
    }

    /// <summary>
    /// Gets the last node in the specified layer's BPM.
    /// </summary>
    /// <param name="layer">ID of the layer</param>
    /// <returns>The BPM</returns>
    public float GetBPM(int layer = 0)
    {
        return lastNodes[layer].bpm;
    }
}