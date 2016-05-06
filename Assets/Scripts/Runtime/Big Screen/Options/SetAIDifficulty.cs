using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ComputerPlayer))]
public class SetAIDifficulty : MonoBehaviour
{
    public AIModuleBase[] modules;

    private void Start()
    {
        ComputerPlayer cpu = GetComponent<ComputerPlayer>();

        cpu.aiModule = modules[(int)GameSettings.difficulty[cpu.playerId]];
    }
}