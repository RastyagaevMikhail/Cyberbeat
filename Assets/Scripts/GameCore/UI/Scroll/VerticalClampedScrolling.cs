using UnityEngine;
namespace GameCore
{
    public abstract class VerticalClampedScrolling<TDataItem> : AClampedScrolling<TDataItem> where TDataItem : IDataItem
    {
        public override Vector2 StartPos
            => new Vector2 (contentRect.anchoredPosition.x, contentRect.rect.yMax - (PrefabRect.rect.size.y / 2f) - (considerBounds ? panOffset : 0));
        public override Vector2 GetStartContentPosition (Rect viewportRect, Vector2 contentSize)
        {
            return new Vector2 (0f, (viewportRect.size.y - contentSize.y) / 2f); // Top to Down
        }
        public override Vector2 GetSizeContent (int count, float offset, Vector2 size)
        {
            return new Vector2 (size.x, count * size.y + (count + (considerBounds ? 1 : -1)) * offset);
        }
        public override Vector2 GetNextPosition (Vector2 prevPosition, Vector2 sizeItem, float offset)
        {
            return new Vector2 (prevPosition.x, prevPosition.y - sizeItem.y - offset);
        }
    }
}
