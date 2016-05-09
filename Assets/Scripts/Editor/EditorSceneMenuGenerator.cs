using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

/* Made by Jeroen Endenburg
 * Edited by Bas Klein
 * 13-04-2016
 */

    /// <summary>
    /// An editor script that will generate quick-menus to certain scenes.
    /// </summary>
public class EditorSceneMenuGenerator
{
	/* The folder that the menu script will be generated to. Has to be an Editor folder.
	 * Default: "Assets/Scripts/Editor/"
	 */
	public static string editorPath = "Assets/Scripts/Editor/";

	/* The string that will be detected in the name of the scene file. The MenuItems' priority will be set according to the order in which the strings appear.
	 * Meaning that you can order the way in which the MenuItems will appear by writing certain tags in the name of the scene file.
	 * eg. "[Level] Level 1".unity and "[Level] Level 2".unity will appear in the same area when you enter the string "[Level]" in this array.
	 * If you for example also enter "[Menu]" into the array before "[Level]", the "[Menu]" scenes will appear before the "[Level]" scenes.
	 * Note: you don't have to write it in brackets, it's just more reliable that way.
	 */
	public static string[] orderSetString = new string[]
	{
		"[Gamepad]",
		"[Screen]",
		"[Global]",
		"[TEST]"
	};

	/* Used to dictate whether or not the scenes that aren't mentioned in the array above should appear in the list of MenuItems.
	 * true = hidden
	 * false = shown (default)
	 */
	public static bool hideOthers = false;

	// The actual code. Don't touch if you don't know what you're doing!

    /// <summary>
    /// Generates the script that contains the scene loading data and respective menus.
    /// </summary>
    [MenuItem("Scenes/Get All &c")]
    public static void GenerateScript ()
    {
		if (editorPath.Contains ("Editor"))
		{
			List<string> paths = new List<string> (AssetDatabase.GetAllAssetPaths ());
			List<string> scenePaths = new List<string> ();

			foreach (string path in paths)
			{
				string[] pathSplit = path.Split ('.');
				if (pathSplit.Length > 0)
				{
					if (pathSplit [pathSplit.Length - 1] == "unity")
						scenePaths.Add (path);
				}
			}

			using (StreamWriter writer = new StreamWriter (editorPath + "EditorMenus.cs"))
			{
				writer.Write ("using UnityEditor;");
				writer.Write ("public class EditorMenus {");

				int priority = 0;

				foreach (string scenePath in scenePaths)
				{
					string[] splitPath = scenePath.Split ('/');
					string sceneMenuName = splitPath [splitPath.Length - 1].Split ('.') [0];
					string sceneName = sceneMenuName.Replace ("\"", "\\\"").Replace ("#", "Hashtag").Replace ("(", "POpen").Replace (")", "PClose").Replace ("*", "Asterik").Replace ("[", "BOpen").Replace ("]", "BClose");

					for (int i = 0; i < orderSetString.Length; i++)
					{
						if (sceneMenuName.Contains (orderSetString [i]))
						{
							priority = i * 50;
							break;
						}
						else
							priority = (orderSetString.Length + 1) * 50;
					}

					if (priority > (orderSetString.Length * 50) && hideOthers)
						Debug.Log (sceneMenuName + " was hidden.");
					else
						WriteSceneData (writer, scenePath, sceneMenuName, sceneName, priority);
				}

				writer.Write ("}");
			}

			AssetDatabase.Refresh ();
		}
		else
		{
			Debug.LogErrorFormat ("Could not generate script. Path {0} is not a valid path. Please enter a valid Editor folder path.", editorPath);
		}
    }

    /// <summary>
    /// Writes the scene-loading editor functions
    /// </summary>
    /// <param name="writer">Streamwriter to use</param>
    /// <param name="scenePath">Path to the scene</param>
    /// <param name="sceneMenuName">Name of the scene in the menu</param>
    /// <param name="sceneName">Name of the scene in the project</param>
    /// <param name="priority">Priority in the menu</param>
	public static void WriteSceneData (StreamWriter writer, string scenePath, string sceneMenuName, string sceneName, int priority = 0)
	{
		writer.Write ("[MenuItem(\"Scenes/OPEN " + sceneMenuName + "\", false, " + priority.ToString() + ")]");
		writer.Write ("public static void OpenScene_" + sceneName.Replace (' ', '_') + "() {");
		writer.Write ("EditorSceneMenuGenerator.OpenScene(\"" + scenePath + "\");");
		writer.Write ("}");
	}

    /// <summary>
    /// Opens the specified scene
    /// </summary>
    /// <param name="path">Path to the specified scene</param>
    public static void OpenScene (string path)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ())
        {
			EditorSceneManager.OpenScene (path);
        }
    }

}