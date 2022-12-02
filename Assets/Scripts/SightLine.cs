using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightLine : MonoBehaviour
{
    public float fieldOfView = 90.0f;
    public bool IsTargetInSightLine;
    public Vector3 LastKnownSighting;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        UpdateSight(player.transform);
    }

    private void UpdateSight(Transform target)
    {
        IsTargetInSightLine = HasClearLineofSightToTarget(target) && TargetInFOV(target);
        if (IsTargetInSightLine)
        {
            LastKnownSighting = target.position;
        }
    }

    private bool HasClearLineofSightToTarget(Transform target)
    {
        RaycastHit info;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        if(Physics.Raycast(transform.position, dirToTarget, out info))
        {
            if (info.transform.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    private bool TargetInFOV(Transform target)
    {
        Vector3 dirToTarget = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, dirToTarget);
        if(angle <= fieldOfView)
        {
            return true;
        }
        return false;
    }
}
