using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PodParameters : MonoBehaviour
{
    public static PodParameters Instance;

    public GameObject podPrefab;

    public GameObject rescuePod;

    public GameObject noticeLanding;
    public Text textNoticeQuality;

    public GameObject sliderEnginePower;

    public Text textVelocityRescuePod;
    public Text textVelocityRescuePodAlternate;

    [HideInInspector]
    public float enginePower = 0f;

    public Slider sliderFuelLevel;
    public Text fuelLevelInNumber;

    public GameObject buttonSelectPlanet;
    public GameObject groundLanding;
    public Text textDistanceToLanding;

    public bool isAlternateParameters = false;

    public float planetAtmosphere;
    public Text textAtmosphere;

    public Vector2 shipPosition = new Vector2(0f, 4f);

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SelectPlanetByID(ListShips.Instance.planetParameters.Length - 1);
    }

    public void BlockSliderEnginePower()
    {
        sliderEnginePower.GetComponent<Slider>().value = 0;
        sliderEnginePower.GetComponent<Slider>().interactable = false;
    }

    public void ActivateSliderEnginePower()
    {
        sliderEnginePower.GetComponent<Slider>().value = 0;
        sliderEnginePower.GetComponent<Slider>().interactable = true;
    }

    public void BlockButtonSelectPlanet()
    {
        buttonSelectPlanet.GetComponent<Button>().interactable = false;
    }

    public void UnBlockButtonSelectPlant()
    {
        buttonSelectPlanet.GetComponent<Button>().interactable = true;
    }

    public void ShowLandingResult(string resultLanding)
    {
        UnBlockButtonSelectPlant();
        textNoticeQuality.text = resultLanding;
        noticeLanding.SetActive(true);
    }

    public void ChangeEnginePower(Slider slider)
    {
        enginePower = (float)slider.value;
    }

    public void SetModuleParameters(int shipID)
    {
        RescuePodParameters rescuePodParameters = ListShips.Instance.rescuePodParameters[shipID];

        sliderEnginePower.GetComponent<Slider>().maxValue = rescuePodParameters.enginePower;
        sliderFuelLevel.maxValue = rescuePodParameters.fuel;
        sliderFuelLevel.value = rescuePodParameters.fuel;

        RescuePod scriptRescuePod = podPrefab.GetComponent<RescuePod>();

        scriptRescuePod.shipName = rescuePodParameters.name;
        scriptRescuePod.shipEnginePower = rescuePodParameters.enginePower;
        scriptRescuePod.fuelLevelMax = rescuePodParameters.fuel;
        scriptRescuePod.shipWeight = rescuePodParameters.weight;
        scriptRescuePod.goodLandingSpeed = rescuePodParameters.goodLanding;
        scriptRescuePod.perfectLandingSpeed = rescuePodParameters.perfectLanding;
        scriptRescuePod.image.sprite = rescuePodParameters.sprite;
        scriptRescuePod.groundLanding = groundLanding;
        scriptRescuePod.textDistanceToLanding = textDistanceToLanding;
    }

    public void SelectShipByID(int shipID)
    {
        if (rescuePod)
        {
            Destroy(rescuePod);
        }

        SetModuleParameters(shipID);

        rescuePod = Instantiate(podPrefab, shipPosition, Quaternion.identity);

        ShowAlternateParameters(isAlternateParameters);
        BlockButtonSelectPlanet();
        RestartLanding();
    }

    public void SelectPlanetByID(int planetID)
    {
        planetAtmosphere = ListShips.Instance.planetParameters[planetID].atmosphere;
        textAtmosphere.text = planetAtmosphere.ToString();
        groundLanding.GetComponent<SpriteRenderer>().color = ListShips.Instance.planetParameters[planetID].colorGround;
        if (rescuePod)
        {
            rescuePod.GetComponent<Rigidbody2D>().drag = planetAtmosphere;
        }
    }

    public void ShowAlternateParameters(bool isAlternateParameters)
    {
        rescuePod.transform.GetChild(1).gameObject.SetActive(isAlternateParameters);
    }   

    public void RestartLanding()
    {
        BlockButtonSelectPlanet();
        noticeLanding.SetActive(false);
        ActivateSliderEnginePower();
        rescuePod.GetComponent<RescuePod>().RestartLandingModule();
    }

    public void OnAlternateParameters()
    {
        isAlternateParameters = true;

        if (rescuePod)
        {
            rescuePod.GetComponent<RescuePod>().SetAlternateParameters(isAlternateParameters);
        }
        
    }

    public void OffAlternateParameters()
    {
        isAlternateParameters = false;

        if (rescuePod)
        {
            rescuePod.GetComponent<RescuePod>().SetAlternateParameters(isAlternateParameters);
        }
    }
}
