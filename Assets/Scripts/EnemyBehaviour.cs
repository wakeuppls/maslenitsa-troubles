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
    float pointDetectorDistance = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x - navPoint[destinationPointId].position.x;
        float y = transform.position.y - navPoint[destinationPointId].position.y;
        float dist = Mathf.Sqrt(x * x + y * y);
        Debug.Log(dist);
        if (dist < pointDetectorDistance)
        {
            destinationPointId = (destinationPointId + 1) % navPoint.Length;
        }
        agent.SetDestination(navPoint[destinationPointId].position);
    }
}
