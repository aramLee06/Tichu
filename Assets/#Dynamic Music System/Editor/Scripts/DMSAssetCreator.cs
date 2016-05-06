using UnityEngine;
using UnityEditor;
using System.IO;

public static class DMSAssetCreator
{
    [MenuItem("Assets/Create/DynaMusic/MusicController")]
    public static void CreateMusicController()
    {
        MusicController mc = ScriptableObject.CreateInstance<MusicController>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == string.Empty)
            path = "Assets";
        else if (Path.GetExtension(path) != string.Empty)
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");

        string assetPath = AssetDatabase.GenerateUniqueAssetPath(path + "/New Music Controller.asset");

        AssetDatabase.CreateAsset(mc, assetPath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = mc;
    }
}