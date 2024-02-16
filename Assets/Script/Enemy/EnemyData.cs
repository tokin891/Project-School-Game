using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData
{
    public NavMeshAgent Agent;
    public EnemyBehaviour EnemyBehaviour;

    public EnemyData(NavMeshAgent agent, EnemyBehaviour enemyBehaviour)
    {
        Agent = agent;
        EnemyBehaviour = enemyBehaviour;
    }
}
