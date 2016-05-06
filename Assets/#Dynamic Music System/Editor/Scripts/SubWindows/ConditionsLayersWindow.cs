using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace DynamicMusicSystem
{
    public delegate void ChangeLayerHandler(int newLayer);
    public delegate void ChangeParameterList(MusicController controller);

    class ConditionsLayersWindow
    {
        public static event ChangeLayerHandler ChangeLayer;
        public static event ChangeParameterList ChangeParameterList;

        private bool initialized = false;
        private SubWindow subWindow = SubWindow.INSPECTOR;
        private WindowDMS master;
        private Vector2 scrollPosition = Vector2.zero;

        private SerializedObject so;
        private List<DynaMusicLayer> layerList = new List<DynaMusicLayer>();
        private int selectedLayer = 0;

        public ConditionsLayersWindow(WindowDMS master)
        {
            this.master = master;
            NodeCanvasWindow.changeSelection += OnChangeSelection;
        }

        public void Initialize()
        {
            if (!initialized)
            {
                layerList = master.target.layers;

                foreach(DynaMusicLayer layer in layerList)
                {
                    foreach(DMSNode node in layer.nodeList)
                    {
                        foreach(DMSNodeTransition transition in node.transitions)
                        {
                            ulong targetUUID = transition.targetUUID;
                            foreach(DMSNode targetNode in layer.nodeList)
                            {
                                if(targetNode.UUID == targetUUID)
                                {
                                    transition._targetNode = targetNode;
                                    transition.targetUUID = targetNode.UUID;
                                    break;
                                }
                            }
                        }
                    }
                }

                initialized = true;
            }
        }

        public void Draw(bool showContent)
        {
            GUI.Box(new Rect(0, 16, showContent ? 300 : 0, master.position.height),string.Empty);
            GUILayout.BeginVertical();
            {
                ConditionsAndLayersToolbar(showContent);
                if (showContent)
                {
                    scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
                    {
                        switch(subWindow)
                        {
                            case SubWindow.LAYERS:
                                DrawLayerList();
                                break;
                            case SubWindow.CONDITIONS:
                                DrawConditionsList();
                                break;
                            case SubWindow.INSPECTOR:
                                DrawInspector();
                                break;
                        }
                    }
                    EditorGUILayout.EndScrollView();
                }
            }
            GUILayout.EndVertical();
        }

        private void ConditionsAndLayersToolbar(bool showContent)
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                if (showContent)
                {
                    subWindow = GUILayout.Toggle(subWindow == SubWindow.LAYERS, "Layers", EditorStyles.toolbarButton) ? SubWindow.LAYERS : subWindow;
                    GUILayout.Space(4);
                    subWindow = GUILayout.Toggle(subWindow == SubWindow.CONDITIONS, "Parameters", EditorStyles.toolbarButton) ? SubWindow.CONDITIONS : subWindow;
                    GUILayout.Space(4);
                    subWindow = GUILayout.Toggle(subWindow == SubWindow.INSPECTOR, "Inspector", EditorStyles.toolbarButton) ? SubWindow.INSPECTOR : subWindow;
                    GUILayout.Space(4);
                }
                Texture2D showHideIcon = showContent ? DMSElements.visibleIcon : DMSElements.hiddenIcon;
                master.showSideWindow = GUILayout.Toggle(showContent, showHideIcon, EditorStyles.toolbarButton, GUILayout.Width(32));
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndHorizontal();
        }

        private enum SubWindow
        {
            CONDITIONS,
            LAYERS,
            INSPECTOR,
        }

        #region Layers

        private void DrawLayerList() //Draws and handles the list of layers. This here crashes upon undo.
        {
            EditorGUILayout.LabelField("Layers",EditorStyles.boldLabel);

            for(int i = 0; i < layerList.Count; i++)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical("Box");
                {
                    DynaMusicLayer layer = layerList[i];

                    EditorGUILayout.BeginHorizontal("Box");
                    {
                        GUIStyle style = new GUIStyle(EditorStyles.label);
                        style.fontStyle = selectedLayer == i ? FontStyle.BoldAndItalic : FontStyle.Italic;
                        EditorGUILayout.LabelField("Layer " + i + " (" + layer.name + ")", style);
                        if (GUILayout.Button("Select"))
                        {
                            selectedLayer = i;
                            selectedNode = null;
                            ChangeLayer(i);
                            master.Repaint();
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("Name:");
                    layer.name = EditorGUILayout.TextField(layer.name);

                    if (i != 0)
                    {
                        Rect removeButton = EditorGUILayout.BeginHorizontal("Button");
                        {
                            if (GUI.Button(removeButton, GUIContent.none))
                            {
                                ChangeLayer(selectedLayer);
                                layerList.RemoveAt(i);
                            }
                            GUILayout.Label(DMSElements.deleteIcon);
                            GUILayout.Label("Delete");
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                GUILayout.Label(DMSElements.breakLine);
                EditorGUILayout.Space();
            }
            Rect newLayerButton = EditorGUILayout.BeginHorizontal("Button");
            {
                if (GUI.Button(newLayerButton, GUIContent.none))
                {
                    layerList.Add(new DynaMusicLayer(master.target.currentLayerUUID));
                    master.target.currentLayerUUID += 2000;
                }
                GUILayout.Label(DMSElements.newLayerIcon);
                GUILayout.Label("New");
            }
            EditorGUILayout.EndHorizontal();
        }

        private void SelectLayer(ReorderableList list)
        {
            if (list.index != selectedLayer)
            {
                ChangeLayer(list.index);
                selectedLayer = list.index;
            }
        }

        private void MayRemoveLayersRemove(ReorderableList list)
        {
            list.displayRemove = list.count > 2;
            ReorderableList.defaultBehaviours.DoRemoveButton(list);
            if(selectedLayer > list.count-2)
            {
                selectedLayer = 0;
                ChangeLayer(selectedLayer);
            }
        }

        private void MayRemoveLayersAdd(ReorderableList list)
        {
            list.displayRemove = true;
            ReorderableList.defaultBehaviours.DoAddButton(list);
        }

        #endregion

        #region Conditions

        private void DrawConditionsList()
        {
            Dictionary<string, float> valueDic= new Dictionary<string, float>(master.target.parameters);
            List<string> keys = new List<string>(valueDic.Keys);
            List<float> values = new List<float>(valueDic.Values);
            Dictionary<string, float> newDic = new Dictionary<string, float>();

            bool change = false;

            for(int i = 0; i < keys.Count; i++)
            {
                if (GUILayout.Button("Insert"))
                {
                    newDic.Add("New Parameter " + Random.value * int.MaxValue, 0);
                    change = true;
                }

                EditorGUILayout.BeginVertical("Box");
                {
                    string oldName = keys[i];
                    keys[i] = EditorGUILayout.DelayedTextField("Variable Name", keys[i]);
                    if (valueDic.ContainsKey(keys[i]))
                        keys[i] = oldName;

                    values[i] = EditorGUILayout.DelayedFloatField("Variable Value", values[i]);
                    newDic.Add(keys[i], values[i]);

                    if (GUILayout.Button("Remove"))
                    {
                        newDic.Remove(keys[i]);
                        change = true;
                    }
                }
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add"))
            {
                newDic.Add("New Parameter " + Random.value * int.MaxValue, 0);
                change = true;
            }

            master.target.parameters = new Dictionary<string, float>(newDic);
            if(change)
                ChangeParameterList(master.target);
        }

        #endregion

        #region Inspector

        public DMSNode selectedNode;

        private void OnChangeSelection(DMSNode node)
        {
            if (transitionMode)
                SetTransition(node);
            else
                selectedNode = node;
        }

        public bool transitionMode = false;

        private void DrawInspector()
        {
            if (selectedNode == null)
                EditorGUILayout.LabelField("No node selected.");
            else
            {
                EditorGUILayout.LabelField(selectedNode.name,EditorStyles.boldLabel);
                string typeName = "Node";
                switch(selectedNode.nodeType)
                {
                    case DMSNode.NodeType.CLIP:
                        typeName = "Clip Node";
                        break;
                    case DMSNode.NodeType.ENTRY:
                        typeName = "Entry Node";
                        break;
                    case DMSNode.NodeType.LOOP:
                        typeName = "Loop Node";
                        break;
                    case DMSNode.NodeType.END:
                        typeName = "End Node";
                        break;
                    case DMSNode.NodeType.BLEND:
                        typeName = "Blend Node";
                        break;
                    case DMSNode.NodeType.PAUSE:
                        typeName = "Pause Node";
                        break;
                    case DMSNode.NodeType.COMMENT:
                        typeName = "Comment";
                        break;
                    case DMSNode.NodeType.OPERATION:
                        typeName = "Operation Node";
                        break;
                }
                EditorGUILayout.LabelField(typeName,new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Italic });

                switch(selectedNode.nodeType)
                {
                    case DMSNode.NodeType.CLIP:
                        InspectorGUI_NodeClip();
                        break;
                    case DMSNode.NodeType.COMMENT:
                        selectedNode.name = EditorGUILayout.TextArea(selectedNode.name);
                        break;
                    case DMSNode.NodeType.PAUSE:
                        DMSNodePause pauseNode = (DMSNodePause)selectedNode;
                        pauseNode.duration = Mathf.Max(EditorGUILayout.FloatField("Length (seconds)", pauseNode.duration), 0);
                        float tailBeats = ((float)pauseNode.bpm / 60) * pauseNode.duration;
                        tailBeats = Mathf.Max(0, EditorGUILayout.FloatField("Length (beats)", tailBeats));
                        pauseNode.duration = tailBeats / ((float)pauseNode.bpm / 60);
                        pauseNode.name = "Pause (" + pauseNode.duration + " sec.)";
                        float w = Mathf.Max((tailBeats / 4f) * 16,8);
                        if (pauseNode.duration == 0)
                            w = 100;
                        pauseNode.position.width = w;
                        break;
                    case DMSNode.NodeType.OPERATION:
                        DMSNodeOperation operationNode = (DMSNodeOperation)selectedNode;
                        InspectorGUI_OperationNode(operationNode);
                        break;
                    case DMSNode.NodeType.PITCH_CHANGE:
                        DMSNodePitchChange pitchNode = (DMSNodePitchChange)selectedNode;
                        pitchNode.targetPitch = EditorGUILayout.FloatField("Target Pitch", pitchNode.targetPitch);
                        pitchNode.speed = EditorGUILayout.FloatField("Change Speed", pitchNode.speed);
                        break;
                    case DMSNode.NodeType.VOLUME_CHANGE:
                        DMSNodeVolumeChange volumeNode = (DMSNodeVolumeChange)selectedNode;
                        volumeNode.targetVolume = EditorGUILayout.FloatField("Target Volume", volumeNode.targetVolume);
                        volumeNode.speed = EditorGUILayout.FloatField("Change Speed", volumeNode.speed);
                        break;
                }

                InspectorGUI_Clip();
            }
        }

        private void InspectorGUI_Clip()
        {
            if (selectedNode.GetType() == typeof(DMSNodeLoop))
            {
                EditorGUILayout.LabelField("Entry", new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Italic });
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField(selectedNode.name + " --> " + "Entry");
                EditorGUI.indentLevel--;
            }
            else
            {
                System.Type type = selectedNode.GetType();

                if(type == typeof(DMSNodeClip) || type == typeof(DMSNodePause))
                    selectedNode.bpm = Mathf.Max(EditorGUILayout.IntField("BPM", selectedNode.bpm),1);
                //selectedNode.pitch = EditorGUILayout.Slider("Pitch", selectedNode.pitch, .01f, 3);
                //selectedNode.volume = EditorGUILayout.Slider("Volume", selectedNode.volume, 0, 1);
            }
            
            //List<DMSNodeTransition> removeList = new List<DMSNodeTransition>();
            //foreach (DMSNodeTransition transition in selectedNode.transitions)
            for(int i = 0; i < selectedNode.transitions.Count; i++)
            {
                DMSNodeTransition transition = selectedNode.transitions[i];
                EditorGUILayout.Space();
                EditorGUILayout.LabelField(transition._targetNode.name, new GUIStyle(EditorStyles.label) { fontStyle = FontStyle.Italic });
                EditorGUI.indentLevel++;
                //InspectorGUI_Transition(transition, ref removeList);
                bool markForDelete = false;
                InspectorGUI_Transition(transition, ref markForDelete);
                if (markForDelete)
                    selectedNode.transitions.Remove(transition);
                EditorGUI.indentLevel--;
            }

            //foreach (DMSNodeTransition removeTransition in removeList)
                //selectedNode.transitions.Remove(removeTransition);

            if (selectedNode.GetType() != typeof(DMSNodeLoop) && selectedNode.GetType() != typeof(DMSNodeEnd))
            {
                if (GUILayout.Button(transitionMode ? "Stop Transition Selection" : "Add Transition"))
                    transitionMode = !transitionMode;
            }

            if (selectedNode.deletable)
            {
                if (GUILayout.Button("Delete"))
                {
                    master.target.layers[selectedLayer].nodeList.Remove(selectedNode);
                    master.RemoveUnnecessary();
                    master.UnsetSelection();
                    master.Repaint();
                }
            }
        }

        private void InspectorGUI_OperationNode(DMSNodeOperation operationNode)
        {
            int currentVar = 0;
            List<string> keys = new List<string>(master.target.parameters.Keys);
            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] == operationNode.targetParameter)
                {
                    currentVar = i;
                    break;
                }
            }

            operationNode.targetParameter = keys[EditorGUILayout.Popup("Target Parameter",currentVar,keys.ToArray())];
            string[] operations = new string[]
            {
                "=", "+=", "-=", "*=", "/=",
            };
            operationNode.operation = (DMSNodeOperation.Operation)EditorGUILayout.Popup("Operation",(int)operationNode.operation,operations);
            operationNode.value = EditorGUILayout.FloatField("Value",operationNode.value);
        }

        private void InspectorGUI_Transition(DMSNodeTransition transition, /*ref List<DMSNodeTransition> removeList*/ ref bool markForDelete)
        {
            EditorGUILayout.BeginVertical("Box");
            {
                EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.LabelField(selectedNode.name, GUILayout.Width(110));
                EditorGUILayout.LabelField("-->", GUILayout.Width(40));
                EditorGUILayout.LabelField(transition._targetNode.name, GUILayout.Width(110));
                EditorGUILayout.EndHorizontal();

                EditorGUI.BeginDisabledGroup(master.target.parameters.Count == 0);
                {
                    transition.useCondition = EditorGUILayout.Toggle("Conditional",transition.useCondition);

                    if (master.target.parameters.Count > 0)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField("if", GUILayout.Width(32));

                            int currentVar = 0;
                            List<string> keys = new List<string>(master.target.parameters.Keys);
                            for (int i = 0; i < keys.Count; i++)
                            {
                                if (keys[i] == transition.condition.variableName)
                                {
                                    currentVar = i;
                                    break;
                                }
                            }

                            transition.condition.variableName = keys[EditorGUILayout.Popup(currentVar, keys.ToArray())];
                            //EditorGUILayout.LabelField("value", GUILayout.Width(64));
                            transition.condition.conditionType = (DMSCondition.ConditionType)EditorGUILayout.Popup((int)transition.condition.conditionType, new string[]
                                    {
                                    "==",
                                    ">",
                                    "<",
                                    "!=",
                                    ">=",
                                    "<=",
                                    }
                                );
                            transition.condition.value = EditorGUILayout.FloatField(transition.condition.value);
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        EditorGUILayout.LabelField("No parameters specified yet.");
                    }
                }
                EditorGUI.EndDisabledGroup();

                if (GUILayout.Button("Remove"))
                    markForDelete = true;//removeList.Add(transition);
            }
            EditorGUILayout.EndVertical();
        }

        private void InspectorGUI_NodeClip()
        {
            DMSNodeClip clipNode = (DMSNodeClip)selectedNode;
            if (clipNode.clip != null)
            {
                Rect r = EditorGUILayout.GetControlRect(false, 120);
                r.width = 292;
                GUI.Box(r,GUIContent.none);
                r.x++;
                r.y++;
                r.width -= 2;
                r.height -= 2;
                GUI.DrawTexture(r, AssetPreview.GetAssetPreview(clipNode.clip));

                float w = r.width * (clipNode.length / clipNode.clip.length);
                r.width = w;

                Color originalColor = GUI.color;
                GUI.color = new Color(0, 0, 1, .2f);
                GUI.Box(r, GUIContent.none);
                GUI.color = originalColor;

                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Play"))
                    {
                        AudioPreview.StopAllClips();
                        AudioPreview.PlayClip(clipNode.clip);
                    }
                    if (GUILayout.Button("Stop"))
                        AudioPreview.StopAllClips();
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.BeginDisabledGroup((float)clipNode.bpm == 0);
            clipNode.length = Mathf.Clamp(EditorGUILayout.FloatField("Length (seconds)",clipNode.length),0,clipNode.clip != null ? clipNode.clip.length : 0);
            float tailBeats = ((float)clipNode.bpm / 60) * clipNode.length;
            tailBeats = Mathf.Max(0, EditorGUILayout.FloatField("Length (beats)", tailBeats));
            clipNode.length = tailBeats / ((float)clipNode.bpm / 60);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            clipNode.name = EditorGUILayout.DelayedTextField("Name", selectedNode.name);
            clipNode.clip = (AudioClip)EditorGUILayout.ObjectField("Clip", clipNode.clip, typeof(AudioClip), false);

            if (clipNode.clip != null)
            {
                float w = (tailBeats / 4f) * 16;
                clipNode.position.width = Mathf.Max(8,w);
                if (clipNode.length == 0)
                    clipNode.position.width = 100;
            }
        }

        private void SetTransition(DMSNode targetNode)
        {
            if (selectedNode.nodeType != DMSNode.NodeType.LOOP && selectedNode.nodeType != DMSNode.NodeType.END && targetNode != selectedNode && !ContainsNode(targetNode) && targetNode.nodeType != DMSNode.NodeType.ENTRY && targetNode.nodeType != DMSNode.NodeType.COMMENT && selectedNode.nodeType != DMSNode.NodeType.COMMENT)
                selectedNode.transitions.Add(new DMSNodeTransition() { _targetNode = targetNode, targetUUID = targetNode.UUID });
            transitionMode = false;
            master.changes = true;
        }

        private bool ContainsNode(DMSNode check)
        {
            foreach(DMSNodeTransition transition in selectedNode.transitions)
            {
                if (transition._targetNode == check)
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}