using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform[] navPoint;
    [SerializeField] float enemyPatrolSpeed;
    [SerializeField] float enemyStalkSpeed;

    [SerializeField] int rayCount;
    [SerializeField] float fov;
    [SerializeField] float walkPlayerDetectorDistance;
    [SerializeField] float runPlayerDetectorDistance;
    [SerializeField] float maxStayingTime;


    PlayerBehaviour playerBehaviour;
    GameObject player;
    Vector3 lastPlayerPos;
    int destinationPointId = 0;
    float pointDetectorDistance = 0.3f;
    float playerDetectorDistance;
    float angle = 0f;

    float stayingTime = 0f;

    bool isPlayerDetected = false;
    bool isPlayerVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        player = GameObject.FindGameObjectWithTag("Player");
        playerBehaviour = player.GetComponent<PlayerBehaviour>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerVisible) SetAngle(lastPlayerPos - transform.position);
        else SetAngle(agent.velocity);
        UpdateRaycast();
        if (playerBehaviour.isRunning) playerDetectorDistance = runPlayerDetectorDistance;
        else playerDetectorDistance = walkPlayerDetectorDistance;

        isPlayerVisible = false;

        if (isPlayerDetected)
        {
            if (agent.velocity.magnitude == 0)
            {
                stayingTime += Time.deltaTime;
                animator.Play("chucha_idle");
            }
            else
            {
                stayingTime = 0;
                animator.Play("chucha_walk");
            }
            if (stayingTime > maxStayingTime)
            {
                isPlayerDetected = false;
                agent.speed = enemyPatrolSpeed;
                stayingTime = 0;
            }

            float x = transform.position.x - lastPlayerPos.x;
            float y = transform.position.y - lastPlayerPos.y;
            float dist = Mathf.Sqrt(x * x + y * y);
            if (dist < pointDetectorDistance)
            {
                isPlayerDetected = false;
                agent.speed = enemyPatrolSpeed;
                stayingTime = 0;
            }
            agent.SetDestination(lastPlayerPos);
        }
        else
        {
            animator.Play("chucha_walk");
            float x = transform.position.x - navPoint[destinationPointId].position.x;
            float y = transform.position.y - navPoint[destinationPointId].position.y;
            float dist = Mathf.Sqrt(x * x + y * y);
            if (dist < pointDetectorDistance)
            {
                destinationPointId = (destinationPointId + 1) % navPoint.Length;
            }
            agent.SetDestination(navPoint[destinationPointId].position);
        }
    }

    void SetAngle(Vector3 angVec)
    {
        if (angVec.magnitude != 0)
        {
            angle = Vector3.Angle(Vector3.right, angVec);
            if (angVec.y < 0) angle *= -1;
        }
    }
    void UpdateRaycast()
    {
        float rayAngleRad = (angle + fov / 2) / 180 * Mathf.PI;
        float angInc = (fov / rayCount) / 180 * Mathf.PI;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 v = new Vector3(Mathf.Cos(rayAngleRad), Mathf.Sin(rayAngleRad));
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, v, playerDetectorDistance, layerMask);
            Debug.DrawRay(transform.position, v * playerDetectorDistance, Color.green);

            if (raycastHit2D.collider != null && raycastHit2D.collider.tag == "Player")
            {
                lastPlayerPos = raycastHit2D.collider.gameObject.transform.position;
                agent.speed = enemyStalkSpeed;
                isPlayerDetected = true;
                isPlayerVisible = true;
            }
            rayAngleRad -= angInc;
        }
    }
}
