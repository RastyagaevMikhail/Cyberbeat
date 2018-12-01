using Sirenix.OdinInspector;

using System;

using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

using GameCore;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "SkinItem", menuName = "CyberBeat/Skin Item")]
    public class SkinItem : ScriptableObject
    {
        [Title ("Icon")]
        [HorizontalGroup ("Split", 135, LabelWidth = 135)]
        [HideLabel, PreviewField (90, ObjectFieldAlignment.Center)]
        public Sprite Icon;
        [Title ("Prefab")]
        [HorizontalGroup ("Split", 135, LabelWidth = 135)]
        [HideLabel, PreviewField (90, ObjectFieldAlignment.Center)]
        [AssetsOnly]
        public Object Prefab;
        public int Price;
        string bouthSaveKey { get { return "{0}.Bougth".AsFormat (name); } }
        public bool Bougth { get { return Tools.GetBool (bouthSaveKey); } set { Tools.SetBool (bouthSaveKey, value); } }
        public bool getByVideo;

        public void InitOnCreate (Sprite icon, Object obj, int price)
        {
            Icon = icon;
            Prefab = obj;
            Price = price;
            Bougth = (Price == 0);
        }
        public GameData gameData { get { return GameData.instance; } }
        public void TryBuy ()
        {
            Bougth = gameData.TryBuy (Price);
        }
        public bool CanBuy { get { return gameData.CanBuy (Price); } }
    }

}
