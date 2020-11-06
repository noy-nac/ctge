
namespace Engine.Entity.Statistic.Set {
    public class VitalSigns : StatSet {

        public VitalStat[] VitalStat() {
            return (VitalStat[])stat;
        }

        public void VitalStat(ref VitalStat[] vital) {
            stat = vital;
        }

        public VitalSigns(ref VitalStat[] vital) : base(ref vital) {

        }

        public VitalSigns(VitalStat[] vital) : base(ref vital) {

        }

    }
}
