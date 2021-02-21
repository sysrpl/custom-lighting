using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Threading;

namespace Neopixels
{
	public class Strip : IDisposable
	{
		private Settings settings = null;
		private Channel channel = null;
		private Controller controller = null;

		public Strip(long numLights)
		{
			if (numLights < 0)
				numLights = 1;
			this.Counter = 0;
			this.Direction = 1;
			this.Running = true;
			this.settings = Settings.CreateDefaultSettings();
			this.channel = new Channel(numLights, 18, 255, false, StripType.WS2812_STRIP);
			this.settings.Channels[0] = channel;
			if (Tools.IsRaspberry)
				this.controller = new Controller(this.settings);
			else
				this.controller = null;
		}

        public Controller Controller => controller;

        public byte Brightness
		{
			get { return channel.Brightness; }
			set { channel.Brightness = value; }
		}

		public Color this[int index]
		{
			get
			{
				return Lights[index].Color;
			}
			set
			{
				Lights[index].Color = value;
			}
		}

		public ReadOnlyCollection<Light> Lights
		{
			get
			{
				return this.channel.Lights;
			}
		}

		public bool Running { get; set; }
		public long Counter { get; set; }
		public int Direction { get; set; }

		public void Delay(int milliseconds)
		{
			Thread.Sleep(milliseconds);
		}

		public void Clear()
		{
			if (controller == null)
				return;
			foreach (var light in Lights)
				light.Color = Color.Black;
			controller.Render();
		}


		private int[] edges = new int[] { 0, 0, 0, 0 };

		public void DefineEdge(int bottom, int right, int top, int left)
		{
			edges[0] = bottom;
			edges[1] = right;
			edges[2] = top;
			edges[3] = left;
		}

		public double AspectRatio
		{
			get
			{
				return Width / Height;
			}
		}

		public double Width
		{
			get { return ((edges[0] - 1) + (edges[2] - edges[1] - 1)) / 2; }
		}

		public double Height
		{
			get { return ((edges[1] - edges[0] - 1) + (edges[3] - edges[2] - 1)) / 2; }
		}

		public long EdgeLight(Light light)
		{
			var id = light.ID;
			if (id < edges[0])
				return 0;
			if  (id < edges[1])
				return 1;
			if  (id < edges[2])
				return 2;
			return 3;
		}

		public float EdgeDistance(int edge, Light light, float max = 1.0f)
		{
			var e = EdgeLight(light);
			if (e == edge)
				return 0.0f;
			var id = light.ID;
			switch (edge)
			{
				case 0:
					{
						if (e == 2)
							return max;
						float a, b;
						if (e == 1)
						{
							a = edges[1] - edges[0];
							b = id - edges[0] + 1;
							return max * (b / a);
						}
						else
						{
							a = edges[3] - edges[2];
							b = id - edges[2] + 1;
							return max - max * (b / a);
						}
					}
				case 1:
					{
						if (e == 3)
							return max;
						float a, b;
						if (e == 0)
						{
							a = edges[1];
							b = id + 1;
							return max - max * (b / a);
						}
						else
						{
							a = edges[2] - edges[1];
							b = id - edges[1] + 1;
							return max * (b / a);
						}
					}
				case 3:
					{
						if (e == 1)
							return max;
						float a, b;
						if (e == 0)
						{
							a = edges[1];
							b = id + 1;
							return max * (b / a);
						}
						else
						{
							a = edges[2] - edges[1];
							b = id - edges[1] + 1;
							return max - max * (b / a);
						}
					}
			}
			return 0.0f;
		}

		public Point EdgePoint(Light light)
		{
			var id = light.ID;
			if (id < edges[0])
				return new Point(id + 2, 0);
			if (id < edges[1])
				return new Point(edges[0] + 1, id - edges[0] + 1);
			if (id < edges[2])
				return new Point(edges[2] - id, edges[1] - edges[0] + 1);
			return new Point(0, edges[3] - id - 1);
		}

		public Point EdgePoint(double distance)
		{
			var total = (double)Lights.Count;
			distance = distance % total;
			if (distance < edges[0])
				return new Point(distance + 2, 0);
			if (distance < edges[1])
				return new Point(edges[0] + 1, distance - edges[0] + 1);
			if (distance < edges[2])
				return new Point(edges[2] - distance, edges[1] - edges[0] + 1);
			return new Point(0, edges[3] - distance - 1);
		}

		public void Render()
		{
			if (controller == null)
				return;
			controller.Render();
		}

		public void Dispose()
		{
			if (controller != null)
				controller.Dispose();
			controller = null;
		}
	}
}
