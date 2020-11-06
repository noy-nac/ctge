using System;

namespace GraphicsR {
    public class Color {

        public enum BlendMode : byte {
            Normal,
            Overwrite,
            Mix,

            Darker,
            Lighter,

            DarkerOnly,
            LighterOnly,
            Difference,

            Add,
            Subtract,
            Multiply,
            Divide,
            Modulo,

            Screen,
            Veil,

            Overlay,
            Exclusion,

            ColorBurn,
            LinearBurn,
            ColorDodge,

            SoftLight,
            HardLight,
            VividLight,
            LinearLight,
            PinLight,

            Hue,
            Saturation,
            Color,
            Luminosity,

            AND,
            NAND,
            OR,
            NOR,
            XOR,
            XNOR,

            ContraharmonicMean,
            CubicMean,
            QuadraticMean,
            CentroidalMean,
            LogarithmicMean,
            ArithmeticMean,
            HeronianMean,
            GeometricMean,
            HarmonicMean,
        }

        protected byte alpha;
        protected byte red;
        protected byte green;
        protected byte blue;

        public byte Alpha {
            get { return alpha; }
            set { alpha = value; }
        }

        public byte Red {
            get { return red; }
            set { red = value; }
        }

        public byte Green {
            get { return green; }
            set { green = value; }
        }

        public byte Blue {
            get { return blue; }
            set { blue = value; }
        }

        public uint ARGB {
            get {
                return ((uint)alpha << 24) | ((uint)red << 16) | ((uint)green << 8) | blue;
            }
            set {
                alpha = (byte)((value >> 24) & 0xFF);
                red = (byte)((value >> 16) & 0xFF);
                green = (byte)((value >> 8) & 0xFF);
                blue = (byte)(value & 0xFF);
            }
        }

        public double Hue {
            get {
                if(red == green && green == blue) {
                    return -1;
                }
                else {
                    double d = Math.Atan2(Math.Sqrt(3) * ((green - blue)/255.0), (2 * red - green - blue)/255.0);
                    return d >= 0 ? d : d + Math.PI;
                }
            }
        }

        public double Chroma {
            get {
                return Math.Sqrt((red * red + green * green + blue * blue - red * green - green * blue - blue * red) / (255.0 * 255.0));
            }
        }

        public double Value {
            get {
                if(red == green && green == red && red == 0) return 0;

                return Math.Sqrt((red * red + green * green + blue * blue) / (red + green + blue));
            }
        }

        public byte Luminance {
            get {
                return (byte)Math.Round(Math.Sqrt(0.299 * red * red + 0.587 * green * green + 0.114 * blue * blue));
            }
        }

        public double LuminanceL {
            get {
                return Math.Sqrt(0.299 * red * red + 0.587 * green * green + 0.114 * blue * blue);
            }
        }

        public byte RedLuminance {
            get {
                if(red != 0) {
                    return (byte)((0.547 * red * red) / (red + green + blue));

                }
                else {
                    return 0;
                }
            }
        }

        public byte GreenLuminance {
            get {
                if(green != 0) {
                    return (byte)((0.766 * green * green) / (red + green + blue));
                }
                else {
                    return 0;
                }
            }
        }

        public byte BlueLuminance {
            get {
                if(blue != 0) {
                    return (byte)((0.338 * blue * blue) / (red + green + blue));
                }
                else {
                    return 0;
                }
            }
        }

        public ConsoleColor ConsoleColor {
            get {
                switch(ARGB) {
                    case 0xFF000000:
                        return ConsoleColor.Black;
                    case 0xFFFFFFFF:
                        return ConsoleColor.White;
                    case 0xFF800000:
                        return ConsoleColor.DarkRed;
                    case 0xFFFF0000:
                        return ConsoleColor.Red;
                    case 0xFF008000:
                        return ConsoleColor.DarkGreen;
                    case 0xFF00FF00:
                        return ConsoleColor.Green;
                    case 0xFF000080:
                        return ConsoleColor.DarkBlue;
                    case 0xFF0000FF:
                        return ConsoleColor.Blue;
                    case 0xFF808080:
                        return ConsoleColor.DarkGray;
                    case 0xFFC0C0C0:
                        return ConsoleColor.Gray;
                    case 0xFF808000:
                        return ConsoleColor.DarkYellow;
                    case 0xFFFFFF00:
                        return ConsoleColor.Yellow;
                    case 0xFF800080:
                        return ConsoleColor.DarkMagenta;
                    case 0xFFFF00FF:
                        return ConsoleColor.Magenta;
                    case 0xFF008080:
                        return ConsoleColor.DarkCyan;
                    case 0xFF00FFFF:
                        return ConsoleColor.Cyan;
                    default:
                        return ConsoleColor.Black;
                }
            }
        }

        public Color() {
            alpha = 0xFF;
            red = 0xFF;
            green = 0xFF;
            blue = 0xFF;
        }

        public Color(uint argb) {
            ARGB = argb;
        }

        public Color(byte red, byte green, byte blue) {
            alpha = 0xFF;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public Color(byte alpha, byte red, byte green, byte blue) {
            this.alpha = alpha;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public void Blend(Color color, double weight, BlendMode mode) {
            ARGB = Blend(this, color, 1 - weight, weight, mode).ARGB;
        }

        public static Color Blend(Color left, Color right, double lweight, double rweight, BlendMode mode) {
            Color color = new Color();

            /*color.alpha = MixAlphaChannels(left.alpha, right.alpha, lweight, rweight);
            color.red = BlendChannels(left.red, right.red, lweight, rweight, mode);
            color.green = BlendChannels(left.green, right.green, lweight, rweight, mode);
            color.blue = BlendChannels(left.blue, right.blue, lweight, rweight, mode);*/

            color.alpha = MixAlphaChannels(left.alpha, right.alpha, lweight, rweight);
            color.red = BlendChannels(left.red, right.red, lweight, rweight, mode);
            color.green = BlendChannels(left.green, right.green, lweight, rweight, mode);
            color.blue = BlendChannels(left.blue, right.blue, lweight, rweight, mode);

            return color;
        }

        public static byte BlendChannels(byte left, byte right, double lweight, double rweight, BlendMode mode) {
            return (byte)((lweight * left + rweight*right) / (lweight+rweight));
            //if(left + right < 512)
            //return (byte)Math.Round(Math.Sqrt((lweight * left * left + rweight * right * right) / (lweight + rweight)));

            //return (byte)(255 - Math.Round(Math.Sqrt( (255 * 255 - left * left * (lweight / (lweight + rweight)) * (lweight / (lweight + rweight)))) +  (255 * 255 - right * right * (rweight / (lweight + rweight)) * (rweight / (lweight + rweight)))));
            //else
            //return (byte)(255 - Math.Round(Math.Sqrt((255 * 255 - left * left) * (lweight / (lweight + rweight)) + (255 * 255 - right * right) * (rweight / (lweight + rweight)))));
        }

        public static byte MixAlphaChannels(byte left, byte right, double lweight, double rweight) {
            return (byte)Math.Round((lweight * left + rweight * 0xFF * right - lweight * rweight * left * right) / (lweight + rweight));
        }
    }
}
