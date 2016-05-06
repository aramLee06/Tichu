using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.SceneManagement;

[CanEditMultipleObjects]
[CustomEditor(typeof(SetAIDifficulty))]
public class EditorAISettingInspector : Editor
{
    public override void OnInspectorGUI()
    {
        SetAIDifficulty said = (SetAIDifficulty)target;

        EditorGUI.BeginChangeCheck();
        {
            int size = Mathf.Max(EditorGUILayout.DelayedIntField("Amount of Modules", said.modules.Length),1);
            EditorGUI.indentLevel++;

            MonoScript[] scripts = new MonoScript[size];

            for (int i = 0; i < Mathf.Min(said.modules.Length,size); i++)
            {
                if(said.modules[i] != null)
                    scripts[i] = MonoScript.FromScriptableObject(said.modules[i]);
            }

            said.modules = new AIModuleBase[size];

            for (int i = 0; i < size; i++)
            {
                MonoScript oldScript = scripts[i];
                scripts[i] = (MonoScript)EditorGUILayout.ObjectField("Module " + i, scripts[i], typeof(MonoScript), false);

                if(scripts[i] != null && !EditorComputerPlayerInspector.IsAcceptedType(scripts[i].GetClass()))
                {
                    EditorUtility.DisplayDialog("Error", scripts[i].GetClass().Name + " is not a valid AI Module. Silly you.", "I'll fix it.");
                    scripts[i] = oldScript;
                }
                
                if (scripts[i] != null)
                    said.modules[i] = ScriptableObject.CreateInstance(scripts[i].GetClass()) as AIModuleBase;
                else
                    said.modules[i] = null;
            }

            EditorGUI.indentLevel--;
        }
        if(EditorGUI.EndChangeCheck())
            EditorSceneManager.MarkAllScenesDirty();
    }
}