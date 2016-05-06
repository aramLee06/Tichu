using UnityEngine;
using UnityEditor;
using System.Collections;

namespace DynamicMusicSystem
{
    public static class DMSElements
    {
        public static bool initialized = false;

        public static Texture2D visibleIcon, hiddenIcon;

        public static Texture2D gridTexture;

        public static Texture2D nodeGraphic, nodeNameBar;
        public static Material nodeMaterial;
        public static Texture2D nodeSelectGraphic;

        public static Texture2D arrowHead;
        public static Texture2D conditionIcon;
        public static Texture2D deleteIcon;
        public static Texture2D newLayerIcon;
        public static Texture2D breakLine;

        public static Texture2D playIcon, stopIcon, loopIcon, clipIcon, pauseIcon, operationIcon, commentIcon, blendIcon, volumeIcon, pitchIcon;

        public static void Initialize()
        {
            if (!initialized)
            {
                visibleIcon = Resources.Load("Textures/visibleIcon") as Texture2D;
                hiddenIcon = Resources.Load("Textures/hiddenIcon") as Texture2D;

                gridTexture = Resources.Load("Textures/background") as Texture2D;
                gridTexture.wrapMode = TextureWrapMode.Repeat;

                nodeGraphic = Resources.Load("Textures/Node") as Texture2D;
                nodeMaterial = Resources.Load("Materials/nodeMaterial") as Material;
                nodeSelectGraphic = Resources.Load("Textures/NodeSelection") as Texture2D;
                nodeNameBar = Resources.Load("Textures/NameBar") as Texture2D;

                arrowHead = Resources.Load("Textures/ArrowHead") as Texture2D;
                deleteIcon = Resources.Load("Textures/trash") as Texture2D;
                newLayerIcon = Resources.Load("Textures/newLayer") as Texture2D;
                breakLine = Resources.Load("Textures/breakLine") as Texture2D;

                conditionIcon = Resources.Load("Textures/condition") as Texture2D;

                playIcon = Resources.Load("Textures/playIcon") as Texture2D;
                stopIcon = Resources.Load("Textures/stopIcon") as Texture2D;
                loopIcon = Resources.Load("Textures/loopIcon") as Texture2D;
                clipIcon = Resources.Load("Textures/clipIcon") as Texture2D;
                pauseIcon = Resources.Load("Textures/pauseIcon") as Texture2D;
                operationIcon = Resources.Load("Textures/operationIcon") as Texture2D;
                commentIcon = Resources.Load("Textures/commentIcon") as Texture2D;
                blendIcon = Resources.Load("Textures/blendIcon") as Texture2D;
                volumeIcon = Resources.Load("Textures/volumeIcon") as Texture2D;
                pitchIcon = Resources.Load("Textures/pitchIcon") as Texture2D;

                initialized = true;
            }
        }
    }
}