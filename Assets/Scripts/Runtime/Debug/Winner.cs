using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Obsolete("Debug script.")]
[RequireComponent(typeof(Text))]
public class Winner : MonoBehaviour
{
    private Text text;

    public void Start()
    {
        text = GetComponent<Text>();
    }

    public void Update()
    {
        if (GameManager.main.currentState == GameState.GAME_RESULT)
        {
            text.text = "Team " + GameManager.main.winTeam + " has won!\n[Insert sounds of fireworks]";
        }
        else
            text.text = "";
    }
}