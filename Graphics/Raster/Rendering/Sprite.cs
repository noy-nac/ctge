
using System;

using Core;

using Graphics.Coloring;

using Graphics.Raster.Text;

using Graphics.Tracking;

using Graphics.Vector.Geometry;

// to-do: refine, add comments
namespace Graphics.Raster.Rendering {
	public class Sprite : Image {

		private Chixel[,] memory;

		private ushort[,] xmap;
		private ushort[,] ymap;

		//

		public Chixel[,] Memory {
			get { return memory; }
			set { memory = value; }
		}

		//

		public Sprite(Box area) : base(area) {
			Initalize(area);
		}

		public Sprite(ushort w, ushort h, Point2D src) : base(new Box(w, h, src)) {
			Initalize(new Box(w, h, src));
		}

		public new void Initalize(Box area) {
			memory = new Chixel[(ushort)area.Width, (ushort)area.Height];

			for(ushort y = 0; y < area.Height; y++) {
				for(ushort x = 0; x < area.Width; x++) {
					memory[x, y] = new Chixel();
				}
			}
			LoadMap(DEFAULT_MAP_MODE);
		}

		//

		// to-do: do
		public void Load(Sprite sprite) {
		}

		// to-do: do
		public Sprite Copy() {
			return new Sprite(new Box(0, 0, new Point2D(0, 0)));
		}

		//

		public void Translate(Point2D delta) {
			Point2D src;

			// Width and Height are always > 0
			short w = (short)(area.Width + Math.Abs(delta.X));
			short h = (short)(area.Height + Math.Abs(delta.Y));

			Chixel[,] mat = new Chixel[w, h];
			Chixel[,] mem = new Chixel[w, h];

			if(delta.X >= 0 && delta.Y >= 0) {
				src = new Point2D(area.LowerLeft);

				for(ushort y = 0; y < h; y++) {
					for(ushort x = 0; x < w; x++) {
						mat[x, y] = x >= w - area.Width && y >= h - area.Height ?
							new Chixel(matrix[(ushort)(x - w + area.Width), (ushort)(y - h + area.Height)]) :
							new Chixel();

						mem[x, y] = x < area.Width && y < area.Height ?
							new Chixel(memory[x, y]) :
							new Chixel();
					}
				}
				//w= (short)(1 * w);
				//h= (short)(1 * h);
			}
			else if(delta.X >= 0 && delta.Y < 0) {
				src = new Point2D(area.UpperLeft);

				for(ushort y = 0; y < h; y++) {
					for(ushort x = 0; x < w; x++) {
						mat[x, y] = x >= w - area.Width && y < area.Height ?
							new Chixel(matrix[(ushort)(x - w + area.Width), y]) :
							new Chixel();

						mem[x, y] = x < area.Width && y >= h - area.Height ?
							new Chixel(memory[x, (ushort)(y - h + area.Height)]) :
							new Chixel();
					}
				}
				//w= (short)(1 * w);
				h = (short)(-1 * h);
			}
			else if(delta.X < 0 && delta.Y >= 0) {
				src = new Point2D(area.LowerRight);

				for(ushort y = 0; y < h; y++) {
					for(ushort x = 0; x < w; x++) {
						mat[x, y] = x < area.Width && y >= h - area.Height ?
							new Chixel(matrix[x, (ushort)(y - h + area.Height)]) :
							new Chixel();

						mem[x, y] = x >= w - area.Width && y < area.Height ?
							new Chixel(memory[(ushort)(x - w + area.Width), y]) :
							new Chixel();
					}
				}
				w = (short)(-1 * w);
				//h= 1 * h;
			}
			else if(delta.X < 0 && delta.Y <= 0) {
				src = new Point2D(area.UpperRight);

				for(ushort y = 0; y < h; y++) {
					for(ushort x = 0; x < w; x++) {
						mat[x, y] = x < area.Width && y < area.Height ?
							new Chixel(matrix[x, y]) :
							new Chixel();

						mem[x, y] = x >= w - area.Width && y >= h - area.Height ?
							new Chixel(memory[(ushort)(x - w + area.Width), (ushort)(y - h + area.Height)]) :
							new Chixel();
					}
				}
				w = (short)(-1 * w);
				h = (short)(-1 * h);
			}
			// delta.X == 0 && delta.Y == 0
			else {
				return;
			}
			matrix = new Chixel[Math.Abs(w), Math.Abs(h)];
			memory = new Chixel[Math.Abs(w), Math.Abs(h)];

			for(ushort y = 0; y < Math.Abs(h); y++) {
				for(ushort x = 0; x < Math.Abs(w); x++) {
					matrix[x, y] = new Chixel(mat[x, y]);
					memory[x, y] = new Chixel(mem[x, y]);
				}
			}
			area = new Box(w, h, src);

			LoadMap(DEFAULT_MAP_MODE);
		}

		//

		public void Rebox() {
			Point2D src;

			Chixel[,] mat;
			Chixel[,] mem;

			ushort w;
			ushort h;

			ushort xmin = ushort.MaxValue;
			ushort xmax = ushort.MinValue;

			ushort ymin = ushort.MaxValue;
			ushort ymax = ushort.MinValue;

			for(ushort y = 0; y < Height; y++) {
				for(ushort x = 0; x < Width; x++) {
					if(matrix[x, y].Glyph != memory[x, y].Glyph ||
						matrix[x, y].Fore.ARGB != memory[x, y].Fore.ARGB ||
						matrix[x, y].Back.ARGB != memory[x, y].Back.ARGB) {

						xmax = x > xmax ? x : xmax;
						xmin = x < xmin ? x : xmin;

						ymax = y > ymax ? y : ymax;
						ymin = y < ymin ? y : ymin;
					}
				}
			}
			xmin = xmin == ushort.MaxValue ? ushort.MinValue : xmin;
			xmax = xmax == ushort.MinValue ? (ushort)(Width - 1) : xmax;

			ymin = ymin == ushort.MaxValue ? ushort.MinValue : ymin;
			ymax = ymax == ushort.MinValue ? (ushort)(Height - 1) : ymax;

			w = (ushort)(xmax - xmin + 1);
			h = (ushort)(ymax - ymin + 1);

			src = new Point2D(area.Source.X + xmin, area.Source.Y + ymin);

			mat = new Chixel[w, h];
			mem = new Chixel[w, h];

			for(ushort y = 0; y < h; y++) {
				for(ushort x = 0; x < w; x++) {
					mat[x, y] = new Chixel(matrix[xmin + x, ymin + y]);
					mem[x, y] = new Chixel(memory[xmin + x, ymin + y]);
				}
			}
			matrix = new Chixel[w, h];
			memory = new Chixel[w, h];

			for(ushort y = 0; y < h; y++) {
				for(ushort x = 0; x < w; x++) {
					matrix[x, y] = new Chixel(mat[x, y]);
					memory[x, y] = new Chixel(mem[x, y]);
				}
			}
			area = new Box(w, h, src);

			LoadMap(DEFAULT_MAP_MODE);
		}

		//

		public void Resize(double scale) {
			Resize(scale, scale);
		}

		// to-do: redo
		public void Resize(double scalex, double scaley) {
		}

		public void Resize(ushort neww, ushort newh) {
			Resize((double)neww / Width, (double)newh / Height);
		}

		//

		// to-do: finish
		public void Rotate(Point2D about, double angle) {
			// use ceiling then directly map exact coordinates
			ushort w = (ushort)Math.Ceiling(area.Height * Math.Cos(angle) * area.Width * Math.Sin(angle));
			ushort h = (ushort)Math.Ceiling(area.Height * Math.Sin(angle) * area.Width * Math.Cos(angle));

			// floor value of x, y
			ushort fx;
			ushort fy;

			// ceiling value of x, y
			ushort cx;
			ushort cy;

			Chixel[,] mat = new Chixel[w, h];
			Chixel[,] mem = new Chixel[w, h];

			Point2D current;

			for(ushort y = 0; y < Height; y++) {
				for(ushort x = 0; x < Width; x++) {
					current = new Point2D(x, y);
					current.Rotate(about, angle);

					fx = (ushort)Math.Floor(current.X);
					fy = (ushort)Math.Floor(current.Y);

					cx = (ushort)Math.Ceiling(current.X);
					cy = (ushort)Math.Ceiling(current.Y);


				}
			}
		}

		//

		private const ushort LAZY_RAND_IT = 12;

		public void LoadMap(MapMode effect) {
			Random rand = new Random();

			ushort ox;
			ushort oy;

			xmap = new ushort[Width, Height];
			ymap = new ushort[Width, Height];

			for(ushort y = 0; y < Height; y++) {
				for(ushort x = 0; x < Width; x++) {
					xmap[x, y] = x;
					ymap[x, y] = y;
				}
			}
			switch(effect) {
				case MapMode.None:
					break;
				case MapMode.TrueRandom:
					for(ushort i = 0; i < LAZY_RAND_IT; i++) {
						for(ushort y = 0; y < Height; y++) {
							for(ushort x = 0; x < Width; x++) {
								ox = (ushort)rand.Next(y % (x + 1), Width);
								oy = (ushort)rand.Next(x % (y + 1), Height);

								if(xmap[x, y] == x && ymap[x, y] == y && xmap[ox, oy] == ox && ymap[ox, oy] == oy) {
									xmap[x, y] = ox;
									ymap[x, y] = oy;

									xmap[ox, oy] = x;
									ymap[ox, oy] = y;
								}
							}
						}
					}
					break;
				case MapMode.Randomize:
					for(ushort y = 0; y < Height; y++) {
						for(ushort x = 0; x < Width; x++) {
							ox = (ushort)rand.Next(y % (x + 1), Width);
							oy = (ushort)rand.Next(x % (y + 1), Height);

							if(xmap[x, y] == x && ymap[x, y] == y && xmap[ox, oy] == ox && ymap[ox, oy] == oy) {
								xmap[x, y] = ox;
								ymap[x, y] = oy;

								xmap[ox, oy] = x;
								ymap[ox, oy] = y;
							}
						}
					}
					break;
			}
		}

		//

		public const FillMode DEFAULT_FILL_MODE = FillMode.Default;

		public enum FillMode : byte {
			// x > 0, y > 0
			XY_I,
			YX_I,
			// x < 0, y > 0
			XY_II,
			YX_II,
			// x < 0 , y < 0
			XY_III,
			YX_III,
			// x > 0, y < 0
			XY_IV,
			YX_IV,

			Default = XY_I,
		}

		//

		public MapMode DEFAULT_MAP_MODE = MapMode.Default;

		public enum MapMode : byte {
			None,
			Randomize,
			TrueRandom,

			Default = None,
		}

		//

		public void Render(RenderOption option) {
			Render(option, DEFAULT_FILL_MODE, DEFAULT_MAP_MODE);
		}

		public void Render(RenderOption option, MapMode effect) {
			Render(option, DEFAULT_FILL_MODE, effect);
		}

		public void Render(RenderOption option, FillMode fill) {
			Render(option, fill, DEFAULT_MAP_MODE);
		}

		public void Render(RenderOption option, FillMode fill, MapMode effect) {
			if(effect == MapMode.None) {
				StrictRender(option, fill);
			}
			else {
				MapRender(option, fill, effect);
			}
		}

		//

		public const RenderOption ALLOW_REBOXING = RenderOption.AllowReboxing;

		public const ushort AUTO_REBOX_TRIG = 24;

		// these will never change
		public const ushort NO_MOTION = 0;
		public const ushort X_MOTION_SCALE = 1;
		public const ushort Y_MOTION_SCALE = 1;

		public void Move(Point2D delta, RenderOption option) {
			Move(delta, option, DEFAULT_FILL_MODE, DEFAULT_MAP_MODE);
		}

		public void Move(Point2D delta, RenderOption option, MapMode effect) {
			Move(delta, option, DEFAULT_FILL_MODE, effect);
		}

		public void Move(Point2D delta, RenderOption option, FillMode fill) {
			Move(delta, option, fill, DEFAULT_MAP_MODE);
		}

		public void Move(Point2D delta, RenderOption option, FillMode fill, MapMode effect) {
			double dx = Math.Abs(delta.X);
			double dy = Math.Abs(delta.Y);

			// use delta.X / |delta.X| = delta.X / dx = +/-1 to keep the sign of delta.X
			if(dx > dy) {
				for(ushort x = 0; x < dx; x++) {
					// to exclude x = 0
					if(x % AUTO_REBOX_TRIG == (ushort)(AUTO_REBOX_TRIG / (x + 1))) {
						Render(ALLOW_REBOXING | option, fill, effect);
						Rebox();
					}
					// prevent artifacts
					Render(option, fill, effect);
					Translate(new Point2D(X_MOTION_SCALE * delta.X / dx,
						((x % (dx / dy) > 0) && (x % (dx / dy) <= 1)) ? Y_MOTION_SCALE * delta.Y / dy : NO_MOTION));
				}
			}
			else if(dy > dx) {
				for(ushort y = 0; y < dy; y++) {
					// to exclude y = 0
					if(y % AUTO_REBOX_TRIG == (ushort)(AUTO_REBOX_TRIG / (y + 1))) {
						Render(ALLOW_REBOXING | option, fill, effect);
						Rebox();
					}
					// prevent artifacts
					Render(option, fill, effect);
					Translate(new Point2D(((y % (dy / dx) > 0) && (y % (dy / dx) <= 1)) ?
						X_MOTION_SCALE * delta.X / dx : NO_MOTION, Y_MOTION_SCALE * delta.Y / dy));
				}
			}
			// dx == dy
			else {
				for(ushort i = 0; i < dx; i++) {
					// to exclude i = 0
					if(i % AUTO_REBOX_TRIG == (ushort)(AUTO_REBOX_TRIG / (i + 1))) {
						Render(ALLOW_REBOXING | option, fill, effect);
						Rebox();
					}
					// prevent artifacts
					Render(option, fill, effect);
					Translate(new Point2D(delta.X / dx, delta.Y / dy));
				}
			}
			// really this should just be Render(ALLOW_REBOXING, effect);
			Render(ALLOW_REBOXING | option, fill, effect);
		}

		//

		// type: Terminal.OutputMode
		// to-do: XY_II - YX_IV
		public enum PointerAdvance : byte {
			// x > 0, y > 0
			XY_I = Terminal.OutputMode.AdvanceCharacter,
			YX_I = Terminal.OutputMode.AdvanceY,
			// x < 0, y > 0
			XY_II = Terminal.OutputMode.AdvanceCharacter,
			YX_II = Terminal.OutputMode.AdvanceY,
			// x < 0 , y < 0
			XY_III = Terminal.OutputMode.AdvanceCharacter,
			YX_III = Terminal.OutputMode.AdvanceY,
			// x > 0, y < 0
			XY_IV = Terminal.OutputMode.AdvanceCharacter,
			YX_IV = Terminal.OutputMode.AdvanceY,

			Default = XY_I,
		}

		//

		public void StrictRender(RenderOption option, FillMode mode) {
			switch(mode) {
				case FillMode.XY_I:
					for(ushort y = 0; y < Height; y++) {
						for(ushort x = 0; x < Width; x++) {
							StrictRender(x, y, option, PointerAdvance.XY_I);
						}
					}
					break;
				case FillMode.YX_I:
					for(ushort x = 0; x < Width; x++) {
						for(ushort y = 0; y < Height; y++) {
							StrictRender(x, y, option, PointerAdvance.YX_I);
						}
					}
					break;
				case FillMode.XY_II:
					for(ushort y = 0; y < Height; y++) {
						for(ushort x = (ushort)(Width - 1); x >= 0 && x < Width; x--) {
							StrictRender(x, y, option, PointerAdvance.XY_II);
						}
					}
					break;
				case FillMode.YX_II:
					for(ushort x = (ushort)(Width - 1); x >= 0 && x < Width; x--) {
						for(ushort y = 0; y < Height; y++) {
							StrictRender(x, y, option, PointerAdvance.YX_II);
						}
					}
					break;
				case FillMode.XY_III:
					for(ushort y = (ushort)(Height - 1); y >= 0 && y < Height; y--) {
						for(ushort x = (ushort)(Width - 1); x >= 0 && x < Width; x--) {
							StrictRender(x, y, option, PointerAdvance.XY_III);
						}
					}
					break;
				case FillMode.YX_III:
					for(ushort x = (ushort)(Width - 1); x >= 0 && x < Width; x--) {
						for(ushort y = (ushort)(Height - 1); y >= 0 && y < Height; y--) {
							StrictRender(x, y, option, PointerAdvance.YX_III);
						}
					}
					break;
				case FillMode.XY_IV:
					for(ushort y = (ushort)(Height - 1); y >= 0 && y < Height; y--) {
						for(ushort x = 0; x < Width; x++) {
							StrictRender(x, y, option, PointerAdvance.XY_IV);
						}
					}
					break;
				case FillMode.YX_IV:
					for(ushort x = 0; x < Width; x++) {
						for(ushort y = (ushort)(Height - 1); y >= 0 && y < Height; y--) {
							StrictRender(x, y, option, PointerAdvance.YX_IV);
						}
					}
					break;
			}
		}

		//

		[Flags]
		public enum RenderOption : byte {
			// order is first to last executed if all were selected

			// overriden by None | Anything
			None = 0,

			// render everything in matrix, not just changes
			Full = 1,

			// copy the matrix buffer into the memory buffer
			Record = 2,

			// set the view level to the default console colors
			Reset = 4,

			// copy the memory buffer into the matrix buffer
			Restore = 8,

			// reset memory buffer after rendering
			CleanUp = 16,

			// reset matrix buffer after rendering
			Blank = 32,

			// descriptions
			Default = Record,
			Motion = Record,
			IgnoreOpacity = Reset,
			AllowReboxing = CleanUp,
			EraseEverything = Full | CleanUp | Blank,
		}

		//

		public void MapRender(RenderOption option, FillMode fill, MapMode effect) {
			LoadMap(effect);

			StrictRender(option, fill);
		}

		//

		public void StrictRender(ushort x, ushort y, RenderOption option, PointerAdvance outmode) {
			if((RenderOption.Full & option) == RenderOption.Full || matrix[x, y].Glyph != memory[x, y].Glyph ||
				matrix[x, y].Fore.ARGB != memory[x, y].Fore.ARGB || matrix[x, y].Back.ARGB != memory[x, y].Back.ARGB) {
				RenderChixel(x, y, option, outmode);
			}
		}

		public void MapRender(ushort x, ushort y, RenderOption option, PointerAdvance outmode) {
			ushort ox = ushort.MaxValue;
			ushort oy = ushort.MaxValue;

			ox = x;
			oy = y;

			x = xmap[ox, oy];
			y = ymap[ox, oy];



			if((RenderOption.Full & option) == RenderOption.Full || matrix[x, y].Glyph != memory[x, y].Glyph ||
				matrix[x, y].Fore.ARGB != memory[x, y].Fore.ARGB || matrix[x, y].Back.ARGB != memory[x, y].Back.ARGB) {
				RenderChixel(x, y, option, outmode);
			}
		}

		//

		private const ushort VIEW_LEVEL = 1;

		private const ushort DEFAULT_REDIR = ushort.MaxValue;

		public void RenderChixel(ushort x, ushort y, RenderOption option, PointerAdvance outmode) {
			Console.Title = "[Sx Sy]: [" + area.Source.X + " " + area.Source.Y + "]";

			Chixel final = new Chixel();
			Chixel current;

			// update the internal image of the screen
			if((RenderOption.Record & option) == RenderOption.Record) {
				memory[x, y] = new Chixel(matrix[x, y]);
			}
			// do not render if chixel is not on the screen
			if((Terminal.PointerX == area.Source.X + x && Terminal.PointerY == area.Source.Y + y) ||
				(area.Source.X + x <= Terminal.Width && area.Source.Y + y <= Terminal.Height)) {
				Terminal.Pointer((ushort)(area.Source.X + x), (ushort)(area.Source.Y + y));
			}
			else {
				return;
			}
			// just-in-time opacity merging
			if(matrix[x, y].MeshColor.Opacity != Color.ARGB_MAX) {
				// override view level for opacity blending
				if((RenderOption.Reset & option) == RenderOption.Reset) {
					Terminal.Colors(Chixel.DEFAULT_FG.Console, Chixel.DEFAULT_BG.Console);
					Terminal.Put(Chixel.EMPTY_GLYPH, (Terminal.OutputMode)outmode, GetHashCode());
				}
				current = new Chixel(Terminal.GlyphAt((ushort)(area.Source.X + x), (ushort)(area.Source.Y + y), VIEW_LEVEL),
					new Color(Terminal.ForeAt((ushort)(area.Source.X + x), (ushort)(area.Source.Y + y), VIEW_LEVEL)),
					new Color(Terminal.BackAt((ushort)(area.Source.X + x), (ushort)(area.Source.Y + y), VIEW_LEVEL)));

				final.MeshColor = Blend.Apply(matrix[x, y].MeshColor, current.MeshColor,
					Blend.DEFAULT_POS, Blend.DEFAULT_USE_GAMMA, Blend.DEFAULT_MODE);

				Terminal.Colors(final.Fore.Console, final.Back.Console);
				Terminal.Put(final.Glyph, (Terminal.OutputMode)outmode, GetHashCode());
			}
			else {
				Terminal.Colors(matrix[x, y].Fore.Console, matrix[x, y].Back.Console);
				Terminal.Put(matrix[x, y].Glyph, (Terminal.OutputMode)outmode, GetHashCode());
			}
			// return the render matrix to the internal image of the screen
			if((RenderOption.Restore & option) == RenderOption.Restore) {
				matrix[x, y] = new Chixel(memory[x, y]);
			}
			// reset the chixels in the internal image
			if((RenderOption.CleanUp & option) == RenderOption.CleanUp) {
				memory[x, y] = new Chixel();
			}
			// reset the chixels in the render matrix
			if((RenderOption.Blank & option) == RenderOption.Blank) {
				matrix[x, y] = new Chixel();
			}
		}
	}
}

