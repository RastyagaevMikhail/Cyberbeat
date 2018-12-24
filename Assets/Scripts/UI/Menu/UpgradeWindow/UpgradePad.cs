using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace CyberBeat
{
    public class UpgradePad : MonoBehaviour
    {
        public MainMenuController menu { get { return MainMenuController.instance; } }

        [Title ("Buttons")]
        [SerializeField] IAPButton UpgradeByCurencyButton;
        [SerializeField] Button UpgradeByNotesButton;
        [SerializeField] ButtonActionByVideoAds UpgradeByVideoAdsButton;
        [Title ("Images")]
        [SerializeField] Image mask;
        [SerializeField] Image Icon;
        [Title ("Texts")]
        [SerializeField] LocalizeTextMeshProUGUI Title;
        [SerializeField] TextMeshProUGUI PriceNotes;
        [SerializeField] TextMeshProUGUI CountVideoAds;
        [SerializeField] TextMeshProUGUI Info;
        [Title ("Animation")]
        // [SerializeField] Animator IconAnimator;
        [SerializeField] Ease fromIcon = Ease.InOutBack;
        [SerializeField] Ease toIcon = Ease.OutExpo;
        [SerializeField] float fromDurationIcon = 0.25f;
        [SerializeField] float toDurationIcon = 0.25f;
        [SerializeField] float toSizeIcon = 444f;
        [SerializeField] float fromSizeIcon = 344;

        [Title ("")]
        [SerializeField] bool isColors;
        [SerializeField] bool useGradient;
        [ShowIf ("useGradient")]
        [SerializeField] Gradient enabledGradient;
        [SerializeField] UpgradeData data;
        [SerializeField] List<Toggle> UpgradeOTiles;

        private void Start ()
        {
            data.Inti ();

            UpdateValues ();
            UpgradeByVideoAdsButton.Init (OnVideoShown);

        }
        private void OnVideoShown ()
        {
            data.OnWatchVideo ();
            UpdateValues ();
            ShakeIcon ();
        }

        private void ShakeIcon ()
        {
            if (Icon /* && IconAnimator */ )
            {
                var seq = DOTween.Sequence ();
                seq.Append (Icon.rectTransform.DOSizeDelta (toSizeIcon * Vector2.one, fromDurationIcon).SetEase (fromIcon));
                seq.Append (Icon.rectTransform.DOSizeDelta (fromSizeIcon * Vector2.one, toDurationIcon).SetEase (toIcon));
                seq.Play ();
            }
        }

        private void UpdateValues ()
        {
            for (int i = 0; i < UpgradeOTiles.Count; i++)
            {
                bool isActive = i <= data.CurrentUpgrade.Value;
                UpgradeOTiles[i].isOn = isActive;
                if (isActive && isColors)
                {
                    UpgradeOTiles[i].graphic.color = Colors.instance.colors[i];
                    UpgradeOTiles[i].targetGraphic.color = Colors.instance.colors[i];
                }
            }
            PriceNotes.text = data.Price.ToString ();

            bool reachedMaxLevel = data.ReachedMaxLevel;
            UpgradeByNotesButton.gameObject.SetActive (!reachedMaxLevel);
            UpgradeByVideoAdsButton.gameObject.SetActive (!reachedMaxLevel);

            mask.enabled = !reachedMaxLevel;

            UpgradeByCurencyButton.gameObject.SetActive (!reachedMaxLevel);

            if (!reachedMaxLevel)
            {
                Graphic targetGraphic = UpgradeByNotesButton.targetGraphic;
                UpgradeByNotesButton.onClick.RemoveAllListeners ();
                UpgradeByNotesButton.onClick.AddListener (
                    data.CanUpgrade ?
                    (UnityAction) UpgradeByNotes :
                    () => menu.header.OnCantBuyAnimation (UpgradeByNotesButton.targetGraphic));
            }

            int level = data.CurrentUpgrade.Value + 1;
            Info.text =
                isColors ?
                "{0}/{1}".AsFormat (level, 7) :
                "+{0}%".AsFormat (level * 10);

            CountVideoAds.text = "{0}".AsFormat (data.CountVideoAds);
        }

        public void UpgradeByCurency ()
        {
            data.Upgrade ();
            UpdateValues ();
            ShakeIcon ();
        }
        public void UpgradeByNotes ()
        {
            data.UpgradeByNotes ();
            UpdateValues ();
            ShakeIcon ();
        }
#if UNITY_EDITOR
        [Button]
        public void InitToggles ()
        {
            UpgradeOTiles = GetComponentsInChildren<Toggle> ().ToList ();
        }

        [Button]
        public void ValidateStylesAndValues ()
        {
            if (Icon) Icon.sprite = data.style.Icon;
            if (Title) Title.Id = data.locTitleTag.ToLower ();
            name = data.locTitleTag.ToCapitalize ();
            if (useGradient)
            {
                float count = UpgradeOTiles.Count;
                for (int i = 0; i < count; i++)
                {
                    var enableGraph = UpgradeOTiles[i].graphic;
                    UnityEditor.Undo.RecordObject (enableGraph, "enableGraph_{0}".AsFormat (i));
                    enableGraph.color = enabledGradient.Evaluate ((float) (i + 1) / count);
                }
            }
            foreach (var controller in GetComponentsInChildren<StyleController> ())
            {
                controller.Validate (data.style);
            }
        }
#endif

    }
}
