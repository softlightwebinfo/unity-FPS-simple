using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBehaviour : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> waypoints;
    private int locationIndex = 0;
    private NavMeshAgent _agent;
    private float currentDelay = 0.0f;
    public float maxDelay = 0.5f;
    public Transform player;
    private float _health = 3.0f;

    public float health
    {
        get { return _health; }
        private set
        {
            _health = value;
            if (_health <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("OOH, me ha mataooo");
            }
        }
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        this.InitializeWaypoints();
        this.MoveToNextWaypoint();
    }

    private void Update()
    {
        if (_agent.remainingDistance < 0.5f && !_agent.pathPending)
        {
            currentDelay += Time.deltaTime;
            if (currentDelay > maxDelay)
            {
                currentDelay = 0.0f;
                MoveToNextWaypoint();
            }
        }
    }

    private void InitializeWaypoints()
    {
        foreach (Transform wp in patrolRoute)
        {
            waypoints.Add(wp);
        }
    }

    private void MoveToNextWaypoint()
    {
        if (waypoints.Count == 0) return;
        _agent.SetDestination(this.waypoints[locationIndex].position);
        //locationIndex = (locationIndex + 1) % waypoints.Count;
        locationIndex = Random.Range(0, waypoints.Count);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _agent.SetDestination(player.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            this.MoveToNextWaypoint();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            this.health--;
            Debug.Log("Auuuh duele, Daño recibido");
        }
    }
}
