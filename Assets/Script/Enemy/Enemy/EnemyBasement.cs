using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasement : EnemyBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speedPatrol = 3f;
    [SerializeField] float delayWaitingInPoint = 5f;

    private NavMeshAgent agent;
    private EnemyData enemyData;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemyData = new EnemyData(agent, this);

        PatrolState = new EnemyPatrolState(enemyData, speedPatrol, points);
        IdleState = new EnemyIdleStateWithDelay(enemyData, delayWaitingInPoint, PatrolState);

        SwitchState(PatrolState);
    }
}
