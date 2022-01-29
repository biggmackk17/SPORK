using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _speed;
    Rigidbody _rb;

    // Update is called once per frame
    void Update()
    {
        var xVel = Input.GetAxis("horizontal") * _speed;
        var yVel = Input.GetAxis("vertical") * _speed;


        
    }
}
