using System.Drawing;

namespace WTimeLogger.Report
{
	internal class ColorsHelper
	{
		private static readonly string[] Colors = {"#ffd1dc", "#dcd0ff", "#fffdd0", "#fafad2", "#ffebcd", "#fffacd", "#ccccff", "#e5e4e2", "#fbcce7", "#f0ead6", "#ffe4c4", "#ffc1cc", "#ffc0cb", "#efdecd", "#ffbcd9", "#f4bbff", "#dcdcdc", "#faf0be", "#ffdab9", "#f4c2c2", "#ffb7c5", "#ffb6c1", "#ffe5b4", "#eae0c8", "#b2ffff", "#fdbcb4", "#d0f0c0", "#e0b0ff", "#fae7b5", "#fdd5b1", "#dbd7d2", "#e3dac9", "#fbceb1", "#ffdead", "#ecebbd", "#fbaed2", "#f5deb3", "#d6cadd", "#fadfad", "#d3d3d3", "#ffa6c9", "#f6adc6", "#ffcba4", "#bcd4e6", "#fad6a5", "#f3e5ab", "#afeeee", "#edc9af", "#fba0e3", "#9bddff", "#aaf0d1", "#abcdef", "#ace5ee", "#eee8aa", "#ff9999", "#ffff99", "#98ff98", "#d8bfd8", "#b0e0e6", "#98fb98", "#add8e6", "#c4c3d0", "#cfcfc4", "#e7accf", "#fdfd96", "#a1caf1", "#a4dded", "#ff91a4", "#f49ac2", "#ace1af", "#addfad", "#ddadaf", "#fc8eac", "#ffa089", "#d19fe8", "#ef98aa", "#ffbd88", "#f78fa7", "#9aceeb", "#f7e98e", "#fc89ac", "#a8e4a0", "#c9c0bb", "#87cefa", "#c0c0c0", "#7fffd4", "#90ee90", "#93ccea", "#9bc4e2", "#aec6cf", "#dda0dd", "#f984e5", "#f984ef", "#7df9ff", "#c9a0dc", "#f0e68c", "#ffc87c", "#89cff0", "#ffa07a", "#bf94e4", "#99badd", "#a0d6b4", "#f77fbe", "#f8de7e", "#ff77ff", "#76ff7a", "#b19cd9", "#e68fac", "#96ded1", "#87ceeb", "#a2a2d0", "#a2add0", "#df73ff", "#f0dc82", "#f88379", "#fc74fd", "#b2beb5", "#e6be8a", "#ee82ee", "#eedc82", "#f08080", "#e18e96", "#73c2fb", "#ff6fff", "#c8a2c8", "#fc6c85", "#ff69b4", "#ff8c69", "#deaa88", "#deb887", "#ff66cc", "#ff9966", "#ffff66", "#a3c1ad", "#cb99c9", "#c9dc87", "#e9967a", "#88d8c0", "#f8d568", "#ff6961", "#96c8a2", "#d2b48c", "#f56991", "#fe6f5e", "#fb607f", "#fcf75e", "#fada5e", "#fbec5d", "#a9ba9d", "#fe59c2", "#ffdb58", "#7b68ee", "#e4717a", "#e5aa70", "#77dd77", "#c3b091", "#cc8899", "#e9d66b", "#f4a460", "#ff55a3", "#b39eb5", "#e4d96f", "#a9a9a9", "#6495ed", "#fd5e53", "#ff7f50", "#fff44f", "#de6fa1", "#e66771", "#fe4eda", "#8fbc8f", "#9370db", "#bc8f8f", "#db7093", "#da70d6", "#f75394", "#b2ec5d", "#ff6e4a", "#ff5349", "#ff6347", "#ffb347", "#966fd6", "#66ddaa", "#779ecb", "#9457eb", "#aa98a9", "#c2b280", "#cd9575", "#ff43a4", "#ff8243", "#ffa343", "#91a3b0", "#979aaa", "#da8a67", "#ffae42", "#5b92e5", "#c08081", "#cf71af", "#e1a95f", "#f64a8a", "#ff4040", "#e2725b", "#8878c3", "#9d81ba", "#b784a7", "#de5d83", "#e75480", "#f9429e", "#bc987e", "#e3a857", "#e97451", "#8c92ac", "#b666d2", "#93c572", "#4166f5", "#73a9c2", "#ff5a36", "#ff355e", "#ffe135", "#6699cc", "#9966cc", "#cc6666", "#e25098", "#ff33cc", "#ff9933", "#ffcc33", "#967bb6", "#bdda57", "#d99058", "#ffff31", "#a99a86", "#d68a59", "#9678b6", "#adff2f", "#71bc78", "#eb4c42", "#6050dc", "#746cc0", "#9ab973", "#c19a6b", "#ecd540", "#cd5c5c", "#74c365", "#ba55d3", "#bdb76b", "#6a5acd", "#b57281", "#ec3b83", "#bab86c", "#fe28a2", "#b76e79", "#f4c430", "#4169e1", "#85bb65", "#b38b6d", "#e5b73b", "#f0e130", "#fefe22", "#ef3038", "#ff2052", "#5a4fcf", "#1e90ff", "#c5b358", "#e08d3c", "#ff1dce", "#cc4e5c", "#ed872d", "#48d1cc", "#4997d0"};

		private int CurrentColorPointer = Colors.Length - 1;

		public string GetNextColor()
		{
			CurrentColorPointer++;
			if (CurrentColorPointer >= Colors.Length) CurrentColorPointer = 0;
			return Colors[CurrentColorPointer];
		}

		public static string InvertHexColor(string hexColor)
		{
			var color = ColorTranslator.FromHtml(hexColor);
			var invertedColor = Color.FromArgb(color.ToArgb() ^ 0xffffff);
			return ColorTranslator.ToHtml(invertedColor);
		}
	}
}