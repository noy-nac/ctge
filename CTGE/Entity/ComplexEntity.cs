
namespace Engine.Entity {
    public class ComplexEntity : BasalEntity {

        protected string name;

        protected double age;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public double Age {
            get { return age; }
            set { age = value; }
        }

        public ComplexEntity(string name, double age, ulong id) : base(id) {
            this.name = name;

            this.age = age;
        }

    }
}
