
using System;

namespace Graphics.Vector.Geometry {
	public class Point2D : Point {

		public const byte DIMENSION = 2;

		//

		public double X {
			get { return coord[0]; }
			set { coord[0] = value; }
		}

		public double Y {
			get { return coord[1]; }
			set { coord[1] = value; }
		}

		//

		public Point2D(double x, double y) : base(x, y) { }

		public Point2D(Point2D old) : base(old.Array) { }

		public Point2D() : base(DIMENSION) { }

		//

		// to-do: use matrix to allow general n-D modification
		public void Rotate(Point2D about, double angle) {
			X = (X - about.X) * Math.Cos(angle) + (Y - about.Y) * Math.Sin(angle) + about.X;
			Y = (about.X - X) * Math.Sin(angle) + (Y - about.Y) * Math.Cos(angle) + about.Y;
		}
	}
}
