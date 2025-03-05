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
        enemyData.RbMoveAudio.volume = 0.65f;
    }

    public void Exit()
    {
        enemyData.RbMoveAudio.volume = 0;
    }

    public void Update()
    {
        if (!enemyData.Agent.pathPending && Vector3.Distance(enemyData.Agent.transform.position, points[destPoint].position) < 2f)
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
        Debug.Log("Go to idle state");
    }

    public void UpdatePatrolPoints(Transform[] newPoints) => points = newPoints;
}
