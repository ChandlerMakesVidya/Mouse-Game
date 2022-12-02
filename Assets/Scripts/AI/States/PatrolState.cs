using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : MonoBehaviour, IFSMState
{
    public float moveSpeed = 5.0f;
    public float acceleration = 2.0f;
    public float angularSpeed = 360.0f;
    public FSMStateType StateName { get { return FSMStateType.Patrol; } }
    private NavMeshAgent agent;
    private SightLine sight;
    public Transform destination;
    private List<Room> validRooms;
    private int currentFocus;
    private int currentProgress;
    private int randroom;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<SightLine>();
        validRooms = new List<Room>();
    }

    private void Start()
    {
        Room[] allRooms = FindObjectsOfType<Room>();
        foreach(Room room in allRooms)
        {
            if (!room.safe)
            {
                validRooms.Add(room);
            }
        }
    }

    public void OnEnter()
    {
        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.angularSpeed = angularSpeed;
        agent.autoBraking = true;
    }

    public void OnExit()
    {
        agent.isStopped = true;
        currentProgress = 0;
    }

    public void DoAction()
    {
        /*if(destination != null)
        {
            agent.SetDestination(destination.position);
        }*/
        if (ReachedEndOfPath())
        {
            if(currentProgress == 0)
            {
                randroom = Random.Range(0, validRooms.Count);
                currentProgress = validRooms[randroom].patrolPoints.Length;
                currentProgress -= 1;
                agent.SetDestination(validRooms[randroom].patrolPoints[currentProgress].transform.position);
            }
            else
            {
                currentProgress -= 1;
                agent.SetDestination(validRooms[randroom].patrolPoints[currentProgress].transform.position);
            }
        }
    }

    private bool ReachedEndOfPath()
    {
        if (!agent.pathPending)
        {
            if(agent.remainingDistance <= 1.0f)
            {
                if(!agent.hasPath || Mathf.Approximately(agent.velocity.sqrMagnitude, Mathf.Epsilon))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public FSMStateType ShouldTransitionToState()
    {
        if (sight.IsTargetInSightLine && GameManager.GM.gameState == GameManager.GameState.Playing)
        {
            return FSMStateType.Chase;
        }
        return StateName;
    }
}
