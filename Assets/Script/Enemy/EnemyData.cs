using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyData
{
    public NavMeshAgent Agent;
    public EnemyBehaviour EnemyBehaviour;
    public Animator EnemyAnimator;
    public AudioSource RbMoveAudio;

    public EnemyData(NavMeshAgent agent, EnemyBehaviour enemyBehaviour, Animator enemyAnimator, AudioSource rbMoveAudio)
    {
        Agent = agent;
        EnemyBehaviour = enemyBehaviour;
        EnemyAnimator = enemyAnimator;
        RbMoveAudio = rbMoveAudio;
    }
}
