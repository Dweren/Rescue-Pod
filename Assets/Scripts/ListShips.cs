using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListShips : MonoBehaviour
{
    public static ListShips Instance;

    public RescuePodParameters[] rescuePodParameters;

    public PlanetParameters[] planetParameters;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
