
using Graphics.Vector.Geometry;
using Graphics.Vector.Geometry.Enclosed;

namespace Graphics.Tracking {
	// does not extent Recangle b/c Box has *limited* functionality
	public class Box {

		private Rectangle box;

		//

		public Rectangle Rectangle {
			get { return box; }
		}
		
		//

		public double Width {
			get { return box.Width; }
		}

		public double Height {
			get { return box.Height; }
		}

		//

		public Point2D UpperRight {
			get { return box.UpperRight; }
		}

		public Point2D UpperLeft {
			get { return box.UpperLeft; }
		}

		public Point2D LowerRight {
			get { return box.LowerRight; }
		}

		public Point2D LowerLeft {
			get { return box.LowerLeft; }
		}

		//

		public Point2D Source {
			get { return box.Source; }
		}

		public Point2D Center {
			get { return box.Center; }
		}

		//

		public Box(double w, double h, Point2D src) {
			Initalize(new Rectangle(w, h, src));
		}

		public Box(Point2D center, double w, double h) {
			Initalize(new Rectangle(center, w, h));
		}

		public Box(Point2D topr, Point2D botl) {
			Initalize(new Rectangle(topr, botl));
		}

		public Box(Box old) {
			Initalize(new Rectangle(old.Rectangle));
		}

		public void Initalize(Rectangle box) {
			this.box = new Rectangle(box);
		}

	}
}
