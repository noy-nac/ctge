
namespace Engine.Entity.Statistic.Set {
    public class AttributeSet : StatSet {

        public Attribute[] Attribute() {
            return (Attribute[])stat;
        }

        public void Attribute(ref Attribute[] attribute) {
            stat = attribute;
        }

        public AttributeSet(ref Attribute[] attribute) : base(ref attribute) {
        }

    }
}
