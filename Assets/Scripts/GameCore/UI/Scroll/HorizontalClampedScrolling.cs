using UnityEngine;

namespace GameCore
{
    public abstract class HorizontalClampedScrolling<TDataItem> : AClampedScrolling<TDataItem> where TDataItem : IDataItem
    {
        public override Vector2 StartPos
            => new Vector2 (contentRect.rect.xMin + (PrefabRect.rect.size.x / 2f) + (considerBounds ? panOffset : 0), contentRect.anchoredPosition.y);

        public override Vector2 GetStartContentPosition (Rect viewportRect, Vector2 contentSize)
        {
            return new Vector2 ((viewportRect.size.x + contentSize.x) / 2f, 0f); // Left To Right
        }
        public override Vector2 GetSizeContent (int count, float offset, Vector2 size)
        {
            return new Vector2 (count * size.x + (count + (considerBounds ? 1 : -1)) * offset, size.y);
        }
        public override Vector2 GetNextPosition (Vector2 prevPosition, Vector2 sizeItem, float offset)
        {
            return new Vector2 (prevPosition.x + sizeItem.x + offset, prevPosition.y); // Left To Right
        }
    }
}
