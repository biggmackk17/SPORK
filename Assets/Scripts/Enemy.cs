using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    [SerializeField]float _attackDistance;
    // Start is called before the first frame update
    public enum EnemyState { 
    CHASING,
    ATTACKING,
    DYING
    }
    [SerializeField] private EnemyState _enemyState;
    void Start()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        _enemyState = EnemyState.CHASING;
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = Player.Instance.transform.position;
        switch (_enemyState)
        {
            case EnemyState.CHASING:
                var distance = Vector3.Distance(playerPos, transform.position);
                _agent.SetDestination(playerPos);
                if (_agent.remainingDistance <= _attackDistance)
                {
                    Debug.Log(_agent.remainingDistance);
                    _enemyState = EnemyState.ATTACKING;
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
