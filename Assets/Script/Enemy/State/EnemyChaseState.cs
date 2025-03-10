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
    private float distance;

    private NavMeshPath path;

    public EnemyChaseState(EnemyData enemyData,Transform target, float speed, float distance)
    {
        this.target = target;
        this.speed = speed;
        this.enemyData = enemyData;
        this.distance = distance;
        path = new NavMeshPath();
    }

    public void Enter()
    {
        enemyData.Agent.speed = speed;
        enemyData.EnemyAnimator.SetInteger("State", 1);
        enemyData.RbMoveAudio.volume = 0.85f;
    }

    public void Exit()
    {
        enemyData.RbMoveAudio.volume = 0f;
    }

    public void Update()
    {
        enemyData.Agent.SetDestination(target.position);

        if(NavMesh.CalculatePath(enemyData.Agent.transform.position, target.position, NavMesh.AllAreas, path) == false)
        {
            enemyData.EnemyBehaviour.SwitchState(enemyData.EnemyBehaviour.IdleStateWithDelay);
        }

        if(Vector3.Distance(enemyData.Agent.transform.position, target.position) < distance)
        {
            GameManagerBasement.Instance.ChangeStateOfGame(GameManagerBasement.State.Gameover);
        }
    }
}
