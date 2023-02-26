using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    [SerializeField] Transform playerTrans;
    [SerializeField] Transform[] navPoint;

    [SerializeField] float enemyPatrolSpeed;
    [SerializeField] float enemyStalkSpeed;

    int destinationPointId = 0;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
