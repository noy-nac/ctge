
using System;

namespace Graphics.Coloring {
	public static class Blend {

		public enum Mode : byte {
			Merge,
			Overwrite,

			Mesh,
			Mix,

			Darker,
			Lighter,

			DarkerOnly,
			LighterOnly,

			Screen,
			Veil,
			Spread,

			Add,
			Subtract,
			Difference,

			Multiply,
			Divide,
			Modulo,

			Overlay,
			Exclusion,

			ColorBurn,
			ColorDodge,

			LinearBurn,

			SoftLight,
			HardLight,
			VividLight,
			LinearLight,
			PinLight,

			AND,
			NAND,
			OR,
			NOR,
			XOR,
			XNOR,
		}

		//

		public static Color Apply(Color top, Color bot, double pos, bool usegamma, Mode mode) {
			switch(mode) {
				case Mode.Merge:
					return Merge(top, bot, pos, usegamma);
				case Mode.Overwrite:
					return Overwrite(top, bot);

				case Mode.Mesh:
					return Mesh(top, bot, pos, usegamma);
				case Mode.Mix:
					return Mix(top, bot, pos, usegamma);

				case Mode.Darker:
					return Darker(top, bot, pos, usegamma);
				case Mode.Lighter:
					return Lighter(top, bot, pos, usegamma);

				case Mode.DarkerOnly:
					return DarkerOnly(top, bot, pos, usegamma);
				case Mode.LighterOnly:
					return LighterOnly(top, bot, pos, usegamma);

				case Mode.Screen:
					return Screen(top, bot, pos, usegamma);
				case Mode.Veil:
					return Veil(top, bot, pos, usegamma);
				case Mode.Spread:
					return Spread(top, bot, pos, usegamma);

				case Mode.Add:
					return Add(top, bot, pos, usegamma);
				case Mode.Subtract:
					return Subtract(top, bot, pos, usegamma);
				case Mode.Difference:
					return Difference(top, bot, pos, usegamma);

				case Mode.Multiply:
					return Multiply(top, bot, pos, usegamma);
				case Mode.Divide:
					return Divide(top, bot, pos, usegamma);
				case Mode.Modulo:
					return Modulo(top, bot, pos, usegamma);

				case Mode.Overlay:
					return Overlay(top, bot, pos, usegamma);
				case Mode.Exclusion:
					return Exclusion(top, bot, pos, usegamma);
				/*
				case Mode.ColorBurn:
					return ColorBurn(top, bot, pos, usegamma);
				case Mode.ColorDodge:
					return ColorDodge(top, bot, pos, usegamma);

				case Mode.LinearBurn:
					return LinearBurn(top, bot, pos, usegamma);

				case Mode.SoftLight:
					return SoftLight(top, bot, pos, usegamma);
				case Mode.HardLight:
					return HardLight(top, bot, pos, usegamma);
				case Mode.VividLight:
					return VividLight(top, bot, pos, usegamma);
				case Mode.LinearLight:
					return LinearLight(top, bot, pos, usegamma);
				case Mode.PinLight:
					return PinLight(top, bot, pos, usegamma);

				case Mode.AND:
					return AND(top, bot, pos, usegamma);
				case Mode.NAND:
					return NAND(top, bot, pos, usegamma);
				case Mode.OR:
					return OR(top, bot, pos, usegamma);
				case Mode.NOR:
					return NOR(top, bot, pos, usegamma);
				case Mode.XOR:
					return XOR(top, bot, pos, usegamma);
				case Mode.XNOR:
					return XNOR(top, bot, pos, usegamma);
				*/
				default:
					return top;
			}
		}

		//

		public const Mode DEFAULT_MODE = Mode.Merge;

		public const bool DEFAULT_USE_GAMMA = false;

		public const double POS_MAX = 1;
		public const double POS_MID = (POS_MAX + POS_MIN) / 2;
		public const double POS_MIN = 0;

		public const double TOP_ONLY_POS = POS_MIN;
		public const double DEFAULT_POS = POS_MID;
		public const double BOT_ONLY_POS = POS_MAX;

		//

		public static double MergeOpacities(double top, double bot, double pos) {
			double alpha = 2 * pos * pos * (2 * top * bot - top - bot) + pos * (top + 3 * bot - 4 * top * bot) + top;
			return alpha <= Color.ARGB_MAX ? alpha : Color.ARGB_MAX;
		}

		public static double MergeChannels(double top, double bot, double topa, double bota, double pos) {
			return ((POS_MAX - pos) * topa * top + pos * bota * bot * (Color.ARGB_MAX - topa)) / ((POS_MAX - pos) * topa + pos * bota * (Color.ARGB_MAX - topa));
		}

		public static Color Merge(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MergeChannels(top.GammaRed, bot.GammaRed, top.Opacity, bot.Opacity, pos),
					MergeChannels(top.GammaGreen, bot.GammaGreen, top.Opacity, bot.Opacity, pos),
					MergeChannels(top.GammaBlue, bot.GammaBlue, top.Opacity, bot.Opacity, pos),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MergeChannels(top.LinearRed, bot.LinearRed, top.Opacity, bot.Opacity, pos),
					MergeChannels(top.LinearGreen, bot.LinearGreen, top.Opacity, bot.Opacity, pos),
					MergeChannels(top.LinearBlue, bot.LinearBlue, top.Opacity, bot.Opacity, pos),
					Color.NOT_USING_Y);
		}

		//

		public static Color Overwrite(Color top, Color bot) {
			return top;
		}

		//

		public static double MeshChannels(double top, double bot, double pos) {
			return Math.Sqrt((POS_MAX - pos) * top * top + pos * bot * bot);
		}

		public static Color Mesh(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color((POS_MAX - pos) * top.Opacity + pos * bot.Opacity,
					MeshChannels(top.GammaRed, bot.GammaRed, pos),
					MeshChannels(top.GammaGreen, bot.GammaGreen, pos),
					MeshChannels(top.GammaBlue, bot.GammaBlue, pos),
					Color.USING_Y)
				: new Color((POS_MAX - pos) * top.Opacity + pos * bot.Opacity,
					MeshChannels(top.LinearRed, bot.LinearRed, pos),
					MeshChannels(top.LinearGreen, bot.LinearGreen, pos),
					MeshChannels(top.LinearBlue, bot.LinearBlue, pos),
					Color.NOT_USING_Y);
		}

		//

		// to-do: mix
		public static double MixChannels(double top, double bot, double pos) {
			return top;
		}

		public static Color Mix(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MixChannels(top.GammaRed, bot.GammaRed, pos),
					MixChannels(top.GammaGreen, bot.GammaGreen, pos),
					MixChannels(top.GammaBlue, bot.GammaBlue, pos),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MixChannels(top.LinearRed, bot.LinearRed, pos),
					MixChannels(top.LinearGreen, bot.LinearGreen, pos),
					MixChannels(top.LinearBlue, bot.LinearBlue, pos),
					Color.NOT_USING_Y);
		}

		//

		public static Color Darker(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? (top.Luma <= bot.Luma ? top : bot)
				: (top.Luminance <= bot.Luminance ? top : bot);
		}

		public static Color Lighter(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? (top.Luma >= bot.Luma ? top : bot)
				: (top.Luminance >= bot.Luminance ? top : bot);
		}

		//

		public static double DarkerChannel(double top, double bot) {
			return top <= bot ? top : bot;
		}

		public static Color DarkerOnly(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					DarkerChannel(top.GammaRed, bot.GammaRed),
					DarkerChannel(top.GammaGreen, bot.GammaGreen),
					DarkerChannel(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					DarkerChannel(top.LinearRed, bot.LinearRed),
					DarkerChannel(top.LinearGreen, bot.LinearGreen),
					DarkerChannel(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		public static double LighterChannel(double top, double bot) {
			return top >= bot ? top : bot;
		}

		public static Color LighterOnly(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					LighterChannel(top.GammaRed, bot.GammaRed),
					LighterChannel(top.GammaGreen, bot.GammaGreen),
					LighterChannel(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					LighterChannel(top.LinearRed, bot.LinearRed),
					LighterChannel(top.LinearGreen, bot.LinearGreen),
					LighterChannel(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double ScreenChannels(double top, double bot) {
			return Color.ARGB_MAX - (Color.ARGB_MAX - top) * (Color.ARGB_MAX - bot);
		}

		public static Color Screen(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ScreenChannels(top.GammaRed, bot.GammaRed),
					ScreenChannels(top.GammaGreen, bot.GammaGreen),
					ScreenChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ScreenChannels(top.LinearRed, bot.LinearRed),
					ScreenChannels(top.LinearGreen, bot.LinearGreen),
					ScreenChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double VeilChannels(double top, double bot) {
			return (Color.ARGB_MAX - top) * (Color.ARGB_MAX - bot);
		}

		public static Color Veil(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					VeilChannels(top.GammaRed, bot.GammaRed),
				 VeilChannels(top.GammaGreen, bot.GammaGreen),
					VeilChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					VeilChannels(top.LinearRed, bot.LinearRed),
					VeilChannels(top.LinearGreen, bot.LinearGreen),
					VeilChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double SpreadChannels(double top, double bot) {
			return Color.ARGB_MAX - top * (Color.ARGB_MAX - bot) - bot * (Color.ARGB_MAX - top);
		}

		public static Color Spread(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					SpreadChannels(top.GammaRed, bot.GammaRed),
					SpreadChannels(top.GammaGreen, bot.GammaGreen),
					SpreadChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					SpreadChannels(top.LinearRed, bot.LinearRed),
					SpreadChannels(top.LinearGreen, bot.LinearGreen),
					SpreadChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double AddChannels(double top, double bot) {
			return top + bot <= Color.ARGB_MAX ? top + bot : Color.ARGB_MAX;
		}

		public static Color Add(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					AddChannels(top.GammaRed, bot.GammaRed),
					AddChannels(top.GammaGreen, bot.GammaGreen),
					AddChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					AddChannels(top.LinearRed, bot.LinearRed),
					AddChannels(top.LinearGreen, bot.LinearGreen),
					AddChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double SubtractChannels(double top, double bot) {
			return top - bot >= Color.ARGB_MIN ? top - bot : Color.ARGB_MIN;
		}

		public static Color Subtract(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					SubtractChannels(top.GammaRed, bot.GammaRed),
					SubtractChannels(top.GammaGreen, bot.GammaGreen),
					SubtractChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					SubtractChannels(top.LinearRed, bot.LinearRed),
					SubtractChannels(top.LinearGreen, bot.LinearGreen),
					SubtractChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double ChannelDifference(double top, double bot) {
			return Math.Abs(top - bot);
		}

		public static Color Difference(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelDifference(top.GammaRed, bot.GammaRed),
					ChannelDifference(top.GammaGreen, bot.GammaGreen),
					ChannelDifference(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelDifference(top.LinearRed, bot.LinearRed),
					ChannelDifference(top.LinearGreen, bot.LinearGreen),
					ChannelDifference(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double MultiplyChannels(double top, double bot) {
			return top * bot;
		}

		public static Color Multiply(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MultiplyChannels(top.GammaRed, bot.GammaRed),
					MultiplyChannels(top.GammaGreen, bot.GammaGreen),
					MultiplyChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					MultiplyChannels(top.LinearRed, bot.LinearRed),
					MultiplyChannels(top.LinearGreen, bot.LinearGreen),
					MultiplyChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		// only use t < b, not t <= b, to prevent 0 / 0
		public static double DivideChannels(double top, double bot) {
			return top < bot ? top / bot : Color.ARGB_MAX;
		}

		public static Color Divide(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					DivideChannels(top.GammaRed, bot.GammaRed),
					DivideChannels(top.GammaGreen, bot.GammaGreen),
					DivideChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					DivideChannels(top.LinearRed, bot.LinearRed),
					DivideChannels(top.LinearGreen, bot.LinearGreen),
					DivideChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		// only use t < b, not a <= b, to prevent 0 % 0
		public static double ModuloChannels(double top, double bot) {
			return top < bot ? top % bot : top;
		}

		public static Color Modulo(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ModuloChannels(top.GammaRed, bot.GammaRed),
					ModuloChannels(top.GammaGreen, bot.GammaGreen),
					ModuloChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ModuloChannels(top.LinearRed, bot.LinearRed),
					ModuloChannels(top.LinearGreen, bot.LinearGreen),
					ModuloChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double OverlayChannels(double top, double bot) {
			return top > Color.ARGB_MID
				? Color.ARGB_MAX - (Color.ARGB_MAX - top) * (Color.ARGB_MAX - bot) / Color.ARGB_MID
				: top * bot / Color.ARGB_MID;
		}

		public static Color Overlay(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					OverlayChannels(top.GammaRed, bot.GammaRed),
					OverlayChannels(top.GammaGreen, bot.GammaGreen),
					OverlayChannels(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					OverlayChannels(top.LinearRed, bot.LinearRed),
					OverlayChannels(top.LinearGreen, bot.LinearGreen),
					OverlayChannels(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double ChannelExclusion(double top, double bot) {
			return Color.ARGB_MID - (top - Color.ARGB_MID) * (bot - Color.ARGB_MID) / Color.ARGB_MID;
		}

		public static Color Exclusion(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelExclusion(top.GammaRed, bot.GammaRed),
					ChannelExclusion(top.GammaGreen, bot.GammaGreen),
					ChannelExclusion(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelExclusion(top.LinearRed, bot.LinearRed),
					ChannelExclusion(top.LinearGreen, bot.LinearGreen),
					ChannelExclusion(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}

		//

		public static double ChannelSoftLight(double top, double bot) {
			return top < Color.ARGB_MID
				? (Color.ARGB_MAX - top / Color.ARGB_MID) / (Color.ARGB_MAX + Math.Exp(Color.ARGB_MAX - bot / Color.ARGB_MID))
				: (Color.ARGB_MAX - top / Color.ARGB_MID) / (Color.ARGB_MAX + Math.Exp(bot / Color.ARGB_MID - Color.ARGB_MAX));
		}

		public static Color SoftLight(Color top, Color bot, double pos, bool usegamma) {
			return usegamma
				? new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelSoftLight(top.GammaRed, bot.GammaRed),
					ChannelSoftLight(top.GammaGreen, bot.GammaGreen),
					ChannelSoftLight(top.GammaBlue, bot.GammaBlue),
					Color.USING_Y)
				: new Color(MergeOpacities(top.Opacity, bot.Opacity, pos),
					ChannelSoftLight(top.LinearRed, bot.LinearRed),
					ChannelSoftLight(top.LinearGreen, bot.LinearGreen),
					ChannelSoftLight(top.LinearBlue, bot.LinearBlue),
					Color.NOT_USING_Y);
		}
	}
}
