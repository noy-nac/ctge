using Engine.Entity.Statistic.Set;

namespace Engine.Entity.Property {
    public class Race : Class {

        public Race(ref AttributeSet attribute, string name) : base(ref attribute, name) {

        }

        public Race(AttributeSet attribute, string name) : base(ref attribute, name) {

        }

        public Race(string name) : base(name) {

        }
    }
}
