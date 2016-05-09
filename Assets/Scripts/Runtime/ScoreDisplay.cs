using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Obsolete("Unused")]
[RequireComponent(typeof(Text))]
public class ScoreDisplay : MonoBehaviour
{
    private Text text;
    private PlayerHandler player;
    public int playerId;

    private void Start()
    {
        text = GetComponent<Text>();
        player = GameManager.main.players[playerId];
    }

    private void FixedUpdate()
    {
        text.text = player.team.score.ToString("D4");
    }
}