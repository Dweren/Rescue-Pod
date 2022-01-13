using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlanet", menuName = "PlanetParameters")]
public class PlanetParameters : ScriptableObject
{
    public new string name;

    public Sprite sprite;

    public float atmosphere;

    public Color colorGround;
}
