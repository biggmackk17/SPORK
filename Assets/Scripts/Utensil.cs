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

    // Start is called before the first frame update
    void Start()
    {
        //Get collider component
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.layer == 6) //If enemy layer
        {
            //If my UType == their EnemyType
            
            var enemy = other.gameObject.GetComponent<Enemy>();
            var eType = enemy._enemyType;

            if (utensilType == UtensilType.SPOON && eType == Enemy.EnemyType.SPOONABLE)
                enemy.TakeDamage(damage);
            else if (utensilType == UtensilType.FORK && eType == Enemy.EnemyType.FORKABLE)
                enemy.TakeDamage(damage);
        }
    }
}
