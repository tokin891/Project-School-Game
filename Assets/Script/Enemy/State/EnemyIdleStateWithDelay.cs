using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleStateWithDelay : IEnemyState
{
    EnemyData data;
    float delayToStartPatrol;
    IEnemyState stateAfterDelay;

    private float nextTimeToChange;

    public EnemyIdleStateWithDelay(EnemyData data, float delayToStartPatrol, IEnemyState stateAfterDelay)
    {
        this.data = data;
        this.delayToStartPatrol = delayToStartPatrol;
        this.stateAfterDelay = stateAfterDelay;
    }

    public void Enter()
    {
        data.Agent.speed = 0;
        data.Agent.SetDestination(data.Agent.transform.position);

        nextTimeToChange = Time.time + delayToStartPatrol;
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if (nextTimeToChange < Time.time)
            data.EnemyBehaviour.SwitchState(stateAfterDelay);
    }
}
