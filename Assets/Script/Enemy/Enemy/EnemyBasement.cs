using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasement : EnemyBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speedPatrol = 3f;
    [SerializeField] float speedChasing = 4.5f;
    [SerializeField] float delayWaitingInPoint = 5f;
    [SerializeField] Light light;
    public ChaseEnemyModule chaseModule;

    private NavMeshAgent agent;
    private EnemyData enemyData;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemyData = new EnemyData(agent, this);

        PatrolState = new EnemyPatrolState(enemyData, speedPatrol, points,chaseModule);
        IdleStateWithDelay = new EnemyIdleStateWithDelay(enemyData, delayWaitingInPoint, PatrolState);
        ChaseState = new EnemyChaseState(enemyData, chaseModule.Target.transform, speedChasing);

        SwitchState(PatrolState);
    }

    public void SwitchLight(Color color)
    {
        light.color = color;
    }
}
