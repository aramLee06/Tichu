using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections;

/// <summary>
/// Custom editor for ProfileIconList.cs monobehaviour
/// </summary>
[CustomEditor(typeof(ProfileIconList))]
public class EditorProfileIconList : Editor
{
    public override void OnInspectorGUI()
    {
        Undo.RecordObject(target, "Adjusting Profile Icon List");
        ProfileIconList profileList = (ProfileIconList)target;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginVertical();
        {
            #region Dog Profile
            EditorGUILayout.LabelField("Dog Profile Icons",EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                profileList.dogEmotions[0] = TextureField("Happy Dog (Upper)", profileList.dogEmotions[0]);
                profileList.dogEmotions[1] = TextureField("Neutral Dog (Upper)", profileList.dogEmotions[1]);
                profileList.dogEmotions[2] = TextureField("Sad Dog (Upper)", profileList.dogEmotions[2]);
                profileList.dogEmotions[3] = TextureField("Happy Dog (Lower)", profileList.dogEmotions[3]);
                profileList.dogEmotions[4] = TextureField("Neutral Dog (Lower)", profileList.dogEmotions[4]);
                profileList.dogEmotions[5] = TextureField("Sad Dog (Lower)", profileList.dogEmotions[5]);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            #endregion

            #region Phoenix Profile
            EditorGUILayout.LabelField("Phoenix Profile Icons", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                profileList.phoenixEmotions[0] = TextureField("Happy Phoenix (Upper)", profileList.phoenixEmotions[0]);
                profileList.phoenixEmotions[1] = TextureField("Neutral Phoenix (Upper)", profileList.phoenixEmotions[1]);
                profileList.phoenixEmotions[2] = TextureField("Sad Phoenix (Upper)", profileList.phoenixEmotions[2]);
                profileList.phoenixEmotions[3] = TextureField("Happy Phoenix (Lower)", profileList.phoenixEmotions[3]);
                profileList.phoenixEmotions[4] = TextureField("Neutral Phoenix (Lower)", profileList.phoenixEmotions[4]);
                profileList.phoenixEmotions[5] = TextureField("Sad Phoenix (Lower)", profileList.phoenixEmotions[5]);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            #endregion

            #region Panda Profile
            EditorGUILayout.LabelField("Panda Profile Icons", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                profileList.pandaEmotions[0] = TextureField("Happy Panda (Upper)", profileList.pandaEmotions[0]);
                profileList.pandaEmotions[1] = TextureField("Neutral Panda (Upper)", profileList.pandaEmotions[1]);
                profileList.pandaEmotions[2] = TextureField("Sad Panda (Upper)", profileList.pandaEmotions[2]);
                profileList.pandaEmotions[3] = TextureField("Happy Panda (Lower)", profileList.pandaEmotions[3]);
                profileList.pandaEmotions[4] = TextureField("Neutral Panda (Lower)", profileList.pandaEmotions[4]);
                profileList.pandaEmotions[5] = TextureField("Sad Panda (Lower)", profileList.pandaEmotions[5]);
            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
            #endregion

            #region Dragon Profile
            EditorGUILayout.LabelField("Dragon Profile Icons", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
                profileList.dragonEmotions[0] = TextureField("Happy Dragon", profileList.dragonEmotions[0]);
                profileList.dragonEmotions[1] = TextureField("Neutral Dragon", profileList.dragonEmotions[1]);
                profileList.dragonEmotions[2] = TextureField("Sad Dragon", profileList.dragonEmotions[2]);
            EditorGUI.indentLevel--;
            #endregion
        }
        EditorGUILayout.EndVertical();

        EditorUtility.SetDirty(target);
        if(EditorGUI.EndChangeCheck())
        {
            serializedObject.ApplyModifiedProperties();
            EditorSceneManager.MarkAllScenesDirty();
        }
    }

    /// <summary>
    /// A quick way to draw a Sprite Object Field
    /// </summary>
    /// <param name="label">Label of the object</param>
    /// <param name="sprite">Default sprite</param>
    /// <returns>The inserted sprite</returns>
    private Sprite TextureField(string label, Sprite sprite)
    {
        Sprite ret = null;
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.LabelField(label);
            ret = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), false);
        }
        EditorGUILayout.EndVertical();
        return ret;
    }
}