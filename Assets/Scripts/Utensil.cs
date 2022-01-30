using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utensil : MonoBehaviour
{
    public enum UtensilType
    {
        SPOON,
        FORK
    }

    [SerializeField] private float damage = 10f;
    [SerializeField] private UtensilType utensilType;

    public UtensilType GetUtensilType()
    {
        return utensilType;
    }

    public float GetUtensilDamage()
    {
        return damage;
    }
}
