using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstacle : MonoBehaviour
{

    int _damage = 20;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("Spork has been hit");
            //get access to player component
            //call player and use TakeDamage(_damage)
            //play sound effect
            //
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
