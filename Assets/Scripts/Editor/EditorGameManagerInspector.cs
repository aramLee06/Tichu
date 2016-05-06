using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;

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

    private void GenerateCards()
    {
        GameManager manager = (GameManager)target;
        manager.regularCards = EditorGenerateCards.GenerateCards();
    }
}