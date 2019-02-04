using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof (ScrollRect))]
public class SnapScrolling : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header ("Settings")]
    [Range (1, 50)]
    [SerializeField] int panCount;
    [Range (0, 500)]
    [SerializeField] int panOffset;
    [SerializeField] bool useSnapping;
    [Range (0f, 20f)]
    [SerializeField] float snapSpeed;
    [SerializeField] bool UseScale;
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
    enum Direction
    {
        Horizontal,
        Vertical,
    }

    [SerializeField] HorizontalOrder horizontalOrder;
    enum HorizontalOrder
    {
        LeftToRight,
        RightToLeft
    }

    [SerializeField] VerticalOrder verticalOrder;

    enum VerticalOrder
    {
        TopToDown,
        DownToTop
    }

    [SerializeField] ScrollAligment scrollAligment;
    enum ScrollAligment
    {
        Center,
        ViewportBounds
    }

    [Header ("Links")]
    [SerializeField] RectTransform prefabRectTransform;
    private ScrollRect _scrollRect = null;
    public ScrollRect scrollRect { get { if (_scrollRect == null) _scrollRect = GetComponent<ScrollRect> (); return _scrollRect; } }
    Vector2 prefabSizeDelta => prefabRectTransform.sizeDelta;
    RectTransform viewport => scrollRect.viewport;
    RectTransform contentRect => scrollRect.content;

    private void Start ()
    {
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        horizontal = (direction == Direction.Horizontal);
        vertical = (direction == Direction.Vertical);

        float dir = horizontal ?
            ((horizontalOrder == HorizontalOrder.RightToLeft) ? -1 : 1) :
            ((verticalOrder == VerticalOrder.TopToDown) ? -1 : 1);

        int sizeWithPanOffset = (panCount - 1) * panOffset;
        contentRect.sizeDelta = horizontal ?
            new Vector2 (panCount * prefabSizeDelta.x + sizeWithPanOffset, prefabSizeDelta.y) :
            new Vector2 (prefabSizeDelta.x, panCount * prefabSizeDelta.y + sizeWithPanOffset);
            
        Debug.LogFormat ("prefabSizeDelta = {0}", prefabSizeDelta);
        Debug.LogFormat ("contentRect.sizeDelta = {0}", contentRect.sizeDelta);

        Vector2 startPos = Vector2.zero;

        if (scrollAligment == ScrollAligment.ViewportBounds)
        {
            Vector2 viewportPos = viewport.anchoredPosition;
            Vector2 viewportSize = viewport.sizeDelta;

            Vector2 contentPos = contentRect.anchoredPosition;
            Vector2 contentSize = contentRect.sizeDelta;

            if (vertical)
            {
                contentRect.anchoredPosition = new Vector2 (viewportPos.x, dir * (contentRect.sizeDelta.y / 2f) - dir * (viewportSize.y / 2f));
                startPos = new Vector2 (contentPos.x, -dir * contentRect.rect.yMax + dir * (prefabSizeDelta.y / 2f) + dir * panOffset);
            }
            else
            {
                contentRect.anchoredPosition = new Vector2 (dir * (contentRect.sizeDelta.x / 2f) - dir * (viewportSize.x / 2f), viewportPos.y);
            }
        }
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
            Vector2 localPosition = newRect.localPosition;

            if (horizontal)
                pansPos[i] = GetPos (GethorizontalPosition, ref localPosition, prevlocalPosition, dir);
            else if (vertical)
                pansPos[i] = GetPos (GetVerticalPosition, ref localPosition, prevlocalPosition, dir);

            newRect.localPosition = localPosition;

            instPans.Add (newRect);

        }
    }

    private bool horizontal { set => scrollRect.horizontal = value; get => scrollRect.horizontal; }
    private bool vertical { set => scrollRect.vertical = value; get => scrollRect.vertical; }

    private Vector2 GetPos (System.Func<Vector2, Vector2, float, Vector2> positionSelector, ref Vector2 localPosition, Vector2 prevlocalPosition, float dir)
    {
        localPosition = positionSelector (localPosition, prevlocalPosition, dir);
        return dir * localPosition;
    }

    private Vector2 GethorizontalPosition (Vector2 localPosition, Vector2 prevlocalPosition, float dir)
    {
        return new Vector2 (prevlocalPosition.x + dir * (prefabSizeDelta.x + panOffset), localPosition.y);
    }
    private Vector2 GetVerticalPosition (Vector2 localPosition, Vector2 prevlocalPosition, float dir)
    {
        return new Vector2 (localPosition.x, prevlocalPosition.y + dir * (prefabSizeDelta.y + panOffset));
    }

    bool horizontalInRange => (contentRect.anchoredPosition.x >= pansPos[0].x || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x);
    bool vertivcalInRange => (contentRect.anchoredPosition.y >= pansPos[0].y || contentRect.anchoredPosition.y <= pansPos[pansPos.Length - 1].y);
    private void FixedUpdate ()
    {
        // UpdateContentPosition();
    }

    private void UpdateContentPosition ()
    {
        if ((horizontalInRange || vertivcalInRange) && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float horizaontalDistanse = contentRect.anchoredPosition.x - pansPos[i].x;
            float verticalDistanse = contentRect.anchoredPosition.y - pansPos[i].y;

            float distance = Mathf.Abs (horizontal ? horizaontalDistanse : verticalDistanse);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            if (UseScale)
            {
                float scale = Mathf.Clamp (1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep (instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep (instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                instPans[i].transform.localScale = pansScale[i];
            }
        }
        float scrollVelocity = Mathf.Abs (horizontal ? scrollRect.velocity.x : scrollRect.velocity.y);

        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;

        if (!useSnapping) return;

        if (horizontal)
            contentVector.x = Mathf.SmoothStep (contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        else
            contentVector.y = Mathf.SmoothStep (contentRect.anchoredPosition.y, pansPos[selectedPanID].y, snapSpeed * Time.fixedDeltaTime);

        contentRect.anchoredPosition = contentVector;
    }

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
