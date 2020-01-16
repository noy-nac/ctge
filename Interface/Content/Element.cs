
namespace Interface.Content {
	public class Element {

		public enum Format : byte {
			Default,
			Center,
			Left,
			Right,
		}

		//

		protected Format format;

		public const bool DEFAULT_ACTIVE = true;
		public const bool DEFAULT_ALLOW_IO = false;

		protected bool active;
		protected bool allowio;

		//

		public Format TextFormat {
			get { return format; }
			set { format = value; }
		}

		public bool Active {
			get { return active; }
			set { active = value; }
		}

		public bool AllowIO {
			get { return allowio; }
			set { allowio = value; }
		}

		//

		public Element(Format fmat, bool act, bool allowio) {
			Initalize(fmat, act, allowio);
		}

		public Element() {
			Initalize(Format.Default, DEFAULT_ACTIVE, DEFAULT_ALLOW_IO);
		}

		public void Initalize(Format fmat, bool act, bool allowio) {
			format = fmat;

			active = act;
			this.allowio = allowio;
		}
	}
}
