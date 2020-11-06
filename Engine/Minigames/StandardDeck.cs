using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Minigames {
    public class StandardDeck {

        public const string NO_CARD = "NC";

        public const string JOKER = "JK";

        public static readonly string[] RANK = {
            "A","2","3","4","5","6","7","8","9","X","J","Q","K"
        };

        public static readonly string[] INVERSE_RANK = {
            "K","Q","J","X","9","8","7","6","5","4","3","2","A"
        };

        public const char SPADE = '\u2660';
        public const char CLUB = '\u2663';
        public const char DIAMOND = '\u2666';
        public const char HEART = '\u2665';

        public const byte SIZE = 52;

        private string[] card;

        private byte position;

        public string NextCard {
            get {
                if(position < SIZE) {
                    position++;

                    return card[position - 1];
                }
                else {
                    return NO_CARD;
                }
            }
        }

        public string[] Cards {
            get { return card; }
        }

        public byte Position {
            get { return position; }
        }

        public StandardDeck() {
            card = new string[SIZE];

            position = 0;

            Reset();
        }

        public void Reset() {
            for(byte i = 0; i < RANK.Length; i++) {
                card[i] = RANK[i] + SPADE;
            }
            for(byte i = 0; i < RANK.Length; i++) {
                card[i + SIZE / 4] = RANK[i] + DIAMOND;
            }
            for(byte i = 0; i < INVERSE_RANK.Length; i++) {
                card[i + SIZE / 2] = INVERSE_RANK[i] + CLUB;
            }
            for(byte i = 0; i < INVERSE_RANK.Length; i++) {
                card[i + 3 * SIZE / 4] = INVERSE_RANK[i] + HEART;
            }
            position = 0;
        }

        public void Shuffle() {
            const byte RANGE = SIZE / 10;

            Random random = new Random();

            string card0;
            string card1;

            byte slot0;
            byte slot1;

            for(byte i = 0; i < SIZE + RANGE || random.Next(0, SIZE) != random.Next(0, SIZE); i++) {
                for(byte j = 0; j < SIZE / 2; j++) {
                    slot0 = (byte)(Math.Abs(j + random.Next(-RANGE, RANGE)) % (SIZE / 2));
                    slot1 = (byte)(Math.Abs(j + random.Next(-RANGE, RANGE)) % (SIZE / 2) + SIZE / 2);

                    card0 = card[slot0];
                    card1 = card[slot1];

                    card[slot0] = card1;
                    card[slot1] = card0;
                }
            }
            position = 0;
        }

    }
}
