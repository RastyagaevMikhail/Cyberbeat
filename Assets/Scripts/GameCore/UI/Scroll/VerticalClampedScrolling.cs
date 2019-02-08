using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace GameCore
{
    [RequireComponent (typeof (ScrollRect))]
    public abstract class VerticalClampedScrolling<TDataItem> : MonoBehaviour, IClampedScrolling
    where TDataItem : IDataItem
    {
        public abstract int panCount { get; }

        [Header ("Settings")]
        [Range (-500, 500)]
        [SerializeField] float panOffset;
        [SerializeField] bool considerBounds = true;

        [Header ("Other Objects")]
        public RectTransform PrefabRect;
        private ScrollRect _scrollRect = null;
        private ScrollRect scrollRect { get { if (_scrollRect == null) _scrollRect = GetComponent<ScrollRect> (); return _scrollRect; } }

        private List<RectTransform> instPans = new List<RectTransform> ();

        protected RectTransform ContentRect => scrollRect.content;
        protected RectTransform ViewportRect => scrollRect.viewport;

        public void Initialize ()
        {
            ContentRect.sizeDelta = GetSizeContent (panCount, panOffset, PrefabRect.rect.size);
            ContentRect.anchoredPosition = GetStartContentposition (ViewportRect.rect, ContentRect.rect.size);

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

        public Vector2 StartPos => new Vector2 (ContentRect.anchoredPosition.x, ContentRect.rect.yMax - (PrefabRect.rect.size.y / 2f) - (considerBounds ? panOffset : 0));

        public Vector2 GetStartContentposition (Rect viewportRect, Vector2 contentSize)
        {
            return new Vector2 (0f, (viewportRect.size.y - contentSize.y) / 2f); // Top to Down
        }

        public Vector2 GetSizeContent (int count, float offset, Vector2 size)
        {
            return new Vector2 (size.x, count * size.y + (count + (considerBounds ? 1 : -1)) * offset);
        }

        public Vector2 GetNextPosition (Vector2 prevPosition, Vector2 sizeItem, float offset)
        {
            return new Vector2 (prevPosition.x, prevPosition.y - sizeItem.y - offset);
        }

    }
}
