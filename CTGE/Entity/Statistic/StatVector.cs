
namespace Engine.Entity.Statistic {
    public class StatVector {

        protected string name;
        protected string prune;

        protected double magnitude;
        protected double intensity;

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public string Prune {
            get { return prune; }
            set { prune = value; }
        }

        public double Magnitude {
            get { return magnitude; }
            set { magnitude = value; }
        }

        public double Intensity {
            get { return intensity; }
            set { intensity = value; }
        }

        public StatVector(double magnitude, double intensity, string name, string prune) {
            this.magnitude = magnitude;
            this.intensity = intensity;

            this.name = name;
            this.prune = prune;
        }

        public StatVector(double magnitude, double intensity, string name) {
            this.magnitude = magnitude;
            this.intensity = intensity;

            this.name = name;
            prune = string.Empty;
        }
    }

}
