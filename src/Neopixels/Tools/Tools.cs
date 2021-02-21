using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.IO;

namespace Neopixels
{
	public static class Tools
	{
		public static Assembly LightsAssembly { get { return Assembly.GetExecutingAssembly(); } }
		public static int NumLights { get; set; }

		public static string RunProgram(string program, params string[] args)
		{
			var s = new StringBuilder();
			var quoted = args.Select(item =>
			{
				if (item.Contains("\""))
					return item;
				if (item.Contains(" "))
					return $"{item}";
				return item;
			});
			string arguments = null;
			if (args.Length > 0)
				arguments = String.Join(" ", quoted);
			using (var proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = program,
					Arguments = arguments,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			})
			{
				proc.Start();
				while (!proc.StandardOutput.EndOfStream)
				{
					var line = proc.StandardOutput.ReadLine();
					s.AppendLine(line);
				}

			}
			return s.ToString();
		}

		private static bool? isRaspberry = null;

		public static bool IsRaspberry
		{

			get
			{
				if (isRaspberry.HasValue)
					return isRaspberry.Value;
				// alternate: RunProgram("lscpu").Contains("armv");
				isRaspberry = Directory.Exists("/home/pi");
				return isRaspberry.Value;
			}
		}

		public static Color ColorMix(float percent, Color c1, Color c2)
		{
			if (percent <= 0.0)
				return c1;
			if (percent >= 1.0)
				return c2;
			var r2 = c2.R * percent;
			var g2 = c2.G * percent;
			var b2 = c2.B * percent;
			percent = 1.0f - percent;
			var r1 = c1.R * percent;
			var g1 = c1.G * percent;
			var b1 = c1.B * percent;
			return Color.FromArgb(255, Convert.ToByte(r1 + r2), Convert.ToByte(g1 + g2), Convert.ToByte(b1 + b2));
		}

		public static Color ColorFromHSV(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);
			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));
			switch (hi)
			{
				case 0:
					return Color.FromArgb(255, v, t, p);
				case 1:
					return Color.FromArgb(255, q, v, p);
				case 2:
					return Color.FromArgb(255, p, v, t);
				case 3:
					return Color.FromArgb(255, p, q, v);
				case 4:
					return Color.FromArgb(255, t, p, v);
				default:
					return Color.FromArgb(255, v, p, q);
			}
		}

		public static double Fract(double d)
		{
			return d - Math.Truncate(d);
		}
	}
}
