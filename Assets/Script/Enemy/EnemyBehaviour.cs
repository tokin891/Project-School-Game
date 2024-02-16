using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    private IEnemyState state;

    public EnemyIdleStateWithDelay IdleState { set; get; }
    public EnemyPatrolState PatrolState { set; get; }

    public void SwitchState(IEnemyState state)
    {
        this.state?.Exit();
        this.state = state;
        this.state.Enter();
    }

    private void Update()
    {
        state?.Update();
    }
}
