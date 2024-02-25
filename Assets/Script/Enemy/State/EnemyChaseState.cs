using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyChaseState : IEnemyState
{
    private EnemyData enemyData;
    public Transform target;
    private float speed;

    private NavMeshPath path;

    public EnemyChaseState(EnemyData enemyData,Transform target, float speed)
    {
        this.target = target;
        this.speed = speed;
        this.enemyData = enemyData;
        path = new NavMeshPath();
    }

    public void Enter()
    {
        enemyData.Agent.speed = speed;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        enemyData.Agent.SetDestination(target.position);

        if(NavMesh.CalculatePath(enemyData.Agent.transform.position, target.position, NavMesh.AllAreas, path) == false)
        {
            enemyData.EnemyBehaviour.SwitchState(enemyData.EnemyBehaviour.IdleStateWithDelay);
        }
    }
}
