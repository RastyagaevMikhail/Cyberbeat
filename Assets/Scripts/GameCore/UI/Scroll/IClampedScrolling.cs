namespace GameCore
{
    using UnityEngine;

    public interface IClampedScrolling
    {
        Vector2 GetNextPosition (Vector2 prevPosition, Vector2 sizeItem, float offset);
        Vector2 GetSizeContent (int count, float offset, Vector2 size);
        Vector2 GetStartContentPosition (Rect viewportRect, Vector2 contentSize);
        Vector2 StartPos { get; }
    }
}
