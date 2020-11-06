
namespace Graphics.Vector.Geometry.Enclosed {
	public class Rectangle : Shape2D {

		// b/c width > 0 and height > 0
		public Point2D Source {
			get { return LowerLeft; }
		}

		//

		public Rectangle(double w, double h, Point2D src)
			: base(new Point2D(src.X, src.Y),
				new Point2D(src.X + w, src.Y),
				new Point2D(src.X + w, src.Y + h),
				new Point2D(src.X, src.Y + h)) {
		}

		public Rectangle(Point2D center, double w, double h)
			: base(new Point2D(center.X + w / 2, center.Y + h / 2),
				  new Point2D(center.X + w / 2, center.Y - h / 2),
				  new Point2D(center.X - w / 2, center.Y - h / 2),
				  new Point2D(center.X - w / 2, center.Y + h / 2)) {
		}

		public Rectangle(Point2D topr, Point2D botl)
			: base(topr, new Point2D(botl.X, topr.Y),
				botl, new Point2D(topr.X, botl.Y)) {
		}

		public Rectangle(Rectangle old) {
			Initalize(old.Array);
		}

	}
}
