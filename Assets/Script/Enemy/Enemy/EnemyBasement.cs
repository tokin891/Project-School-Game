using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBasement : EnemyBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speedPatrol = 3f;
    [SerializeField] float speedChasing = 4.5f;
    [SerializeField] float delayWaitingInPoint = 5f;
    [SerializeField] float distanceToCatchPlayer = 1.5f;
    [SerializeField] Light light;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource rbMoveAudio;
    public ChaseEnemyModule chaseModule;

    private NavMeshAgent agent;
    private EnemyData enemyData;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        enemyData = new EnemyData(agent, this, animator, rbMoveAudio);

        PatrolState = new EnemyPatrolState(enemyData, speedPatrol, points,chaseModule);
        IdleStateWithDelay = new EnemyIdleStateWithDelay(enemyData, delayWaitingInPoint, PatrolState);
        ChaseState = new EnemyChaseState(enemyData, chaseModule.Target.transform, speedChasing,distanceToCatchPlayer);

        SwitchState(PatrolState);
    }

    public void SwitchLight(Color color)
    {
        light.color = color;
    }

    public void AddPatrolPoints(Transform[] points_)
    {
        List<Transform> _list = points.ToList();
        _list.AddRange(points_);

        points = _list.ToArray();
        PatrolState.UpdatePatrolPoints(points);
    }

    public void AddPatrolPoint(Transform points_)
    {
        List<Transform> _list = points.ToList();
        _list.Add(points_);

        points = _list.ToArray();
        PatrolState.UpdatePatrolPoints(points);
    }

    public void DoorAreClose()
    {

    }
}
