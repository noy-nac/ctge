using System;

namespace Engine.Entity.Statistic.Set {
    public class EmotionRing : StatSet {

        public Emotion[] Emotion() {
            return (Emotion[])stat;
        }

        public void Emotion(ref Emotion[] emotion) {
            stat = emotion;
        }

        public EmotionRing(ref Emotion[] emotion) : base(ref emotion) {
        }

        public EmotionRing(ref Emotion[] emotion, ref BasalEntity target) : base(ref emotion) {
            for(byte i = 0; i < stat.Length; i++) {
                emotion[i].Target(ref target);
            }
        }

        public void Exert(double magnitude, ushort focus, byte target) {
            for(byte i = 0; i < stat.Length; i++) {
                stat[i].Magnitude += magnitude * Math.Pow(Math.Cos(2 * Math.PI * (i - target) / stat.Length), 2 * focus + 1);
            }
        }

        public void Exert(StatVector vector, byte target) {
            Exert(vector.Magnitude, (ushort)vector.Intensity, target);
        }

        public void Exert(double magnitude, ushort focus, ref Emotion target) {
            for(byte i = 0; i < stat.Length; i++) {
                if(target == Emotion()[i]) {
                    Exert(magnitude, focus, i);
                }
            }
        }

        public void Exert(StatVector vector, ref Emotion target) {
            Exert(vector.Magnitude, (ushort)vector.Intensity, ref target);
        }
    }
}
