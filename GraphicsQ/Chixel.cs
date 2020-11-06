
namespace GraphicsQ {
    public class Chixel {

        protected char glyph;

        protected Color fore;
        protected Color back;

        public char Glyph {
            get { return glyph; }
            set { glyph = value; }
        }

        public Color Foreground {
            get { return fore; }
            set { fore = value; }
        }

        public Color Background {
            get { return back; }
            set { back = value; }
        }

        public Chixel(char glyph) {
            Initzalize(glyph, Terminal.Foreground, Terminal.Background);
        }

        public Chixel(char glyph, Color fore, Color back) {
            Initzalize(glyph, fore, back);
        }

        public void Initzalize(char glyph, Color fore, Color back) {
            this.glyph = glyph;

            this.fore = fore;
            this.back = back;
        }

    }
}
