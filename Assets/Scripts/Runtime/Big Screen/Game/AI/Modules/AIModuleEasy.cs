using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Easy AI's Module
/// </summary>
[Serializable]
public class AIModuleEasy : AIModuleBase
{
    /// <summary>
    /// Performs the AI's turn
    /// </summary>
    /// <param name="computerPlayer">The ComputerPlayer this module is attached to</param>
    /// <param name="kind">The kind of play the AI did</param>
    /// <param name="earlyPass">Did the AI pass earlier?</param>
    /// <param name="wishCards">List of cards for the Mahjong's wish</param>
    public override void PerformTurn(ComputerPlayer computerPlayer, out PlayKind kind, out bool earlyPass, List<Card> wishCards)
    {
        kind = PlayKind.NONE;
        earlyPass = false;
        if (computerPlayer.manager.lastPlayKind == PlayKind.NONE)
        {
            int maxVal = Enum.GetNames(typeof(PlayKind)).Length;
            kind = (PlayKind)UnityEngine.Random.Range(1, maxVal);
            if (UnityEngine.Random.value > .7f)
                kind = PlayKind.SINGLE_PLAY;

            switch (kind)
            {
                case PlayKind.ONE_PAIR:
                    if (computerPlayer.pairCombos.Count == 0)
                        kind = PlayKind.SINGLE_PLAY;
                    break;
                case PlayKind.STAIRS:
                    if (computerPlayer.doublePairCombos.Count == 0)
                    {
                        kind = PlayKind.ONE_PAIR;
                        goto case PlayKind.ONE_PAIR;
                    }
                    break;
                case PlayKind.THREE_OF_A_KIND:
                    if (computerPlayer.threeOfAKindCombos.Count == 0)
                    {
                        kind = PlayKind.STAIRS;
                        goto case PlayKind.STAIRS;
                    }
                    break;
                case PlayKind.FULL_HOUSE:
                    if (computerPlayer.fullHouseCombos.Count == 0)
                    {
                        kind = PlayKind.THREE_OF_A_KIND;
                        goto case PlayKind.THREE_OF_A_KIND;
                    }
                    break;
                case PlayKind.STRAIGHT:
                    if (computerPlayer.straightCombos.Count == 0)
                    {
                        kind = PlayKind.FULL_HOUSE;
                        goto case PlayKind.FULL_HOUSE;
                    }
                    break;
                case PlayKind.BOMB:
                    if (computerPlayer.bombCombos.Count == 0)
                    {
                        kind = PlayKind.STRAIGHT;
                        goto case PlayKind.STRAIGHT;
                    }
                    break;
            }
        }
        else
        {
            kind = computerPlayer.manager.lastPlayKind;

            if (UnityEngine.Random.value > .8f)
            {
                earlyPass = true;
                return;
            }
        }

        if (kind == PlayKind.NONE)
            kind = PlayKind.SINGLE_PLAY;

        float bombChance = 0;
        if (computerPlayer.bombCombos.Count > 0)
            bombChance = .2f;

        if (UnityEngine.Random.value < bombChance)
            kind = PlayKind.BOMB;

        computerPlayer.playCards = new List<Card>();

        float lowVal = 255;
        float highval = 255;

        if (computerPlayer.manager.lastPlay != null && computerPlayer.manager.lastPlay.Count > 0)
        {
            if (computerPlayer.manager.lastPlay[0] != null && computerPlayer.manager.lastPlay[0].card != null)
                lowVal = (int)computerPlayer.manager.lastPlay[0].card.value;
            if (computerPlayer.manager.lastPlay[computerPlayer.manager.lastPlay.Count - 1] != null && computerPlayer.manager.lastPlay[computerPlayer.manager.lastPlay.Count - 1].card != null)
                highval = (int)computerPlayer.manager.lastPlay[computerPlayer.manager.lastPlay.Count - 1].card.value;
        }

        for (int i = 0; i < 25; i++)
        {
            if (i == 24 && computerPlayer.manager.lastPlayKind == PlayKind.NONE && computerPlayer.orderedHand.Count > 0)
            {
                i = 0;
                kind = PlayKind.SINGLE_PLAY;
            }

            int maxAmountDecrement = 10;
            if (i >= 20)
                maxAmountDecrement = 2;

            switch (kind)
            {
                case PlayKind.NONE:
                    goto case PlayKind.SINGLE_PLAY;
                case PlayKind.SINGLE_PLAY:
                    {
                        if (computerPlayer.orderedHand.Count > 0)
                        {
                            Card card = computerPlayer.orderedHand[UnityEngine.Random.Range(0, Mathf.Max(0,computerPlayer.orderedHand.Count- maxAmountDecrement))];
                            if (wishCards.Count > 0 && wishCards[0].value > computerPlayer.manager.lastValue)
                            {
                                card = wishCards[0];
                                NetworkEmulator.main.SendDataToHost("J"+computerPlayer.playerId+"X");
                            }
                            if (card.value > computerPlayer.manager.lastValue || computerPlayer.manager.lastPlayKind == PlayKind.NONE)
                                computerPlayer.playCards.Add(card);
                        }
                    }
                    break;
                case PlayKind.ONE_PAIR:
                    {
                        CardCombination combo;
                        if (computerPlayer.manager.lastPlay.Count == 2 && computerPlayer.pairCombos.Count > 0)
                            combo = computerPlayer.pairCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.pairCombos.Count - maxAmountDecrement))];
                        else if (computerPlayer.manager.lastPlay.Count == 4 && computerPlayer.doublePairCombos.Count > 0)
                            combo = computerPlayer.doublePairCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.doublePairCombos.Count - maxAmountDecrement))];
                        else
                            break;
                        if (combo.combinedValue > computerPlayer.manager.lastValue)
                            computerPlayer.playCards = combo.combination;
                    }
                    break;
                case PlayKind.STAIRS:
                    goto case PlayKind.ONE_PAIR;
                case PlayKind.THREE_OF_A_KIND:
                    {
                        CardCombination combo = null;
                        if (computerPlayer.threeOfAKindCombos.Count > 0)
                            combo = computerPlayer.threeOfAKindCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.threeOfAKindCombos.Count - maxAmountDecrement))];
                        if (combo != null && combo.combinedValue > computerPlayer.manager.lastValue)
                            computerPlayer.playCards = combo.combination;
                    }
                    break;
                case PlayKind.FULL_HOUSE:
                    {
                        CardCombination combo = null;
                        if (computerPlayer.fullHouseCombos.Count > 0)
                            combo = computerPlayer.fullHouseCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.fullHouseCombos.Count - maxAmountDecrement))];
                        if (combo != null && combo.combinedValue > computerPlayer.manager.lastValue)
                            computerPlayer.playCards = combo.combination;
                    }
                    break;
                case PlayKind.STRAIGHT:
                    {
                        CardCombination combo = null;
                        if (computerPlayer.straightCombos.Count > 0)
                            combo = computerPlayer.straightCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.straightCombos.Count - maxAmountDecrement))];
                        if (combo != null && combo.combination.Count == computerPlayer.manager.lastPlay.Count && combo.combinedValue > computerPlayer.manager.lastValue)
                            computerPlayer.playCards = combo.combination;
                    }
                    break;
                case PlayKind.BOMB:
                    {
                        CardCombination combo = null;
                        if (computerPlayer.bombCombos.Count > 0)
                            combo = computerPlayer.bombCombos[UnityEngine.Random.Range(0, Mathf.Max(0, computerPlayer.bombCombos.Count - maxAmountDecrement))];
                        if (combo != null && combo.combinedValue > computerPlayer.manager.lastValue)
                            computerPlayer.playCards = combo.combination;
                        else
                            kind = computerPlayer.manager.lastPlayKind;
                    }
                    break;
            }

            if (computerPlayer.playCards != null && computerPlayer.playCards.Count > 0)
            {
                if (wishCards.Count != 0 && CombinationHandler.CanPlayWishCard(wishCards, kind, computerPlayer.manager.lastValue, computerPlayer.manager.lastPlayKind, computerPlayer.orderedHand, computerPlayer.manager.lastPlay.Count, computerPlayer.hand, lowVal == 255 ? 0 : lowVal, highval == 255 ? 0 : highval))
                {
                    bool containsWishCard = false;

                    foreach (Card c in computerPlayer.playCards)
                    {
                        if (c.value == wishCards[0].value)
                        {
                            containsWishCard = true;
                        }
                    }

                    if (containsWishCard)
                        break;
                }
                else
                    break;
            }
        }
    }

    /// <summary>
    /// Sets the AI's trade
    /// </summary>
    /// <param name="computerPlayer">The ComputerPlayer this module is attached to</param>
    /// <param name="friendId">The player ID of the AI's teammate</param>
    public override void SetupTrade(ComputerPlayer computerPlayer, int friendId)
    {
        int slotId = 0;
        for (int i = 0; i < 4; i++)
        {
            if (i != computerPlayer.playerId)
            {
                if (i == friendId)
                    computerPlayer.tradeSlots[slotId] = computerPlayer.orderedHand[computerPlayer.orderedHand.Count - 1];
                else
                    computerPlayer.tradeSlots[slotId] = computerPlayer.orderedHand[slotId];
                slotId++;
            }
        }
    }
}