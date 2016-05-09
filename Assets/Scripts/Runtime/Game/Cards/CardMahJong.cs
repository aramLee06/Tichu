using UnityEngine;
using System.Collections;

/// <summary>
/// Mahjong's Special Card
/// </summary>
[System.Serializable]
public class CardMahJong : Card
{

    /// <summary>
    /// Called when the card has been placed
    /// </summary>
    /// <param name="playerId">ID of the player that places the card</param>
    public override void OnPlacement(int playerId)
    {
        SpecialCardEffectHandler.MahJongCard();
        Manager.main.HandleMahjong();
        //Insert code that specifies that a menu should pop-up that allows the player to select a card value.
    }

    /// <summary>
    /// Applies the Card's wish value
    /// </summary>
    /// <param name="value">The chosen card value</param>
    public void ApplyValueWish(int value)
    {
        Debug.LogWarning("<color=#1a7>Mahjong value set to " + value + "</color>");

        int v = value;
        if (v == -1)
            v = 255;

        NetworkEmulator.main.SendData("W9"+(char)value);
    }
}