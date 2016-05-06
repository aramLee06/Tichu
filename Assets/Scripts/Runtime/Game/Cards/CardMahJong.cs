using UnityEngine;
using System.Collections;

[System.Serializable]
public class CardMahJong : Card
{

    public override void OnPlacement(int playerId)
    {
        SpecialCardEffectHandler.MahJongCard();
        Manager.main.HandleMahjong();
        //Insert code that specifies that a menu should pop-up that allows the player to select a card value.
    }

    public void ApplyValueWish(int value)
    {
        Debug.LogWarning("<color=#1a7>Mahjong value set to " + value + "</color>");

        int v = value;
        if (v == -1)
            v = 255;

        NetworkEmulator.main.SendData("W9"+(char)value);
    }
}