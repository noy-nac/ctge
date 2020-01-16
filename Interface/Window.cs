
//using Core;

namespace Interface {
	public class Window {

		public const ushort DEFAULT_WIDTH = 124;
		public const ushort DEFAULT_HEIGHT = 72;

		public const byte DEFAULT_FONT_WIDTH = 8;
		public const byte DEFAULT_FONT_HEIGHT = 8;

		//

		private ushort width;
		private ushort height;

		private byte fwidth;
		private byte fheight;

		//

		public Window(ushort w, ushort h, byte fw, byte fh) {
			Initalize(w, h, fw, fh);
		}

		public Window(ushort w, ushort h) {
			Initalize(w, h, DEFAULT_FONT_WIDTH, DEFAULT_FONT_HEIGHT);
		}

		public Window() {
			Initalize(DEFAULT_WIDTH, DEFAULT_HEIGHT, DEFAULT_FONT_WIDTH, DEFAULT_FONT_HEIGHT);
		}

		public void Initalize(ushort w, ushort h, byte fw, byte fh) {
			width = w;
			height = h;

			fwidth = fw;
			fheight = fh;
		}

		//

		public void Size() {
			//Terminal.Size(width, height);
		}
	}
}
