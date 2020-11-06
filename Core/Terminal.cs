
using System;

namespace Core {
	public static class Terminal {

		public const ushort INVOKER_MIN_COUNT = 0;
		public const ushort INVOKER_MAX_COUNT = 4;

		public const int DEFAULT_HASH = -1;
		public const ushort DEFAULT_HASH_ORDER = ushort.MaxValue;

		private static char[,,] cell;

		private static int[] hashcode;

		public static ushort[] hashorder;

		private static ConsoleColor[,,] fore;
		private static ConsoleColor[,,] back;

		public const bool NOT_SETUP = false;
		public const bool SETUP_DONE = true;

		private static bool setup = NOT_SETUP;

		//

		public const ConsoleColor DEFAULT_FG = ConsoleColor.White;
		public const ConsoleColor DEFAULT_BG = ConsoleColor.Black;

		public const ConsoleColor ERROR_COLOR = ConsoleColor.Red;

		public const char NULL_CHAR = '\0';
		public const char ERROR_CHAR = '!';

		public const char DEFAULT_CHAR = ' ';
		public const char OBSCURE_CHAR = '*';


		public static ushort LookUp(int hash) {
			for(ushort i = 0; i < hashcode.Length; i++) {
				if(hashcode[i] == hash) {
					return i;
				}
				if(hashcode[i] == DEFAULT_HASH) {
					hashcode[i] = hash;
					return i;
				}
			}
			ushort index = Lookup(INVOKER_MAX_COUNT - 1);
			hashcode[index] = hash;

			return index;
		}

		// why tf did I do this?
		public static ushort Lookup(ushort id) {
			for(ushort i = 0; i < hashcode.Length; i++) {
				if(hashorder[i] == id) {
					return i;
				}
				else if(hashorder[i] == DEFAULT_HASH_ORDER) {
					hashorder[i] = id;
					return i;
				}
			}		
			return INVOKER_MAX_COUNT;
		}

		public static void ShiftStack(ushort index) {
			if(hashorder[index] != 0 && hashorder[index] != DEFAULT_HASH_ORDER) {
				for(ushort i = 0; i < hashcode.Length; i++) {
					if(hashorder[i] <= hashorder[index]) {
						hashorder[i]++;
					}
				}
				hashorder[index] = 0;
			}
		}

		//

		public static char GlyphAt(ushort x, ushort y, ushort id) {
			if(!setup) {
				SetUp();
			}
			return cell[Lookup(id), x, y];

		}

		public static ConsoleColor ForeAt(ushort x, ushort y, ushort id) {
			if(!setup) {
				SetUp();		
			}
			return fore[Lookup(id), x, y];
		}

		public static ConsoleColor BackAt(ushort x, ushort y, ushort id) {
			if(!setup) {
				SetUp();
			}
			return back[Lookup(id), x, y];
		}

		//

		public static void SetUp() {
			hashcode = new int[INVOKER_MAX_COUNT];

			hashorder = new ushort[INVOKER_MAX_COUNT];

			cell = new char[INVOKER_MAX_COUNT, Width, Height];

			fore = new ConsoleColor[INVOKER_MAX_COUNT, Width, Height];
			back = new ConsoleColor[INVOKER_MAX_COUNT, Width, Height];

			for(ushort i = 0; i < INVOKER_MAX_COUNT; i++) {
				for(ushort y = 0; y < Height; y++) {
					for(ushort x = 0; x < Width; x++) {
						cell[i, x, y] = DEFAULT_CHAR;

						fore[i, x, y] = Console.ForegroundColor;
						back[i, x, y] = Console.BackgroundColor;
					}
				}
				hashcode[i] = DEFAULT_HASH;
				hashorder[i] = DEFAULT_HASH_ORDER;
			}
			setup = SETUP_DONE;
		}

		public static void Adapt() {
			char[,,] cellcpy = new char[cell.GetLength(0), cell.GetLength(1), cell.GetLength(2)];

			ConsoleColor[,,] forecpy = new ConsoleColor[fore.GetLength(0), fore.GetLength(1), fore.GetLength(2)];
			ConsoleColor[,,] backcpy = new ConsoleColor[fore.GetLength(0), back.GetLength(1), fore.GetLength(2)];

			// size of cell = size of fore = size of back or bad things happen
			for(ushort i = 0; i < cell.GetLength(0); i++) {
				for(ushort y = 0; y < cell.GetLength(2); y++) {
					for(ushort x = 0; x < cell.GetLength(1); x++) {
						cellcpy[i, x, y] = cell[i, x, y];

						forecpy[i, x, y] = fore[i, x, y];
						backcpy[i, x, y] = back[i, x, y];
					}
				}
			}
			SetUp();

			for(ushort i = 0; i < cell.GetLength(0); i++) {
				for(ushort y = 0; y < (Height < cellcpy.GetLength(2) ? Height : cellcpy.GetLength(2)); y++) {
					for(ushort x = 0; x < (Width < cellcpy.GetLength(1) ? Width : cellcpy.GetLength(1)); x++) {
						cell[i, x, y] = cellcpy[i, x, y];

						fore[i, x, y] = forecpy[i, x, y];
						back[i, x, y] = backcpy[i, x, y];
					}
				}
			}
		}

		//

		public static ushort Width {
			get { return (ushort)Console.WindowWidth; }
			set {
				if(value > Console.BufferWidth) {
					Console.BufferWidth = value;
					Console.WindowWidth = value;
				}
				else if(value < Console.BufferWidth) {
					Console.WindowWidth = value;
					Console.BufferWidth = value;
				}
				Adapt();
			}
		}

		public static ushort Height {
			get { return (ushort)Console.WindowHeight; }
			set {
				if(value > Console.BufferHeight) {
					Console.BufferHeight = value;
					Console.WindowHeight = value;
				}
				else if(value < Console.BufferHeight) {
					Console.WindowHeight = value;
					Console.BufferHeight = value;
				}
				Adapt();
			}
		}

		public static void Size(ushort w, ushort h) {
			Width = w;
			Height = h;
		}

		//

		public static ushort PointerX {
			get { return (ushort)Console.CursorLeft; }
			set {
				if(value >= Console.WindowWidth) {
					Console.CursorLeft = Console.WindowWidth - 1;
				}
				// don't need to check value < 0 b/c it is a ushort
				else {
					Console.CursorLeft = value;
				}
			}
		}

		public static ushort PointerY {
			get { return (ushort)Console.CursorTop; }
			set {
				if(value >= Console.WindowHeight) {
					Console.CursorTop = Console.WindowHeight - 1;
				}
				// don't need to check value < 0 b/c it is a ushort
				else {
					Console.CursorTop = value;
				}
			}
		}

		public static void Pointer(ushort x, ushort y) {
			PointerX = x;
			PointerY = y;
		}

		//

		public static ConsoleColor Fore {
			get { return Console.ForegroundColor; }
			set { Console.ForegroundColor = value; }
		}

		public static ConsoleColor Back {
			get { return Console.BackgroundColor; }
			set { Console.BackgroundColor = value; }
		}

		public static void Colors(ConsoleColor fore, ConsoleColor back) {
			Console.ForegroundColor = fore;
			Console.BackgroundColor = back;
		}

		public static void ResetColors() {
			Console.ForegroundColor = DEFAULT_FG;
			Console.BackgroundColor = DEFAULT_BG;
		}

		//

		public enum OutputMode : byte {
			NoAdvance,

			AdvanceCharacter,
			AdvanceLine,
			
			DevanceCharacter,
			DevanceLine,

			AdvanceX,
			AdvanceY,

			DevanceX,
			DevanceY,
		}

		public static void Put(char glyph, OutputMode mode, int hash) {
			if(!setup) {
				SetUp();
			} 

			cell[LookUp(hash), PointerX, PointerY] = glyph;

			fore[LookUp(hash), PointerX, PointerY] = Fore;
			back[LookUp(hash), PointerX, PointerY] = Back;

			ShiftStack(LookUp(hash));

			ushort x;
			ushort y;

			switch(mode) {
				case OutputMode.NoAdvance:
					// to-do: no advance
					break;

				case OutputMode.AdvanceCharacter:
					Console.Write(glyph);
					break;
				case OutputMode.AdvanceLine:
					Console.WriteLine(glyph);
					break;
				
				// to-do: devance char / line
				case OutputMode.DevanceCharacter:
					Console.Write(glyph);
					break;
				case OutputMode.DevanceLine:
					Console.WriteLine(glyph);
					break;

				// to-do: what is the point of this?
				case OutputMode.AdvanceX:
					y = PointerY;
					Console.Write(glyph);
					PointerY = y;
					break;
				case OutputMode.AdvanceY:
					x = PointerX;
					Console.WriteLine(glyph);
					PointerX = x;
					break;

				case OutputMode.DevanceX:
					y = PointerY;
					Console.Write(glyph);
					PointerY = (--y);
					break;
				case OutputMode.DevanceY:
					x = PointerX;
					Console.WriteLine(glyph);
					PointerX = (--x);
					break;
			}
		}

		public static void Print(string line, OutputMode mode, OutputMode end, int hash) {
			for(byte i = 0; i < line.Length - 1; i++) {
				// allows printing backwards and stuff!
				Put(line[i], mode, hash);
			}
			Put(line[line.Length - 1], end, hash);
		}

		//

		public enum InputMode : byte {
			Refuse,
			AcceptAndDisplay,
			AcceptAndObscure,
			AcceptNoDisplay,
		}

		public static string Read(InputMode mode) {
			return "0";
		}
	}
}
