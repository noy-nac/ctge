
using System;

namespace Graphics.Vector.Geometry {
	public class Segment {

		protected Point start;
		protected Point end;

		//

		public Point Start {
			get { return start; }
			set { start.Initalize(value.Array); }
		}

		public Point End {
			get { return end; }
			set { start.Initalize(value.Array); }
		}

		public double Length {
			get {
				double sum = 0;

				for(byte i = 0; i < Dimension; i++) {
					sum += (start[i] - end[i]) * (start[i] - end[i]);
				}
				return Math.Sqrt(sum);
			}
		}

		public byte Dimension {
			// if dimensions are mismatched, use the lower one
			get { return start.Dimension > end.Dimension ? end.Dimension : start.Dimension; }
		}

		//

		public Segment(Point start, Point end) {
			Initalize(start, end);
		}

		public void Initalize(Point start, Point end) {
			byte dim = start.Dimension > end.Dimension ? end.Dimension : start.Dimension;

			this.start = new Point(dim);
			this.end = new Point(dim);

			for(byte i = 0; i < dim; i++) {
				this.start[i] = start[i];
				this.end[i] = end[i];
			}
		}

		//

		public Segment[] Split(params Point[] point) {
			Segment[] sub = new Segment[point.Length];

			for(byte i = 0; i < sub.Length; i++) {
				// ensures that the fist split goes from start to point[0] and the last split goes from point[len - 1] to end
				sub[i] = new Segment(i == 0 ? start : point[i], i == 0 ? point[i] : (i == sub.Length - 1 ? end : point[i + 1]));
			}
			return sub;
		}

		public void Translate(Point delta) {
			start.Translate(delta);
			end.Translate(delta);
		}
	}
}
