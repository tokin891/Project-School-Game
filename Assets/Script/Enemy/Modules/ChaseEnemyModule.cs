using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class ChaseEnemyModule : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;
    [SerializeField] private Transform eyes;
    [SerializeField] public Movement Target;
    [SerializeField] float maxAngle;
    [SerializeField] float maxDistancePlayer;
    [SerializeField] float maxDistancePlayerWithoutCrouch;
    [SerializeField] float maxDistancePlayerRunning;
    [SerializeField] private LayerMask ignoreLayer;

    private float angle;
    private NavMeshPath path;

    public bool IsEnemySeePlayer
    {
        get
        {
            return IsEnemySeePlayerLocal();
        }
    }

    private void Awake()
    {
        path = new NavMeshPath();
    }

    public void GetUpdate()
    {
        Vector3 targetDir = Target.transform.position - enemy.transform.position;
        angle = Vector3.Angle(targetDir, enemy.transform.forward);
        eyes.LookAt(Target.transform.position);
    }

    private bool IsEnemySeePlayerLocal()
    {
        if(!NavMesh.CalculatePath(enemy.transform.position, Target.transform.position,NavMesh.AllAreas, path))
        {
            Debug.Log(NavMesh.CalculatePath(enemy.transform.position, Target.transform.position, NavMesh.AllAreas, path));
            return false;
        }

        RaycastHit hit;
        if(Physics.Raycast(eyes.position, eyes.transform.forward, out hit,100, ~ignoreLayer))
        {
            if(hit.transform == Target.transform)
            {
                if(angle < maxAngle)
                {
                    return true;
                }
                if (Vector3.Distance(Target.transform.position, enemy.transform.position) < maxDistancePlayer)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
