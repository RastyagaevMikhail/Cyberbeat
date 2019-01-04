using GameCore;

using System;
using System.Collections.Generic;
namespace CyberBeat
{
	[Serializable]
	public class Preset
	{
		public int Id;
		public List<SpawnedObject> Objects;
	}
}
