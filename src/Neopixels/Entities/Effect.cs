using System;
using System.Drawing;

namespace Neopixels
{
	public abstract class Effect
	{
		private double position;
		private int direction;

		private void InternalReset()
		{
			position = uint.MaxValue;
			Direction = 1;
			Speed = Length = Brightness = Saturation = 1;
			Color1 = Color2 = Color3 = Color.White;
		}

		public Effect()
		{
			InternalReset();
		}

		public virtual void Reset()
		{
            InternalReset();
		}

		protected double Step()
		{
			double p;
			p = position + Speed * direction;
			position = p;
			return p;
		}

		public int Direction
		{
			get
			{
				return direction;
			}
			set
			{
				if (value != -1)
					direction = 1;
				else
					direction = -1;
			}
		}

		public Color Mix(Color color)
		{
			var r = (byte)(0.21 * color.R);
			var g = (byte)(0.72 * color.G);
			var b = (byte)(0.07 * color.B);
			var gray = r + g + b;
			color = Tools.ColorMix((float)Saturation, Color.FromArgb(gray, gray, gray), color);
			return Tools.ColorMix((float)Brightness, Color.Black, color);
		}

		public double Speed { get; set; }
		public double Length { get; set; }
		public double Brightness { get; set; }
		public double Saturation { get; set; }
		public Color Color1 { get; set; }
		public Color Color2 { get; set; }
		public Color Color3 { get; set; }
		public virtual string Name { get { return "Long Name"; } }
		public virtual string Description { get { return "Description"; } }
		public abstract void Execute(Strip strip);
	}
}
