using UnityEngine;
using UnityEditor;
using System.Collections;

namespace DynamicMusicSystem
{
    public class NodeWindow
    {
        public DMSNode node;
        public Material material;
        public bool selected;

        public NodeWindow(DMSNode node)
        {
            this.node = node;
            SetMaterial();
            ConditionsLayersWindow.ChangeParameterList += UpdateTransitions;
        }

        private void SetMaterial()
        {
            if (DMSElements.nodeMaterial == null)
                DMSElements.Initialize();
            material = new Material(DMSElements.nodeMaterial);
            Color targetColor = node.color / 2;
            targetColor.a *= 2;
            material.SetColor("_TintColor", targetColor);
        }

        /*public void Draw(int id)
        {
            GUILayout.Label("Hello");
            GUI.DragWindow();
        }*/

        private void UpdateTransitions(MusicController musicController)
        {
            foreach(DMSNodeTransition transition in node.transitions)
            {
                if(transition.useCondition)
                {
                    if (!musicController.parameters.ContainsKey(transition.condition.variableName))
                    {
                        transition.useCondition = false;
                    }
                }
            }
        }

        public Rect GetRealPosition(Vector2 offset)
        {
            Rect offPos = new Rect(node.position);
            offPos.position += offset;
            return offPos;
        }

        public void Draw(Vector2 offset)
        {
            Rect offPos = GetRealPosition(offset);
            Vector2 s = offPos.size * (NodeCanvasWindow.windowScale);

            offPos.position -= (s - offPos.size)/2;

            offPos.size = s;

            Rect drawRect = GUILayoutUtility.GetRect(0,0);
            drawRect.x = offPos.x;
            drawRect.y = offPos.y;
            drawRect.width = offPos.width;
            drawRect.height = offPos.height;

            //GUI.Box(offPos, "Hello");
            if (material == null)
                SetMaterial();
            EditorGUI.DrawPreviewTexture(drawRect, DMSElements.nodeGraphic, material);

            string name = node.name;
            Texture2D icon = null;

            switch (node.nodeType)
            {
                case DMSNode.NodeType.ENTRY:
                    icon = DMSElements.playIcon;
                    break;
                case DMSNode.NodeType.END:
                    icon = DMSElements.stopIcon;
                    break;
                case DMSNode.NodeType.LOOP:
                    icon = DMSElements.loopIcon;
                    break;
                case DMSNode.NodeType.PAUSE:
                    icon = DMSElements.pauseIcon;
                    break;
                case DMSNode.NodeType.COMMENT:
                    icon = DMSElements.commentIcon;
                    break;
                case DMSNode.NodeType.CLIP:
                    {
                        icon = DMSElements.clipIcon;
                        DMSNodeClip clipNode = (DMSNodeClip)node;
                        if (clipNode.clip != null)
                        {
                            Texture2D previewTex = AssetPreview.GetAssetPreview(clipNode.clip);
                            Rect r = new Rect(offPos);
                            r.x += 4;
                            r.y += 4;
                            r.width -= 8;
                            r.height -= 8;

                            if (previewTex != null)
                            {
                                Material m = new Material(DMSElements.nodeMaterial);
                                m.SetColor("_TintColor", new Color(1,.5f,0));
                                EditorGUI.DrawPreviewTexture(r, previewTex, m);
                            }

                            if(selected)
                                name += "\n(" + clipNode.clip.length + " sec.)";
                        }
                    }
                    break;
                case DMSNode.NodeType.OPERATION:
                    {
                        icon = DMSElements.operationIcon;
                        DMSNodeOperation operationNode = (DMSNodeOperation)node;
                        string[] character = new string[]
                        {
                            "=", "+=", "-=", "*=", "/=",
                        };

                        name = operationNode.targetParameter + " " + character[(int)operationNode.operation] + " " + operationNode.value;
                    }
                    break;
                case DMSNode.NodeType.VOLUME_CHANGE:
                    icon = DMSElements.volumeIcon;
                    break;
                case DMSNode.NodeType.PITCH_CHANGE:
                    icon = DMSElements.pitchIcon;
                    break;
            }

            if (icon != null)
            {
                Rect iconPos = new Rect(offPos.center - new Vector2(16, 16), new Vector2(32, 32));
                EditorGUI.DrawPreviewTexture(iconPos, icon, DMSElements.nodeMaterial);
            }

            GUIStyle style = EditorStyles.label;
            style.richText = true;

            string colorCode = "<color=#000000ff>";
            int fontSize = 11;
            if (node.nodeType == DMSNode.NodeType.COMMENT)
            {
                colorCode = "<color=#ffffffff>";
                node.position.size = EditorStyles.label.CalcSize(new GUIContent(name)) + new Vector2(16, 8);
                fontSize = (int)((float)fontSize * NodeCanvasWindow.windowScale);
            }

            Rect textRect = new Rect(offPos);
            if (node.nodeType != DMSNode.NodeType.COMMENT)
            {
                textRect.width = Mathf.Max(offPos.width, EditorStyles.label.CalcSize(new GUIContent(name)).x + 8);
                textRect.height = EditorStyles.label.CalcSize(new GUIContent(name)).y;
                textRect.y -= textRect.height;

                EditorGUI.DrawPreviewTexture(textRect, DMSElements.nodeNameBar, material);
            }

            EditorGUI.LabelField(textRect, new GUIContent(colorCode + name + "</color>"),new GUIStyle(EditorStyles.label) { alignment = TextAnchor.MiddleCenter, fontSize = fontSize });

            if (selected)
                EditorGUI.DrawPreviewTexture(offPos, DMSElements.nodeSelectGraphic, DMSElements.nodeMaterial);
        }
    }
}