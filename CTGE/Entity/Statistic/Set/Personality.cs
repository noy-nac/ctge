
namespace Engine.Entity.Statistic.Set {
    public class Personality : StatSet {

        public PersonalityAspect[] Aspect() {
            return (PersonalityAspect[])stat;
        }

        public void Aspect(ref PersonalityAspect[] aspect) {
            stat = aspect;
        }

        public Personality(ref PersonalityAspect[] aspect) : base(ref aspect) {

        }

        public Personality(PersonalityAspect[] aspect) : base(ref aspect) {

        }

    }
}
