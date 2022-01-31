using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(float amount, Transform source = null, Vector3 contactPoint = default(Vector3));

}
