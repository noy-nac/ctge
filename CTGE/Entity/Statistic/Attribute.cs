
namespace Engine.Entity.Statistic {
    public class Attribute : StatVector {

        public Attribute(double magnitude, double intensity, string name, string prune) : base(magnitude, intensity, name, prune) {
        }

        public Attribute(double magnitude, double intensity, string name) : base(magnitude, intensity, name) {
        }

    }
}
