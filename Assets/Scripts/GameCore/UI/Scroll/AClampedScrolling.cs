using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    [RequireComponent (typeof (ScrollRect))]
    public abstract class AClampedScrolling<TDataItem> : MonoBehaviour, IClampedScrolling
    where TDataItem : IDataItem
    {

        public abstract int panCount { get; }

        [Header ("Settings")]
        [Range (-500, 500)]
        [SerializeField] protected float panOffset;
        [SerializeField] protected bool considerBounds = true;

        [Header ("Other Objects")]
        [SerializeField] protected RectTransform PrefabRect;
        private ScrollRect _scrollRect = null;
        protected ScrollRect scrollRect { get { if (_scrollRect == null) _scrollRect = GetComponent<ScrollRect> (); return _scrollRect; } }

        private List<RectTransform> instPans = new List<RectTransform> ();

        protected RectTransform contentRect => scrollRect.content;
        protected RectTransform viewportRect => scrollRect.viewport;

        public void ForeEach (Action<RectTransform> action)
        {
            instPans.ForEach (i => action (i));
        }
        public void ForeEach<T> (Action<T> action) where T : Component
        {
            instPans.ForEach (i => action (i.GetComponent<T> ()));
        }
        public void ForeEach<T> (Action<T, int> action) where T : Component
        {
            instPans.ForEach (i => action (i.GetComponent<T> (), instPans.IndexOf (i)));
        }

        public void Initialize ()
        {
            contentRect.sizeDelta = GetSizeContent (panCount, panOffset, PrefabRect.rect.size);
            contentRect.anchoredPosition = GetStartContentPosition (viewportRect.rect, contentRect.rect.size);

            for (int i = 0; i < panCount; i++)
            {
                RectTransform newRect = GetPrefabInstance (i);
                if (i == 0)
                {
                    newRect.anchoredPosition = StartPos;
                    instPans.Add (newRect);
                    continue;
                }
                var prevRect = instPans.Last ();

                newRect.localPosition = GetNextPosition (prevRect.anchoredPosition, PrefabRect.rect.size, panOffset);

                instPans.Add (newRect);
            }
        }
        public abstract RectTransform GetPrefabInstance (int i);
        public abstract Vector2 StartPos { get; }
        public abstract Vector2 GetStartContentPosition (Rect viewportRect, Vector2 contentSize);
        public abstract Vector2 GetSizeContent (int count, float offset, Vector2 size);
        public abstract Vector2 GetNextPosition (Vector2 prevPosition, Vector2 sizeItem, float offset);
    }
}
