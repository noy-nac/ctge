
using Graphics.Raster;
using Graphics.Raster.Text;

using Graphics.Tracking;

namespace Interface.Content {
    public class Display : Element {

		private Image content;

		//

		public Image Content {
			get { return content; }
			set { content = value; }
		}

		//

		public Chixel this[ushort x, ushort y] {
			get { return content[x, y]; }
			set { content[x, y] = value; }
		}

		//

		public Display(Box area) : base() {
			Initalize(area);
		}

		public void Initalize(Box area) {
			content = new Image(area);
		}
	}
}
