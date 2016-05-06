using UnityEngine;
using System.Collections.Generic;

public class AIModuleBase : ScriptableObject
{
    public virtual void PerformTurn(ComputerPlayer computerPlayer, out PlayKind kind, out bool earlyPass, List<Card> wishCards) { kind = PlayKind.NONE; earlyPass = true; }
    public virtual void SetupTrade(ComputerPlayer computerPlayer, int friendId) { }
}