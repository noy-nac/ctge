
namespace Engine.Entity.Statistic {
    public class VitalStat : StatVector {

        public VitalStat(double magnitude, double intensity, string name, string prune) : base(magnitude, intensity, name, prune) {

        }

        public VitalStat(double magnitude, double intensity, string name) : base(magnitude, intensity, name) {

        }

    }
}
