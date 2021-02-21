using System.Drawing;

namespace Neopixels
{
	/// <summary>
	/// Represents a LED which can be controlled by the Controller controller
	/// </summary>
	public class Light
	{
		/// <summary>
		/// LED which can be controlled by the Controller controller
		/// </summary>
		/// <param name="id">ID / index of the LED</param>
		public Light(long id)
		{
			ID = id;
			Color = Color.Empty;
		}

		/// <summary>
		/// Returns the ID / index of the LED
		/// </summary>
		public long ID { get; private set; }

		/// <summary>
		/// Gets or sets the color for the LED
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// Returns the RGB value of the color
		/// </summary>
		public int RGBValue { get { return Color.ToArgb(); } }

	}
}
