
using System;

namespace GraphicsQ {
    public class Color {

        protected Vector argb;

        public Vector ARGB {
            get { return argb; }
            set { argb = value; }
        }

        public double Alpha {
            get { return argb.Element(0); }
            set { argb.Element(value,0); }
        }

        public double Red {
            get { return argb.Element(1); }
            set { argb.Element(value, 1); }
        }

        public double Green {
            get { return argb.Element(2); }
            set { argb.Element(value, 2); }
        }

        public double Blue {
            get { return argb.Element(3); }
            set { argb.Element(value, 3); }
        }

        public Color(double a, double r, double g, double b) {
            argb = new Vector(a, r, g, b);
        }

        public void Mix(Color color, double scale) {
            argb = Mix(this, color, scale).ARGB;
        }

        public void Decompose(Color color, double scale) {
            argb = Decompose(this, color, scale).ARGB;
        }

        public static Color Mix(Color left, Color right, double scale) {
            return new Color(
                MixAlpha(left.Alpha, right.Alpha, scale),
                MixChannel(left.Red, right.Red, scale),
                MixChannel(left.Green,right.Green, scale),
                MixChannel(left.Blue, right.Blue, scale)
            );
        }

        public static Color Decompose(Color mix, Color left, double scale) {
            return new Color(
                DecomposeAlpha(mix.Alpha, left.Alpha, scale),
                DecomposeChannel(mix.Red, left.Red, scale),
                DecomposeChannel(mix.Green, left.Green, scale),
                DecomposeChannel(mix.Blue, left.Blue, scale)
            );
        }

        public static double MixAlpha(double left, double right, double scale) {
            double alpha = 2 * scale * scale * (2 * left * right - left - right) + scale * (left + 3 * right - 4 * left * right) + left;
            return alpha <= 1 ? alpha : 1;
        }

        public static double DecomposeAlpha(double mix, double left, double scale) {
            return (2 * scale * scale * left - scale * left + mix - left) / (2 * scale * scale * (2 * left - 1) + scale * (3 - 4 * left));
        }

        public static double MixChannel(double left, double right, double scale) {
            return Math.Sqrt((1 - scale) * left * left + scale * right * right);
        }

        public static double DecomposeChannel(double mix, double left, double scale) {
            return Math.Sqrt((mix * mix - (1 - scale) * left * left) / scale);
        }

        public static double Similarity(Color left, Color right) {
            return 1 - (
                (left.Alpha * left.Red - right.Alpha * right.Red) * (left.Alpha * left.Red - right.Alpha * right.Red) +
                (left.Alpha * left.Green - right.Alpha * right.Green) * (left.Alpha * left.Green - right.Alpha * right.Green) +
                (left.Alpha * left.Blue - right.Alpha * right.Blue) * (left.Alpha * left.Blue - right.Alpha * right.Blue)
            ) / 3;
        }
    }
}
