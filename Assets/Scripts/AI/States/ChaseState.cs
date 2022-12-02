using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : MonoBehaviour, IFSMState
{
    public float moveSpeed = 30.0f;
    public float acceleration = 90.0f;
    public float angularSpeed = 720.0f;
    public AudioClip catScream;
    public FSMStateType StateName { get { return FSMStateType.Chase; } }
    private NavMeshAgent agent;
    private SightLine sight;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        sight = GetComponent<SightLine>();
    }

    public void OnEnter()
    {
        agent.isStopped = false;
        agent.speed = moveSpeed;
        agent.acceleration = acceleration;
        agent.angularSpeed = angularSpeed;
        agent.autoBraking = false;
        AudioSource.PlayClipAtPoint(catScream, transform.position);
    }

    public void OnExit()
    {
        agent.isStopped = true;
    }

    public void DoAction()
    {
        if (sight.IsTargetInSightLine)
        {
            agent.SetDestination(sight.LastKnownSighting);
        }
    }

    private bool ReachedEndOfPath()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= 1.0f)
            {
                if (!agent.hasPath || Mathf.Approximately(agent.velocity.sqrMagnitude, Mathf.Epsilon))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public FSMStateType ShouldTransitionToState()
    {
        if (!sight.IsTargetInSightLine && ReachedEndOfPath())
        {
            return FSMStateType.Patrol;
        }
        return StateName;
    }
}
