/* This wizard will replace a selection with an object or prefab.
 * Scene objects will be cloned (destroying their prefab links).
 * Original coding by 'yesfish', nabbed from Unity Forums
 * 'keep parent' added by Dave A (also removed 'rotation' option, using localRotation
 */
using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorReplaceSelection : ScriptableWizard
{
	static GameObject replacement = null;
	static bool keep = false;

	public GameObject ReplacementObject = null;
	public bool KeepOriginals = false;

	[MenuItem("GameObject/Replace Selection... &r")]
	static void CreateWizard()
	{
		ScriptableWizard.DisplayWizard(
			"Replace Selection", typeof(EditorReplaceSelection), "Replace");
	}

	public EditorReplaceSelection()
	{
		ReplacementObject = replacement;
		KeepOriginals = keep;
		Vector2 wizardPosition = new Vector2 (Screen.width * 0.5f, Screen.height * 0.5f);
		Vector2 wizardSize = new Vector2 (200, 200);

		position = new Rect(wizardPosition, wizardSize);
	}

	void OnWizardUpdate()
	{
		replacement = ReplacementObject;
		keep = KeepOriginals;
	}

	void OnWizardCreate()
	{
		if (replacement == null)
			return;

		Undo.RegisterSceneUndo("Replace Selection");

		Transform[] transforms = Selection.GetTransforms(
			SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable);

		foreach (Transform t in transforms)
		{
			GameObject g;
			PrefabType pref = PrefabUtility.GetPrefabType(replacement);

			if (pref == PrefabType.Prefab || pref == PrefabType.ModelPrefab)
			{
				g = (GameObject)PrefabUtility.InstantiatePrefab(replacement);
			}
			else
			{
				g = (GameObject)Editor.Instantiate(replacement);
			}
			Undo.RegisterCreatedObjectUndo (g, "Replace " + t.name);

			Transform gTransform = g.transform;
			gTransform.parent = t.parent;
			g.name = replacement.name;
			gTransform.localPosition = t.localPosition;
			gTransform.localScale = t.localScale;
			gTransform.localRotation = t.localRotation;

			EditorUtility.SetDirty(g);
		}

		if (!keep)
		{
			foreach (GameObject g in Selection.gameObjects)
			{
				Undo.DestroyObjectImmediate(g);
			}
		}
	}
}