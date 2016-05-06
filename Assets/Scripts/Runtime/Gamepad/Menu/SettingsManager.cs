using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SettingsManager : MonoBehaviour
{
    private int targetId;

    public void SetTarget(int id)
    {
        targetId = id;
    }

    public void SetAIEnabled(Toggle toggle)
    {
        string msg = "Ma" + targetId;
        msg += toggle.isOn ? "+" : "-";
        NetworkEmulator.main.SendDataToHost(msg);
    }

    public void SetAIDifficulty(Dropdown dropdown)
    {
        string msg = "Md" + targetId.ToString() + dropdown.value.ToString();
        NetworkEmulator.main.SendDataToHost(msg);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        NetworkEmulator.main.SendDataToHost("Mp");
    }
}