using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstShips : SnapScrolling
{
    private void Start()
    {
        panelCount = ListShips.Instance.rescuePodParameters.Length;

        contentRect = GetComponent<RectTransform>();
        
        instPanels = new GameObject[panelCount];
        panelsPosition = new Vector2[panelCount];
        panelsScale = new Vector2[panelCount];
        
        for (int i = 0; i < panelCount; i++)
        {
            instPanels[i] = Instantiate(panelPrefab, transform, false);
            instPanels[i].GetComponent<TestPoly>().selectByID = i;
            instPanels[i].GetComponent<TestPoly>().selectMenu = selectMenu;
        
            if (i == 0) continue;
            instPanels[i].transform.localPosition = new Vector2(instPanels[i - 1].transform.localPosition.x + panelPrefab.GetComponent<RectTransform>().sizeDelta.x + panelOffset,
                                                                instPanels[i].transform.localPosition.y);
            panelsPosition[i] = -instPanels[i].transform.localPosition;
        }
    }

}
