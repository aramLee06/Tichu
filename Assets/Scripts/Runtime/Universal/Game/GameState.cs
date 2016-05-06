public enum GameState
{
    ROUND_START, //Start of round. Player can decide to Grand Grand Tichu or not.
    FIRST_DEAL, //First deal. Players get 8 cards each, and can decide to Grand Tichu or not.
    SECOND_DEAL, //Second deal. Players get 6 additional cards, and trade cards with the others. They can also decide to call Tichu or not.
    TRADING, //Players pass 1 card to each player, and receive back 1 card from each player.
    TRICK, //The main part of the round. The lead places a card/combination, and the others can do so as well, or pass.
    TRICK_RESULT, //End of a trick, triggered if all players pass in a row. The trick cards will be given to the winner.
    ROUND_RESULT, //End of the round. Scores will be counted, and either the game ends, or another round will start.
    GAME_RESULT, //End of the game.
    SCOREBOARD,

    DRAGON_CARD, //Added as of 2016.03.30

    TRANSITION, //Transition, for music purposes
}