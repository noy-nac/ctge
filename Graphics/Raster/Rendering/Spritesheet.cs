using Graphics.Coloring;

using Graphics.Raster.Text;

using Graphics.Tracking;

using Graphics.Vector.Geometry;

namespace Graphics.Raster.Rendering {
	public class Spritesheet : Image {

		private byte xcells;
		private byte ycells;

		private byte cellw;
		private byte cellh;

		public Spritesheet(byte xcells, byte ycells, byte cellw, byte cellh) : base(new Box(xcells * cellw, ycells * cellh, new Point2D())) {
			Initalize(xcells, ycells, cellw, cellh);
		}

		public void Initalize(byte xc, byte yc, byte cw, byte ch) {
			xcells = xc;
			ycells = yc;

			cellw = cw;
			cellh = ch;
		}

		public Image Get(byte cx, byte cy) {
			return Sample(cellw, cellh, new Point2D(cx * cellw, cy * cellh));
		}
	}
}
