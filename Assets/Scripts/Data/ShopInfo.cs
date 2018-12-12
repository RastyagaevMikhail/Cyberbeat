using GameCore;

using Sirenix.OdinInspector;

namespace CyberBeat
{
	[System.Serializable]
	public class ShopInfo
	{
		public GameData gameData { get { return GameData.instance; } }
		public bool PlayByWatch;
		public int Price = 2000;
		public string SaveKey;
		[ShowInInspector]
		public bool Buyed { get { return Tools.GetBool (SaveKey, false); } set { Tools.SetBool (SaveKey, value); } }
		public bool TryBuy ()
		{
			Buyed = gameData.TryBuy (Price);
			return Buyed;
		}
	}
}
