
using System;

namespace Graphics.Coloring {
	public static class Gamma {

		public const short NEUTRAL = 0;
		public const short APPLIED = NEUTRAL + 1;
		public const short INVERSE = NEUTRAL - 1;

		public static double Linearize(double element, short gamma) {
			if(gamma > NEUTRAL) {
				for(short i = NEUTRAL; i < gamma; i++) {
					element = Reduce(element);
				}
				return element;
			}
			else if(gamma < NEUTRAL) {
				for(short i = gamma; i < NEUTRAL; i++) {
					element = Apply(element);
				}
				return element;
			}
			else {
				return element;
			}
		}

		public const double APP_MIN = 0.0031308;
		public const double INV_MIN = 0.04045;

		public const double APP_EXP = 1 / INV_EXP;
		public const double INV_EXP = 2.4;

		public const double REG_SCALE = 1.055;
		public const double REG_OFFSET = REG_SCALE - 1;

		public const double ALT_SCALE = 12.92;

		public static double Apply(double element) {
			return element >= APP_MIN
				? REG_SCALE * Math.Pow(element, APP_EXP) - REG_OFFSET
				: ALT_SCALE * element;
		}

		public static double Reduce(double element) {
			return element >= INV_MIN
				? Math.Pow((element + REG_OFFSET) / REG_SCALE, INV_EXP)
				: element / ALT_SCALE;
		}
	}
}
