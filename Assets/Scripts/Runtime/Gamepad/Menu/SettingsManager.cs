using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Settings manager.
/// </summary>
public class SettingsManager : MonoBehaviour
{
	/// <summary>
	/// The target identifier.
	/// </summary>
    private int targetId;

	/// <summary>
	/// Sets the target.
	/// </summary>
	/// <param name="id">Identifier.</param>
    public void SetTarget(int id)
    {
        targetId = id;
    }

	/// <summary>
	/// Sets whether to enable the AI.
	/// </summary>
	/// <param name="toggle">Toggle.</param>
    public void SetAIEnabled(Toggle toggle)
    {
        string msg = "Ma" + targetId;
        msg += toggle.isOn ? "+" : "-";
        NetworkEmulator.main.SendDataToHost(msg);
    }

	/// <summary>
	/// Sets the AI difficulty.
	/// </summary>
	/// <param name="dropdown">Dropdown.</param>
    public void SetAIDifficulty(Dropdown dropdown)
    {
        string msg = "Md" + targetId.ToString() + dropdown.value.ToString();
        NetworkEmulator.main.SendDataToHost(msg);
    }

	/// <summary>
	/// Changes the scene.
	/// </summary>
	/// <param name="sceneName">Scene name.</param>
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        NetworkEmulator.main.SendDataToHost("Mp");
    }
}