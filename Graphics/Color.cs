using S = System;

namespace CTGE.Graphics {
    public class Color {

        public const double CHANNEL_MINIMUM = 0;
        public const double CHANNEL_MAXIMUM = 1;

        public const double RED_LUMINANCE = 0.299;
        public const double GREEN_LUMINANCE = 0.587;
        public const double BLUE_LUMINANCE = 0.114;

        private double alpha;
        private double red;
        private double green;
        private double blue;

        public double Alpha {
            get { return alpha; }
            set {
                if(value > CHANNEL_MAXIMUM)
                    alpha = CHANNEL_MAXIMUM;
                else if(value < CHANNEL_MINIMUM)
                    alpha = CHANNEL_MINIMUM;
                else
                    alpha = value;
            }
        }

        public double Red {
            get { return red; }
            set {
                if(value > CHANNEL_MAXIMUM)
                    red = CHANNEL_MAXIMUM;
                else if(value < CHANNEL_MINIMUM)
                    red = CHANNEL_MINIMUM;
                else
                    red = value;
            }
        }

        public double Green {
            get { return green; }
            set {
                if(value > CHANNEL_MAXIMUM)
                    green = CHANNEL_MAXIMUM;
                else if(value < CHANNEL_MINIMUM)
                    green = CHANNEL_MINIMUM;
                else
                    green = value;
            }
        }

        public double Blue {
            get { return blue; }
            set {
                if(value > CHANNEL_MAXIMUM)
                    blue = CHANNEL_MAXIMUM;
                else if(value < CHANNEL_MINIMUM)
                    blue = CHANNEL_MINIMUM;
                else
                    blue = value;
            }
        }

        public double Luminance {
            get { return S.Math.Sqrt(RED_LUMINANCE * red * red + GREEN_LUMINANCE * green * green + BLUE_LUMINANCE * blue * blue); }
        }

        public double RedLuminance {
            get { return RED_LUMINANCE * red; }
        }

        public double GreenLuminance {
            get { return GREEN_LUMINANCE * green; }
        }

        public double BlueLuminance {
            get { return BLUE_LUMINANCE * blue; }
        }

        public Color(double red, double green, double blue) {
            Initalize(CHANNEL_MAXIMUM, red, green, blue);
        }

        public Color(double alpha, double red, double green, double blue) {
            Initalize(alpha, red, green, blue);
        }
        
        public void Initalize(double alpha, double red, double green, double blue) {
            Alpha = alpha;
            Red = red;
            Green = green;
            Blue = blue;
        }
         
    }
}
