using DG.Tweening;

using GameCore;

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
        public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
        private Animator _animator = null;
        public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }
        int hashShow,
        hashHide;
        SkinItem skin
        {
            get
            {
                if (!skinsData.skinsSelector.ContainsKey (skinType)) return null;
                return skinsData.skinsSelector[skinsData.SkinType][_skinIndex];
            }
        }
        int SkinIndex { get { return skinsData.SkinIndex; } set { skinsData.SkinIndex = value; } }

        [Header ("UI")] //-----------------------------------------------------------------------
        [SerializeField] ContentButton BuyButton;
        [SerializeField] ContentButton SelectButton;

        [SerializeField] ButtonActionByVideoAds WatchAdsFromBuyButton;
        [SerializeField] SkinsScrollList scrollList;
        [SerializeField] Color selectedColor = Color.green, unselectedColor = Color.white;
        [SerializeField] UnityEventGraphic onCantNotEnuthMoney;

        private void Awake ()
        {

            scrollList.UpdateData (skinsData.RoadSkins.Select (si => new SkinsScrollData (si)).ToList ());

            WatchAdsFromBuyButton.Init (OnWatchetdVideo);
            hashShow = Animator.StringToHash ("Show");
            hashHide = Animator.StringToHash ("Hide");
            // CurrentSelectedIndex = SkinIndex;
        }
        public void Show ()
        {
            gameObject.SetActive (true);
            animator.Play (hashShow);
            scrollList.UpdateData (skinsData.RoadSkins.Select (si => new SkinsScrollData (si)).ToList ());
        }

        public void Hide ()
        {
            if (gameObject.activeInHierarchy)
                animator.Play (hashHide);
        }
        private void OnWatchetdVideo ()
        {
            skin.getByVideo = true;

            _UpdateValues (skin);
        }

        bool currentSkinIsRaod = false;
        public void _OnSkinTypeChanged (Object obj)
        {
            skinType = obj as SkinType;
            currentSkinIsRaod = skinsData.isRoadType (skinType);
            if (currentSkinIsRaod)
                _skinIndex = skinsData.RoadSkinTypeIndex;
            else
            {
                scrollList.ReSelectionTo (skinsData.RoadSkinTypeIndex);
            }
            (currentSkinIsRaod ? (System.Action) Show : Hide) ();
            _UpdateValues (skin);
        }
        public void _SetSkinIndex (int skinIdex)
        {
            _skinIndex = skinIdex;
            _UpdateValues (skin);
        }

        
        public void _UpdateValues (SkinItem skin)
        {
            if (!skin) return;
            WatchAdsFromBuyButton.gameObject.SetActive (!skin.IsAvalivable);

            BuyButton.SetActive (!skin.IsAvalivable);

            BuyButton.onClick.RemoveAllListeners ();
            BuyButton.onClick.AddListener (
                skin.CanBuy ?
                (UnityAction) BuySkin :
                () => onCantNotEnuthMoney.Invoke (BuyButton.targetGraphic)
            );

            BuyButton.text = skin.Price.ToString ();
            UpdateSelection (skin);
        }
        private void BuySkin ()
        {
            if (skin.TryBuy ())
                _Select ();

            _UpdateValues (skin);
        }

        [SerializeField]
        int _skinIndex;

        [SerializeField] UnityEventInt onSkinSelceted;
        private SkinType skinType;

        bool currentIsSelected { get { return _skinIndex == SkinIndex; } }

        void UpdateSelection (SkinItem skin)
        {
            if (!skin) return;
            // Debug.Log ("Update Selection");
            // Debug.LogFormat ("skinIsAvalivable = {0}", skinIsAvalivable);
            // Debug.LogFormat ("currentSkinIsRaod = {0}", currentSkinIsRaod);
            SelectButton.SetActive (skin.IsAvalivable /* && !currentSkinIsRaod */ );
            // Debug.LogFormat ("currentIsSelected = {0}", currentIsSelected);
            SelectButton.textLocalizationID = (currentIsSelected ? "selected" : "select");
            SelectButton.textColor = currentIsSelected ? selectedColor : unselectedColor;
            SelectButton.interactable = !currentIsSelected;
        }
        public void _Select ()
        {
            // Debug.Log ("_Select");,, ,   ,SkinsMenuController.cs:56ty
            SkinIndex = _skinIndex;
            UpdateSelection (skin);
            onSkinSelceted.Invoke (_skinIndex);
        }
    }
}
