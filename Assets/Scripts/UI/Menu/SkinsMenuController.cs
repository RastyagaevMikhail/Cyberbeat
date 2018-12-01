using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace CyberBeat
{
    public class SkinsMenuController : RectTransformObject
    {

        private static SkinsMenuController _instance = null;
        public static SkinsMenuController instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<SkinsMenuController> (); return _instance; } }
        public MainMenuController menu { get { return MainMenuController.instance; } }
        public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
        SkinItem skin { get { return skinsData.currrentSkinItem; } }

        [Title ("Data")] //-----------------------------------------------------------------------
        [SerializeField] UnityObjectVariable CurrentSkinType;

        [Title ("UI")] //-----------------------------------------------------------------------
        [SerializeField] Button BuyButton;
        [SerializeField] TextMeshProUGUI Price;
        [SerializeField] ButtonActionByVideoAds WatchAdsFromBuyButton;

        [Title ("Skins Objects")] //-----------------------------------------------------------------------
        [SerializeField] GameObject Gate;
        [SerializeField] Material Road;
        [SerializeField] GameObject player;
        [Title ("Action Selector")] //-----------------------------------------------------------------------
        [SerializeField] Dictionary<SkinType, System.Action> SkinChangeActionSelector = new Dictionary<SkinType, System.Action> ();
        private void Awake ()
        {
            skinsData.SkinType = CurrentSkinType.As<SkinType> ();

            WatchAdsFromBuyButton.Init (OnWatchetdVideo);
            OnSkinSelected ();
            UpdateValues ();
        }

        private void OnWatchetdVideo ()
        {
            skin.getByVideo = true;

            UpdateValues ();

        }

        private void OnEnable ()
        {
            CurrentSkinType.OnValueChanged += OnSkinTypeChanged;
        }
        private void OnDisable ()
        {
            CurrentSkinType.OnValueChanged -= OnSkinTypeChanged;
        }
        private void OnSkinSelected ()
        {
            SkinChangeActionSelector[skinsData.SkinType] ();
            UpdateValues ();
        }

        public void OnGateSeleted ()
        {
            InstatiateNewObject (ref Gate);
            Gate.transform.localScale = Vector3.one * 5;
        }

        public void OnRoadChanged ()
        {
            Road.SetTexture ("_EmissionMap", skin.Prefab as Texture);
        }

        public void OnSelectPlayer ()
        {
            InstatiateNewObject (ref player);
        }

        private void InstatiateNewObject (ref GameObject obj)
        {
            Vector3 position = obj.transform.position;
            Transform parent = obj.transform.parent;
            Destroy (obj);
            obj = Instantiate (skin.Prefab as GameObject);
            obj.transform.SetParent (parent);
            obj.transform.position = position;
        }

        private void OnSkinTypeChanged (UnityEngine.Object obj)
        {
            skinsData.SkinType = CurrentSkinType.As<SkinType> ();
        }
        void UpdateValues ()
        {
            bool skinNotAvalivable = !skin.Bougth || !skin.getByVideo;
            WatchAdsFromBuyButton.gameObject.SetActive (skinNotAvalivable);

            BuyButton.gameObject.SetActive (skinNotAvalivable);

            BuyButton.onClick.RemoveAllListeners ();
            BuyButton.onClick.AddListener (
                skin.CanBuy ?
                (UnityAction) BuySkin :
                () => menu.header.OnCantBuyAnimation (BuyButton.targetGraphic)
            );

            Price.text = skin.Price.ToString ();

        }
        private void BuySkin ()
        {
            skin.TryBuy ();
            UpdateValues ();
        }

    }
}
