using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int HealAmount = 50;

    public enum _powerUpType
    {
        Health,
        SporkTime
    }
    public _powerUpType Type;
}
