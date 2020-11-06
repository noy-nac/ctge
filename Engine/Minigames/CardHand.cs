using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Minigames {
    public class CardHand {

        private string[] hand;

        private byte size;

        public string[] Hand {
            get { return hand; }
            set { hand = value; }
        }

        public byte Size {
            get { return size; }
            set {
                Allocate((byte)(value - size));

                size = value;
            }
        }

        public CardHand(byte size) {
            hand = new string[size];

            this.size = size;
        }

        public void Allocate(byte cards) {
            string[] newhand = new string[hand.Length + cards];

            for(byte i = 0; i < hand.Length; i++) {
               newhand[i] = hand[i];
            }
            hand = new string[newhand.Length];

            for(byte i = 0; i < (newhand.Length < hand.Length ? newhand.Length : hand.Length); i++) {
                hand[i] = newhand[i];
            }
        }
    }
}
