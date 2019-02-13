using GameCore;

using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
    public class SkinsMenuController : MonoBehaviour
    {
        [Header ("Data")]
        [SerializeField] SkinsDataCollection skinsData;
        [SerializeField] SkinsEnumDataSelector skinsSelector;

        [Header ("UI")] //-----------------------------------------------------------------------
        [SerializeField] Text videoCountTextComponent;
        [SerializeField] ContentButton BuyButton;
        [SerializeField] ContentButton SelectButton;
        [SerializeField] ButtonActionByVideoAds WatchAdsFromBuyButton;
        [SerializeField] SkinsScrollList scrollList;
        [SerializeField] Color selectedColor = Color.green, unselectedColor = Color.white;

        [Header ("Events")] //-----------------------------------------------------------------------
        [SerializeField] UnityEventGraphic onCantNotEnuthMoney;
        [SerializeField] UnityEventInt onSkinSelceted;

        int _skinIndex;
        SkinItem skin => skinsSelector[skinsData.SkinType][_skinIndex];
        int SkinIndex { get { return skinsData.SkinIndex; } set { skinsData.SkinIndex = value; } }
        private SkinType skinType;

        bool currentIsSelected { get { return _skinIndex == SkinIndex; } }

        public void OnWatchetdVideo ()
        {
            bool buyed = skin.BuyByVideo ();
            if (buyed) _Select();

            _UpdateValues (skin);
        }

        bool currentSkinIsRaod = false;

        public void _OnSkinTypeChanged (SkinType skinType)
        {
            currentSkinIsRaod = skinsData.isRoadType (skinType);
            if (currentSkinIsRaod)
                _skinIndex = skinsData.RoadSkinTypeIndex;
            else
            {
                //? При выходе из скинов дороги поставить текщий скин на материал
                scrollList.ReSelectionTo (skinsData.RoadSkinTypeIndex);
            }
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

            BuyButton.text = skin.Price.ToString ();
            videoCountTextComponent.text = skin.VideoCount.ToString ();
            UpdateSelection (skin);
        }
        public void BuySkin ()
        {
            if (skin.TryBuy ())
                _Select ();
            else
                onCantNotEnuthMoney.Invoke (BuyButton.targetGraphic);

            _UpdateValues (skin);
        }

        void UpdateSelection (SkinItem skin)
        {
            if (!skin) return;

            SelectButton.SetActive (skin.IsAvalivable);

            SelectButton.textLocalizationID = (currentIsSelected ? "selected" : "select");
            SelectButton.textColor = currentIsSelected ? selectedColor : unselectedColor;
            SelectButton.interactable = !currentIsSelected;
        }
        public void _Select ()
        {
            SkinIndex = _skinIndex;
            _UpdateValues (skin);
            onSkinSelceted.Invoke (_skinIndex);
        }
    }
}
