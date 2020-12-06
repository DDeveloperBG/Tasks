let result = (function(){
    let validFaces = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
    let validSuits = ['♠', '♥', '♦', '♣'];
    let Suits = {
        SPADES: '♠',
        HEARTS: '♥',
        DIAMONDS: '♦',
        CLUBS: '♣',
    };
 
    class Card {
        constructor(face, suit) {
            this.face = face;
            this.suit = suit;
        }
 
        get face() {
            return this._face;
        }
 
        set face(face) {
            if (!validFaces.includes(face)) {
                throw new Error('Invalid Card Face!');
            }
            this._face = face;
            return;
        }
 
        get suit() {
            return this._suit;
        }
 
        set suit(suit) {
            if (!validSuits.includes(suit)) {
                throw new Error('Invalid Card Suit!');
            }
            this._suit = suit;
            return;
        }
    }
 
    return {
        Suits: Suits,
        Card: Card
    }
}());

function test() {
    let Card = result.Card;
    let Suits = result.Suits;

    let card = new Card('Q', Suits.CLUBS);
    card.face = 'A';
    card.suit = 5;
}

test();