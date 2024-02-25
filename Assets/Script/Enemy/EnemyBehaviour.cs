using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    private IEnemyState state;

    public EnemyIdleStateWithDelay IdleStateWithDelay { set; get; }
    public EnemyPatrolState PatrolState { set; get; }
    public EnemyChaseState ChaseState { set; get; }

    public void SwitchState(IEnemyState state)
    {
        this.state?.Exit();
        this.state = state;
        this.state.Enter();

        // Testing
        if(state is EnemyChaseState)
        {
            if(this is EnemyBasement eb)
            {
                eb.SwitchLight(Color.red);
            }
        }else
        {
            if (this is EnemyBasement eb)
            {
                eb.SwitchLight(Color.white);
            }
        }
    }

    private void Update()
    {
        state?.Update();

        if(this is EnemyBasement enemyBasement)
        {
            enemyBasement.chaseModule.GetUpdate();
        }
    }
}
