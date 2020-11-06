using Engine.Entity.Statistic.Set;

namespace Engine.Entity.Property {
    public class Class {

        protected AttributeSet attribute;

        protected string name;

        public AttributeSet AttributeSet() {
            return attribute;
        }

        public void AttributeSet(ref AttributeSet attribute) {
            this.attribute = attribute;
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public Class(ref AttributeSet attribute, string name) {
            this.attribute = attribute;

            this.name = name;
        }

        public Class(AttributeSet attribute, string name) {
            this.attribute = attribute;

            this.name = name;
        }

        public Class(string name) {
            this.name = name;
        }

    }
}
