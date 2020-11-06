
namespace Graphics.Vector.Geometry.Enclosed {
	public class Shape {

		protected Point[] vertex;

		//

		public Point this[byte vertex] {
			get { return this.vertex[vertex]; }
			set { this.vertex[vertex] = value; }
		}

		public Point[] Array {
			get { return vertex; }
			set { Initalize(value); }
		}

		public byte Vertices {
			get { return (byte)vertex.Length; }
		}

		public byte Dimension {
			// if dimensions are mismatched, use the lowest one
			get {
				byte dim = byte.MaxValue;

				for(byte i = 0; i < Vertices; i++) {
					dim = dim > vertex[i].Dimension ? vertex[i].Dimension : dim;
				}
				return dim;
			}
		}

		public Point Centroid {
			get {
				Point center = new Point(Dimension);

				double sum = 0;

				for(byte i = 0; i < Dimension; i++) {
					for(byte j = 0; j < Vertices; j++) {
						sum += vertex[j][i];
					}
					center[i] = sum / Vertices;
				}
				return center;
			}
		}

		//

		public Shape(params Point[] vertex) {
			Initalize(vertex);
		}

		public Shape(byte verticies) {
			Initalize(new Point[verticies]);
		}

		public void Initalize(Point[] vertex) {
			this.vertex = new Point[vertex.Length];

			for(byte i = 0; i < Vertices; i++) {
				this.vertex[i] = new Point(vertex[i]);
			}
		}

		//

		public void Translate(Point delta) {
			for(byte i = 0; i < Vertices; i++) {
				vertex[i].Translate(delta);
			}
		}
	}
}
