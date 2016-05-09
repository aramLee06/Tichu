using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Obsolete("Debug script.")]
[RequireComponent(typeof(Text))]
public class PlayerReady : MonoBehaviour
{
    private Text text;
    public int player;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = GameManager.main.players[player].isReady ? "Ready" : "Not Ready";
    }
}