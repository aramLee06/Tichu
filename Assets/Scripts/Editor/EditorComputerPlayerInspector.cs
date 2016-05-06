using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(ComputerPlayer))]
public class EditorComputerPlayerInspector : Editor
{
    private MonoScript script;

    private static System.Type[] acceptedModuleTypes = new System.Type[]
    {
        typeof(AIModuleEasy),
        typeof(AIModuleNormal),
        typeof(AIModuleHard),
    };

    public void OnEnable()
    {
        try
        {
            script = MonoScript.FromScriptableObject(((ComputerPlayer)target).aiModule);
        }
		catch (System.Exception e) { Debug.Log (e); }
    }

    public static bool IsAcceptedType(System.Type type)
    {
        foreach(System.Type acceptedType in acceptedModuleTypes)
        {
            if (type == acceptedType)
                return true;
        }
        return false;
    }

    public override void OnInspectorGUI()
    {
        ComputerPlayer cpu = (ComputerPlayer)target;

        EditorGUI.BeginChangeCheck();
        script = (MonoScript)EditorGUILayout.ObjectField("AI Module", script, typeof(MonoScript), false);

        if (script != null && IsAcceptedType(script.GetClass()))
        {
            cpu.aiModule = ScriptableObject.CreateInstance(script.GetClass()) as AIModuleBase;
        }
        else
        {
            if(script != null)
            {
                EditorUtility.DisplayDialog("Error", script.GetClass().Name + " is not a valid AI Module. Silly you.", "I'll fix it.");
            }

            cpu.aiModule = null;
            script = null;
        }
        if (EditorGUI.EndChangeCheck())
            EditorSceneManager.MarkAllScenesDirty();

        EditorGUILayout.Space();
        base.OnInspectorGUI();
    }
}