using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float _speed;
    [SerializeField] float _jumpForce;
    Rigidbody _rb;
    [SerializeField]bool _grounded  = true;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var xVel = Input.GetAxis("Horizontal") * _speed;
        var zVel = Input.GetAxis("Vertical") * _speed;
        _rb.velocity = new Vector3(xVel, _rb.velocity.y, zVel);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_grounded)
            {
                _rb.AddForce(Vector3.up*_jumpForce);
            }
        }

        Debug.DrawLine(transform.position, transform.position + Vector3.down,Color.red);
        if(Physics.Raycast(transform.position,Vector3.down, out var hit, .25f))
        {
            Debug.Log(hit.transform.gameObject.name);
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
    }
}
