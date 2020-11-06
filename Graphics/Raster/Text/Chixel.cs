
using Graphics.Coloring;

namespace Graphics.Raster.Text {
	public class Chixel {

		//public static readonly Color DEFAULT_FG = new Color((uint)0); // new Color(Color.ARGB_MIN, Color.ARGB_MAX, Color.ARGB_MAX, Color.ARGB_MAX);
		//public static readonly Color DEFAULT_BG = new Color((uint)0); // new Color(Color.ARGB_MIN, Color.ARGB_MIN, Color.ARGB_MIN, Color.ARGB_MIN);

		public static readonly Color DEFAULT_FG = new Color(Color.ARGB_MIN, Color.ARGB_MAX, Color.ARGB_MAX, Color.ARGB_MAX);
		public static readonly Color DEFAULT_BG = new Color(Color.ARGB_MIN, Color.ARGB_MIN, Color.ARGB_MIN, Color.ARGB_MIN);

		public const char NULL_GLYPH = '\0';
		public const char ERROR_GLYPH = '!';
		public const char EMPTY_GLYPH = ' ';

		public const char DEFAULT_GLYPH = NULL_GLYPH;

		//

		protected Color fore;
		protected Color back;

		protected char glyph;

		//

		public Color Fore {
			get { return fore; }
			set { fore = value; }
		}

		public Color Back {
			get { return back; }
			set { back = value; }
		}

		public char Glyph {
			get { return glyph; }
			set { glyph = value; }
		}

		//

		public static readonly Fusion[] COLOR_OPTION = Fusion.Explore(Color.CONSOLE,
			new double[] { NO_S_POS, QTR_S_POS, HALF_S_POS }, Blend.DEFAULT_USE_GAMMA, CHAR_COLOR_MODE);

		public const char NO_SHADE = ' ';
		public const char QTR_SHADE = '░';
		public const char HALF_SHADE = '▒';
		public const char ERROR_SHADE = ERROR_GLYPH;

		// 0 - - - - - - - - - 1
		// FG  25%  50%  75%  BG
		public const double NO_S_POS = Blend.POS_MAX;
		public const double QTR_S_POS = (NO_S_POS + HALF_S_POS) / 2;
		public const double HALF_S_POS = Blend.POS_MID;
		public const double ERROR_S_POS = Blend.POS_MIN;

		public const Blend.Mode CHAR_COLOR_MODE = Blend.Mode.Mesh;

		public Color MeshColor {
			get {
				Fusion col = new Fusion(new Color(fore), new Color(back),
					(glyph == NO_SHADE ? NO_S_POS : (glyph == QTR_SHADE ? QTR_S_POS :
					(glyph == HALF_SHADE ? HALF_S_POS : ERROR_S_POS))),
					Blend.DEFAULT_USE_GAMMA, CHAR_COLOR_MODE);

				return new Color(col.Create());
			}
			set {
				Fusion setting = Fusion.Match(new Color(Color.A_MASK | value.ARGB), COLOR_OPTION);

				fore = new Color(setting.Top.ARGB);
				back = new Color(setting.Bottom.ARGB);

				// maintain opacity
				fore.Opacity = value.Opacity;
				back.Opacity = value.Opacity;

				glyph = (setting.Position == NO_S_POS ? NO_SHADE
					: (setting.Position == QTR_S_POS ? QTR_SHADE
					: (setting.Position == HALF_S_POS ? HALF_SHADE : ERROR_SHADE)));
			}
		}

		//

		public Chixel(char glyph, Color fore, Color back) {
			Initalize(glyph, fore, back);
		}

		public Chixel(Color fore, Color back) {
			Initalize(DEFAULT_GLYPH, fore, back);
		}

		public Chixel(char glyph) {
			Initalize(glyph, DEFAULT_FG, DEFAULT_BG);
		}

		public Chixel(Color target) {
			MeshColor = new Color(target);
		}

		public Chixel(Chixel old) {
			Initalize(old.Glyph, old.Fore, old.Back);
		}

		public Chixel() {
			Initalize(DEFAULT_GLYPH, DEFAULT_FG, DEFAULT_BG);
		}

		public void Initalize(char glyph, Color fore, Color back) {
			this.glyph = glyph;

			this.fore = new Color(fore);
			this.back = new Color(back);
		}
	}
}
