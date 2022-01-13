using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RescuePod : MonoBehaviour
{
    [Header("Parameters")]
    public string shipName;
    public float shipEnginePower;
    public float fuelLevelMax;
    public float shipWeight;
    public float goodLandingSpeed;
    public float perfectLandingSpeed;
    
    [Header("Other")]
    public Rigidbody2D rb;

    public GameObject groundLanding;
    public Text textDistanceToLanding;

    public Text textDistanceToLandingAlternate;
    public Text textVelocityRescuePodAlternate;

    public float fuelLevel;
    public Slider sliderFuelLevelAlternate;
    public Text fuelLevelInNumber;

    public GameObject jetOfFlame;

    public Image image;

    public GameObject alternateParameters;

    private float distanceСorrection;

    private void Start()
    {
        distanceСorrection = ((groundLanding.GetComponent<BoxCollider2D>().size.y + transform.GetComponent<BoxCollider2D>().size.y * transform.localScale.y) / 2)
                               + groundLanding.GetComponent<BoxCollider2D>().offset.y;

        rb = GetComponent<Rigidbody2D>();
        rb.mass = shipWeight;
        rb.drag = PodParameters.Instance.planetAtmosphere;

        sliderFuelLevelAlternate.maxValue = fuelLevelMax;
        sliderFuelLevelAlternate.value = fuelLevelMax;
    }

    private void Update()
    {
        float distanceForText = Mathf.Abs(transform.position.y - groundLanding.transform.position.y) - distanceСorrection;
        textDistanceToLanding.text = distanceForText.ToString("0.00");

        PodParameters.Instance.fuelLevelInNumber.text = fuelLevel.ToString("0.00");
        PodParameters.Instance.sliderFuelLevel.value = fuelLevel;
        PodParameters.Instance.textVelocityRescuePod.text = rb.velocity.y.ToString("00.00");

        //Parameters Alternate
        textDistanceToLandingAlternate.text = distanceForText.ToString("0.00");
        textVelocityRescuePodAlternate.text = rb.velocity.y.ToString("00.00");
        sliderFuelLevelAlternate.value = fuelLevel;
        fuelLevelInNumber.text = fuelLevel.ToString("0.00");

        if(PodParameters.Instance.enginePower > 0)
        {
            jetOfFlame.SetActive(true);
        }
        else
        {
            jetOfFlame.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        fuelLevel -= PodParameters.Instance.enginePower / 100;

        if (fuelLevel < 0)
        {
            fuelLevel = 0;
            PodParameters.Instance.BlockSliderEnginePower();
        }       

        if (PodParameters.Instance.enginePower > 0)
        {
            PushUpRescueModule();
        }
    }

    public void PushUpRescueModule()
    {
        Vector2 pushForce = new Vector2(0f, PodParameters.Instance.enginePower);
        rb.AddForce(pushForce, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckResultLanding(rb.velocity.y);
        PodParameters.Instance.BlockSliderEnginePower();
    }

    public void CheckResultLanding(float velocityLanding)
    {
        if (velocityLanding < goodLandingSpeed)
        {
            PodParameters.Instance.ShowLandingResult("You Crashed!");
        }
        
        if (velocityLanding >= goodLandingSpeed && velocityLanding < perfectLandingSpeed)
        {
            PodParameters.Instance.ShowLandingResult("Good Landing!");
        }
        
        if (velocityLanding >= perfectLandingSpeed)
        {
            PodParameters.Instance.ShowLandingResult("Perfect Landing!");
        }
    }

    public void RestartLandingModule()
    {
        fuelLevel = fuelLevelMax;
        transform.position = PodParameters.Instance.shipPosition;
    }

    public void SetAlternateParameters(bool isAlternateParameters)
    {
        alternateParameters.SetActive(isAlternateParameters);
    }
}
