using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (ScrollRect))]
public class HorizontalSnapScrolling : MonoBehaviour
{

    [Range (1, 50)]
    [Header ("Controllers")]
    public int panCount;
    [Range (0, 500)]
    public int panOffset;
    [Range (0f, 20f)]
    public float snapSpeed;
    [SerializeField] bool useScale;

    [Range (0f, 10f)]
    public float scaleOffset;
    [SerializeField] float MinScale = 0.5f;
    [SerializeField] float MaxScale = 1f;
    [Range (1f, 20f)]
    public float scaleSpeed;
    [Header ("Other Objects")]
    public RectTransform panPrefab;
    private ScrollRect _scrollRect = null;
    private ScrollRect scrollRect { get { if (_scrollRect == null) _scrollRect = GetComponent<ScrollRect> (); return _scrollRect; } }

    private List<RectTransform> instPans = new List<RectTransform> ();
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect => scrollRect.content;
    private RectTransform viewportRect => scrollRect.viewport;
    private Vector2 contentVector;

    private int selectedPanID;
    private bool isScrolling;

    private void Start ()
    {

        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];

        for (int i = 0; i < panCount; i++)
        {
            var newRect = Instantiate (panPrefab, transform, false);
            if (i == 0)
            {
                instPans.Add (newRect);
                continue;
            }
            var prevRect = instPans.Last ();
            newRect.localPosition =
                new Vector2 (
                    prevRect.localPosition.x + panPrefab.sizeDelta.x + panOffset,
                    newRect.localPosition.y);

            pansPos[i] = -newRect.localPosition;
            instPans.Add (newRect);
        }
    }

    private void FixedUpdate ()
    {
        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs (contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID = i;
            }
            if (useScale)
            {
                float scale = Mathf.Clamp (1 / (distance / panOffset) * scaleOffset, MinScale, MaxScale);
                pansScale[i].x = Mathf.SmoothStep (instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep (instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                instPans[i].transform.localScale = pansScale[i];
            }
        }
        float scrollVelocity = Mathf.Abs (scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep (contentRect.anchoredPosition.x, pansPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling (bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}
