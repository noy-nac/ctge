
namespace Engine.Entity.Statistic.Set {
    public class StatSet {

        protected StatVector[] stat;

        public StatVector[] Stat() {
            return stat;
        }

        public void Stat(ref StatVector[] stat) {
            this.stat = stat;
        }

        public StatSet(ref StatVector[] stat) {
            this.stat = stat;
        }

        public StatSet(ref Emotion[] emotion) {
            stat = emotion;
        }

        public StatSet(ref Attribute[] attribute) {
            stat = attribute;
        }

        public StatSet(ref VitalStat[] vital) {
            stat = vital;
        }

        public StatSet(ref PersonalityAspect[] aspect) {
            stat = aspect;
        }
    }
}
