using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

/// <summary>
/// Custom editor for GameManager.cs Monobehaviour
/// </summary>
[CustomEditor(typeof(GameManager))]
public class EditorGameManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        if(GUILayout.Button("Generate Regular Cards"))
        {
            GenerateCards();
        }
        if (EditorGUI.EndChangeCheck())
            EditorSceneManager.MarkAllScenesDirty();

        base.OnInspectorGUI();
    }

    /// <summary>
    /// Generate default deck
    /// </summary>
    private void GenerateCards()
    {
        GameManager manager = (GameManager)target;
        manager.regularCards = EditorGenerateCards.GenerateCards();
    }
}