using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPod", menuName = "RescuePodParameters")]
public class RescuePodParameters : ScriptableObject
{
    public new string name;

    public Sprite sprite;

    public float enginePower;
    public float fuel;
    public float weight;
    public float goodLanding;
    public float perfectLanding;
}
