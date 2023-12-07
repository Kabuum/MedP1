using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    private int DirectionIndex = 0;
    private Animator EnemyAnimator;
    private bool Monster = false;
    public bool Lantern = false;
    public bool animated = false;

    public UnityEvent openDoor;

    public DetectionTriangle triScript;



    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponentInChildren<Animator>();
        PlayerSpotted = false;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = walkspeed;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("animated : " + animated);
        Debug.Log(agent.destination);
        PlayerSpotted = triScript.CheckInSight();

        if (Vector3.Distance(transform.position, target) < 0.5f && !PlayerSpotted && animated == false)
        {
            if (animated == false)
            {
                NextWaypoint();
                DestinationUpdate();
            }
        }
        else if (Vector3.Distance(transform.position, target) < 0.5f && PlayerSpotted && animated == false)
        {
            DestinationUpdate();
            //death
        }
        else
        {
            DestinationUpdate();
        }

        Vector3 AgentVel = agent.velocity;
        if (MathF.Abs(AgentVel.x) > MathF.Abs(AgentVel.y)) // left and right
        {
            if (AgentVel.x >= 0) //right
            {
                DirectionIndex = 1;// 1 is right
            }
            else //left
            {
                DirectionIndex = 2;// 2 is left
            }
        }
        else//up and down
        {
            if (AgentVel.y >= 0)//up
            {
                DirectionIndex = 3;// 3 is up
            }
            else
            {
                DirectionIndex = 4;// 4 is down
            }
        }
        EnemyAnim(DirectionIndex, Lantern, Monster);
    }
    public void TransformToMonster()
    { //Her skal player ændre skin til monster. 
    }
    void NextWaypoint()
    {
        WaypointIndex++;
        if (WaypointIndex == Waypoints.Length)
        {
            WaypointIndex = 0;
        }
    }
    public void DestinationUpdate()
    {
        if (PlayerSpotted && animated == false)
        {
            agent.speed = runspeed;
            target = Player.position;
            agent.SetDestination(target);
        }
        else if (PlayerSpotted == false && animated == false)
        {
            agent.speed = walkspeed;
            target = Waypoints[WaypointIndex].position;
            agent.SetDestination(target);
        }
    }

    void EnemyAnim(int direction, bool isLantern, bool isMonster)
    {
        if (Monster)
        {
            switch (direction)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
            }
        }
        else if (Lantern)
        {
            switch (direction)
            {
                case 1:
                    EnemyAnimator.Play("OlLadyLantern-Walk-Right");
                    triScript.UpdateDetectionAngle(90);
                    break;
                case 2:
                    EnemyAnimator.Play("OlLadyLantern-Walk-Left");
                    triScript.UpdateDetectionAngle(270);
                    break;
                case 3:
                    EnemyAnimator.Play("OlLadyLantern-Walk-Up");
                    triScript.UpdateDetectionAngle(180);
                    break;
                case 4:
                    EnemyAnimator.Play("OlLadyLantern-Walk-Down");
                    triScript.UpdateDetectionAngle(0);
                    break;
            }
        }
        else
        {
            switch (direction)
            {
                case 1:
                    EnemyAnimator.Play("OlLady-Walk-Right");
                    triScript.UpdateDetectionAngle(90);
                    break;
                case 2:
                    EnemyAnimator.Play("OlLady-Walk-Left");
                    triScript.UpdateDetectionAngle(270);
                    break;
                case 3:
                    EnemyAnimator.Play("OlLady-Walk-Up");
                    triScript.UpdateDetectionAngle(180);
                    break;
                case 4:
                    EnemyAnimator.Play("OlLady-Walk-Down");
                    triScript.UpdateDetectionAngle(0);
                    break;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D Coll)
    {
        if (Coll.gameObject.CompareTag("Doors"))
        {
            openDoor.Invoke();
        }
    }
}
