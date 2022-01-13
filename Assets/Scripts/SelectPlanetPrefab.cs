using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlanetPrefab : TestPoly
{
    public Image imagePlanet;
    public Text textPlanetName;
    public Text textAtmosphere;

    private void Start()
    {
        imagePlanet.preserveAspect = true;

        PlanetParameters planetParameters = ListShips.Instance.planetParameters[selectByID];

        textPlanetName.text = planetParameters.name;
        imagePlanet.sprite = planetParameters.sprite;
        textAtmosphere.text = planetParameters.atmosphere.ToString();
    }

    public void SelectPlanet()
    {
        selectMenu.SetActive(false);
        PodParameters.Instance.SelectPlanetByID(selectByID);
    }
}
