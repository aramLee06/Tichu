using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Custom editor for GamepadManager.cs monobehaviour
/// </summary>
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

    /// <summary>
    /// Generate default decks
    /// </summary>
    private void GenerateCards()
    {
        GamepadManager gpm = (GamepadManager)target;
        gpm.regularCards = EditorGenerateCards.GenerateCards();
    }
}