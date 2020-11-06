
using System;

namespace Graphics.Vector.Geometry {
	public class Point {

		public const double ORIGIN = 0;

		// max is only upper bound (theta = 2pi maps to theta = 0)
		public const double ANGLE_MAX = 2 * Math.PI;
		public const double ANGLE_MIN = 0;

		public const double ANGLE_DEFAULT = ANGLE_MIN;

		//

		protected double[] coord;

		//

		public double this[byte dim] {
			get { return coord[dim]; }
			set { coord[dim] = value; }
		}

		public double[] Array {
			get { return coord; }
			set { Initalize(value); }
		}

		public Point2D TwoD {
			get { return new Point2D(coord[0], coord[1]); }
		}

		// distance from the orgin (0, 0, ...)
		public double Distance {
			get {
				double sum = 0;

				for(byte i = 0; i < Dimension; i++) {
					sum += coord[i] * coord[i];
				}
				return Math.Sqrt(sum);
			}
		}

		public byte Dimension {
			get { return (byte)coord.Length; }
		}

		//

		public Point(Point old) {
			Initalize(old.Array);
		}

		public Point(params double[] coord) {
			Initalize(coord);
		}

		public Point(byte dim) {
			Initalize(new double[dim]);
		}

		public void Initalize(double[] coord) {
			this.coord = new double[coord.Length];

			for(byte i = 0; i < coord.Length; i++) {
				this.coord[i] = coord[i];
			}
		}

		//

		public void Translate(Point delta) {
			for(byte i = 0; i < (Dimension < delta.Dimension ? Dimension : delta.Dimension); i++) {
				coord[i] += delta[i];
			} 
		}
	}
}
