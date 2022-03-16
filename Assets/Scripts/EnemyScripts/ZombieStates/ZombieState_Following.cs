using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Following : ZombieState
{
    private GameObject followTarget;
    float stoppingDistance = 2;

    public ZombieState_Following(GameObject followTarget, ZombieComponent owningZombie, ZombieStateMachine stateMachine) : base(owningZombie, stateMachine)
    {
        this.followTarget = followTarget;
        updateInterval = 2;
    }

    public override void Enter()
    {
        base.Enter();
        owningZombie.navMeshAgent.SetDestination(followTarget.transform.position);
        owningZombie.navMeshAgent.isStopped = false;
        owningZombie.animator.SetFloat(movementZHash, 0);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        owningZombie.navMeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void Update()
    {
        base.Update();
        float moveZ = owningZombie.navMeshAgent.velocity.normalized.z != 0 ? 1 :0;
        owningZombie.animator.SetFloat(movementZHash, moveZ);

        float distance = Vector3.Distance(owningZombie.transform.position, followTarget.transform.position);
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
