using Engine.Entity.Statistic.Set;

namespace Engine.Entity.Property {
    public class Suit : Class {

        public Suit(ref AttributeSet attribute, string name) : base(ref attribute, name) {

        }

        public Suit(AttributeSet attribute, string name) : base(ref attribute, name) {

        }

        public Suit(string name) : base(name) {

        }

    }
}
