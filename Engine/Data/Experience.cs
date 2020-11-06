using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Data {
    public class Experience {

        public const short MAXIMUM_LEVEL = 32000;
        
        private short level;

        private double xp;
        private double skill;

        public short Level {
            get { return level; }
        }

        public double XP {
            get { return xp; }
            set { xp = value; }
        }

        public double Skill {
            get { return skill; }
            set { skill = value; }
        }

        public Experience() {
            level = 0;

            xp = 0;
            skill = 0;
        }

        public void LevelUp() {
            if(xp >= XPRequired((short)(level + 1))) {
                xp -= XPRequired((short)(level + 1));

                level++;
            }
        }

        public static double LogS(double value) {
            if(value == 1 || value == -1 || value == 0) {
                return value;
            }
            else {
                return value / (Math.Abs(value) - 1) * Math.Log(Math.Abs(value));
            }
        }

        public static double XPRequired(short nextlevel) {
            if(nextlevel <= 1) {
                return 0;
            }
            else {
                double primesum = 0;

                for(short i = 2; i < nextlevel; i++) {
                    primesum += Math.Pow((i - 1.0) / (i + 1.0), i) * ((Math.Pow(i, 2) - 1.0) * Math.Log(i - 1) - (Math.Pow(i, 2) - 1.0) * Math.Log(i + 1) + 4 * i);
                }
                return primesum;
            }
        }
    }
}
