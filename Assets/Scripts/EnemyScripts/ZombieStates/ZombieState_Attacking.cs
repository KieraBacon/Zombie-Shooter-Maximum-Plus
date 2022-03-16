using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieState_Attacking : ZombieState
{
    GameObject followTarget;
    float attackRange = 2;

    public ZombieState_Attacking(GameObject followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        this.followTarget = followTarget;
        updateInterval = 2;
    }

    public override void Enter()
    {
        base.Enter();
        owningZombie.navMeshAgent.isStopped = true;
        owningZombie.navMeshAgent.ResetPath();
        owningZombie.animator.SetFloat(movementZHash, 0);
        owningZombie.animator.SetBool(isAttackingHash, true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
    }

    public override void Update()
    {
        base.Update();
        owningZombie.transform.LookAt(new Vector3(followTarget.transform.position.x, owningZombie.transform.position.y, followTarget.transform.position.z), Vector3.up);
        
        float distance = Vector3.Distance(owningZombie.transform.position, followTarget.transform.position);
        if (distance > attackRange)
        {
            stateMachine.ChangeState(ZombieState.Type.Following);
        }
    }

    public override void Exit()
    {
        base.Exit();
        owningZombie.navMeshAgent.isStopped = false;
        owningZombie.animator.SetBool(isAttackingHash, false);
    }
}
