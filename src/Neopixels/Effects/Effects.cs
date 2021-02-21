using System;
using System.Drawing;
using static Neopixels.Tools;

namespace Neopixels
{
	public class RainbowEffect : Effect
	{
		public override void Execute(Strip strip)
		{
			var position = (long)Step();
			var a = (position % 100) / 100f;
			foreach (var light in strip.Lights)
			{
				var f = strip.EdgeDistance(0, light) / Length;
				f = f + a;
				var c = ColorFromHSV(f * 360, 1, 1);;
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Rainbow Colors"; }
		}

		public override string Description
		{
			get { return "A vibrant array of colors from the rainbow scrolling downwards."; }
		}
	}

	public class RainbowWipeEffect : Effect
	{
		public override void Execute(Strip strip)
		{
			var position = (long)Step();
			var a = (position % 100) / 100f;
			foreach (var light in strip.Lights)
			{
				var f = strip.EdgeDistance(3, light) / Length;
				f = f + a;
				var c = ColorFromHSV(f * 360, 1, 1);;
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Rainbow Wipe Colors"; }
		}

		public override string Description
		{
			get { return "A vibrant array of colors from the rainbow scrolling from left to right."; }
		}
	}

	public class RainbowChaseEffect : Effect
	{
		public override void Execute(Strip strip)
		{
			var position = (double)Step() / NumLights;
			foreach (var light in strip.Lights)
			{
				var h = (double)light.ID / NumLights / Length + position;
				var c = ColorFromHSV(h * 360, 1, 1); ;
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Rainbow Chasing Colors"; }
		}

		public override string Description
		{
			get { return "A vibrant array of colors from the rainbow chasing itself around the edge of the window frame."; }
		}
	}

	public class CabinetEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.Teal;
			Color2 = Color.Purple;
			Color3 = Color.Green;
		}

		public override void Execute(Strip strip)
		{
			var position = (double)Step() / NumLights;
			foreach (var light in strip.Lights)
			{
				var d = (double)light.ID / NumLights * Length + position;
				d = Tools.Fract(d);
				Color c;
				if (d < 0.33)
					c = Color1;
				else if (d < 0.66)
					c = Color2;
				else
					c = Color3;
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Cabinet Colors"; }
		}

		public override string Description
		{
			get { return "A selections three colors matching our cabinets chasing around the edge of the window frame."; }
		}
	}

	public class NightfallEffect : Effect
	{
		private float[] twinkles;
		private const float DURATION = 100;


		public override void Reset()
		{
			base.Reset();
			Color1 = Color.DarkBlue;
			Color2 = Color.Purple;
			twinkles = new float[NumLights];
			for (var i = 0; i < twinkles.Length; i++)
				twinkles[i] = 0;
		}

		public override void Execute(Strip strip)
		{
			var rand = new Random();
			var chance = Math.Abs(Speed) * 4;
			float fade = 10.25f - (float)Length;
			float percent = 0;
			Color c;
			int i = 0;
			foreach (var light in strip.Lights)
			{
				if (rand.Next(8000) > 7999 - chance)
					twinkles[i] = DURATION;
				else
				{
					twinkles[i] -= fade;
					if (twinkles[i] < 0)
						twinkles[i] = 0;
				}
				percent = twinkles[i] / DURATION;
				i++;
				c = ColorMix(percent, Color1, Color2);
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Storm Falls at night"; }
		}

		public override string Description
		{
			get { return "A blue stormy nighy delights peeks of sparkles and light."; }
		}
	}

	public class BarberShopEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.White;
			Color2 = Color.Red;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 20d;
			Point p = new Point(position, position);
			var scale = Length * 5;
			var half = scale / 2;
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(p);
				var stripe = d % scale;
				var color = stripe < half ? Color1 : Color2;
				light.Color = Mix(color);
			}
		}

		public override string Name
		{
			get { return "Barber Shop Pole"; }
		}

		public override string Description
		{
			get { return "An animated striping of red and white, much like a barber shop pole."; }
		}
	}

	public class MarqueEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.White;
			Color2 = Color.Black;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 20d;
			Point p = new Point(position, position);
			var scale = Length * 5;
			var half = scale / 2;
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(p);
				var stripe = d % scale;
				var color = stripe < half ? Color1 : Color2;
				light.Color = Mix(color);
			}
		}

		public override string Name
		{
			get { return "Marque Sign"; }
		}

		public override string Description
		{
			get { return "An white light dancing around a sign to create attraction."; }
		}
	}

	public class MSWindowsEffect : Effect
	{
		public override void Execute(Strip strip)
		{
			var w = strip.Width / 2;
			var h = strip.Height / 2;
			foreach (var light in strip.Lights)
			{
				var p = strip.EdgePoint(light);
				Color c;
				if (p.X < w)
					if (p.Y > h)
						c = Color.Red;
					else
						c = Color.Blue;
				else if (p.Y > h)
					c = Color.Green;
				else
					c = Color.Yellow;
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Microsoft Windows"; }
		}

		public override string Description
		{
			get { return "We are the borg."; }
		}
	}

    public class DebugEffect : Effect
    {
        public override void Execute(Strip strip)
        {
            foreach (var light in strip.Lights)
            {
                Color c;
                switch (strip.EdgeLight(light))
                {
                    case 0:
                        c = Color.Red;
                        break;
                    case 1:
                        c = Color.Green;
                        break;
                    case 2:
                        c = Color.Blue;
                        break;
                    default:
                        c = Color.White;
                        break;
                }
                light.Color = c;
            }
        }

        public override string Name
        {
            get { return "Solid Colors"; }
        }

        public override string Description
        {
            get { return "Solid red, green, blue, and white colors useful in debugging the strip."; }
        }
    }

    public class SubwayEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.White;
			Color2 = Color.Black;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() * 2;
			var a = Length * strip.Width;
			var b = a * 2;
			foreach (var light in strip.Lights)
			{
				var p = strip.EdgePoint(light);
				if ((position - p.X) % b < a)
					light.Color = Mix(Color1);
				else
					light.Color = Mix(Color2);
			}
		}

		public override string Name
		{
			get { return "Subway View"; }
		}

		public override string Description
		{
			get { return "A view from inside a speeding subway car."; }
		}
	}

	public class GelatinEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.Blue;
			Color2 = Color.Green;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 2;
			var radius = Length * strip.Width / 2;
			var source = strip.EdgePoint(position);
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(source);
				if (d > radius)
					light.Color = Mix(Color2);
				else
				{
					Color c = ColorMix((float)(d / radius), Color1, Color2);
					light.Color = Mix(c);
				}
			}
		}

		public override string Name
		{
			get { return "Gel Flowing"; }
		}

		public override string Description
		{
			get { return "A blue gelatin ball flowing through a sea of green."; }
		}
	}

	public class CampFireEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.Red;
			Color2 = Color.Yellow;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 20;
			var radius = Length * strip.Width / 2;
			var w = strip.Width;
			var h = strip.Height;
			var source = new Point(w / 2 + Math.Sin(position) * 20, h / -2);
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(source);
				if (d > radius)
					light.Color = Mix(Color2);
				else
				{
					Color c = ColorMix((float)(d / radius), Color1, Color2);
					light.Color = Mix(c);
				}
			}
		}

		public override string Name
		{
			get { return "Camp Fire Lighting"; }
		}

		public override string Description
		{
			get { return "A red glow from a campfire as the abse of the frame."; }
		}
	}

	public class SunsetEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
			Color1 = Color.Pink;
			Color2 = Color.Purple;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 20;
			var radius = Length * strip.Width;
			var w = strip.Width;
			var h = strip.Height;
			var source = new Point(w / 2, Math.Sin(position) * 50 - 50);
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(source);
				if (d > radius)
					light.Color = Mix(Color2);
				else
				{
					Color c = ColorMix((float)(d / radius), Color1, Color2);
					light.Color = Mix(c);
				}
			}
		}

		public override string Name
		{
			get { return "Sunset Sky"; }
		}

		public override string Description
		{
			get { return "The light in the sky as sunset approaches."; }
		}
	}

	public class RipplesEffect : Effect
	{
		public override void Reset()
		{
			base.Reset();
				Color1 = Color.Cyan;
				Color2 = Color.BlueViolet;
		}

		public override void Execute(Strip strip)
		{
			var position = Step() / 5;
			var radius = Length * 5;
			var w = strip.Width;
			var h = strip.Height;
			var source = new Point(w / 2, h / 2);
			foreach (var light in strip.Lights)
			{
				var d = strip.EdgePoint(light).Distance(source) + position;
				d = (Math.Sin(d / radius) + 1) / 2;
				Color c = ColorMix((float)d, Color1, Color2);
				light.Color = Mix(c);
			}
		}

		public override string Name
		{
			get { return "Water Ripples"; }
		}

		public override string Description
		{
			get { return "Ripples along the surface of water."; }
		}
}
}
