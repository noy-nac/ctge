using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Entities;
using Engine.Data;

namespace Engine.Events {
    public class Battle {

        private Character aggressor;
        private Character defender;

        private short length;

        private double aggressordamage;
        private double defenderdamage;

        private bool aggressorwin;
        private bool defenderwin;

        public Character Aggressor {
            get { return aggressor; }
        }

        public Character Defender {
            get { return defender; }
        }

        public short Length {
            get { return length; }
        }

        public double AggressorDamageOutput {
            get { return aggressordamage; }
        }

        public double DefenderDamageOutput {
            get { return defenderdamage; }
        }

        public bool AggressorWin {
            get { return aggressorwin; }
        }

        public bool DefenderWin {
            get { return defenderwin; }
        }

        public Battle(Character aggressor, Character defender) {
            this.aggressor = aggressor;
            this.defender = defender;

            length = 0;

            aggressordamage = 0;
            defenderdamage = 0;

            aggressorwin = false;
            defenderwin = false;
        }

        public void Begin() {

        }

        public double AggressorXPGain() {
            double difference = Experience.XPRequired((short)(Experience.LogS(defender.Experience.Level) - Experience.LogS(aggressor.Experience.Level)));
            double numerator = Math.Pow(aggressor.Experience.Level, Experience.LogS(Experience.LogS(length))) + Math.Pow(defender.Experience.Level, Experience.LogS(length));
            double denominator = Math.Pow(defender.Experience.Level, Experience.LogS(Experience.LogS(length))) + Math.Pow(aggressor.Experience.Level, Experience.LogS(length));
            double damageratio = aggressordamage / (aggressordamage + defenderdamage);

            return Experience.LogS(length) * (numerator / denominator + damageratio) + difference;
        }

        public double DefenderXPGain() {
            double difference = Experience.XPRequired((short)(Experience.LogS(aggressor.Experience.Level) - Experience.LogS(defender.Experience.Level)));
            double numerator = Math.Pow(defender.Experience.Level, Experience.LogS(Experience.LogS(length))) + Math.Pow(aggressor.Experience.Level, Experience.LogS(length));
            double denominator = Math.Pow(aggressor.Experience.Level, Experience.LogS(Experience.LogS(length))) + Math.Pow(defender.Experience.Level, Experience.LogS(length));
            double damageratio = defenderdamage / (aggressordamage + defenderdamage);

            return Experience.LogS(length) * (numerator / denominator + damageratio) + difference;
        }

        public double AggressorSkillGain() {
            if(aggressorwin) {
                return aggressor.Experience.Skill * AggressorLoseProbability() / (1 + Math.Exp((defender.Experience.Skill - aggressor.Experience.Skill) / Math.E));
            }
            else if(defenderwin) {
                return -1 * (aggressor.Experience.Skill * AggressorWinProbability() / (1 + Math.Exp((aggressor.Experience.Skill - defender.Experience.Skill) / Math.E)));
            }
            else {
                return 0;
            }
        }

        public double DefenderSkillGain() {
            if(defenderwin) {
                return defender.Experience.Skill * DefenderLoseProbability() / (1 + Math.Exp((aggressor.Experience.Skill - defender.Experience.Skill) / Math.E));
            }
            else if(aggressorwin) {
                return -1 * (defender.Experience.Skill * DefenderWinProbability() / (1 + Math.Exp((defender.Experience.Skill - aggressor.Experience.Skill) / Math.E)));
            }
            else {
                return 0;
            }
        }

        public double AggressorWinProbability() {
            return aggressor.Experience.Skill / (aggressor.Experience.Skill + defender.Experience.Skill);
        }

        public double DefenderWinProbability() {
            return defender.Experience.Skill / (aggressor.Experience.Skill + defender.Experience.Skill);
        }

        public double AggressorLoseProbability() {
            return 1 - AggressorWinProbability();
        }

        public double DefenderLoseProbability() {
            return 1 - DefenderWinProbability();
        }
    }
}
