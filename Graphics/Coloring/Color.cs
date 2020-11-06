
using System;

namespace Graphics.Coloring {
	public class Color {

		// in ARGB format
		// match the standard color options in the Windows console
		public enum Sixteen : uint {
			White = 0xFFFFFFFF,
			LightGray = 0xFFC0C0C0,
			Gray = 0xFF808080,
			Black = 0xFF000000,

			Red = 0xFFFF0000,
			Green = 0xFF00FF00,
			Blue = 0xFF0000FF,

			DarkRed = 0xFF800000,
			DarkGreen = 0xFF008000,
			DarkBlue = 0xFF000080,

			Magenta = 0xFFFF00FF,
			Yellow = 0xFFFFFF00,
			Cyan = 0xFF00FFFF,

			DarkMagenta = 0xFF800080,
			DarkYellow = 0xFF808000,
			DarkCyan = 0xFF008080,
		}

		// in Color object format
		// match the standard color options in the Windows console
		public static readonly Color[] CONSOLE = {
			new Color((uint)Sixteen.White),
			new Color((uint)Sixteen.LightGray),
			new Color((uint)Sixteen.Gray),
			new Color((uint)Sixteen.Black),

			new Color((uint)Sixteen.Red),
			new Color((uint)Sixteen.Green),
			new Color((uint)Sixteen.Blue),

			new Color((uint)Sixteen.DarkRed),
			new Color((uint)Sixteen.DarkGreen),
			new Color((uint)Sixteen.DarkBlue),

			new Color((uint)Sixteen.Magenta),
			new Color((uint)Sixteen.Yellow),
			new Color((uint)Sixteen.Cyan),

			new Color((uint)Sixteen.DarkMagenta),
			new Color((uint)Sixteen.DarkYellow),
			new Color((uint)Sixteen.DarkCyan),
		};

		//

		private double alpha;
		private double red;
		private double green;
		private double blue;

		private short gamma;

		//

		public double Opacity {
            get { return alpha; }
            set { alpha = value; }
        }

        public double Red {
            get { return red; }
            set { red = value; }
        }

        public double Green {
			get { return green; }
            set { green = value; }
        }

        public double Blue {
            get { return blue; }
            set { blue = value; }
        }

		//

        public double LinearRed {
            get { return Gamma.Linearize(red, gamma); }
        }

        public double LinearGreen {
            get { return Gamma.Linearize(green, gamma); }
        }

        public double LinearBlue {
            get { return Gamma.Linearize(blue, gamma); }
        }

		//

		public double GammaRed {
			get { return Gamma.Apply(LinearRed); }
		}

		public double GammaGreen {
			get { return Gamma.Apply(LinearGreen); }
		}

		public double GammaBlue {
			get { return Gamma.Apply(LinearBlue); }
		}

		//

		public short GammaState {
            get { return gamma; }
        }

		//

        public const uint A_MASK = 0xFF000000;
        public const uint R_MASK = 0x00FF0000;
        public const uint G_MASK = 0x0000FF00;
        public const uint B_MASK = 0x000000FF;

        public const byte INT_ARGB_MAX = byte.MaxValue;
		public const byte INT_ARGB_MIN = byte.MinValue;

		public byte A {
            get { return (byte)(INT_ARGB_MAX * alpha); }
            set { alpha = value / (double)INT_ARGB_MAX; }
        }

        public byte R {
            get { return (byte)(INT_ARGB_MAX * red); }
			set { red = value / (double)INT_ARGB_MAX; }
        }

        public byte G {
            get { return (byte)(INT_ARGB_MAX * green); }
            set { green = value / (double)INT_ARGB_MAX; }
        }

        public byte B {
            get { return (byte)(INT_ARGB_MAX * blue); }
            set { blue = value / (double)INT_ARGB_MAX; }
        }

		//

        public byte LinearR {
            get { return (byte)(INT_ARGB_MAX * LinearRed); }
        }

        public byte LinearG {
            get { return (byte)(INT_ARGB_MAX * LinearGreen); }
        }

        public byte LinearB {
            get { return (byte)(INT_ARGB_MAX * LinearBlue); }
        }

		//

		public byte GammaR {
			get { return (byte)(INT_ARGB_MAX * GammaRed); }
		}

		public byte GammaG {
			get { return (byte)(INT_ARGB_MAX * GammaGreen); }
		}

		public byte GammaB {
			get { return (byte)(INT_ARGB_MAX * GammaBlue); }
		}

		//

		public uint ARGB {
            get {
                return ((uint)(alpha * A_MASK) & A_MASK)
                    | ((uint)(red * R_MASK) & R_MASK)
                    | ((uint)(green * G_MASK) & G_MASK)
                    | ((uint)(blue * B_MASK) & B_MASK);
            }
            set {
                alpha = (value & A_MASK) / (double)A_MASK;
                red = (value & R_MASK) / (double)R_MASK;
                green = (value & G_MASK) / (double)G_MASK;
                blue = (value & B_MASK) / (double)B_MASK;
            }
        }

        public uint LinearARGB {
            get {
                return ((uint)(alpha * A_MASK) & A_MASK)
					| ((uint)(LinearRed * R_MASK) & R_MASK)
					| ((uint)(LinearGreen * G_MASK) & G_MASK)
					| ((uint)(LinearBlue * B_MASK) & B_MASK);
            }
        }

		public uint GammaARGB {
			get {
				return ((uint)(alpha * A_MASK) & A_MASK)
					| ((uint)(GammaRed * R_MASK) & R_MASK)
					| ((uint)(GammaGreen * G_MASK) & G_MASK)
					| ((uint)(GammaBlue * B_MASK) & B_MASK);
			}
		}

		//
		
		// max is only upper bound (hue = 2pi maps to hue = 0)
		public const double HUE_MAX = 2 * Math.PI;
		public const double HUE_MIN = 0;

		// ensures red and bwg are not confused
		public const double HUE_BWG = HUE_MAX;

		// each increases by pi / 6
		public const double HUE_RED = 0;
		public const double HUE_ORANGE = Math.PI / 6;
		public const double HUE_YELLOW = Math.PI / 3;
		public const double HUE_LIME = Math.PI / 2;
		public const double HUE_GREEN = 2 * Math.PI / 3;
		public const double HUE_TEAL = 5 * Math.PI / 6;
		public const double HUE_CYAN = Math.PI;
		public const double HUE_AZURE = 7 * Math.PI / 6;
		public const double HUE_BLUE = 4 * Math.PI / 3;
		public const double HUE_VIOLET = 3 * Math.PI / 2;
		public const double HUE_MAGENTA = 5 * Math.PI / 3;
		public const double HUE_ROSE = 11 * Math.PI / 6;

		// direction to vector sum of color elements
		public double Hue {
            get {
				if(red == green && green == blue) {
					return HUE_BWG;
				}
				else {
					double hue = Math.Atan2(Math.Sqrt(3) * (green - blue), 2 * red - green - blue);
					return hue >= HUE_MIN ? hue : hue + HUE_MAX;	
				}
			}
        }

        // vector sum of color elements
        public double Chroma {
            get { return Math.Sqrt(red * red + green * green + blue * blue - red * green - green * blue - blue * red); }
        }

		// vector sum of color elements relative to magnitude
        public double Saturation {
            get { return Value > ARGB_MIN ? Chroma / Value : ARGB_MIN; }
        }

		// relative magnitude of color elements
		public double Value {
			get { return red + green + blue == ARGB_MIN ? ARGB_MIN : Math.Sqrt((red * red + green * green + blue * blue) / (red + green + blue)); }
		}

		// quadratic mean magnitude of color elements
		public double Brightness {
            get { return Math.Sqrt((red * red + green * green + blue * blue) / 3); }
        }

        public const double R_LUM = 0.299;
        public const double G_LUM = 0.587;
        public const double B_LUM = 0.114;

        // perception-weighted magnitude of color elements
        public double Luminance {
            get { return Math.Sqrt(R_LUM * LinearRed * LinearRed + G_LUM * LinearGreen * LinearGreen + B_LUM * LinearBlue * LinearBlue); }
        }

        public const double Y_R_LUM = 0.2126;
        public const double Y_G_LUM = 0.7152;
        public const double Y_B_LUM = 0.0722;

        // perception-weighted, gamma-compressed magnitude of color elements
        public double Luma {
            get { return Y_R_LUM * GammaRed + Y_G_LUM * GammaGreen + Y_B_LUM * GammaBlue; }
        }

		//

		public ConsoleColor Console {
			get {
				double err = double.MaxValue;
				double tmp;

				byte minid = 0;

				for(byte i = 0; i < CONSOLE.Length; i++) {
					tmp = Fusion.Error(this, CONSOLE[i]);

					if(tmp < err) {
						minid = i;
						err = tmp;
					}
				}
				switch((Sixteen)(A_MASK | CONSOLE[minid].ARGB)) {
					case Sixteen.White:
						return ConsoleColor.White;
					case Sixteen.LightGray:
						return ConsoleColor.Gray;
					case Sixteen.Gray:
						return ConsoleColor.DarkGray;
					case Sixteen.Black:
						return ConsoleColor.Black;
					case Sixteen.Red:
						return ConsoleColor.Red;
					case Sixteen.Blue:
						return ConsoleColor.Blue;
					case Sixteen.Green:
						return ConsoleColor.Green;
					case Sixteen.DarkRed:
						return ConsoleColor.DarkRed;
					case Sixteen.DarkBlue:
						return ConsoleColor.DarkBlue;
					case Sixteen.DarkGreen:
						return ConsoleColor.DarkGreen;
					case Sixteen.Magenta:
						return ConsoleColor.Magenta;
					case Sixteen.Yellow:
						return ConsoleColor.Yellow;
					case Sixteen.Cyan:
						return ConsoleColor.Cyan;
					case Sixteen.DarkMagenta:
						return ConsoleColor.DarkMagenta;
					case Sixteen.DarkYellow:
						return ConsoleColor.DarkYellow;
					case Sixteen.DarkCyan:
						return ConsoleColor.DarkCyan;
					// error
					default:
						return ConsoleColor.Green;
				}
			}
			set {
				switch(value) {
					case ConsoleColor.White:
						ARGB = (uint)Sixteen.White;
						break;
					case ConsoleColor.Gray:
						ARGB = (uint)Sixteen.LightGray;
						break;
					case ConsoleColor.DarkGray:
						ARGB = (uint)Sixteen.Gray;
						break;
					case ConsoleColor.Black:
						ARGB = (uint)Sixteen.Black;
						break;
					case ConsoleColor.Red:
						ARGB = (uint)Sixteen.Red;
						break;
					case ConsoleColor.Blue:
						ARGB = (uint)Sixteen.Blue;
						break;
					case ConsoleColor.Green:
						ARGB = (uint)Sixteen.Green;
						break;
					case ConsoleColor.DarkRed:
						ARGB = (uint)Sixteen.DarkRed;
						break;
					case ConsoleColor.DarkBlue:
						ARGB = (uint)Sixteen.DarkBlue;
						break;
					case ConsoleColor.DarkGreen:
						ARGB = (uint)Sixteen.DarkGreen;
						break;
					case ConsoleColor.Magenta:
						ARGB = (uint)Sixteen.Magenta;
						break;
					case ConsoleColor.Yellow:
						ARGB = (uint)Sixteen.Yellow;
						break;
					case ConsoleColor.Cyan:
						ARGB = (uint)Sixteen.Cyan;
						break;
					case ConsoleColor.DarkMagenta:
						ARGB = (uint)Sixteen.DarkMagenta;
						break;
					case ConsoleColor.DarkYellow:
						ARGB = (uint)Sixteen.DarkYellow;
						break;
					case ConsoleColor.DarkCyan:
						ARGB = (uint)Sixteen.DarkCyan;
						break;
					// error
					default:
						ARGB = (uint)Sixteen.Red;
						break;
				}
			}
		}

		//

		public const double ARGB_MAX = 1;
		public const double ARGB_MID = (ARGB_MAX + ARGB_MIN) / 2;
		public const double ARGB_MIN = 0;

		public const double DEFAULT_A = ARGB_MAX;
		public const double DEFAULT_RGB = ARGB_MIN;

		public const short USING_Y = Gamma.APPLIED;
		public const short NOT_USING_Y = Gamma.NEUTRAL;
		public const short DEFAULT_Y = NOT_USING_Y;

		public Color(double a, double r, double g, double b, short y) {
            Initalize(a, r, g, b, y);
        }

        public Color(double a, double r, double g, double b) {
            Initalize(a, r, g, b, DEFAULT_Y);
        }

		// does not go through initalization
		public Color(uint argb) {
			ARGB = argb;
		}

		public Color(double r, double g, double b, short y) {
			Initalize(DEFAULT_A, r, g, b, y);
		}

		public Color(double r, double g, double b) {
            Initalize(DEFAULT_A, r, g, b, DEFAULT_Y);
        }

		public Color(ConsoleColor col) {
			Console = col;
		}

		public Color(Color col) {
			Initalize(col.alpha, col.red, col.green, col.blue, col.gamma);
		}

		public Color() {
			Initalize(DEFAULT_A, DEFAULT_RGB, DEFAULT_RGB, DEFAULT_RGB, DEFAULT_Y);
		}

        public void Initalize(double a, double r, double g, double b, short y) {
            alpha = a;
            red = r;
            green = g;
            blue = b;
            gamma = y;
        }

		//

        public void Linearize() {
			red = Gamma.Linearize(red, gamma);
			green = Gamma.Linearize(green, gamma);
			blue = Gamma.Linearize(blue, gamma);

            gamma = Gamma.NEUTRAL;
        }

        public void ApplyGamma(short addy) {
            if(addy > 0) {
                for(short i = 0; i < addy; i++) {
					red = Gamma.Apply(red);
					green = Gamma.Apply(green);
					blue = Gamma.Apply(blue);
                }
                gamma += addy;
            }
            else if(addy < 0) {
                ReduceGamma(Math.Abs(addy));
            }
            else {
                return;
            }
        }

		public void ReduceGamma(short suby) {
			if(suby > 0) {
				for(short i = 0; i < suby; i++) {
					red = Gamma.Reduce(red);
					green = Gamma.Reduce(green);
					blue = Gamma.Reduce(blue);
				}
				gamma -= suby;
			}
			else if(suby < 0) {
				ApplyGamma(Math.Abs(suby));
			}
			else {
				return;
			}
		}
	}
}
