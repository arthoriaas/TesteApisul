using Newtonsoft.Json;
using System;


namespace ElevadorApisul
{
	public class Input
	{
		[JsonProperty("andar")]
		public int Andar { get; set; }
		[JsonProperty("elevador")]
		public char Elevador { get; set; }
		[JsonProperty("turno")]
		public char Turno { get; set; }

		public Input()
		{

		}


	}

}
