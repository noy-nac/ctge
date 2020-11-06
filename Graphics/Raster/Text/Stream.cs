
namespace Graphics.Raster.Text {
	public class Stream {

		private Chixel[] line;

		//

		public Chixel this[ushort id] {
			get { return line[id]; }
			set { line[id] = value; }
		}

		public Chixel[] Array {
			get { return line; }
			set { Initalize(value); }
		}

		public ushort Length {
			get { return (ushort)line.Length; }
		}

		//

		public Stream(params Chixel[] line) {
			Initalize(line);
		}

		public Stream(ushort length) {
			Initalize(new Chixel[length]);
		}

		public void Initalize(Chixel[] line) {
			this.line = new Chixel[line.Length];

			for(ushort i = 0; i < line.Length; i++) {
				this.line[i] = line[i];
			}
		}

	}
}
