using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class Enemy : MonoBehaviour, IDamageable
{
    NavMeshAgent _agent;
    Rigidbody _rb;
    Animator _animator;

    [SerializeField] private float _attackDistance;
    [SerializeField] private float _health;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    
    private bool _alive;
    [SerializeField] private bool invincible = false;

    [SerializeField] private SkinnedMeshRenderer myMesh;
    private Material myMat;

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

    
    void OnEnable()
    {
        GameManager.Instance.OnGameOver += Die; //Placeholder logic, enemy can do any number of things upon game over
    }

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _agent = transform.GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyState = EnemyState.CHASING;
        StartCoroutine(EnemyAI());
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        myMat = myMesh.material;
    }

    void Update()
    {
        myMat.color = Color.Lerp(myMat.color, Color.white, Time.deltaTime * 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            //_agent.enabled = false;
            _agent.speed = 0;
            _agent.angularSpeed = 0;
            //Turn down angular speed too?
            _rb.isKinematic = false;
            StartCoroutine(CollisionDelay());
        }
        else if(collision.gameObject.layer == 7)
        {
            if(_enemyType == EnemyType.FORKABLE)
            {
                TakeDamage(_damage);
            }
        }
    }

    public void TakeDamage(float amount, Transform contactPoint = null) //pass in vector3 contactpoint
    {
        if (!invincible)
        {
            _health -= amount;
            StartCoroutine(DamageCooldown());
            Debug.Log(gameObject.name + " taking damage: " + amount + ":: remaining health: " + _health);
            if(contactPoint != null)
                Knockback(contactPoint.position);
            //Particles on contact point
            myMat.color = Color.red;

            if (_health <= amount)
            {
                _enemyState = EnemyState.DYING;
            }
        }
    }

    public void Knockback(Vector3 source) //Simple knockback that works with Broccoli @ Drag & Angular drag of 10
    {
        //Vert offset to lower pos
        Vector3 yOffset = new Vector3(0f, 0f, 0f);
        Vector3 vdiff = source - transform.position;
        Debug.Log(Math.Atan2(source.y - transform.position.y, source.x - transform.position.x));
        _rb.AddForceAtPosition(vdiff*20, transform.position, ForceMode.Impulse);
    }

    private IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(1f);
        //_agent.enabled = true;
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
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

    void OnDisable()
    {
        GameManager.Instance.OnGameOver -= Die;
    }
}
