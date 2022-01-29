using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField]float _attackDistance;
    Rigidbody _rb;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 3)
        {
            _agent.enabled = false;
            _rb.isKinematic = false;
            StartCoroutine(CollisionDelay());
        }
    }

    private IEnumerator CollisionDelay()
    {
        yield return new WaitForSeconds(1f);
        _agent.enabled = true;
        _rb.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = Player.Instance.transform.position;
        switch (_enemyState)
        {
            case EnemyState.CHASING:
                var distance = Vector3.Distance(playerPos, transform.position);
                if (_agent.enabled)
                {
                    _agent.SetDestination(playerPos);
                }
                if (distance <= _attackDistance)
                {
                    //_enemyState = EnemyState.ATTACKING;
                }
                break;
            case EnemyState.ATTACKING:
                //play attacking animation
                break;
            case EnemyState.DYING:
                break;
            default:
                break;
        }
    }
}
