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
        [SerializeField] SkinsEnumDataSelector skinsSelector;
        [SerializeField] SkinIndexSelector skinIndexsSelector;
        [SerializeField] SkinType currentSkinType;

        [Header ("UI")] //-----------------------------------------------------------------------
        [SerializeField] ContentButton BuyButton;
        [SerializeField] ContentButton SelectButton;
        [SerializeField] ContentButton WatchAdsFromBuyButton;
        [SerializeField] Color selectedColor = Color.green, unselectedColor = Color.white;

        [Header ("Events")] //-----------------------------------------------------------------------
        [SerializeField] UnityEventGraphic onCantNotEnuthMoney;
        [SerializeField] UnityEventInt onSkinSelceted;

        public int HighlightedSkinIndex { set { highlightedSkinIndex = value; UpdateValues (skin); } }
        int highlightedSkinIndex;
        SkinItem skin => skinsSelector[currentSkinType][highlightedSkinIndex];
        int selectedSkinIndex
        {
            get { return skinIndexsSelector[currentSkinType].Value; }
            set { skinIndexsSelector[currentSkinType].Value = value; }
        }

        bool currentHighlightedIsSelected { get { return highlightedSkinIndex == selectedSkinIndex; } }

        public void OnWatchetdVideo ()
        {
            bool buyed = skin.BuyByVideo ();
            if (buyed) _Select ();

            UpdateValues (skin);
        }
        public void _OnSkinTypeChanged (SkinType skinType)
        {
            currentSkinType = skinType;
            //  подсвечиваем скин при смене типа,
            //  в который был выбран в другой категории при прошлом выборе,
            //  на основе смены типа скина
            HighlightedSkinIndex = selectedSkinIndex;
            
            UpdateValues (skin);
        }

        void UpdateValues (SkinItem skin)
        {
            if (!skin) return;

            WatchAdsFromBuyButton.SetActive (!skin.IsAvalivable);
            WatchAdsFromBuyButton.text = skin.VideoCount.ToString ();

            BuyButton.SetActive (!skin.IsAvalivable);
            BuyButton.text = skin.Price.ToString ();

            UpdateSelection (skin);
        }
        public void OnTryBuySkin ()
        {
            if (skin.TryBuy ())
                _Select ();
            else
                onCantNotEnuthMoney.Invoke (BuyButton.targetGraphic);

            UpdateValues (skin);
        }

        void UpdateSelection (SkinItem skin)
        {
            if (!skin) return;

            SelectButton.SetActive (skin.IsAvalivable);

            SelectButton.textLocalizationID = (currentHighlightedIsSelected ? "selected" : "select");
            SelectButton.textColor = currentHighlightedIsSelected ? selectedColor : unselectedColor;
            SelectButton.interactable = !currentHighlightedIsSelected;
        }
        public void _Select ()
        {
            selectedSkinIndex = highlightedSkinIndex;
            UpdateValues (skin);
            onSkinSelceted.Invoke (highlightedSkinIndex);
        }
    }
}
