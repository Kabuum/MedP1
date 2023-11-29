using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Vector3 target;
    [SerializeField] Transform[] Waypoints;
    private NavMeshAgent agent;
    public bool PlayerSpotted;
    [SerializeField] Transform Player;
    private int WaypointIndex;
    public float runspeed = 1f;
    public float walkspeed = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerSpotted = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = walkspeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.5f && !PlayerSpotted)
        {
            NextWaypoint();
            DestinationUpdate();
        }
        else if (Vector3.Distance(transform.position, target) < 0.5f && PlayerSpotted)
        {
            DestinationUpdate();
            //death
        }
        else
        {
            DestinationUpdate();
        }
    }
    void NextWaypoint()
    {
        WaypointIndex++;
        if (WaypointIndex == Waypoints.Length)
        {
            WaypointIndex = 0;
        }
    }

    private void DestinationUpdate()
    {
        if (PlayerSpotted)
        {
            agent.speed = runspeed;
            target = Player.position;
            agent.SetDestination(target);
        }
        else
        {
            agent.speed = walkspeed;
            target = Waypoints[WaypointIndex].position;
            agent.SetDestination(target);
        }
    }
}
