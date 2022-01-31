using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] float _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            if (other.gameObject.layer == 10 || other.gameObject.layer == 11)
            {
                target.TakeDamage(_damage / 3);
            }
            else
            {
                target.TakeDamage(_damage);
            }
        }
    }
}
