using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(GamepadManager))]
public class EditorGamepadManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Generate Regular Cards"))
        {
            GenerateCards();
        }
        if (EditorGUI.EndChangeCheck())
            EditorSceneManager.MarkAllScenesDirty();

        base.OnInspectorGUI();
    }

    private void GenerateCards()
    {
        GamepadManager gpm = (GamepadManager)target;
        gpm.regularCards = EditorGenerateCards.GenerateCards();
    }
}