using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Following : ZombieState
{
    float stoppingDistance = 2;

    public ZombieState_Following(ZombieComponent owningZombie, ZombieStateMachine stateMachine) : base(owningZombie, stateMachine)
    {
        updateInterval = 2;
    }

    public override void Enter()
    {
        base.Enter();
        owningZombie.navMeshAgent.SetDestination(owningZombie.followTarget.transform.position);
        owningZombie.navMeshAgent.isStopped = false;
        owningZombie.animator.SetFloat(movementZHash, 0);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        if (!owningZombie.followTarget)
        {
            stateMachine.ChangeState(ZombieState.Type.Idle);
            return;
        }

        owningZombie.navMeshAgent.SetDestination(owningZombie.followTarget.transform.position);
    }

    public override void Update()
    {
        base.Update();
        if (!owningZombie.followTarget)
        {
            stateMachine.ChangeState(ZombieState.Type.Idle);
            return;
        }

        float moveZ = owningZombie.navMeshAgent.velocity.normalized.z != 0 ? 1 :0;
        owningZombie.animator.SetFloat(movementZHash, moveZ);

        float distance = Vector3.Distance(owningZombie.transform.position, owningZombie.followTarget.transform.position);
        if (distance < stoppingDistance)
        {
            stateMachine.ChangeState(ZombieState.Type.Attacking);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
