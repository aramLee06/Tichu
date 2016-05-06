using UnityEditor;
using UnityEngine;
using System.IO;

namespace DynamicMusicSystem
{
    public delegate void AssignedMusicControllerHandler(MusicController musicController);

    /// <summary>
    /// Main Editor Window for DynaMusic.
    /// </summary>
    public class WindowDMS : EditorWindow
    {
        private ConditionsLayersWindow conditionsLayersWindow;
        private NodeCanvasWindow nodeCanvasWindow;
        public MusicController target;

        public bool showSideWindow = true; //Show the conditionsLayersWindow?

        public event AssignedMusicControllerHandler assignedMusicControllerEvent;

        public static Texture2D icon;

        [MenuItem("Window/DynaMusic %m")]
        public static void OpenWindow()
        {
            WindowDMS window = GetWindow<WindowDMS>("DynaMusic");

            if(icon == null)
                icon = Resources.Load("Textures/Icon") as Texture2D;
            window.titleContent = new GUIContent("DynaMusic", icon);

            /*if (Selection.activeObject.GetType() == typeof(MusicController))
            {
                window.target = Selection.activeObject as MusicController;
                window.SetTarget();
            }*/
        }

        public void UnsetSelection()
        {
            conditionsLayersWindow.selectedNode = null;
            nodeCanvasWindow.selectedNode = null;
        }

        public void OnEnable()
        {
            conditionsLayersWindow = new ConditionsLayersWindow(this);
            nodeCanvasWindow = new NodeCanvasWindow(this);
            //if (!DMSElements.initialized)
                DMSElements.Initialize();
        }

        public void SetTransitionMode()
        {
            if(conditionsLayersWindow.selectedNode != null)
                conditionsLayersWindow.transitionMode = true;
        }

        public void RemoveUnnecessary()
        {
            nodeCanvasWindow.CreateNodeWindows();
            nodeCanvasWindow.RemoveNonExistantTransitions();
            Repaint();
        }

        private void OnDisable()
        {
            SaveData();
        }

        private void OnLostFocus()
        {
            SaveData();
        }

        private void SaveData()
        {
            if (target != null)
            {
                foreach (DynaMusicLayer layer in target.layers)
                    layer.SaveClass();
                target.SaveClass();
                EditorUtility.SetDirty(target);
                //AssetDatabase.SaveAssets();
            }
        }

        public void SetTarget()
        {
            assignedMusicControllerEvent(target);
            if (target != null)
            {
                foreach (DynaMusicLayer layer in target.layers)
                {
                    layer.LoadClass();
                    target.LoadClass();
                    nodeCanvasWindow.CreateNodeWindows();
                }
            }
        }

        public bool changes = false;

        private void OnGUI()
        {
            try
            {
                if (target != null)
                {
                    changes = false;
                    EditorGUI.BeginChangeCheck();
                    Undo.RecordObject(target, "Music Controller Edit");

                    conditionsLayersWindow.Initialize(); //Sets up the sidebar.

                    nodeCanvasWindow.Draw(); //Draws the grid. Needs direct reference to the actual function, really.
                    nodeCanvasWindow.DrawNodes(); //Draws the nodes into the grid area.
                    nodeCanvasWindow.HideGridTopMostElements(showSideWindow ? 300 : 1f); //The top-bar, hacky way to prevent draw-over-the-gui bug.

                    if (conditionsLayersWindow.transitionMode)
                        nodeCanvasWindow.DrawTransitionModeArrow(conditionsLayersWindow.selectedNode); //Draws the brazier + arrow in case you're selecting a transition.

                    nodeCanvasWindow.DrawRightClickMenu(); //Draw the create-menu.
                    nodeCanvasWindow.Toolbar(); //Draws the toolbar.
                    
                    GUILayout.BeginHorizontal(GUILayout.Width(showSideWindow ? 300 : 1f));
                    {
                        conditionsLayersWindow.Draw(showSideWindow); //Draws the sidewindow. This piece does a lot more than that. Needs a better name. 
                        //Also handles transition creation/removal.
                    }
                    GUILayout.EndHorizontal();
                    nodeCanvasWindow.HandleEvents(); //Handles editor events.

                    //EditorUtility.SetDirty(target);
                    //SerializedObject so = new SerializedObject(target);
                    //so.ApplyModifiedProperties();
                    if (EditorGUI.EndChangeCheck() || changes)
                    {
                        //Debug.Log("Changes were made");
                        SaveData();
                    }
                }
                else //Begin screen, where you select what asset you want. Needs cleanup.
                {
                    EditorGUILayout.LabelField("Please select a music controller for editing.");
                    EditorGUI.BeginChangeCheck();
                    //target = EditorGUILayout.ObjectField(string.Empty, target, typeof(MusicController), false) as MusicController;

                    string[] guids = AssetDatabase.FindAssets("t:MusicController");
                    int i = 0;
                    while(i < guids.Length)
                    {
                        EditorGUILayout.BeginHorizontal();
                        for (int x = 0; x < 4; x++)
                        {
                            if (i < guids.Length)
                            {
                                Rect r = EditorGUILayout.GetControlRect(false, 64);
                                string guid = guids[i];
                                string path = AssetDatabase.GUIDToAssetPath(guid);
                                
                                MusicController controller = AssetDatabase.LoadAssetAtPath<MusicController>(path);

                                if (GUI.Button(r, Path.GetFileName(path)))
                                    target = controller;

                                if (controller.icon != null)
                                    GUI.Label(r, controller.icon);
                            }
                            else
                                break;
                            i++;
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (EditorGUI.EndChangeCheck())
                    {
                        SetTarget();
                    }
                }
            }
            catch(System.Exception e)
            {
                if (e.GetType() != typeof(UnityEngine.ExitGUIException))
                {
                    Debug.LogError(e.GetType() + " - " + e.Message + "\n" + e.StackTrace);
                    target = null;
                    Close();
                    OpenWindow();
                }
            }
        }
    }
}