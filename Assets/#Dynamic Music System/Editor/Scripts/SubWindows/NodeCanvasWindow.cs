using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DynamicMusicSystem
{
    public delegate void ChangeSelectionHandler(DMSNode node);

    class NodeCanvasWindow
    {
        private GUISkin defaultSkin;

        private WindowDMS master;
        private List<NodeWindow> nodeWindows = new List<NodeWindow>();

        private Vector2 offset = Vector2.zero;
        private bool isPanning = false;
        private Vector2 panMouseInit = Vector2.zero;

        private bool isDragging = false;
        private Vector2 dragMouseInit = Vector2.zero;
        public NodeWindow selectedNode;

        private int currentLayer;

        public static float windowScale = 1;

        public static event ChangeSelectionHandler changeSelection;

        public NodeCanvasWindow(WindowDMS master)
        {
            this.master = master;
            ConditionsLayersWindow.ChangeLayer += OnChangeLayer;

            CreateNodeWindows();
            master.assignedMusicControllerEvent += OnChangeMusicController;
        }

        public void OnChangeMusicController(MusicController controller)
        {
            if (controller != null)
                CreateNodeWindows();
            else
                nodeWindows.Clear();
        }

        public void RemoveNonExistantTransitions()
        {
            foreach(NodeWindow nw in nodeWindows)
            {
                DMSNode node = nw.node;
                List<DMSNodeTransition> deleteNodes = new List<DMSNodeTransition>();
                for(int i = 0; i < node.transitions.Count; i++)
                {
                    if (node.transitions[i]._targetNode == null)
                        deleteNodes.Add(node.transitions[i]);
                    else
                    {
                        ulong targetUUID = node.transitions[i].targetUUID;
                        bool isFine = false;
                        foreach(NodeWindow targetNw in nodeWindows)
                        {
                            if (targetNw.node.UUID == targetUUID)
                            {
                                isFine = true;
                                break;
                            }
                        }
                        if (!isFine)
                            deleteNodes.Add(node.transitions[i]);
                    }
                }

                foreach (DMSNodeTransition di in deleteNodes)
                    node.transitions.Remove(di);
            }
        }

        public void CreateNodeWindows()
        {
            nodeWindows.Clear();
            if (master.target != null && master.target.layers != null)
            {
                DynaMusicLayer layer = master.target.layers[currentLayer];
                if (layer.nodeList != null)
                {
                    foreach (DMSNode node in layer.nodeList)
                    {
                        nodeWindows.Add(new NodeWindow(node));
                    }
                }
            }
        }

        public void Draw()
        {
            //GUILayout.BeginVertical();
            {
                //Toolbar();
                DrawGrid();
                //DrawNodes();
            }
            //GUILayout.EndVertical();
        }

        public void OnChangeLayer(int layerIndex)
        {
            offset = Vector2.zero;
            currentLayer = layerIndex;

            selectedNode = null;

            CreateNodeWindows();
        }

        private bool rightClickMenuActive = false;
        private Rect rightClickMenuPosition = new Rect(0, 0, 150, 16);

        private void RightClickMenu(Vector2 position)
        {
            rightClickMenuActive = true;
            rightClickMenuPosition.position = position;
        }

        public void DrawRightClickMenu()
        {
            if (rightClickMenuActive)
            {
                int chosen = EditorGUI.Popup(rightClickMenuPosition, 0, new string[] { string.Empty, "New Node", "New Pause", "New Transition (T)", "New Comment", "New Operation", "New Pitch Operation", "New Volume Operation" });
                switch (chosen)
                {
                    case 1:
                        {
                            DMSNodeClip node = new DMSNodeClip() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                    case 2:
                        {
                            DMSNodePause node = new DMSNodePause() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                    case 3:
                        master.SetTransitionMode();
                        break;
                    case 4:
                        {
                            DMSNodeComment node = new DMSNodeComment() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                    case 5:
                        {
                            DMSNodeOperation node = new DMSNodeOperation() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            if(master.target.parameters.Count > 0)
                                node.targetParameter = new List<string>(master.target.parameters.Keys)[0];
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                    case 6:
                        {
                            DMSNodePitchChange node = new DMSNodePitchChange() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                    case 7:
                        {
                            DMSNodeVolumeChange node = new DMSNodeVolumeChange() { UUID = master.target.layers[currentLayer].GetUUID() };
                            node.position.position = rightClickMenuPosition.position - trueOffset;
                            master.target.layers[currentLayer].nodeList.Add(node);
                            CreateNodeWindows();
                        }
                        break;
                }

                if (chosen != 0)
                    rightClickMenuActive = false;
            }
        }

        public void Toolbar()
        {
            GUILayout.BeginHorizontal(EditorStyles.toolbar);
            {
                GUILayout.Label("Layer " + currentLayer + " (" + master.target.layers[currentLayer].name + ")",new GUIStyle(EditorStyles.toolbar) { alignment = TextAnchor.MiddleCenter });
                GUILayout.Space(master.position.width);
            }
            GUILayout.EndHorizontal();
        }

        public void DrawGrid()
        {
            Rect controlRect = new Rect(0, 100, 0, 0);
            Texture2D tex = DMSElements.gridTexture;
            Vector2 gridSize = master.position.size;
            GUI.DrawTextureWithTexCoords(new Rect(controlRect.position, gridSize), tex, new Rect(offset, new Vector2(gridSize.x / tex.width, gridSize.y / tex.height)));
        }

        public void HideGridTopMostElements(float xPos)
        {
            Rect area = new Rect(Vector2.zero, new Vector2(master.position.size.x, 100));
            GUI.Box(area,GUIContent.none);

            float totalLength = 0;
            float totalClipLength = 0;
            float totalBeats = 0;
            foreach(NodeWindow nw in nodeWindows)
            {
                if(nw.node.nodeType == DMSNode.NodeType.CLIP)
                {
                    DMSNodeClip clipNode = nw.node as DMSNodeClip;
                    if (clipNode.clip != null)
                    {
                        totalClipLength += clipNode.clip.length;
                        totalLength += clipNode.length;
                        totalBeats += ((float)clipNode.bpm / 60) * clipNode.length;
                    }
                }
                else if(nw.node.nodeType == DMSNode.NodeType.PAUSE)
                {
                    DMSNodePause pauseNode = nw.node as DMSNodePause;
                    totalLength += pauseNode.duration;
                    totalBeats += ((float)pauseNode.bpm / 60) * pauseNode.duration;
                }
            }

            area.x = xPos + 48;
            area.y += EditorGUIUtility.singleLineHeight * 1.2f;

            Rect sliderRect = new Rect(area);
            sliderRect.width /= 3;
            sliderRect.height = EditorGUIUtility.singleLineHeight;
            windowScale = EditorGUI.Slider(sliderRect, "Window Scale", windowScale, .5f, 1.5f);

            area.y += EditorGUIUtility.singleLineHeight;
            GUI.Label(area,"Total Clip Length: " + totalClipLength + " sec.");

            GUI.Label(new Rect(area.width-100 - area.y/2, area.y-8,64,16),"Icon");
            master.target.icon = (Texture2D)EditorGUI.ObjectField(new Rect(area.width - 64 - area.y/2, area.y-8, 64, 64), master.target.icon, typeof(Texture2D), false);

            area.y += EditorGUIUtility.singleLineHeight;
            GUI.Label(area, "Total Length: " + totalLength + " sec.");
            area.y += EditorGUIUtility.singleLineHeight;
            GUI.Label(area, "Transition Length: " + (totalClipLength - totalLength) + " sec.");
            area.y += EditorGUIUtility.singleLineHeight;
            GUI.Label(area, "Total Beats: " + totalBeats + " beats");
        }

        public void HandleEvents()
        {
            Event e = Event.current;

            switch(e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 2)
                    {
                        isPanning = true;
                        panMouseInit = e.mousePosition;
                    }
                    if (e.button == 0 || e.button == 1)
                    {
                        bool gotSelection = false;
                        for(int i = nodeWindows.Count-1; i >= 0; i--)
                        {
                            NodeWindow nw = nodeWindows[i];

                            Rect selectionRect = nw.GetRealPosition(trueOffset);

                            Vector2 s = selectionRect.size * (NodeCanvasWindow.windowScale);
                            selectionRect.position -= (s - selectionRect.size) / 2;
                            selectionRect.size = s;

                            if (!gotSelection && selectionRect.Contains(e.mousePosition))
                            {
                                nw.selected = true;
                                gotSelection = true;
                                isDragging = true;
                                selectedNode = nw;
                                dragMouseInit = e.mousePosition;
                            }
                            else
                                nw.selected = false;
                        }
                        if (selectedNode != null)
                            changeSelection(selectedNode.node);
                        else
                            changeSelection(null);
                    }
                    if (e.button == 1)
                    {
                        RightClickMenu(e.mousePosition);
                    }
                    else
                        rightClickMenuActive = false;
                    e.Use();
                    break;
                case EventType.MouseDrag:
                    if (e.button == 2 && isPanning && !isDragging)
                    {
                        offset.x += (panMouseInit.x - e.mousePosition.x) / DMSElements.gridTexture.width;
                        offset.y -= (panMouseInit.y - e.mousePosition.y) / DMSElements.gridTexture.height;

                        panMouseInit = e.mousePosition;
                        master.Repaint();
                    }
                    if(e.button == 0 && selectedNode != null && isDragging)
                    {
                        selectedNode.node.position.x -= dragMouseInit.x - e.mousePosition.x;
                        selectedNode.node.position.y -= dragMouseInit.y - e.mousePosition.y;

                        dragMouseInit = e.mousePosition;
                        master.changes = true;
                        master.Repaint();
                    }
                    e.Use();
                    break;
                case EventType.MouseUp:
                    if(e.button == 2)
                        isPanning = false;
                    if (e.button == 0)
                        isDragging = false;
                    goto case EventType.MouseDrag;
                case EventType.KeyDown:
                    if (e.keyCode == KeyCode.Delete && selectedNode != null)
                    {
                        if (selectedNode.node.deletable)
                        {
                            master.target.layers[currentLayer].nodeList.Remove(selectedNode.node);
                            CreateNodeWindows();
                            RemoveNonExistantTransitions();
                            master.Repaint();
                            master.UnsetSelection();
                        }
                    }
                    if(e.keyCode == KeyCode.T && selectedNode != null)
                    {
                        master.SetTransitionMode();
                    }
                    break;
                case EventType.Repaint:
                    mousePosition = e.mousePosition;
                    e.Use();
                    break;
            }
        }

        public Vector2 mousePosition = Vector2.zero;

        /*public void DrawNodes()
        {
            master.BeginWindows();
            for(int i = 0; i < nodeWindows.Count; i++)
            {
                NodeWindow nodeWindow = nodeWindows[i];
                GUIStyle style = new GUIStyle(GUI.skin.GetStyle("Window"));

                //texCol.SetPixel(0, 0, nodeWindow.node.color);
                //texCol.Apply();
                //style.normal.background = texCol;
                nodeWindow.position = GUILayout.Window(i, nodeWindow.position, nodeWindow.Draw, nodeWindow.node.name);
            }
            master.EndWindows();
        }*/

        private Vector2 trueOffset = Vector2.zero;

        public void DrawNodes()
        {
            trueOffset.x = offset.x * -DMSElements.gridTexture.width;
            trueOffset.y = offset.y * DMSElements.gridTexture.height;

            foreach(NodeWindow nw in nodeWindows)
            {
                if (nw.GetRealPosition(trueOffset).y > 14)
                    nw.Draw(trueOffset);
            }

            DrawTransitionArrows();
        }

        public void DrawTransitionArrows()
        {
            foreach(NodeWindow nw in nodeWindows)
            {
                DMSNode node = nw.node;
                if (node.transitions != null)
                {
                    foreach (DMSNodeTransition transition in node.transitions)
                    {
                        foreach (NodeWindow targetNw in nodeWindows)
                        {
                            if (targetNw.node == transition._targetNode)
                            {
                                DrawBezier(nw, targetNw,transition.useCondition,transition.condition.conditionType,transition.condition.value,transition.condition.variableName);
                            }
                        }
                    }
                }
                if(node.nodeType == DMSNode.NodeType.LOOP)
                {
                    NodeWindow startWindow = null;
                    foreach(NodeWindow targetNw in nodeWindows)
                    {
                        if(targetNw.node.nodeType == DMSNode.NodeType.ENTRY)
                        {
                            startWindow = targetNw;
                            break;
                        }
                    }

                    if(startWindow != null)
                        DrawBezier(nw, startWindow,false, DMSCondition.ConditionType.EQUAL,0,"var",true);
                }
            }
        }

        private void DrawBezier(NodeWindow nw, NodeWindow targetNw, bool conditional, DMSCondition.ConditionType conditionType, float conditionVal, string conditionName, bool loopSpecial = false)
        {
            Vector2 si = nw.GetRealPosition(offset).size;
            Vector2 nSi = si * NodeCanvasWindow.windowScale;

            Vector2 start = nw.GetRealPosition(offset).position;
            start.y += nw.GetRealPosition(offset).height / 2;
            start.x += nw.GetRealPosition(offset).width + (nSi.x - si.x) / 2;

            Vector2 end = targetNw.GetRealPosition(offset).position;
            end.x -= (nSi.x - si.x)/2;
            end.y += targetNw.GetRealPosition(offset).height / 2;

            start.x -= offset.x * DMSElements.gridTexture.width * 1.007f;
            end.x -= offset.x * DMSElements.gridTexture.width * 1.007f + 8;
            start.y += offset.y * DMSElements.gridTexture.height;
            end.y += offset.y * DMSElements.gridTexture.height;

            Vector2 mid1 = new Vector2(Mathf.Lerp(start.x, end.x, .4f), start.y);
            mid1.x = Mathf.Max(mid1.x, start.x + 64);
            Vector2 mid2 = new Vector2(Mathf.Lerp(start.x, end.x, .6f), end.y);
            mid2.x = Mathf.Min(mid2.x, end.x - 64);

            if(loopSpecial)
            {
                mid1 = new Vector2(start.x + 512,start.y + 256);
                mid2 = new Vector2(end.x - 512, end.y + 256);
            }

            Color col = loopSpecial ? Color.green : targetNw.node.color;
            if (targetNw.node.nodeType == DMSNode.NodeType.CLIP && ((DMSNodeClip)targetNw.node).clip == null)
                col = new Color(.5f, .5f, .6f);

            Handles.DrawBezier(start, end, mid1, mid2, col, null, nw.selected ? 8 : 4);
            Rect r = new Rect(end.x - 8, end.y - 8, 16, 16);
            if(r.y > 14)
                Graphics.DrawTexture(r, DMSElements.arrowHead);

            if(conditional)
            {
                Vector2 center = Vector2.Lerp(mid1, mid2, .5f);
                Rect cR = new Rect(new Vector2(center.x-64,center.y-16), new Vector2(128,32));
                if(cR.y > 14)
                    Graphics.DrawTexture(cR,DMSElements.conditionIcon);

                string[] symbol = new string[]
                {
                    "==", ">", "<", "!=", ">=", "<="
                };

                string name = conditionName;
                if (name.Length > 12)
                    name = name.Substring(0, 12);

                GUI.Label(cR,name + " " + symbol[(int)conditionType] + " " + conditionVal,new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter });
            }
        }

        public void DrawTransitionModeArrow(DMSNode origin)
        {
            NodeWindow nw = null;
            foreach(NodeWindow nodeWindow in nodeWindows)
            {
                if(nodeWindow.node == origin)
                {
                    nw = nodeWindow;
                    break;
                }
            }

            if (nw != null)
            {
                Vector2 start = nw.GetRealPosition(offset).position;
                start.y += nw.GetRealPosition(offset).height / 2;
                start.x += nw.GetRealPosition(offset).width;
                Vector2 end = mousePosition;

                start.x -= offset.x * DMSElements.gridTexture.width;
                //end.x -= offset.x * DMSElements.gridTexture.width + 8;
                start.y += offset.y * DMSElements.gridTexture.height;
                //end.y += offset.y * DMSElements.gridTexture.height;

                Vector2 mid1 = new Vector2(Mathf.Lerp(start.x, end.x, .4f), start.y);
                mid1.x = Mathf.Max(mid1.x, start.x + 64);
                Vector2 mid2 = new Vector2(Mathf.Lerp(start.x, end.x, .6f), end.y);
                mid2.x = Mathf.Min(mid2.x, end.x - 64);

                Handles.DrawBezier(start, end, mid1, mid2, Color.cyan, null, 8);
            }
        }

    }
}