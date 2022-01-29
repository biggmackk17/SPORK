using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]float _speed;
    [SerializeField] float _jumpForce;
    [SerializeField]
    private float _gravityFall = 20f;
    Rigidbody _rb;
    [SerializeField]bool _grounded  = true;
    private float _yvelocity;
    [SerializeField]
    private bool _jumping = false;
    private bool _jumpHeld = false;
    [SerializeField]
    private LayerMask _groundLayer;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

  
    // Update is called once per frame
    void Update()
    {

        _jumping = Input.GetKeyDown(KeyCode.Space);

        if (_jumping == true)
            Debug.Log("Hit the space key");

        _jumpHeld = Input.GetKey(KeyCode.Space);

        _grounded = Physics.CheckSphere(transform.position, 0.1f, _groundLayer);

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");


        var direction = new Vector3(horizontal, 0, vertical);

        _yvelocity = _rb.velocity.y;

        if (_jumping)
            Debug.Log("FixedUpdate:: Jumping");

        if (_jumping == true && _grounded)
        {
            _yvelocity += _jumpForce;
            _jumping = false;
            Debug.Log("Should Jump");
        }


        if (!_jumpHeld & !_grounded)
        {
            _yvelocity -= _gravityFall * Time.deltaTime;
        }


        var newVelocity = direction * _speed;
        newVelocity.y = _yvelocity;


        _rb.velocity = newVelocity;


        if (direction.magnitude > 0)
        {
            var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10.0f);
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
