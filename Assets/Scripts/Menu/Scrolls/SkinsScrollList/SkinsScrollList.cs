using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;
namespace CyberBeat
{

    public class SkinsScrollList : HorizontalClampedScrolling<SkinsScrollData>
    {
        [SerializeField] float durationScroll = 0.25f;
        [SerializeField] SkinType skinType;
        [SerializeField] SkinsEnumDataSelector selector;
        [SerializeField] IntVariable indexVariable;
        [SerializeField] Material material;
        [SerializeField] UnityEventInt skinHightlighted;
        [SerializeField] UnityEventInt skinSelected;
        Dictionary<int, SkinItem> skinsHash = new Dictionary<int, SkinItem> ();

        int indexSelected
        {
            get => indexVariable.Value;
            set => indexVariable.Value = value;
        }
        public override int panCount => selector[skinType].Count;

        void Awake ()
        {
            Initialize ();
        }
        private void OnEnable ()
        {
            selector[skinType][indexSelected].Apply (material);
            skinHightlighted.Invoke (indexSelected);
            skinSelected.Invoke (indexSelected);
            ForeEach<SkinsScrollViewCell> (cell => cell.OnSkinItemSelected (indexSelected));
        }
        private void OnDisable ()
        {
            selector[skinType][indexSelected].Apply (material);
        }
        public void OnSkinItemHighLighted (int indexHighlighted)
        {
            if (!enabled) return;

            selector[skinType][indexHighlighted].Apply (material);
        }

        List<SkinsScrollViewCell> cels = new List<SkinsScrollViewCell> ();
        public override RectTransform GetPrefabInstance (int i)
        {
            var instance = Instantiate (PrefabRect, contentRect, false);

            SkinItem skin = selector[skinType][i];
            skinsHash[i] = skin;

            SkinsScrollViewCell skinsScrollViewCell = instance.GetComponent<SkinsScrollViewCell> ();

            bool isSelectedCell = (i == indexSelected);

            skinsScrollViewCell.Init (skin.Icon, i, isSelectedCell);
            return instance;
        }
        public void ScrollContentToLeft ()
        {
            MoveToDirection (Vector2.left);
        }
        public void ScrollContentToRight ()
        {
            MoveToDirection (Vector2.right);
        }
        private void MoveToDirection (Vector2 dir)
        {
            if (DOTween.IsTweening (contentRect)) return;

            Vector2 endValue = contentRect.anchoredPosition + dir * PrefabRect.rect.size.x;

            contentRect.DOAnchorPos (endValue, durationScroll);
        }
    }
}
