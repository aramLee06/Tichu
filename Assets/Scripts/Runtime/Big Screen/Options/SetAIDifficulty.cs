using UnityEngine;
using System.Collections;

/// <summary>
/// AI Adjusting script
/// </summary>
[RequireComponent(typeof(ComputerPlayer))]
public class SetAIDifficulty : MonoBehaviour
{
	/// <summary>
	/// The modules.
	/// </summary>
    public AIModuleBase[] modules;

    private void Start()
    {
        ComputerPlayer cpu = GetComponent<ComputerPlayer>();

        cpu.aiModule = modules[(int)GameSettings.difficulty[cpu.playerId]];
    }
}