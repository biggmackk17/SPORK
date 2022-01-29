using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent _agent;
    // Start is called before the first frame update
    

    void Start()
    {
        _agent = transform.GetComponent<NavMeshAgent>();
        _agent.stoppingDistance =1.3f;
        
    }

    // Update is called once per frame
    void Update()
    {
        _agent.destination = Player.Instance.transform.position;
    }
}
