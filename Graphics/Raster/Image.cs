
using Graphics.Coloring;

using Graphics.Raster.Text;

using Graphics.Tracking;

using Graphics.Vector.Geometry;

namespace Graphics.Raster {
	public class Image {

		protected Chixel[,] matrix;

		protected Box area;

		//

		public Chixel[,] Matrix {
			get { return matrix; }
			set { matrix = value; }
		}

		public Point2D Source {
			get { return area.LowerLeft; }
		}

		public Point2D Center {
			get { return area.Center; }
		}

		public Box Area {
			get { return area; }
		}

		public ushort Width {
			get { return (ushort)area.Width; }
			/*set {
				Chixel[,] copy = new Chixel[value, Height];

				for(ushort y = 0; y < Height; y++) {
					for(ushort x = 0; x < value; x++) {
						copy[x, y] = chixel[x, y];
					}
				}
				chixel = new Chixel[value, Height];

				for(ushort y = 0; y < Height; y++) {
					for(ushort x = 0; x < value; x++) {
						chixel[x, y] = copy[x, y];
					}
				}
			}*/
		}

		public ushort Height {
			get { return (ushort)area.Height; }
			/*set {
				Chixel[,] copy = new Chixel[Width, value];

				for(ushort y = 0; y < value; y++) {
					for(ushort x = 0; x < Width; x++) {
						copy[x, y] = chixel[x, y];
					}
				}
				chixel = new Chixel[Width, value];

				for(ushort y = 0; y < value; y++) {
					for(ushort x = 0; x < Width; x++) {
						chixel[x, y] = copy[x, y];
					}
				}
			}*/
		}

		//

		public Chixel this[ushort x, ushort y] {
			get { return matrix[x, y]; }
			set { matrix[x, y] = value; }
		}

		//

		public Image(Box area) {
			Initalize(area);
		}

		public Image(ushort w, ushort h, Point2D src) {
			Initalize(new Box(w, h, src));
		}

		public void Initalize(Box area) {
			matrix = new Chixel[(ushort)area.Width, (ushort)area.Height];

			for(ushort y = 0; y < area.Height; y++) {
				for(ushort x = 0; x < area.Width; x++) {
					matrix[x, y] = new Chixel();
				}
			}
			this.area = new Box(area);
		}

		//

		public void Apply(Image img, double pos, bool usegamma, Blend.Mode mode) {
			for(ushort y = 0; y < Height; y++) {
				for(ushort x = 0; x < Width; x++) {
					matrix[x, y] = new Chixel(Blend.Apply(img[x, y].MeshColor, matrix[x, y].MeshColor,
						pos, usegamma, mode));
				}
			}
		}

		//

		public Image Sample(ushort w, ushort h, Point2D src) {
			return Sample(new Box(w, h, src));
		}

		public Image Sample(Box suba) {
			Image sub = new Image(suba);

			for(ushort y = 0; y < sub.Height; y++) {
				for(ushort x = 0; x < sub.Width; x++) {
					sub[x, y] = matrix[(ushort)(sub.Width + x), (ushort)(sub.Height + y)];
				}
			}
			return sub;
		}

	}
}
