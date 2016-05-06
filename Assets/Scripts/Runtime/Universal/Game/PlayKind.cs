public enum PlayKind
{
    NONE, //Also known as "There is no last play" or "You're the lead".
    SINGLE_PLAY, //A single card is being played. Like ♦4 or ♣A.
    ONE_PAIR, //A pair is being played. Like ♦5♣5 or ♥K♠K.
    STAIRS, //Multiple pairs are being played. Like ♥3♦3♣4♥4 or ♦2♠2♦3♠3♣4♠4 etc. NOTE: Only consecutive pairs! So NOT: ♦4♣4♥7♠7 !
    THREE_OF_A_KIND, //Like ♦5♣5♠5 or ♥A♦A♣A.
    STRAIGHT, //5 or more consecutive cards. Like ♥2♦3♠4♣5♠6 or ♣4♠5♣6♦7♦8♥9♠10♦J.
    FULL_HOUSE, //3 of a kind + 2 pair. Like ♥3♣3♠3♥5♦5 or ♥6♣6♠6♣A♥A.
    BOMB, //4 of a kind OR straight flush. Like ♥4♦4♣4♠4 or ♥2♥3♥4♥5♥6♥7.
}