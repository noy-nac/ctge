using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Minigames {
    public class Poker {

        private StandardDeck deck;

        private CardHand[] hand;

        private byte round;

        public Poker(byte players) {
            deck = new StandardDeck();

            CardHand[] hand = new CardHand[players];
        }

        public void BeginGame() {
            deck.Shuffle();

            round = 0;
        }

        public void TexasHoldemRound() {
            
        }

        public void FiveCardDrawRound() {

        }

        public void SevenCardStudRound() {

        }
    }
}
