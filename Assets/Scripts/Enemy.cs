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
    [SerializeField] float currentHealth;
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    
    private bool _alive;
    [SerializeField] private bool invincible = false;

    [SerializeField] private SkinnedMeshRenderer myMesh;
    private Material myMat;

    [SerializeField] private GameObject _bloodType;

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
        Debug.Log(transform.name + " starting.");
        _rb = transform.GetComponent<Rigidbody>();
        _agent = transform.GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _enemyState = EnemyState.CHASING;
        StartCoroutine(EnemyAI());
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        myMat = myMesh.material;
        currentHealth = _health;
    }

    void Update()
    {
        myMat.color = Color.Lerp(myMat.color, Color.white, Time.deltaTime * 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3 || collision.gameObject.layer == 7)
        {
            
            StartCoroutine(CollisionDelay());
        }
    }

    public void TakeDamage(float amount, Transform source = null, Vector3 contactPoint = default(Vector3)) //pass in vector3 contactpoint
    {
        if (!invincible)
        {
            var yOffset = new Vector3(0, 1f, 0);
            currentHealth -= amount;
            //Debug.Log(gameObject.name + " taking damage: " + amount + ":: remaining health: " + _health);
            StartCoroutine(DamageCooldown());

            if (source != null)
            {
                Debug.Log("player calling knockback");
                Splatter(contactPoint);
                Knockback(source.position);
            }
            else
            {
                Splatter(transform.position + yOffset);
            }

            myMat.color = Color.red;

            if (currentHealth <= 0)
            {
                _enemyState = EnemyState.DYING;
            }
        }
    }

    public void Knockback(Vector3 source) //Simple knockback that works with Broccoli @ Drag & Angular drag of 10
    {
        //Vert offset to lower pos
        Vector3 yOffset = new Vector3(0f, 0f, 0f);
        Vector3 vdiff = transform.position - source;
        //Debug.Log(Math.Atan2(source.y - transform.position.y, source.x - transform.position.x));
        Debug.Log(vdiff);
        _rb.AddForceAtPosition(vdiff*10, source, ForceMode.Impulse);
    }

    private IEnumerator CollisionDelay()
    {
        _agent.speed = 0;
        _agent.angularSpeed = 0;
        //_rb.isKinematic = false;
        yield return new WaitForSeconds(1f);
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        //_rb.isKinematic = true;
    }

    private IEnumerator DamageCooldown()
    {
        invincible = true;
        yield return new WaitForSeconds(1f);
        invincible = false;
    }

    private void Splatter(Vector3 pos)
    {
        var splatter = Instantiate<GameObject>(_bloodType, pos, Quaternion.identity);
        Destroy(splatter, 20f);
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
        AudioManager.Instance.PlayAudioClip(AudioManager.Instance._reactionClips[4]);
        OnEnemyDie?.Invoke();
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("TriggerEntered");
        if (other.gameObject.layer == 3)
            //Debug.Log("Layer 3");
        {
            if(other.TryGetComponent<IDamageable>(out var target))
            {
                //Debug.Log("player take damage!!!");
                target.TakeDamage(_damage);
            }
        }
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameOver -= Die;
    }
}
