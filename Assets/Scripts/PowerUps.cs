using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    int AddHealth = 50;
    //power ups 
    // turn into spork
    //regen health

    private enum _powerUpType
    {
        RegenHealth,
        SporkTime
    }
    [SerializeField] _powerUpType Type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void RegenHealth()
    {
        // AddHealth() to player
        // Sound Effect
        Debug.Log("Congratulations! You got some health back!! Yahooo!!");
    }

    void SporkTime()
    {
        //turn player into spork
        //Sound effect
        Debug.Log("Congratulations! You turned into a spork!! You are now unstoppableee!!!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
            {
                switch (Type)
                {
                    case _powerUpType.RegenHealth:
                        RegenHealth();
                        break;
                    case _powerUpType.SporkTime:
                        SporkTime();
                        break;
                    default:
                        Debug.Log("Default");
                        break;
                }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
