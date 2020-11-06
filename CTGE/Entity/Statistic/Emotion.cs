
namespace Engine.Entity.Statistic {
    public class Emotion : StatVector {

        private BasalEntity target;

        public BasalEntity Target() {
            return target;
        }

        public void Target(ref BasalEntity target) {
            this.target = target;
        }

        public Emotion(ref BasalEntity target, double magnitude, double intensity, string name, string prune) : base(magnitude, intensity, name, prune) {
            this.target = target;
        }

        public Emotion(ref BasalEntity target, double magnitude, double intensity, string name) : base(magnitude, intensity, name) {
            this.target = target;
        }

        public Emotion(BasalEntity target, double magnitude, double intensity, string name, string prune) : base(magnitude, intensity, name, prune) {
            this.target = target;
        }

        public Emotion(BasalEntity target, double magnitude, double intensity, string name) : base(magnitude, intensity, name) {
            this.target = target;
        }

        public Emotion(double magnitude, double intensity, string name, string prune) : base(magnitude, intensity, name, prune) {
        }

        public Emotion(double magnitude, double intensity, string name) : base(magnitude, intensity, name) {
        }

    }
}
