using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : IEnemyState
{
    EnemyData enemyData;
    private float speed;

    Transform[] points;
    private int destPoint = 0;
    private ChaseEnemyModule chaseEnemyModule;

    public EnemyPatrolState(EnemyData enemyData, float speed, Transform[] points, ChaseEnemyModule chaseEnemyModule)
    {
        this.enemyData = enemyData;
        this.speed = speed;
        this.points = points;
        this.chaseEnemyModule = chaseEnemyModule;
    }

    public void Enter()
    {
        enemyData.Agent.speed = speed;
        enemyData.Agent.SetDestination(points[destPoint].position);
        enemyData.EnemyAnimator.SetInteger("State", 1);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (!enemyData.Agent.pathPending && enemyData.Agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }

        if (chaseEnemyModule.IsEnemySeePlayer)
        {
            enemyData.EnemyBehaviour.SwitchState(enemyData.EnemyBehaviour.ChaseState);
        }
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        enemyData.Agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;

        enemyData.EnemyBehaviour.SwitchState(enemyData.EnemyBehaviour.IdleStateWithDelay);
    }
}
