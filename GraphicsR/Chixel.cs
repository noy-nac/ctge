using System;

namespace GraphicsR {
    public class Chixel {

        public const char DefaultGlyph = '\0';

        public static readonly Color DefaultFore = new Color(0xFFFFFFFF);
        public static readonly Color DefaultBack = new Color(0xFF000000);

        protected char glyph;

        protected Color fore;
        protected Color back;

        public char Glyph {
            get { return glyph; }
            set { glyph = value; }
        }

        public Color Fore {
            get { return fore; }
            set { fore = value; }
        }

        public Color Back {
            get { return back; }
            set { back = value; }
        }     

        public Chixel() {
            glyph = DefaultGlyph;

            fore = DefaultFore;
            back = DefaultBack;
        }

        public Chixel(char glyph) {
            this.glyph = glyph;

            fore = DefaultFore;
            back = DefaultBack;
        }

        public Chixel(char glyph, Color fore, Color back) {
            this.glyph = glyph;

            this.fore = fore;
            this.back = back;
        }

        public void Print() {
            Console.ForegroundColor = fore.ConsoleColor;
            Console.BackgroundColor = back.ConsoleColor;

            Console.Write(glyph);
        }
    }
}
