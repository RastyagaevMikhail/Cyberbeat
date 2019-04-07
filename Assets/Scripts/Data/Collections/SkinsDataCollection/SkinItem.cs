using System;

using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

using GameCore;
namespace CyberBeat
{
    public abstract class SkinItem : ScriptableObject
    {
        [Header ("Icon")]
        public Sprite Icon;
        [Header ("Prefab")]
        public Object Prefab;
        public int Price;
        [SerializeField] int videoPrice;
        string videoCountKey => $"{name}.VideoCount";
        public int VideoCount
        {
            get => PlayerPrefs.GetInt (videoCountKey, videoPrice);
            set => PlayerPrefs.SetInt (videoCountKey, value);
        }

        public void ResetDefault ()
        {
            Bougth = (Price == 0);
            VideoCount = videoPrice;
        }

        string bouthSaveKey => $"{name}.Bougth";

        public bool Bougth
        {
            get => Tools.GetBool (bouthSaveKey, (Price == 0));
            set => Tools.SetBool (bouthSaveKey, value);
        }

        [SerializeField] protected SkinType type;
        public void InitOnCreate (Sprite icon, Object obj, int price, SkinType type)
        {
            Icon = icon;
            Prefab = obj;
            Price = price;
            Bougth = (Price == 0);
            this.type = type;
        }
        public bool TryBuy () { return Bougth = Buyer.TryBuyDefaultCurency (Price); }
        public bool BuyByVideo () => Bougth = (--VideoCount <= 0);
        public bool IsAvalivable { get { return Bougth; } }
        // public bool CanBuy { get { return gameData.CanBuy (Price); } }

        public abstract void Apply (Object target, params object[] args);
        public bool Applyed<T> (out T target, Object targetObject) where T : UnityEngine.Object
        {
            target = targetObject as T;
            if (target == null)
            {
                GameObject go = (targetObject as GameObject);
                if (!go) return false;
                target = go.GetComponent<T> ();
            }

            return target;
        }
    }

}
