using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Neopixels
{
	/// <summary>
	/// Represents the channel which holds the LEDs
	/// </summary>
	public class Channel
	{
		public Channel() : this(0, 0) { }
		
		public Channel(long ledCount, int gpioPin) : this(ledCount, gpioPin, 255, false, Neopixels.StripType.Unknown)	{ }

		public Channel(long ledCount, int gpioPin, byte  brightness, bool invert, StripType stripType)
		{
			GPIOPin = gpioPin;
			Invert = invert;
			Brightness = brightness;
			StripType = stripType;

			var ledList = new List<Light>();
			for(int i= 0; i<= ledCount-1; i++)
			{
				ledList.Add(new Light(i));
			}
			Lights = new ReadOnlyCollection<Light>(ledList);
		}
		
		/// <summary>
		/// Returns the GPIO pin which is connected to the LED strip
		/// </summary>
		public int GPIOPin { get; private set; }

		/// <summary>
		/// Returns a value which indicates if the signal needs to be inverted.
		/// Set to true to invert the signal (when using NPN transistor level shift).
		/// </summary>
		public bool Invert { get; private set; }

		/// <summary>
		/// Gets or sets the brightness of the LEDs
		/// 0 = darkest, 255 = brightest
		/// </summary>
		public byte Brightness { get; set; }

		/// <summary>
		/// Returns the type of the channel.
		/// The type defines the ordering of the colors.
		/// </summary>
		public StripType StripType { get; private set; }

		/// <summary>
		/// Returns all lights on this channel
		/// </summary>
		public ReadOnlyCollection<Light> Lights { get; private set; }
	}
}
