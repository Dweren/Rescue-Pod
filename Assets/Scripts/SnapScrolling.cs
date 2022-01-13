using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour
{
    public int panelCount;

    [Range(0, 500)]
    public int panelOffset;

    [Range(0f, 20f)]
    public float snapSpeed;

    [Range(0f, 5f)]
    public float scaleOffset;

    [Range(1f, 20f)]
    public float scaleSpeed;

    [Header("Other Objects")]
    public GameObject panelPrefab;
    public ScrollRect scrollRect;

    public GameObject[] instPanels;
    public Vector2[] panelsPosition;
    public Vector2[] panelsScale;

    public RectTransform contentRect;
    private Vector2 contentVector;

    private int selectedPanelID;
    private bool isScrolling;

    public GameObject selectMenu;

    private void FixedUpdate()
    {
        if(contentRect.anchoredPosition.x >= panelsPosition[0].x && !isScrolling || contentRect.anchoredPosition.x <= panelsPosition[panelsPosition.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }
        float nearestPosition = float.MaxValue;
        for (int i = 0; i < panelCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - panelsPosition[i].x);
            if(distance < nearestPosition)
            {
                nearestPosition = distance;
                selectedPanelID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panelOffset) * scaleOffset, 1f, 1.1f);
            panelsScale[i].x = Mathf.SmoothStep(instPanels[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            panelsScale[i].y = Mathf.SmoothStep(instPanels[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            instPanels[i].transform.localScale = panelsScale[i];
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity >= 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, panelsPosition[selectedPanelID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    public void Scrolling (bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }
}