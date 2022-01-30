using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField]
    private int _damageAmount = 5;
    [SerializeField]
    private float _damageDelay = 3.0f;

    [SerializeField]
    private Vector3 _rotSpeed;

    private void Update()
    {
        transform.localRotation *= Quaternion.Euler(_rotSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))

            target.TakeDamage(_damageAmount * Time.deltaTime);
    }
}
    
