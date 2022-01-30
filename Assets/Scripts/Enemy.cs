using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    NavMeshAgent _agent;
    [SerializeField]float _attackDistance;
    Rigidbody _rb;
    [SerializeField]private float _health;
    [SerializeField] private float _damage;
    Animator _animator;
    private bool _alive;
    [SerializeField] private bool invincible = false;
    public static Action OnEnemyDie;

    public enum EnemyType
    {
        SPOONABLE,
        FORKABLE
    }
    [SerializeField] public EnemyType _enemyType;
    // Start is called before the first frame update

    public enum EnemyState { 
    CHASING,
    ATTACKING,
    DYING
    }
    [SerializeField] private EnemyState _enemyState;
    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _agent = transform.GetComponent<NavMeshAgent>();
        _enemyState = EnemyState.CHASING;
        _animator = GetComponent<Animator>();
        StartCoroutine(EnemyAI());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            _agent.enabled = false;
            _rb.isKinematic = false;
            StartCoroutine(CollisionDelay());
        }
        else if(collision.gameObject.layer == 7)
        {
            if(_enemyType == EnemyType.FORKABLE)
            {
                TakeDamage(10f);
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (!invincible)
        {
            _health -= amount;
            StartCoroutine(DamageCooldown());
            Debug.Log(gameObject.name + " taking damage: " + amount + ":: remaining health: " + _health);
            if (_health <= amount)
            {
                _enemyState = EnemyState.DYING;
            }
        }
    }

    private IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(1f);
        _agent.enabled = true;
        _rb.isKinematic = true;

    }

    private IEnumerator DamageCooldown()
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }

    // Update is called once per frame
     IEnumerator EnemyAI()
    {
        while (_enemyState != EnemyState.DYING)
        {
            while (_enemyState == EnemyState.CHASING)
            {
            var playerPos = Player.Instance.transform.position;
                var distance = Vector3.Distance(playerPos, transform.position);
                if (_agent.enabled)
                {
                    _agent.SetDestination(playerPos);
                }

                if (distance <= _attackDistance)
                {
                    _enemyState = EnemyState.ATTACKING;
                }
                yield return null;
            }

            while (_enemyState == EnemyState.ATTACKING)
            {
                var playerPos = Player.Instance.transform.position;
                var distance = Vector3.Distance(playerPos, transform.position);
                _animator.SetBool("Attacking", true);
                if(distance > _attackDistance)
                {
                    _animator.SetBool("Attacking", false);
                    _enemyState = EnemyState.CHASING;
                }
                    yield return null;
            }
            yield return null;
            }
        Die();
    }

    private void Die()
    {
        //set anim to dead
        OnEnemyDie?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEntered");
        if (other.gameObject.layer == 3)
            Debug.Log("Layer 3");
        {
            if(other.TryGetComponent<IDamageable>(out var target))
            {
                Debug.Log("player take damage!!!");
                target.TakeDamage(_damage);
            }
        }
    }
}
