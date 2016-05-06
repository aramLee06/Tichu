using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class PlayerTeam : MonoBehaviour
{
    private Text text;
    public int playerId;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Score: " + GameManager.main.players[playerId].team.score.ToString();
    }
}