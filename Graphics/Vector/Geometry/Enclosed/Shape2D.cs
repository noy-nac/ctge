
using System;

namespace Graphics.Vector.Geometry.Enclosed {
	public class Shape2D : Shape {

		public double Width {
			get {
				double max = double.MinValue;
				double min = double.MaxValue;

				for(byte i = 0; i < Vertices; i++) {
					max = vertex[i].TwoD.X > max ? vertex[i].TwoD.X : max;
					min = vertex[i].TwoD.X < min ? vertex[i].TwoD.X : min;
				}
				return Math.Abs(max - min);
			}
		}

		public double Height {
			get {
				double max = double.MinValue;
				double min = double.MaxValue;

				for(byte i = 0; i < Vertices; i++) {
					max = vertex[i].TwoD.Y > max ? vertex[i].TwoD.Y : max;
					min = vertex[i].TwoD.Y < min ? vertex[i].TwoD.Y : min;
				}
				return Math.Abs(max - min);
			}
		}

		//

		public Point2D UpperRight {
			get {
				double maxx = double.MinValue;
				double maxy = double.MinValue;

				for(byte i = 0; i < Vertices; i++) {
					maxx = vertex[i].TwoD.X > maxx ? vertex[i].TwoD.X : maxx;
					maxy = vertex[i].TwoD.Y > maxy ? vertex[i].TwoD.Y : maxy;
				}
				return new Point2D(maxx, maxy);
			}
		}

		public Point2D UpperLeft {
			get {
				double minx = double.MaxValue;
				double maxy = double.MinValue;

				for(byte i = 0; i < Vertices; i++) {
					minx = vertex[i].TwoD.X < minx ? vertex[i].TwoD.X : minx;
					maxy = vertex[i].TwoD.Y > maxy ? vertex[i].TwoD.Y : maxy;
				}
				return new Point2D(minx, maxy);
			}
		}

		public Point2D LowerRight {
			get {
				double maxx = double.MinValue;
				double miny = double.MaxValue;

				for(byte i = 0; i < Vertices; i++) {
					maxx = vertex[i].TwoD.X > maxx ? vertex[i].TwoD.X : maxx;
					miny = vertex[i].TwoD.Y < miny ? vertex[i].TwoD.Y : miny;
				}
				return new Point2D(maxx, miny);
			}
		}

		public Point2D LowerLeft {
			get {
				double minx = double.MaxValue;
				double miny = double.MaxValue;

				for(byte i = 0; i < Vertices; i++) {
					minx = vertex[i].TwoD.X < minx ? vertex[i].TwoD.X : minx;
					miny = vertex[i].TwoD.Y < miny ? vertex[i].TwoD.Y : miny;
				}
				return new Point2D(minx, miny);
			}
		}

		//

		public Point2D Center {
			get { return Centroid.TwoD; }
		}

		//

		public Shape2D(params Point2D[] vertex) : base(vertex) { }

		//

		public void Rotate(double angle) {
			Rotate(Centroid.TwoD, angle);
		}

		public void Rotate(Point2D about, double angle) {
			Point2D corner;

			for(byte i = 0; i < Vertices; i++) {
				corner = new Point2D(vertex[i].TwoD);
				corner.Rotate(about, angle);

				vertex[i] = new Point2D(corner);
			}
		}
	}
}
