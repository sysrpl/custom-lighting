using System;
using System.Linq;
using Codebot.Web;
using Neopixels;

namespace Splashdown.Lights.Web
{
	[DefaultPage("/typescript/pages/home.html", IsTemplate = true)]
	public class HomePage : PageHandler
	{
		public bool IsRaspberry { get { return Tools.IsRaspberry; } }


		[MethodPage("reset")]
		public void StopReset()
		{
			Global.Reset();
		}

		[MethodPage("stop")]
		public void StopMethod()
		{
			Global.Stop();
		}

		[MethodPage("effects")]
		public void EffectsMethod()
		{
			var effects = Global.GetEffectNames();
			var names = String.Join(",", effects.Select(e => $"\"{e}\""));
			Write($"[ {names} ]");
		}

		[MethodPage("geteffect")]
		public void GetEffectsMethod()
		{
			var name = Global.GetEffectName();
			Write(name);
		}

		[MethodPage("seteffect")]
		public void SetEffectsMethod()
		{
			var name = Read("name");
			Global.SetEffectName(name);
		}

		[MethodPage("getdetails")]
		public void GetDetailsMethod()
		{
			var details = Global.GetDetails();
			Write(details);
		}

		[MethodPage("getborder")]
		public void GetBorder()
		{
			Write(Global.GetBorder());
		}

		[MethodPage("getpixels")]
		public void GetPixels()
		{
			Write(Global.GetPixels());
		}

		[MethodPage("setspeed")]
		public void SetSpeedMethod()
		{
			if (double.TryParse(Read("value"), out double d)) Global.SetSpeed(d);
		}

		[MethodPage("setlength")]
		public void SetLengthMethod()
		{
			if (double.TryParse(Read("value"), out double d)) Global.SetLength(d);
		}

		[MethodPage("setbrightness")]
		public void SetBrightnessMethod()
		{
			if (double.TryParse(Read("value"), out double d)) Global.SetBrightness(d);
		}

		[MethodPage("setsaturation")]
		public void SetSaturationMethod()
		{
            if (double.TryParse(Read("value"), out double d)) Global.SetSaturation(d);
        }

        [MethodPage("setcolor")]
		public void SetColor1Method()
		{
            if (TryRead("index", out int index))
                Global.SetColor(Read("value"), index);
        }

        [MethodPage("framerate")]
		public void FramerateMethod()
		{
			Write(Global.GetFramerate());
		}
	}
}
