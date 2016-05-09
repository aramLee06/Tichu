using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base of the AI Module
/// </summary>
public class AIModuleBase : ScriptableObject
{
    /// <summary>
    /// Performs the AI's turn
    /// </summary>
    /// <param name="computerPlayer">The ComputerPlayer this module is attached to</param>
    /// <param name="kind">The kind of play the AI did</param>
    /// <param name="earlyPass">Did the AI pass earlier?</param>
    /// <param name="wishCards">List of cards for the Mahjong's wish</param>
    public virtual void PerformTurn(ComputerPlayer computerPlayer, out PlayKind kind, out bool earlyPass, List<Card> wishCards) { kind = PlayKind.NONE; earlyPass = true; }

    /// <summary>
    /// Sets the AI's trade
    /// </summary>
    /// <param name="computerPlayer">The ComputerPlayer this module is attached to</param>
    /// <param name="friendId">The player ID of the AI's teammate</param>
    public virtual void SetupTrade(ComputerPlayer computerPlayer, int friendId) { }
}