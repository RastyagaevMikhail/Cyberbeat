using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (ScrollRect))]
public class Scrolling : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header ("Links")]
    [SerializeField] RectTransform prefabRectTransform;

    [Header ("Settings")]
    [Range (1, 50)]
    [SerializeField] int panCount;

    [Range (0, 500)]
    [SerializeField] int panOffset;
    [Header ("Snap settings")]
    [SerializeField] bool useSnapping;
    [Range (0f, 20f)]
    [SerializeField] float snapSpeed;
    [Range (100, 1000)]

    [SerializeField] float maxScrollVelocity = 400;
    [Header ("Scale settings")]
    [SerializeField] bool UseScale;
    [Range (0, 2)]
    [SerializeField] float minScaleRange = 0.5f;
    [Range (0, 2)]
    [SerializeField] float maxScaleRange = 1.3f;
    [Range (0f, 10f)]
    [SerializeField] float scaleOffset;
    [Range (1f, 20f)]
    [SerializeField] float scaleSpeed;

    private List<RectTransform> instPans = new List<RectTransform> ();
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private Vector2 contentVector;
    private int selectedPanID;
    private bool isScrolling;

    [Header ("Aligment Settings")]
    [SerializeField] Direction direction;
    public enum Direction
    {
        Horizontal,
        Vertical, 
    }

    [SerializeField] HorizontalOrder horizontalOrder;
    public enum HorizontalOrder
    {
        LeftToRight,
        RightToLeft
    }

    [SerializeField] VerticalOrder verticalOrder;

    public enum VerticalOrder
    {
        TopToDown,
        DownToTop
    }

    [SerializeField] ScrollAligment scrollAligment;
    public enum ScrollAligment
    {
        Center,
        ViewportBounds
    }
    public interface IScrollConfigurator
    {
        Direction Direction { get; }
        IScrollingDirector Director { get; }
        IScrollOrderer Oder { get; }
        IScrollAligmenter ScrollAligmenter { get; }
    }
    public class ScrollConfigurator : IScrollConfigurator
    {
        object _order;
        public Direction Direction { get; }
        public ScrollAligment Aligment { get; }

        public IScrollingDirector Director => directorSelector[Direction];
        public ScrollConfigurator (Direction direction, ScrollAligment scrollAligment, Dictionary<Direction, object> orderObjectByTypeSelector)
        {
            Direction = direction;
            Aligment = scrollAligment;
            _order = orderObjectByTypeSelector[Direction];
        }

        // direction == ?(object) horizontalOrder : verticalOrder
        Dictionary<Direction, IScrollingDirector> directorSelector = new Dictionary<Direction, IScrollingDirector> ()
        {
            //
            { Direction.Horizontal, new HorizontalDirector () },
            //
            { Direction.Vertical, new VerticalDirector () }
        };
        Dictionary<Type, IDictionary> dictTypeSelector = new Dictionary<Type, IDictionary> ()
        {
            //
            { typeof (HorizontalDirector), horizontalOrderSelector },
            //
            { typeof (VerticalDirector), verticalOrderSelector },
        };
        static Dictionary<HorizontalOrder, AHorizontalOrderer> horizontalOrderSelector = new Dictionary<HorizontalOrder, AHorizontalOrderer> ()
        {
            //
            { HorizontalOrder.LeftToRight, new LeftToRightOrderer () },
            //
            { HorizontalOrder.RightToLeft, new RightToLeftOrderer () },
        };
        static Dictionary<VerticalOrder, AVerticalOrderer> verticalOrderSelector = new Dictionary<VerticalOrder, AVerticalOrderer> ()
        {
            //
            { VerticalOrder.TopToDown, new TopToDownOrderer () },
            //
            { VerticalOrder.DownToTop, new DownToTopOrderer () },
        };
        public IScrollOrderer Oder
        {
            get
            {
                Type directorType = Director.GetType ();

                IDictionary selector = dictTypeSelector[directorType];

                IScrollOrderer scrollOrderer = (IScrollOrderer) selector[_order];

                scrollOrderer.Init (Director.ViewportRect, Director.PrefabRect, aligmenterSelector[Aligment]);

                return scrollOrderer;
            }
        }
        Dictionary<ScrollAligment, IScrollAligmenter> aligmenterSelector = new Dictionary<ScrollAligment, IScrollAligmenter> ()
        {
            //Aligment by Center
            { ScrollAligment.Center, new CenterAligmenter () },
            // Aligment by Bounds Viewport
            { ScrollAligment.ViewportBounds, new ViewportBoundsAligmenter () },
        };
        public IScrollAligmenter ScrollAligmenter => aligmenterSelector[Aligment];
    }

    private ScrollRect _scrollRect = null;
    public ScrollRect scrollRect { get { if (_scrollRect == null) _scrollRect = GetComponent<ScrollRect> (); return _scrollRect; } }
    Vector2 prefabSizeDelta => prefabRectTransform.sizeDelta;
    RectTransform viewport => scrollRect.viewport;
    RectTransform contentRect => scrollRect.content;
    Vector2 startPos = Vector2.zero;
    Vector2 endPos = Vector2.zero;

    IScrollingDirector scrollingDirector;
    Dictionary<Direction, object> orderObjectByTypeSelector = null;

    private void Start ()
    {

        IScrollConfigurator scrollConfigurator =
            new ScrollConfigurator (
                direction,
                scrollAligment,
                new Dictionary<Direction, object>
                {
                    //
                    { Direction.Horizontal, horizontalOrder },
                    //
                    { Direction.Vertical, verticalOrder }
                });

        scrollingDirector = scrollConfigurator.Director;
        scrollingDirector.Init (scrollRect, prefabRectTransform, scrollConfigurator.Oder);

        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        scrollingDirector.UpdateContentSizeRect (panCount, panOffset);
        scrollingDirector.UpdateContentAnchorPosition ();

        for (int i = 0; i < panCount; i++)
        {
            var newRect = Instantiate (prefabRectTransform, contentRect, false);
            newRect.name = $"{prefabRectTransform.name}_{i}";
            if (i == 0)
            {
                newRect.anchoredPosition = startPos;
                instPans.Add (newRect);
                continue;
            }

            Vector2 prevlocalPosition = instPans.Last ().localPosition;

            newRect.localPosition = scrollingDirector.GetLocalPosition (prevlocalPosition, panOffset);;
            pansPos[i] = scrollingDirector.direction * newRect.localPosition;

            instPans.Add (newRect);

        }
    }

    bool horizontalInRange => (contentRect.anchoredPosition.x >= pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x);
    bool vertivcalInRange
    {
        get
        {
            // if (scrollAligment == ScrollAligment.ViewportBounds) 
            return Mathf.Abs (startPos.y) + prefabSizeDelta.y >= Mathf.Abs (contentRect.anchoredPosition.y) || Mathf.Abs (contentRect.anchoredPosition.y) <= Mathf.Abs (endPos.y);
            // return (contentRect.anchoredPosition.y >= pansPos[0].y || contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y);
        }
    }
    private void FixedUpdate ()
    {
        // scrollingDirector.
        // UpdateContentPosition ();
    }

    // private void UpdateContentPosition ()
    // {
    //     if ((horizontalInRange || vertivcalInRange) && !isScrolling)
    //         scrollRect.inertia = false;
    //     float nearestPos = float.MaxValue;
    //     for (int i = 0; i < panCount; i++)
    //     {
    //         float horizaontalDistanse = contentRect.anchoredPosition.x - pansPos[i].x;
    //         float verticalDistanse = contentRect.anchoredPosition.y - pansPos[i].y;

    //         float distance = Mathf.Abs (horizontal ? horizaontalDistanse : verticalDistanse);
    //         if (distance < nearestPos)
    //         {
    //             nearestPos = distance;
    //             selectedPanID = i;
    //         }
    //         if (UseScale)
    //         {
    //             float scale = Mathf.Clamp (1 / (distance / panOffset) * scaleOffset, minScaleRange, maxScaleRange);
    //             pansScale[i].x = Mathf.SmoothStep (instPans[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
    //             pansScale[i].y = Mathf.SmoothStep (instPans[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
    //             instPans[i].transform.localScale = pansScale[i];
    //         }
    //     }
    //     float scrollVelocity = Mathf.Abs (horizontal ? scrollRect.velocity.x : scrollRect.velocity.y);

    //     if (scrollVelocity < maxScrollVelocity && !isScrolling) scrollRect.inertia = false;
    //     if (isScrolling || scrollVelocity > maxScrollVelocity) return;

    //     if (!useSnapping) return;

    //     if (horizontal)
    //         contentVector.x = Mathf.SmoothStep (contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
    //     else
    //         contentVector.y = Mathf.SmoothStep (contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);
    //     // Debug.Log (Tools.LogCollection (pansPos));
    //     contentRect.anchoredPosition = contentVector;
    // }

    public void OnBeginDrag (PointerEventData eventData)
    {
        isScrolling = true;
        scrollRect.inertia = true;
    }

    public void OnEndDrag (PointerEventData eventData)
    {
        isScrolling = false;
    }
}
/// <summary>
/// Определяет в каком направлении (по какой оси ) будет двигаться контент.
/// </summary>
public interface IScrollingDirector
{
    int direction { get; }
    IScrollOrderer Orderer { get; }

    RectTransform ContentRect { get; }
    RectTransform ViewportRect { get; }
    RectTransform PrefabRect { get; }
    ScrollRect ScrollRect { get; }
    void UpdateContentSizeRect (int panCount, float panOffset);
    void Init (ScrollRect scrollRect, RectTransform prefabRect, IScrollOrderer orderer);
    Vector2 GetLocalPosition (Vector2 prevPosition, float panOffset);
    void UpdateContentPosition ();
    void UpdateContentAnchorPosition (int index = 0);
}
/// <summary>
/// Реализация интерфейса IScrollingDirector для движения контента в горизотальной оси (оси х)
/// </summary>
public class HorizontalDirector : IScrollingDirector
{
    public RectTransform ContentRect => ScrollRect.content;
    public RectTransform ViewportRect => ScrollRect.viewport;
    public RectTransform PrefabRect { get; private set; }
    public ScrollRect ScrollRect { get; private set; }
    public IScrollOrderer Orderer { get; private set; }

    public int direction => Orderer.Direction;

    public Vector2 GetLocalPosition (Vector2 prevPosition, float panOffset)
    {
        return new Vector2 (prevPosition.x + direction * (PrefabRect.sizeDelta.x + panOffset), prevPosition.y);
    }

    public void Init (ScrollRect scrollRect, RectTransform prefabRect, IScrollOrderer orderer)
    {
        this.ScrollRect = scrollRect;
        this.ScrollRect.horizontal = true;
        this.ScrollRect.vertical = false;
        this.PrefabRect = prefabRect;

        this.Orderer = orderer;
    }

    public void UpdateContentAnchorPosition (int index = 0)
    {
        ContentRect.anchoredPosition = 
            new Vector2 (
                direction * (ContentRect.rect.size.x / 2f) - direction * (ViewportRect.rect.size.x / 2f),
                ViewportRect.localPosition.y);
        //         startPos = new Vector2 (-dir * contentRect.rect.xMax + dir * (prefabSizeDelta.x / 2f) + dir * panOffset, contentPos.y);
    }

    public void UpdateContentPosition ()
    {
        throw new System.NotImplementedException ();
    }

    public void UpdateContentSizeRect (int panCount, float panOffset)
    {
        ContentRect.sizeDelta = new Vector2 (panCount * PrefabRect.sizeDelta.x + (panCount + 1) * panOffset, PrefabRect.sizeDelta.y);
    }
}
/// <summary>
/// Реализация интерфейса IScrollingDirector для движения контента в вертикальной оси (оси у)
/// </summary>
public class VerticalDirector : IScrollingDirector
{
    public RectTransform ContentRect => ScrollRect.content;
    public RectTransform ViewportRect => ScrollRect.viewport;
    public RectTransform PrefabRect { get; private set; }
    public ScrollRect ScrollRect { get; private set; }
    public IScrollOrderer Orderer { get; private set; }
    public int direction => Orderer.Direction;

    public Vector2 GetLocalPosition (Vector2 prevPosition, float panOffset)
    {
        return new Vector2 (prevPosition.x, prevPosition.y + direction * (PrefabRect.sizeDelta.y + panOffset));
    }

    public void Init (ScrollRect scrollRect, RectTransform prefabRect, IScrollOrderer orderer)
    {
        //ScrollRect intiialization
        this.ScrollRect = scrollRect;
        this.ScrollRect.horizontal = false;
        this.ScrollRect.vertical = true;
        //
        this.PrefabRect = prefabRect;
        this.Orderer = orderer;
        

    }

    public void UpdateContentAnchorPosition (int index = 0)
    {
        ContentRect.anchoredPosition =
            new Vector2 (
                ViewportRect.localPosition.x,
                PrefabRect.sizeDelta.y * index + 
                direction * (ContentRect.rect.size.y / 2f) - direction * (ViewportRect.rect.size.y / 2f));

        //         startPos = new Vector2 (contentPos.x, -dir * contentRect.rect.yMax + dir * (prefabSizeDelta.y / 2f) + dir * panOffset);
        //         endPos = new Vector2 (contentPos.x, dir * contentRect.rect.yMin + dir * (prefabSizeDelta.y / 2f) + dir * panOffset);
    }

    public void UpdateContentPosition ()
    {
        throw new System.NotImplementedException ();
    }

    public void UpdateContentSizeRect (int panCount, float panOffset)
    {
        ContentRect.sizeDelta = new Vector2 (PrefabRect.sizeDelta.x, panCount * PrefabRect.sizeDelta.y + (panCount + 1) * panOffset);
    }
}

/// <summary>
/// Определяет в каком порядке будут выстраиваться объекты контента
/// </summary>
public interface IScrollOrderer
{
    IScrollAligmenter scrollAligmenter { get; }
    RectTransform Viewport { get; set; }
    RectTransform Prefab { get; set; }
    void Init (RectTransform viewport, RectTransform prefab, IScrollAligmenter aligmenter);
    int Direction { get; }
}
public abstract class AHorizontalOrderer : AScrollOrderer
{

}
public class RightToLeftOrderer : AHorizontalOrderer
{
    public override int Direction => -1;

}
public class LeftToRightOrderer : AHorizontalOrderer
{
    public override int Direction => 1;
}
/// <summary>
/// Абстрактаная реализация интерфейса IScrollOrderer.Используется для 7
/// </summary>
public abstract class AScrollOrderer : IScrollOrderer
{
    public IScrollAligmenter scrollAligmenter { get; set; }
    public RectTransform Viewport { get; set; }
    public RectTransform Prefab { get; set; }
    public void Init (RectTransform viewport, RectTransform prefab, IScrollAligmenter aligmenter)
    {
        this.Prefab = prefab;
        this.Viewport = viewport;
        
        scrollAligmenter = aligmenter;
        scrollAligmenter.Init (viewport, prefab);
    }
    public abstract int Direction { get; }
}
public abstract class AVerticalOrderer : AScrollOrderer
{

}

public class TopToDownOrderer : AVerticalOrderer
{
    public override int Direction => -1;
}
public class DownToTopOrderer : AVerticalOrderer
{
    public override int Direction => 1;
}

public interface IScrollAligmenter
{
    RectTransform Viewport { get; set; }
    RectTransform Prefab { get; set; }
    void Init (RectTransform viewport, RectTransform prefab);
    void UpdateContentAnchorPosition (int index = 0);
}
public abstract class AScrollAligmenter : IScrollAligmenter
{
    public RectTransform Viewport { get; set; }
    public RectTransform Prefab { get; set; }

    public void Init (RectTransform viewport, RectTransform prefab)
    {
        this.Prefab = prefab;
        this.Viewport = viewport;
    }

    public abstract void UpdateContentAnchorPosition(int index = 0);
}
public class CenterAligmenter : AScrollAligmenter
{
    public override void UpdateContentAnchorPosition(int index = 0)
    {
        throw new NotImplementedException();
    }
}
public class ViewportBoundsAligmenter : AScrollAligmenter
{
    public override void UpdateContentAnchorPosition(int index = 0)
    {
        throw new NotImplementedException();
    }
}
