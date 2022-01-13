using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectShipPrefab : TestPoly
{
    public Image imageShip;
    public Text textShipName;
    public Text textShipEnginePower;
    public Text textShipFuel;
    public Text textShipWeight;
    public Text textShipGoodLanding;
    public Text textShipPerfectLanding;

    private void Start()
    {
        imageShip.preserveAspect = true;
        imageShip.sprite = ListShips.Instance.rescuePodParameters[selectByID].sprite;

        RescuePodParameters rescuePodParameters = ListShips.Instance.rescuePodParameters[selectByID];

        textShipName.text = rescuePodParameters.name;
        textShipEnginePower.text = rescuePodParameters.enginePower.ToString();
        textShipFuel.text = rescuePodParameters.fuel.ToString();
        textShipWeight.text = rescuePodParameters.weight.ToString();
        textShipGoodLanding.text = rescuePodParameters.goodLanding.ToString();
        textShipPerfectLanding.text = rescuePodParameters.perfectLanding.ToString();
    }

    public void SelectShip()
    {
        selectMenu.SetActive(false);
        PodParameters.Instance.SelectShipByID(selectByID);
    }
}
