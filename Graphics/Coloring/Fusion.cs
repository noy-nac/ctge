
using System;

namespace Graphics.Coloring {
	public class Fusion {

		private Color top;
		private Color bot;

		private Color blend;

		private double pos;

		private bool usegamma;

		private Blend.Mode mode;

		//

		public Color Top {
			get { return top; }
			set { top = value; }
		}

		public Color Bottom {
			get { return bot; }
			set { bot = value; }
		}

		public Color Result {
			get { return blend; }
		}

		public double Position {
			get { return pos; }
			set { pos = value; }
		}

		public bool UseGamma {
			get { return usegamma; }
			set { usegamma = value; }
		}

		public Blend.Mode Mode {
			get { return mode; }
			set { mode = value; }
		}

		//

		public Fusion(Color top, Color bot, double pos, bool usegamma, Blend.Mode mode) {
			Initalize(top, bot, pos, usegamma, mode);
		}

		public void Initalize(Color top, Color bot, double pos, bool usegamma, Blend.Mode mode) {
			this.top = new Color(top);
			this.bot = new Color(bot);

			this.pos = pos;

			this.usegamma = usegamma;

			this.mode = mode;
		}

		//

		public Color Create() {
			return blend = Blend.Apply(top, bot, pos, usegamma, mode);
		}

		//

		public double Error(Color target) {
			return Error(target, blend);
		}

		public static double Error(Color target, Color blend) {
			/**/
			const double T = 2 * Math.PI;

			return Math.Sqrt(
				Math.Min(Math.Min((blend.Hue - target.Hue) * (blend.Hue - target.Hue),
			(blend.Hue - target.Hue + T) * (blend.Hue - target.Hue + T)),
				(blend.Hue - target.Hue - T) * (blend.Hue - target.Hue - T)) +
				(blend.Value - target.Value) * (blend.Value - target.Value) +
				(blend.Chroma - target.Chroma) * (blend.Chroma - target.Chroma));
			/**/
			/*
			return (Math.Abs(blend.Red - target.Red) + 1) *
				(Math.Abs(blend.Green - target.Green) + 1) *
				(Math.Abs(blend.Blue - target.Blue) + 1);
			*/
			/*
			return Math.Sqrt(
				(blend.Red - target.Red) * (blend.Red - target.Red) +
				(blend.Green - target.Green) * (blend.Green - target.Green) +
				(blend.Blue - target.Blue) * (blend.Blue - target.Blue));
			*/
		}

		//

		public static Fusion[] Explore(Color[] source, double[] pos, bool usegamma, Blend.Mode mode) {
			Fusion[] option = new Fusion[source.Length * source.Length * pos.Length]; // max size
			Fusion[] unique;

			ushort count = 0;

			// I wrote this a long time ago
			// this is really bad style!
			// I've got a way to make this twice as efficient, but it only works for ideal blend modes: b(u, v) = b(v, u) and b(u, u) = u
			// basically when pos = 0 OR 1, there are only L colors @ i = j; pos = 0.5, there are L(L-1)/2 colors @ i < j; else, there are L(L-1) colors @ i != j
			for(byte i = 0; i < source.Length; i++) {
				for(byte j = 0; j < source.Length; j++) {
					for(byte k = 0; k < pos.Length; k++) {
						option[count] = new Fusion(source[i], source[j], pos[k], usegamma, mode);
						option[count].Create();

						for(ushort l = 0; l < count; l++) {
							if(option[l].blend.ARGB == option[count].blend.ARGB) {
								count--;
								break;
							}
						}
						count++;
					}
				}
			}
			// b/c count is always incremented last and is 0-indexed, it is the length of the unique colors
			unique = new Fusion[count];

			for(ushort i = 0; i < unique.Length; i++) {
				unique[i] = option[i];
			}
			return unique;
		}

		public static Fusion Match(Color target, Fusion[] option) {
			ushort minid = 0;

			for(ushort i = 0; i < option.Length; i++) {
				minid = option[i].Error(target) < option[minid].Error(target) ? i : minid;
			}
			return option[minid];
		}

		//
	}
}
